using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using GTA;
using iFruitAddon2;

namespace NoMoreShortcuts
{
    public class NMS : Script
    {
        private CustomiFruit _iFruit;
        private Dictionary<iFruitContact, List<Keys>> contactCollection = new Dictionary<iFruitContact, List<Keys>>();
        private List<ProfilesXmlReader> profileCollection = new List<ProfilesXmlReader>();

        public NMS()
        {
            // Reset log file
            Logger.ResetLogFile();

            _iFruit = new CustomiFruit();
            GetAllProfiles();
            AddProfilesContacts();

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

            }
        }

        // Tick Event
        void OnTick(object sender, EventArgs e)
        {
            _iFruit.Update();
        }

        private void ContactAnswered(iFruitContact contact)
        {
            SendKeys.SendWait("{F4}");
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
                    Logger.Log(file);
                    profileCollection.Add(new ProfilesXmlReader(file));
                }
            }
            else
                Directory.CreateDirectory(directory);            
        }

        private void AddProfilesContacts()
        {
            foreach (ProfilesXmlReader profile in profileCollection)
            {
                iFruitContact contact = profile.GetiFruitContact();
                contact.Answered += ContactAnswered;
                _iFruit.Contacts.Add(contact);
            }
        }
    }
}
