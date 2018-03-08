# Menu

The `Menu` section is *optional* and let you create a menu to select differents choices if you have many shortcuts.  

The menu must contain at least one of the following parameters:
* `<SubItem text="name of the item"></SubItem>`
* `<SubMenu text="name of the sub menu"></SubMenu>`

---

SubItem
---
Create an item in the menu (button).  
A `SubItem` **must** contain at least one `Key` parameter (you click on the item to press a key).
```XML
<SubItem text="My button">
    <Key>F1</Key>
</SubItem>
```

SubMenu
---
Create a sub menu which will contain SubItem(s).   
A `SubMenu` **must** contain at least one `SubItem` parameter.  
When entering the sub menu, it will display the sub items.
```XML
<SubMenu text="Name of the sub menu">
    <SubItem text="My button 1">
        <Key>F1</Key>
    </SubItem>
    <SubItem text="My button 2">
        <Key>F2</Key>
    </SubItem>
</SubMenu>
```

(Optional) Banner
---
The name with extension of the .png file to display at the top of the menu.  
File's dimension: 512x128
```XML
<Banner>MyBanner.png</Banner>
```
