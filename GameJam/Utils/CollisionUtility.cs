﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Utils
{
    public static class CollisionUtility
    {
        public static bool HasCollision(RectangleF rectangle1, RectangleF rectangle2)
        {
            return (int)rectangle1.X == (int)rectangle2.X && (int)rectangle1.Y == (int)rectangle2.Y;
        }
    }
}
