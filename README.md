# NoMoreShortcuts

![Phone menu](https://i.imgur.com/gddGwUE.png)

No More Shortcuts lets you add phone contacts that will trigger other mods shortcuts for you!

**It is simple:**
* It uses a XML file to create a phone contact
* When you call this contact, it will press the defined key(s) for you

Instead of remembering every shortcuts, simply go through your contact’s list and call the mod’s functionality.

It works using a **profile** system (a XML file containing informations about the phone contact and the key(s) to press).
Each mod must have a XML profile file to be added to the phone's contact list.

When creating the profile file, you might have to tweak the keys a little bit. Some keys are not supported by some mods very well. Just try another key.

**If the mod lets you edit the key(s), you will have to use the same key(s) in both the mod config file and the XML profile file.**

Note that the shortcut set by the mods will still work.

Download link
---


Prerequisites
---
* [ScriptHookV](http://www.dev-c.com/gtav/scripthookv/)
* [ScriptHookVDotNet2](https://github.com/crosire/scripthookvdotnet/releases)
* [NativeUI](https://github.com/Guad/NativeUI/releases)
* [iFruitAddon2](https://github.com/Bob74/iFruitAddon2/releases)
 
Mod installation
---
* Install all prerequisites.
* Create a folder named **scripts** in your GTA V main directory (the one that contains **GTAV.exe**).
* Copy **NoMoreShortcuts.dll** and the **NoMoreShortcuts** folder to your scripts directory.
* Create or install an existing XML profile.

Installing an existing profile
---
* Paste XML file into **scripts\NoMoreShortcuts** folder.
* Edit XML file in order to have the same key(s) between the mod config and the profile.

Finding key values
---
Here are links to website giving keys codes: [Keys](https://github.com/Bob74/NoMoreShortcuts/blob/master/doc/keys.md).

Creating a new profile
---
Follow this guide: [Creating a new profile](https://github.com/Bob74/NoMoreShortcuts/blob/master/doc/creatingProfiles.md).  
Examples are available [here](https://github.com/Bob74/NoMoreShortcuts/tree/master/Example).

