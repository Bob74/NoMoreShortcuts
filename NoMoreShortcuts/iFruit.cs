
using GTA.Native;
using iFruitAddon2;

namespace NoMoreShortcuts
{
    class iFruit
    {
        private CustomiFruit _customiFruit;
        public CustomiFruit CustomiFruit { get => _customiFruit; }

        public iFruit()
        {
            _customiFruit = new CustomiFruit();
            AddContacts();
        }

        private void AddContacts()
        {
            foreach (Profile profile in NMS.ProfileCollection)
            {
                if (profile.Contact != null)
                {
                    profile.Contact.Answered += ContactAnswered;
                    _customiFruit.Contacts.Add(profile.Contact);
                }
            }
        }

        private void ContactAnswered(iFruitContact contact)
        {
            Profile profile = NMS.ProfileCollection.Find(x => x.Contact == contact);
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
                    {
                        KeySender.SendKeys(profile.Keys);
                        profile.ShowNotificationIfAvailable(profile.Notification.Icon,
                                                            profile.Notification.Title,
                                                            profile.Notification.Subtitle,
                                                            profile.Notification.Message,
                                                            profile.Notification.Delay,
                                                            profile.Notification.Sound);
                    }
                }
            }

            _customiFruit.Close();
        }

    }
}
