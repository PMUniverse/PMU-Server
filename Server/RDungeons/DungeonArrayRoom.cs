﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.RDungeons {
    public class DungeonArrayRoom {

        public int Opened { get; set; }

        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public DungeonArrayRoom() {
            StartX = -1;
            StartY = -1;
            EndX = -1;
            EndY = -1;
        }

    }
}
