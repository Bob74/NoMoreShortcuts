using System;
using System.Collections.Generic;
using System.IO;

namespace NoMoreShortcuts
{
    static class KeySender
    {
        private static StreamWriter _pipeWriter;
        public static StreamWriter PipeWriter { get => _pipeWriter; set => _pipeWriter = value; }

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

            if (string.IsNullOrEmpty(keySequence)) return;
            try
            {
                PipeWriter.Write(keySequence);
                PipeWriter.Flush();
            }
            catch (IOException ex)
            {
                Logger.Log("Error: SendKeys - Pipe is not available " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Log("Error: SendKeys - Unknown error " + ex.Message);
            }
        }

    }
}
