# Phone

The `Phone` section is **mandatory** and describes the contact to add to the phone.  
It contains its name, icon, the key(s) to press and other parameters.

---
ContactName
---
Name displayed in the contacts list.
```XML
<ContactName>Name of the contact</ContactName>
```

Key
---
The key the contact will press for you.
```XML
<Key>F8</Key>
```
You can set multiple keys if the mod allow it:
```XML
<Key>ALT</Key>
<Key>F4</Key>
```

(Optional) ContactIcon
---
The icon of the contact.  
Find the full list of values here: https://wiki.gtanet.work/index.php?title=Notification_Pictures (try to be creative!)
>If you don't put `<ContactIcon>` in your profile, the value will be automatically set to CHAR_DEFAULT.
```XML
<ContactIcon>CHAR_DEFAULT</ContactIcon>
```

(Optional) DialTimeout
---
Delay before the contact pickup the phone.  
Time is set in millisecond. 0 = Instant.
>If you don't put `<DialTimeout>` in your profile, the value will be automatically set to 0.
```XML
<DialTimeout>0</DialTimeout>
```

(Optional) SoundFile
---
The name with extension of the .wav file to play when the contact pickup the phone.  
The file must be placed in the same folder as the profile.
```XML
<SoundFile>MySoundFile.wav</SoundFile>
```

(Optional) Volume
---
Set the volume the sound file must be played.  
The value is from 0 to 100.
>If you don't put `<Volume>` in your profile, the value will be automatically set to 25.
```XML
<Volume>25</Volume>
```

(Optional) Notification parameters
---
**NotificationIcon**  
The picture of the notification.  
It needs a phone contact picture to work: https://wiki.gtanet.work/index.php?title=Notification_Pictures
```XML
<NotificationIcon>CHAR_DEFAULT</NotificationIcon>
```

**NotificationTitle**  
The title of the notification.
```XML
<NotificationTitle>Notification title</NotificationTitle>
```

**NotificationSubtitle**  
The subtitle of the notification (below the title and smaller).
```XML
<NotificationSubtitle>Notification subtitle</NotificationSubtitle>
```

**NotificationMessage**  
The body of the notification.
```XML
<NotificationMessage>Body message of the notification.</NotificationMessage>
```

**NotificationDelay**  
Delay before the notification shows.
```XML
<NotificationDelay>0</NotificationDelay>
```
