using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA.Native;
using GTA;
using System.Net;
using System.Reflection;

namespace NoMoreShortcuts
{
    internal static class Tools
    {
        /// <summary>
        /// Display a notification.
        /// </summary>
        /// <param name="text"></param>
        public static void DrawNotification(string text)
        {
            Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "CELL_EMAIL_BCON");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, "CELL_EMAIL_BCON");          //  ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME
            Function.Call(Hash._DRAW_NOTIFICATION, false, true);
        }

        /// <summary>
        /// Display a notification with a picture.
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        public static void DrawNotification(string picture, string title, string subtitle, string message)
        {
            Function.Call(Hash.REQUEST_STREAMED_TEXTURE_DICT, picture, false);
            while (!Function.Call<bool>(Hash.HAS_STREAMED_TEXTURE_DICT_LOADED, picture))
                Script.Yield();

            Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, message);
            Function.Call(Hash._SET_NOTIFICATION_MESSAGE, picture, picture, false, 4, title, subtitle);
            Function.Call(Hash._DRAW_NOTIFICATION, false, true);
        }


        public static bool IsUpdateAvailable()
        {
            string downloadedString = "";
            Version onlineVersion;

            try
            {
                WebClient client = new WebClient();
                downloadedString = client.DownloadString("https://raw.githubusercontent.com/Bob74/NoMoreShortcuts/master/version");

                downloadedString = downloadedString.Replace("\r", "");
                downloadedString = downloadedString.Replace("\n", "");

                onlineVersion = new Version(downloadedString);

                client.Dispose();

                if (onlineVersion.CompareTo(Assembly.GetExecutingAssembly().GetName().Version) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Logger.Log("Error: IsUpdateAvailable - " + e.Message);
            }

            return false;
        }

        public static void NotifyNewUpdate()
        {
            UI.Notify("NoMoreShortcuts: A new update is available!", true);
        }

    }
}
