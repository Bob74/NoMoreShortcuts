# Creating a new profile
Profiles are XML files (= text files with `.xml` extension) and must be placed in `<GTA5 main folder>\scripts\NoMoreShortcuts`.  
A profile has some mandatory informations that needs to be written.

Profiles contains two main parts: `Phone` and `Menu`
```XML
<?xml version="1.0" encoding="utf-8"?>
<NMS>
    <Phone>
        ...
    </Phone>
    <Menu>
        ...
    </Menu>
</NMS>
```
The `Phone` section is **mandatory** since it creates the phone contact.  
The `Menu` section is *optional* (creates a menu when you call the contact).  
>Menus are useful if a mod uses many shortcuts or if you want to pack differents mods into a single contact.

Documentations
---
[Phone documentation](https://github.com/Bob74/NoMoreShortcuts/blob/master/doc/phone.md) contains all parameters supported for the phone section.  
[Menu documentation](https://github.com/Bob74/NoMoreShortcuts/blob/master/doc/menu.md) contains everything you need to know to create a menu.

Example 1 - Simple
---
[Example files are available here](https://github.com/Bob74/NoMoreShortcuts/tree/master/Example/Simple%20example)  
**Here is an example of the simplest profile:**
```XML
<?xml version="1.0" encoding="utf-8"?>
<NMS>
    <Phone>
        <ContactName>Native Trainer</ContactName>
        <Key>F4</Key>
    </Phone>
</NMS>
```
It will add contact named "Name of the contact" which will press "F1" for you when you call it.  

Example 2 - Complete
---
[Example files are available here](https://github.com/Bob74/NoMoreShortcuts/tree/master/Example/Complete%20example)  
**Here is an example of a very complete profile using a menu:**
```XML
<?xml version="1.0" encoding="utf-8"?>
<NMS>
    <Phone>
        <ContactName>Trainers</ContactName>
        <ContactIcon>CHAR_AMMUNATION</ContactIcon>
        <SoundFile>Trainers.wav</SoundFile>
        <Volume>50</Volume>
    </Phone>
    <Menu>
        <Banner>Trainers.png</Banner>
        <SubItem text="Native Trainer">
            <Key>F4</Key>
        </SubItem>
        <SubMenu text="Other trainers">
            <SubItem text="Menyoo">
                <Key>F8</Key>
            </SubItem>
            <SubItem text="Police Menu">
                <Key>ALT</Key>
                <Key>F7</Key>
            </SubItem>
        </SubMenu>
    </Menu>
</NMS>
```

The result:  
![Phone](https://i.imgur.com/tFoAypl.png)

Main menu | Submenu "Other trainers"
------------ | -------------
![Main menu](https://i.imgur.com/veJHHPL.png) | ![Sub menu](https://i.imgur.com/hAZtnP7.png)
