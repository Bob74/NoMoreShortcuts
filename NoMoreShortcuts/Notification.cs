﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMoreShortcuts
{
    public class Notification
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Message { get; set; }
        public int Delay { get; set; }
        public bool Sound { get; set; }
        public int EndTimer { get; set; }
    }
}