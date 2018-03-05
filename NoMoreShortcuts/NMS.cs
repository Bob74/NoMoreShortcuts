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
            // The contact has answered, we can execute our code
            UI.Notify("The contact has answered.");
            UI.Notify("The contact has answered.");
            _iFruit.Close();

            SendKeys.SendWait("{F4}");
        }

        private void GetAllProfiles()
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory + "\\NoMoreShortcuts\\";

            // Directory.CreateDirectory(_configDir);

            if (Directory.Exists(directory))
            {
                string[] profiles = Directory.GetFiles(directory, "*.xml");
                Logger.Log("Profiles count: " + profiles.Count());

                foreach (string file in profiles)
                    profileCollection.Add(new ProfilesXmlReader(file));
            }
            else
                Logger.Log("Error: GetAllProfiles - Path doesn't exist " + directory);
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
