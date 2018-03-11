using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;

using GTA;
using GTA.Native;
using iFruitAddon2;
using System.IO.Pipes;
using System.Runtime.InteropServices;

/*
    TODO:
        - Replace exchange file with Pipes between InputSimulator & NoMoreShortcuts

*/
namespace NoMoreShortcuts
{
    public class NMS : Script
    {
        private CustomiFruit _iFruit;
        private List<Profile> profileCollection = new List<Profile>();
        internal static string BaseDir = AppDomain.CurrentDomain.BaseDirectory + "\\NoMoreShortcuts";
        internal static string BannerBlank = BaseDir + "\\blank.png";

        public NMS()
        {
            // Reset log file
            Logger.ResetLogFile();

            Tick += Initialize;
        }

        private void Initialize(object sender, EventArgs e)
        {
            while (Game.IsLoading)
                Yield();
            while (Game.IsScreenFadingIn)
                Yield();

            // Check for updates
            if (IsUpdateAvailable()) NotifyNewUpdate();

            // Check if blank banner file exists
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);
            if (!File.Exists(BannerBlank))
                Properties.Resources.blank.Save(BannerBlank);

            _iFruit = new CustomiFruit();
            GetAllProfiles();
            AddContacts();

            Tick -= Initialize;
            Tick += OnTick;
        }

        // Dispose Event
        protected override void Dispose(bool A_0)
        {
            if (A_0)
            {
                foreach (Profile profile in profileCollection)
                    profile.Contact.EndCall();
            }
        }

        // Tick Event
        void OnTick(object sender, EventArgs e)
        {
            foreach (Profile profile in profileCollection)
                if (profile.Pool != null) profile.Pool.ProcessMenus();

            _iFruit.Update();
        }

        private void ContactAnswered(iFruitContact contact)
        {
            Profile profile = profileCollection.Find(x => x.Contact == contact);
            if (profile != null)
            {
                if (profile.SoundFile != null) WaveStream.PlaySound(profile.SoundFile, profile.Volume);

                if (profile.Menu != null)
                {
                    profile.Menu.Visible = true;
                    Function.Call(Hash._0xFC695459D4D0E219, 0.5f, 0.5f);    // Cursor position centered
                }
                else
                {
                    if (profile.Keys.Count > 0)
                        KeySender.SendKeys(profile.Keys);
                }
            }

            _iFruit.Close();
        }

        private void GetAllProfiles()
        {
            if (Directory.Exists(BaseDir))
            {
                string[] profiles = Directory.GetFiles(BaseDir, "*.xml");
                Logger.Log(profiles.Count() + " profiles detected.");
                foreach (string file in profiles)
                {
                    Logger.Log(new FileInfo(file).Name);
                    profileCollection.Add(new Profile(file));
                }
            }         
        }

        private void AddContacts()
        {
            foreach (Profile profile in profileCollection)
            {
                profile.Contact.Answered += ContactAnswered;
                _iFruit.Contacts.Add(profile.Contact);
            }
        }



        private bool IsUpdateAvailable()
        {
            string downloadedString = "";
            Version onlineVersion;

            try
            {
                WebClient client = new WebClient();
                downloadedString = client.DownloadString("https://raw.githubusercontent.com/Bob74/NoMoreShortcuts/master/version");

                downloadedString = downloadedString.Replace("\r", "");
                downloadedString = downloadedString.Replace("\n", "");

                onlineVersion = new Version(downloadedString);

                client.Dispose();

                if (onlineVersion.CompareTo(Assembly.GetExecutingAssembly().GetName().Version) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Logger.Log("Error: IsUpdateAvailable - " + e.Message);
            }

            return false;
        }

        private void NotifyNewUpdate()
        {
            UI.Notify("NoMoreShortcuts: A new update is available!", true);
        }

    }
}
