# Keys

Notations
---
Keys can be written in different ways, here are the supported types:
* Key’s string value (name) (ie: F4)
  * `<Key>F4</Key>`
* Key’s decimal value (ie: 115)
  * `<Key>115</Key>`
* Key’s hexadecimal value (ie: 0x73)
  * `<Key>0x73</Key>`

Remember you can set multiple keys by adding multiple `<Key>`:  
```XML
<Key>ALT</Key>
<Key>F4</Key>
```

Keys table
---

Description | String value | Hexadecimal value | Decimal value
------------ | ------------ | ------------ | ------------
Left mouse button | LMB | 0x01 | 1
Right mouse button | RMB | 0x02 | 2
Control-break processing | CANCEL | 0x03 | 3
Middle mouse button (three-button mouse) | MMB | 0x04 | 4
BACKSPACE key | BACKSPACE | 0x08 | 8
TAB key | TAB | 0x09 | 9
CLEAR key | CLEAR | 0x0C | 12
ENTER key | RETURN | 0x0D | 13
SHIFT key | SHIFT | 0x10 | 16
CTRL key | CTRL | 0x11 | 17
ALT key | ALT | 0x12 | 18
PAUSE key | PAUSE | 0x13 | 19
CAPS LOCK key | CAPSLOCK | 0x14 | 20
ESC key | ESCAPE | 0x1B | 27
SPACEBAR | SPACE | 0x20 | 32
PAGE UP key | PAGEUP | 0x21 | 33
PAGE DOWN key | PAGEDOWN | 0x22 | 34
END key | END | 0x23 | 35
HOME key | HOME | 0x24 | 36
LEFT ARROW key | LEFT | 0x25 | 37
UP ARROW key | UP | 0x26 | 38
RIGHT ARROW key | RIGHT | 0x27 | 39
DOWN ARROW key | DOWN | 0x28 | 40
SELECT key | SELECT | 0x29 | 41
PRINT key | PRINT | 0x2A | 42
EXECUTE key | EXECUTE | 0x2B | 43
PRINT SCREEN key | PRINTSCREEN | 0x2C | 44
INS key | INSERT | 0x2D | 45
DEL key | DELETE | 0x2E | 46
HELP key | HELP | 0x2F | 47
0 key |  | 0x30 | 48
1 key |  | 0x31 | 49
2 key |  | 0x32 | 50
3 key |  | 0x33 | 51
4 key |  | 0x34 | 52
5 key |  | 0x35 | 53
6 key |  | 0x36 | 54
7 key |  | 0x37 | 55
8 key |  | 0x38 | 56
9 key |  | 0x39 | 57
A key | A | 0x41 | 65
B key | B | 0x42 | 66
C key | C | 0x43 | 67
D key | D | 0x44 | 68
E key | E | 0x45 | 69
F key | F | 0x46 | 70
G key | G | 0x47 | 71
H key | H | 0x48 | 72
I key | I | 0x49 | 73
J key | J | 0x4A | 74
K key | K | 0x4B | 75
L key | L | 0x4C | 76
M key | M | 0x4D | 77
N key | N | 0x4E | 78
O key | O | 0x4F | 79
P key | P | 0x50 | 80
Q key | Q | 0x51 | 81
R key | R | 0x52 | 82
S key | S | 0x53 | 83
T key | T | 0x54 | 84
U key | U | 0x55 | 85
V key | V | 0x56 | 86
W key | W | 0x57 | 87
X key | X | 0x58 | 88
Y key | Y | 0x59 | 89
Z key | Z | 0x5A | 90
Left Windows key (Natural keyboard) | LWIN | 0x5B | 91
Right Windows key (Natural keyboard) | RWIN | 0x5C | 92
Applications key (Natural keyboard) | APPS | 0x5D | 93
Computer Sleep key | SLEEP | 0x5F | 95
Numeric keypad 0 key | NUM0 | 0x60 | 96
Numeric keypad 1 key | NUM1 | 0x61 | 97
Numeric keypad 2 key | NUM2 | 0x62 | 98
Numeric keypad 3 key | NUM3 | 0x63 | 99
Numeric keypad 4 key | NUM4 | 0x64 | 100
Numeric keypad 5 key | NUM5 | 0x65 | 101
Numeric keypad 6 key | NUM6 | 0x66 | 102
Numeric keypad 7 key | NUM7 | 0x67 | 103
Numeric keypad 8 key | NUM8 | 0x68 | 104
Numeric keypad 9 key | NUM9 | 0x69 | 105
Multiply key | * | 0x6A | 106
Add key | PLUS | 0x6B | 107
Separator key | , | 0x6C | 108
Subtract key | - | 0x6D | 109
Decimal key | . | 0x6E | 110
Divide key | / | 0x6F | 111
F1 key | F1 | 0x70 | 112
F2 key | F2 | 0x71 | 113
F3 key | F3 | 0x72 | 114
F4 key | F4 | 0x73 | 115
F5 key | F5 | 0x74 | 116
F6 key | F6 | 0x75 | 117
F7 key | F7 | 0x76 | 118
F8 key | F8 | 0x77 | 119
F9 key | F9 | 0x78 | 120
F10 key | F10 | 0x79 | 121
F11 key | F11 | 0x7A | 122
F12 key | F12 | 0x7B | 123
F13 key | F13 | 0x7C | 124
F14 key | F14 | 0x7D | 125
F15 key | F15 | 0x7E | 126
F16 key | F16 | 0x7F | 127
F17 key | F17 | 0x80 | 128
F18 key | F18 | 0x81 | 129
F19 key | F19 | 0x82 | 130
F20 key | F20 | 0x83 | 131
F21 key | F21 | 0x84 | 132
F22 key | F22 | 0x85 | 133
F23 key | F23 | 0x86 | 134
F24 key | F24 | 0x87 | 135
NUM LOCK key | NUMLOCK | 0x90 | 144
SCROLL LOCK key | SCROLL | 0x91 | 145
Left SHIFT key | LSHIFT | 0xA0 | 160
Right SHIFT key | RSHIFT | 0xA1 | 161
Left CONTROL key | LCTRL | 0xA2 | 162
Right CONTROL key | RCTRL | 0xA3 | 163
Left MENU key | LMENU | 0xA4 | 164
Right MENU key | RMENU | 0xA5 | 165
Browser Back key | BROWSER_BACK | 0xA6 | 166
Browser Forward key | BROWSER_FORWARD | 0xA7 | 167
Browser Refresh key | BROWSER_REFRESH | 0xA8 | 168
Browser Stop key | BROWSER_STOP | 0xA9 | 169
Browser Search key | BROWSER_SEARCH | 0xAA | 170
Browser Favorites key | BROWSER_FAVORITES | 0xAB | 171
Browser Start and Home key | BROWSER_HOME | 0xAC | 172
Volume Mute key | VOLUME_MUTE | 0xAD | 173
Volume Down key | VOLUME_DOWN | 0xAE | 174
Volume Up key | VOLUME_UP | 0xAF | 175
Next Track key | MEDIA_NEXT_TRACK | 0xB0 | 176
Previous Track key | MEDIA_PREV_TRACK | 0xB1 | 177
Stop Media key | MEDIA_STOP | 0xB2 | 178
Play/Pause Media key | MEDIA_PLAY_PAUSE | 0xB3 | 179
Start Mail key | LAUNCH_MAIL | 0xB4 | 180
Select Media key | LAUNCH_MEDIA_SELECT | 0xB5 | 181
Start Application 1 key | LAUNCH_APP1 | 0xB6 | 182
Start Application 2 key | LAUNCH_APP2 | 0xB7 | 183
"Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ';:' key" |  | 0xBA | 186
For any country/region, the '+' key |  | 0xBB | 187
For any country/region, the ',' key |  | 0xBC | 188
For any country/region, the '-' key |  | 0xBD | 189
For any country/region, the '.' key |  | 0xBE | 190
"Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '/?' key" |  | 0xBF | 191
"Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '\`~' key" |  | 0xC0 | 192
"Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '\[{' key" |  | 0xDB | 219
"Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '\|' key" |  | 0xDC | 220
"Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ']}' key" |  | 0xDD | 221
"Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the 'single-quote/double-quote' key" |  | 0xDE | 222
Used for miscellaneous characters; it can vary by keyboard. |  | 0xDF | 223
OEM specific |  | 0xE1 | 225
Either the angle bracket key or the backslash key on the RT 102-key keyboard |  | 0xE2 | 226
OEM specific |  | 0xE3-E4 | 227-228
IME PROCESS key |  | 0xE5 | 229
OEM specific |  | 0xE6 | 230
OEM specific |  | 0xE9-F5 | 233-245
Attn key |  | 0xF6 | 246
CrSel key |  | 0xF7 | 247
ExSel key |  | 0xF8 | 248
Erase EOF key |  | 0xF9 | 249
Play key | PLAY | 0xFA | 250
Zoom key | ZOOM | 0xFB | 251
PA1 key |  | 0xFD | 253
Clear key |  | 0xFE | 254

Useful websites
---
Keys decimal value: [Javascript Char Codes (Key Codes)](https://www.cambiaresearch.com/articles/15/javascript-char-codes-key-codes)
