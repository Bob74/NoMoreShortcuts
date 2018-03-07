# Phone

The ``Phone`` part is mandatory and describes the contact to add to the phone.  
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
Find the full list of values here: https://wiki.gtanet.work/index.php?title=Notification_Pictures
```XML
<ContactIcon>CHAR_DEFAULT</ContactIcon>
```

(Optional) DialTimeout
---
Delay before the contact pickup the phone.  
Time is set in millisecond. 0 = Instant.
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
If you don't put <Volume> in your profile, the value will beautomatically set to 25.
```XML
<Volume>25</Volume>
```
