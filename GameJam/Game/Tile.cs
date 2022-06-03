using System.Drawing;
using GameJam.TileEvents;

namespace GameJam.Game
{
    public class Tile
    {
        public char graphic;
        public Rectangle sprite;
        public float frame;
        internal Rectangle rectangle;
        public TileBehaviour objectBehaviour;
    }
}



