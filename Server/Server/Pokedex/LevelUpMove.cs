﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Pokedex
{
    public class LevelUpMove
    {
        public int Level { get; set; }
        public int Move { get; set; }

        public LevelUpMove(int name, int level) {
            Move = name;
            Level = level;
        }
    }
}
