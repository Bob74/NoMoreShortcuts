using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iFruitAddon2;
using NativeUI;

namespace NoMoreShortcuts
{
    class Profile
    {
        private XElement _file;

        internal string FilePath { get; private set; }
        internal iFruitContact Contact { get; private set; }
        internal List<string> Keys { get; private set; }
        internal MenuPool Pool { get; private set; }
        internal UIMenu Menu { get; private set; }

        public Profile(string xmlFile)
        {
            if (File.Exists(xmlFile))
            {
                try
                {
                    _file = XElement.Load(xmlFile);
                    FilePath = xmlFile;
                    Contact = GetiFruitContact();
                    Keys = GetShortcutKeys();

                    // If the profile contains menus
                    object[] menu = GetMenu();
                    if (menu != null)
                    {
                        if (menu.GetUpperBound(0) == 1)
                        {
                            Pool = (MenuPool)menu[0];
                            Menu = (UIMenu)menu[1];
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log("Error: Profile - Cannot parse profile. " + e.Message);
                }
            }
            else
                Logger.Log("Error: ContactXmlReader - File doesn't exist (" + xmlFile + ").");
        }




        private iFruitContact GetiFruitContact()
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

        private List<string> GetShortcutKeys()
        {
            XElement phone = _file.Element("Phone");

            // Getting shortcut keys
            List<string> keys = new List<string>();
            foreach (XElement key in phone.Elements("Key"))
                keys.Add(key.Value);

            return keys;
        }

        private object[] GetMenu()
        {
            if (_file.Element("Menu") != null)
            {
                XElement menuElement = _file.Element("Menu");

                // Creating menu
                MenuPool pool = new MenuPool();
                UIMenu mainMenu = CreateMenu("", "");
                pool.Add(mainMenu);

                // Creating root subitems
                AddSubitems(menuElement, mainMenu);

                // Creating submenus subitems
                foreach (XElement submenu in menuElement.Elements("SubMenu"))
                {
                    UIMenu menu = AddSubMenu(pool, mainMenu, "", submenu.Attribute("text")?.Value ?? "Unknown");
                    AddSubitems(submenu, menu);
                }

                return new object[] { pool, mainMenu };
            }
            return null;
        }

        private void AddSubitems(XElement startElement, UIMenu menu)
        {
            foreach (XElement subitem in startElement.Elements("SubItem"))
            {
                // Getting shortcut keys
                List<string> keys = new List<string>();
                foreach (XElement key in subitem.Elements("Key"))
                    keys.Add(key.Value);
                
                UIMenuItem menuItem = new UIMenuItem(subitem.Attribute("text")?.Value ?? "Unknown", "");
                menu.AddItem(menuItem);

                menu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == menuItem)
                    {
                        KeySender.SendKeys(keys);
                        menu.Visible = false;
                    }
                };
            }
        }

        /// <summary>
        /// Creates an UIMenu handling banners.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <returns></returns>
        private UIMenu CreateMenu(string title, string subtitle)
        {
            string banner = _file?.Element("Menu")?.Element("Banner")?.Value ?? null;

            if (banner != null)
            {
                UIMenu menu = new UIMenu(title, subtitle);
                if (File.Exists(NMS.BaseDir + "\\" + banner))
                    menu.SetBannerType(NMS.BaseDir + "\\" + banner);
                return menu;
            }
            else
            {
                if (File.Exists(NMS.BannerBlank))
                {
                    UIMenu menu = new UIMenu(title, subtitle, new Point(0, -107));
                    menu.SetBannerType(NMS.BannerBlank);
                    return menu;
                }
            }
            
            return new UIMenu(title, subtitle);
        }

        private UIMenu AddSubMenu(MenuPool pool, UIMenu menu, string title, string text)
        {
            var item = new UIMenuItem(text);
            menu.AddItem(item);
            var submenu = CreateMenu(title, text);
            pool.Add(submenu);
            menu.BindMenuToItem(submenu, item);
            return submenu;
        }

    }

}
