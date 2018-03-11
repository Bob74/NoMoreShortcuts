using System;
using System.Collections.Generic;
using System.IO;

namespace NoMoreShortcuts
{
    static class KeySender
    {
        private static string exchangeFile = AppDomain.CurrentDomain.BaseDirectory + "\\..\\" + "NoMoreShortcuts.tmp";

        public static void SendKeys(List<string> keys)
        {
            string keySequence = "";

            foreach (string key in keys)
                keySequence += "+" + key;
            
            keySequence = keySequence.Remove(0, 1); // Remove the first '+'


            // Sending 
#if DEBUG
            Logger.Log("Sending: " + keySequence);
#endif

            StreamWriter sw = new StreamWriter(exchangeFile);
            bool written = false;
            while (!written)
            {
                try
                {
                    sw.Write(keySequence);
                    written = true;
                }
                catch (IOException)
                {
                    // The file is locked when StreamReader or StreamWriter has opened it.
                    // We must wait until the file is released.
                }
                catch (Exception ex)
                {
                    // Unknown error occured
                    Logger.Log("Error: SendKeys (" + keySequence + ") - " + ex.Message);
                    written = true;
                }
            }

            sw.Close();
        }

    }
}
