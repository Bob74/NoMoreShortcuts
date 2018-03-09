using System.Collections.Generic;
using System.Windows.Forms;

namespace NoMoreShortcuts
{
    static class KeySender
    {
        public static List<string> nonDisplayedCharacter = new List<string>
            {
                "BACKSPACE", "BS", "BKSP", "BREAK", "CAPSLOCK", "DELETE", "DEL", "DOWN", "END", "ENTER", "ESC", "HELP", "HOME", "INSERT", "INS", "LEFT", "NUMLOCK", "PGDN", "PGUP",
                "PRTSC", "RIGHT", "SCROLLLOCK", "TAB", "UP", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "F13", "F14", "F15", "F16",
                "ADD", "SUBTRACT", "MULTIPLY", "DIVIDE"
            };

        public static bool SendKeys(List<string> keys)
        {
            string keySequence = "(";
            KeysConverter converter = new KeysConverter();

            foreach (string key in keys)
            {
                int keyValue;

                // Modifier
                if (string.Compare(key, "SHIFT", true) == 0)
                {
                    keySequence = "+" + keySequence;
                    continue;
                }
                else if (string.Compare(key, "CTRL", true) == 0)
                {
                    keySequence = "^" + keySequence;
                    continue;
                }
                else if (string.Compare(key, "ALT", true) == 0)
                {
                    keySequence = "%" + keySequence;
                    continue;
                }

                // Non displayed character
                if (nonDisplayedCharacter.Contains(key.ToUpper()))
                {
                    keySequence += "{" + key + "}";
                    continue;
                }

                // is Hexadecimal?
                if (key.StartsWith("0x"))
                {
                    string hexKey = key.Remove(0, 2); 
                    if (int.TryParse(hexKey, System.Globalization.NumberStyles.HexNumber, null, out keyValue))
                    {
                        string keyChar = converter.ConvertToString(keyValue);
                        if (nonDisplayedCharacter.Contains(keyChar.ToUpper())) keyChar = "{" + keyChar + "}";

                        keySequence += keyChar;
                        continue;
                    }
                }
                // is Integer?
                else
                {
                    if (int.TryParse(key, out keyValue))
                    {
                        string keyChar = converter.ConvertToString(keyValue);
                        if (nonDisplayedCharacter.Contains(keyChar.ToUpper())) keyChar = "{" + keyChar + "}";

                        keySequence += keyChar;
                        continue;
                    }
                }

                // Normal character
                keySequence += key;
                continue;
            }

            // Sending 
            keySequence += ")";
#if DEBUG
            Logger.Log("Sending: " + keySequence);
#endif
            try
            {
                System.Windows.Forms.SendKeys.SendWait(keySequence);
            }
            catch (Exception ex)
            {
                Logger.Log("Error: SendKeys (" + keySequence + ") - " + ex.Message);
                return false;
            }
            
            return true;
        }

    }
}
