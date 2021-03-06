using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Game
{
    public class Vector2Comparer : IEqualityComparer<Vector2>
    {
        public bool Equals(Vector2 x, Vector2 y)
        {
            return x.x == y.x && x.y == y.y;
        }

        public int GetHashCode(Vector2 obj)
        {
            return obj.x.GetHashCode() + obj.y.GetHashCode();
        }
    }
}
