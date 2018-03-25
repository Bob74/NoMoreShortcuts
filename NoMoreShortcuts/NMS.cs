using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;

using GTA;
using System.IO.Pipes;
#if DEBUG
using System.Windows.Forms;
#endif
/*
    2.0.2 (): - Added the ability play the phone's notification sound when showing the notification

	2.0.1 (14/03/2018): - Now using Pipes to communicate with InputSimulator instead of a file.
                        - Added ability to show a notification right after the contact answer the call.

	2.0.0 (11/03/2018): - Entirely rewrote the keypress system. It now uses an ASI plugin to send the keys. 
						  Some keys names may have changed a bit. Decimal and hexadecimal values remains the same.

	1.0.0 (07/03/2018): - Initial release


    TODO:
        X Add notification support
        X Replace exchange file with Pipes between InputSimulator & NoMoreShortcuts

        - Notification sound
        - Contact name Bold
        - Open profile's menu with shortcut
        - InputSimulator => Test with Russian & Chinese characters


*/
namespace NoMoreShortcuts
{
    public class NMS : Script
    {
        private static NMS _currentInstance;
        public static NMS CurrentInstance { get => _currentInstance; set => _currentInstance = value; }

        private NamedPipeClientStream _pipeClient;
        private Thread _pipeConnectThread;

        // 0 = Not connected
        // 1 = Connected
        // 2 = Error trying to connect
        public int PipeStatus { get => _pipestatus; }
        private int _pipestatus = 0;

        private iFruit _iFruit;

        private static List<Profile> _profileCollection = new List<Profile>();
        internal static List<Profile> ProfileCollection { get => _profileCollection; }

        private static List<Notification> _notificationCollection = new List<Notification>();
        internal static List<Notification> NotificationCollection { get => _notificationCollection; }

        internal static string BaseDir = AppDomain.CurrentDomain.BaseDirectory + "\\NoMoreShortcuts";
        internal static string BannerBlank = BaseDir + "\\blank.png";


        public NMS()
        {
            // Reset log file
            Logger.ResetLogFile();

            Tick += Initialize;
        }

        /// <summary>
        /// Using Tick event allow us to use Script.Yield() or Script.Wait() whereas it is forbidden to call these from the constructor.
        /// That's why Initialize is removed from the Tick event as soon as it reach its end.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Initialize(object sender, EventArgs e)
        {
            while (Game.IsLoading)
                Yield();
            while (Game.IsScreenFadingIn)
                Yield();

            _currentInstance = this;

            // Check for updates
            if (Tools.IsUpdateAvailable()) Tools.NotifyNewUpdate();

            // Check if blank banner file exists
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);
            if (!File.Exists(BannerBlank))
                Properties.Resources.blank.Save(BannerBlank);

            // Profiles
            GetAllProfiles();

            // iFruit
            _iFruit = new iFruit();

            // Pipe setup
            SetupPipe();

            Tick -= Initialize;
            Tick += OnTick;

            KeyDown += OnKeyDown;
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            foreach (Profile profile in _profileCollection)
            {
                if (profile.MenuHotKey != 0 && profile.MenuHotKeyModifier != 0)
                {
                    if (e.KeyCode == (Keys)profile.MenuHotKey && e.Modifiers == (Keys)profile.MenuHotKeyModifier
                        && !profile.Pool.IsAnyMenuOpen())
                        profile.Menu.Visible = !profile.Menu.Visible;
                }
            }
        }

        // Dispose Event
        protected override void Dispose(bool A_0)
        {
            if (A_0)
            {
                foreach (Profile profile in ProfileCollection)
                    profile.Contact.EndCall();

                if (_pipeConnectThread.IsAlive) _pipeConnectThread.Abort();
                if (KeySender.PipeWriter != null) KeySender.PipeWriter.Close();
                if (_pipeClient != null) _pipeClient.Close();
            }
        }

        // Tick Event
        void OnTick(object sender, EventArgs e)
        {
            // Checking Pipe connection
            if (_pipeConnectThread != null && _pipestatus == 0)
            {
                if (_pipeConnectThread.ThreadState == ThreadState.Stopped)
                {
                    Logger.Log("Info: Connected to the pipe!");
                    KeySender.PipeWriter = new StreamWriter(_pipeClient);
                    _pipestatus = 1;
                }
                else if (_pipeConnectThread.ThreadState == ThreadState.Aborted)
                {
                    Logger.Log("Error: Pipe connection aborted");
                    _pipestatus = 2;
                }
            }

            // Check queued notifications and show them if their timer has ended
            for (int i = NotificationCollection.Count - 1; i >= 0; i--)
            {
                if (NotificationCollection[i].EndTimer <= Game.GameTime)
                {
                    Tools.DrawNotification(NotificationCollection[i].Icon,
                                           NotificationCollection[i].Title,
                                           NotificationCollection[i].Subtitle,
                                           NotificationCollection[i].Message);

                    if (NotificationCollection[i].Sound)
                        Audio.PlaySoundFrontend("Text_Arrive_Tone", Tools.GetPhoneSoundSet());

                    NotificationCollection.RemoveAt(i);
                }
            }

            // Process menus from all profiles
            foreach (Profile profile in ProfileCollection)
                if (profile.Pool != null) profile.Pool.ProcessMenus();

            // Update iFruit (draw contact list)
            _iFruit.CustomiFruit.Update();
        }

        /// <summary>
        /// Show the notification after the delay has passed without blocking the thread.
        /// </summary>
        /// <param name="notif">Notification parameters.</param>
        public void HandleNotification(Notification notif)
        {
            notif.EndTimer = Game.GameTime + notif.Delay;
            _notificationCollection.Add(notif);
        }

        // Fill the profile collection
        private void GetAllProfiles()
        {
            if (Directory.Exists(BaseDir))
            {
                string[] profiles = Directory.GetFiles(BaseDir, "*.xml");
                Logger.Log(profiles.Count() + " profiles detected.");
                foreach (string file in profiles)
                {
                    Logger.Log(new FileInfo(file).Name);
                    ProfileCollection.Add(new Profile(file));
                }
            }         
        }

        // Create a Pipe and connect to the server's Pipe
        private void SetupPipe()
        {
            Logger.Log("Info: Creating Pipe");
            _pipeClient = new NamedPipeClientStream(".", "GTA-Input-Pipe", PipeDirection.Out);
            Logger.Log("Info: Connecting to Pipe");
            _pipeConnectThread = new Thread(ThreadableConnectPipeToServer);

            try
            {
                _pipeConnectThread.Start();
            }
            catch (OutOfMemoryException ex)
            {
                Logger.Log("Error: SetupPipe - Not enough memory to start the thread: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Log("Error: SetupPipe - Unable to start the thread: " + ex.Message);
            }
            
        }

        // Must thread this method to avoid hanging
        private void ThreadableConnectPipeToServer()
        {
            try
            {
                _pipeClient.Connect(); // Thread blocking operation
                Logger.Log("Info: Pipe connection has finished");
            }
            catch (InvalidOperationException ex)
            {
                Logger.Log("Error: Client already connected to the pipe: " + ex.Message);
            }
            catch (ThreadAbortException ex)
            {
                Logger.Log("Error: Pipe connection thread has been aborted: " + ex.Message);
            }
        }
    }
}
