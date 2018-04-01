using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        internal string ContactIcon { get; private set; }
        internal bool Bold { get; private set; }
        internal string SoundFile { get; private set; }
        internal int Volume { get; private set; }
        internal Notification Notification { get; private set; }
        internal List<string> Keys { get; private set; }
        internal MenuPool Pool { get; private set; }
        internal UIMenu Menu { get; private set; }
        internal int MenuHotKeyModifier { get; private set; }
        internal int MenuHotKey { get; private set; }
        internal int MenuGamepadHotKey { get; private set; }

        public Profile(string xmlFile)
        {
            if (File.Exists(xmlFile))
            {
                try
                {
                    _file = XElement.Load(xmlFile);
                    FilePath = xmlFile;
                    
                    // If the profile contains a phone contact
                    if (_file?.Element("Phone") != null)
                    {
                        SoundFile = GetSoundFile();
                        Volume = GetSoundVolume();
                        Contact = GetiFruitContact();
                        Keys = GetShortcutKeys();

                        // Notification parameters (phone contact only)
                        // Menu items notifications are handled by the menu item itself.
                        object[] notif = GetNotificationParameters();
                        if (notif.GetUpperBound(0) == 5)
                            Notification = new Notification()
                            {
                                Icon = (string)notif[0],
                                Title = (string)notif[1],
                                Subtitle = (string)notif[2],
                                Message = (string)notif[3],
                                Delay = (int)notif[4],
                                Sound = (bool)notif[5]
                            };
                        else
                            Logger.Log("Error: Profile - Error reading notifications parameters.");

                    }

                    // If the profile contains menus
                    if (_file?.Element("Menu") != null)
                    {
                        object[] menu = GetMenu();
                        if (menu != null)
                        {
                            if (menu.GetUpperBound(0) == 4)
                            {
                                Pool = (MenuPool)menu[0];
                                Menu = (UIMenu)menu[1];
                                MenuHotKey = (int)menu[2];
                                MenuGamepadHotKey = (int)menu[3];
                                MenuHotKeyModifier = (int)menu[4];
                            }
                            else
                                Logger.Log("Error: Profile - Error reading menu informations.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log("Error: Profile - Cannot parse XML file: " + e.Message);
                }
            }
            else
                Logger.Log("Error: ContactXmlReader - File doesn't exist (" + xmlFile + ").");
        }

        /// <summary>
        /// Show a notification depending the notifications informations set in the profile.
        /// Check if everything is filled correctly and send the notification's parameters to the main script.
        /// </summary>
        /// <param name="icon">Icon's name ("CHAR_XXX...").</param>
        /// <param name="title">Title of the notification.</param>
        /// <param name="subtitle">Subtitle of the notification.</param>
        /// <param name="message">Message of the body of the notification.</param>
        /// <param name="delay">Delay before the notification shows up (in milliseconds).</param>
        public void ShowNotificationIfAvailable(string icon, string title, string subtitle, string message, int delay, bool sound)
        {
            // We need at least the body message to display a notification
            if (string.IsNullOrEmpty(message)) return;

            Notification notif = new Notification()
            {
                Icon = (string.IsNullOrEmpty(icon)) ? ContactIcon : icon,
                Title = (title == null) ? Contact.Name : title,
                Subtitle = (subtitle == null) ? "" : subtitle,
                Message = message,
                Delay = delay,
                Sound = sound
            };

            // Wait and display the notification without blocking the current thread
            NMS.CurrentInstance.HandleNotification(notif);
        }


        /// <summary>
        /// Return the sound file name to play when the contact answer the call.
        /// </summary>
        /// <returns>File name.</returns>
        private string GetSoundFile()
        {
            string file = null;

            // Backward compatibility
            if (_file?.Element("Phone")?.Element("Sound") != null)
                file = _file.Element("Phone").Element("Sound").Element("SoundFile")?.Value ?? null;
            else
                file = _file?.Element("Phone")?.Element("SoundFile")?.Value ?? null;

            if (file != null)
                if (File.Exists(NMS.BaseDir + "\\" + file))
                    return NMS.BaseDir + "\\" + file;

            return null;
        }

        /// <summary>
        /// Return the volume level the sound will be played at.
        /// </summary>
        /// <returns>Sound volume in percent.</returns>
        private int GetSoundVolume()
        {
            // Backward compatibility
            if (_file?.Element("Phone")?.Element("Sound") != null)
                return int.Parse(_file.Element("Phone").Element("Sound").Element("Volume")?.Value ?? "25");
            else
                return int.Parse(_file?.Element("Phone")?.Element("Volume")?.Value ?? "25");
        }

        /// <summary>
        /// Return the notification parameters used to show a notification.
        /// The notification is shown when the contact answer the call.
        /// </summary>
        /// <returns></returns>
        private object[] GetNotificationParameters()
        {
            object[] parameters = { null, null, null, null, null, null };

            // Backward compatibility
            if (_file?.Element("Phone")?.Element("Notification") != null)
            {
                parameters[0] = _file.Element("Phone").Element("Notification").Element("NotificationIcon")?.Value ?? null;
                parameters[1] = _file.Element("Phone").Element("Notification").Element("NotificationTitle")?.Value ?? "";
                parameters[2] = _file.Element("Phone").Element("Notification").Element("NotificationSubtitle")?.Value ?? "";
                parameters[3] = _file.Element("Phone").Element("Notification").Element("NotificationMessage")?.Value ?? null;
                parameters[4] = int.Parse(_file.Element("Phone").Element("Notification").Element("NotificationDelay")?.Value ?? "0");
                parameters[5] = bool.Parse(_file.Element("Phone").Element("Notification").Element("NotificationSound")?.Value ?? "True");
            }
            else
            {
                parameters[0] = _file?.Element("Phone")?.Element("NotificationIcon")?.Value ?? null;
                parameters[1] = _file?.Element("Phone")?.Element("NotificationTitle")?.Value ?? "";
                parameters[2] = _file?.Element("Phone")?.Element("NotificationSubtitle")?.Value ?? "";
                parameters[3] = _file?.Element("Phone")?.Element("NotificationMessage")?.Value ?? null;
                parameters[4] = int.Parse(_file?.Element("Phone")?.Element("NotificationDelay")?.Value ?? "0");
                parameters[5] = bool.Parse(_file?.Element("Phone")?.Element("NotificationSound")?.Value ?? "True");
            }
            return parameters;
        }

        /// <summary>
        /// Returns an iFruit contact ready to be added to the contact collection of a customiFruit.
        /// </summary>
        /// <returns>The phone contact.</returns>
        private iFruitContact GetiFruitContact()
        {
            string contactName = _file?.Element("Phone")?.Element("ContactName")?.Value ?? "Unknown";
            ContactIcon = _file?.Element("Phone")?.Element("ContactIcon")?.Value ?? "CHAR_DEFAULT";
            Bold = bool.Parse(_file?.Element("Phone")?.Element("Bold")?.Value ?? "False");
            int dialTimeout = int.Parse(_file?.Element("Phone")?.Element("DialTimeout")?.Value ?? "0");

            iFruitContact contact = new iFruitContact(contactName);
            contact.Bold = Bold;
            contact.Active = true;
            contact.DialTimeout = dialTimeout;
            contact.Icon = new ContactIcon(ContactIcon);

            return contact;
        }

        /// <summary>
        /// Returns a list of string representing the shortcut keys.
        /// If the contact triggers a menu, the menu handles the differents shortcuts itself.
        /// </summary>
        /// <returns>Key list as strings (ex: "F4", "115", "0x73", ...).</returns>
        private List<string> GetShortcutKeys()
        {
            XElement phone = _file.Element("Phone");
            List<string> keys = new List<string>();

            if (phone.Element("Keys") != null)
            {
                foreach (XElement key in phone.Element("Keys").Elements("Key"))
                    keys.Add(key.Value);
            }
            else
            {
                // Menu items don't use <Keys> since they are already key containers themselves
                foreach (XElement key in phone.Elements("Key"))
                    keys.Add(key.Value);
            }

            return keys;
        }

        /// <summary>
        /// Return the MenuPool (used to process menu inputs and drwaing) and the root UIMenu element (containing items, submenus, etc).
        /// Access menuPool and UIMenu:
        /// </summary>
        /// <returns>
        /// Two elements array containing MenuPool and UIMenu.
        /// Pool = (MenuPool)menu[0] and Menu = (UIMenu)menu[1]
        /// </returns>
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

                int hotkeyModifier = 0, hotkey = 0, gamepadHotkey = 0;
                try
                {
                    // Backward compatibility
                    if (menuElement.Element("Keys") != null)
                    {
                        hotkeyModifier = int.Parse(menuElement.Element("Keys").Element("ModifierKey")?.Value ?? "0");
                        hotkey = int.Parse(menuElement.Element("Keys").Element("Key")?.Value ?? "0");
                        gamepadHotkey = int.Parse(menuElement.Element("Keys").Element("GamepadKey")?.Value ?? "0");
                    }
                    else
                    {
                        hotkeyModifier = int.Parse(menuElement.Element("ModifierKey")?.Value ?? "0");
                        hotkey = int.Parse(menuElement.Element("Key")?.Value ?? "0");
                        gamepadHotkey = int.Parse(menuElement.Element("GamepadKey")?.Value ?? "0");
                    }
                }
                catch
                {
                    // Hotkeys to open the menu aren't required.
                }


                return new object[] { pool, mainMenu, hotkey, gamepadHotkey, hotkeyModifier };
            }
            return null;
        }

        /// <summary>
        /// Creates an UIMenu handling banners.
        /// </summary>
        /// <param name="title">Title (shown on the banner).</param>
        /// <param name="subtitle">Subtitle (shown below the banner).</param>
        /// <returns>Created menu.</returns>
        private UIMenu CreateMenu(string title, string subtitle)
        {
            string banner = _file?.Element("Menu")?.Element("Banner")?.Value ?? null;

            // If banner file exist, set the banner
            if (banner != null)
            {
                UIMenu menu = new UIMenu(title, subtitle);
                if (File.Exists(NMS.BaseDir + "\\" + banner))
                    menu.SetBannerType(NMS.BaseDir + "\\" + banner);
                return menu;
            }
            // Otherwise, no banner (blank file + move the menu upward)
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

        /// <summary>
        /// Add an item to an existing menu.
        /// The item informations are read from the current <SubItem> node.
        /// </summary>
        /// <param name="startElement">XML element containing the subitem(s)</param>
        /// <param name="menu">UIMenu element that will contain the item.</param>
        private void AddSubitems(XElement startElement, UIMenu menu)
        {
            foreach (XElement subitem in startElement.Elements("SubItem"))
            {
                // Getting shortcut keys
                List<string> itemKeys = new List<string>();
                foreach (XElement key in subitem.Elements("Key"))
                    itemKeys.Add(key.Value);
                
                UIMenuItem menuItem = new UIMenuItem(subitem.Attribute("text")?.Value ?? "Unknown", "");
                menu.AddItem(menuItem);

                menu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == menuItem)
                    {
                        if (itemKeys.Count > 0)
                        {
                            menu.Visible = false;
                            KeySender.SendKeys(itemKeys);
                            ShowNotificationIfAvailable(subitem.Element("Notification")?.Element("NotificationIcon")?.Value ?? null,
                                                        subitem.Element("Notification")?.Element("NotificationTitle")?.Value ?? null,
                                                        subitem.Element("Notification")?.Element("NotificationSubtitle")?.Value ?? null,
                                                        subitem.Element("Notification")?.Element("NotificationMessage")?.Value ?? null,
                                                        int.Parse(subitem.Element("Notification")?.Element("NotificationDelay")?.Value ?? "0"),
                                                        bool.Parse(subitem.Element("Notification")?.Element("NotificationSound")?.Value ?? "True"));

                            if (subitem.Element("Sound") != null)
                            {
                                string file = subitem.Element("Sound").Element("SoundFile")?.Value ?? null;
                                int volume = int.Parse(subitem.Element("Sound").Element("Volume")?.Value ?? "25");
                                if (file != null) WaveStream.PlaySound(NMS.BaseDir + "\\" + file, volume);
                            }
                        }
                    }
                };
            }
        }

        /// <summary>
        /// Add a submenu to an existing menu.
        /// The submenu is read from the current <Submenu> node.
        /// </summary>
        /// <param name="pool">MenuPool that will handle the submenu.</param>
        /// <param name="menu">UIMenu element that will contain the submenu.</param>
        /// <param name="title">Title of the menu.</param>
        /// <param name="text">Text displayed on the submenu element.</param>
        /// <returns></returns>
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
