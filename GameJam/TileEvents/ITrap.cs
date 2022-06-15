using GameJam.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public interface ITrap
    {
        Vector2 GetSuitableLocation(Room room, int tileSize, Random rnd);
        Rectangle[] GetFrames(SpriteMap spriteMap);
    }
}
