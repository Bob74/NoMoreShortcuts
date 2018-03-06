using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using GTA;
using GTA.Native;
using iFruitAddon2;
using NativeUI;

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

            // Check if blank banner file exists
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);
            if (!File.Exists(BannerBlank))
                Properties.Resources.blank.Save(BaseDir + "\\blank.png");

            _iFruit = new CustomiFruit();
            GetAllProfiles();
            AddContacts();

            Tick += Initialize;
        }

        void Initialize(object sender, EventArgs e)
        {
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
            string directory = AppDomain.CurrentDomain.BaseDirectory + "\\NoMoreShortcuts\\";

            if (Directory.Exists(directory))
            {
                string[] profiles = Directory.GetFiles(directory, "*.xml");
                Logger.Log(profiles.Count() + " profiles detected.");
                foreach (string file in profiles)
                {
                    Logger.Log(new FileInfo(file).Name);
                    profileCollection.Add(new Profile(file));
                }
            }
            else
                Directory.CreateDirectory(directory);            
        }

        private void AddContacts()
        {
            foreach (Profile profile in profileCollection)
            {
                profile.Contact.Answered += ContactAnswered;
                _iFruit.Contacts.Add(profile.Contact);
            }
        }

    }
}
