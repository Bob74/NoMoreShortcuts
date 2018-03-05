using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

using iFruitAddon2;
using NativeUI;

namespace NoMoreShortcuts
{
    class ProfilesXmlReader
    {
        private XElement _file;

        public ProfilesXmlReader(string xmlFile)
        {
            if (File.Exists(xmlFile))
            {
                try
                {
                    _file = XElement.Load(xmlFile);
                }
                catch (Exception e)
                {
                    Logger.Log("Error: InsuranceManager - Cannot load database file. " + e.Message);
                }
            }
            else
                Logger.Log("Error: ContactXmlReader - File doesn't exist (" + xmlFile + ").");
        }

        public iFruitContact GetiFruitContact()
        {
            string contactName = _file?.Element("Phone")?.Element("ContactName")?.Value ?? "Unknown";
            string contactIcon = _file?.Element("Phone")?.Element("ContactIcon")?.Value ?? "CHAR_DEFAULT";
            int dialTimeout = int.Parse(_file?.Element("Phone")?.Element("DialTimeout")?.Value ?? "0");

            iFruitContact contact = new iFruitContact(contactName);
            contact.Active = true;
            contact.DialTimeout = dialTimeout;
            contact.Icon = new ContactIcon(contactIcon);

            return contact;
        }

        public List<Keys> GetShortcutKeys()
        {
            return new List<Keys>();
        }

        public UIMenu GetMenu()
        {
            return new UIMenu("", "");
        }

    }
}
