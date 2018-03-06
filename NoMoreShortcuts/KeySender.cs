using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace NoMoreShortcuts
{
    static class KeySender
    {

        public static void SendKeys(List<string> keys)
        {
            foreach (string key in keys)
                System.Windows.Forms.SendKeys.SendWait("{" + key + "}");
        }

    }
}
