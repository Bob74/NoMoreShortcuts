# Notifications

Parameters
---
Notifications can be set in the `<Phone>` section and in the `<SubItem>` sections.  
The `<Phone>` section will show a notification when the contact answer the phone except if your profile use a menu.  
The `<SubItem>` section will show a notification when you select the item. Each `<SubItem>` will trigger its own notification, so if you want each `<SubItem>` to display a notification, you will need to declare the notifications parameters to each ones.  

Let see the parameters we need to put to have a notification.

### NotificationMessage
The body of the notification.
```XML
<NotificationMessage>Body message of the notification.</NotificationMessage>
```

### (Optional) NotificationIcon
The picture of the notification.  
It needs a phone contact picture to work.  
Find the full list of values here: https://wiki.gtanet.work/index.php?title=Notification_Pictures
>If you don't put `<NotificationIcon>` in your profile, the value will be automatically set to the same as `<ContactIcon>`.
```XML
<NotificationIcon>CHAR_DEFAULT</NotificationIcon>
```

### (Optional) NotificationTitle
The title of the notification.
>If you don't put `<NotificationTitle>` in your profile, there will simply be no title.
```XML
<NotificationTitle>Notification title</NotificationTitle>
```

### (Optional) NotificationSubtitle
The subtitle of the notification (below the title and smaller).
>If you don't put `<NotificationSubtitle>` in your profile, there will simply be no subtitle.
```XML
<NotificationSubtitle>Notification subtitle</NotificationSubtitle>
```

### (Optional) NotificationDelay
Delay before the notification shows.  
Time is set in millisecond. 0 = Instant.  
>If you don't put `<NotificationDelay>` in your profile, the value will be automatically set to 0.
```XML
<NotificationDelay>0</NotificationDelay>
```

Examples
---
### Calling the phone contact
```XML
<Phone>
    <ContactName>Native Trainer</ContactName>
    <Key>F1</Key>
    <NotificationIcon>CHAR_DEFAULT</NotificationIcon>
    <NotificationTitle>Notification title</NotificationTitle>
    <NotificationSubtitle>Notification subtitle</NotificationSubtitle>
    <NotificationMessage>Body message of the notification.</NotificationMessage>
    <NotificationDelay>0</NotificationDelay>
</Phone>
...
```

### Using a menu subitem
```XML
...
<SubItem text="My button">
    <NotificationIcon>CHAR_DEFAULT</NotificationIcon>
    <NotificationTitle>Notification title</NotificationTitle>
    <NotificationSubtitle>Notification subtitle</NotificationSubtitle>
    <NotificationMessage>Body message of the notification.</NotificationMessage>
    <NotificationDelay>0</NotificationDelay>
    <Key>F1</Key>
</SubItem>
...
```
