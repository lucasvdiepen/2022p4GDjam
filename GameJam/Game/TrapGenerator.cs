using GameJam.Enums;
using GameJam.TileEvents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Game
{
    public static class TrapGenerator
    {
        private static readonly Func<ObjectBehaviour>[] traps = {
            () => { return new Trap(2); }
        };

        private const int trapsPerRoom = 5;

        public static void GenerateTraps(Room room, SpriteMap spriteMap, int tileSize)
        {
            Random rnd = new Random();
            for(int i = 0; i < trapsPerRoom; i++)
            {
                var newTrap = traps[rnd.Next(0, traps.Length)]();
                var newTrapInterface = (ITrap)newTrap;
                Vector2 suitableLocation = newTrapInterface.GetSuitableLocation(room, tileSize);

                var newRenderObject = new RenderObject()
                {
                    frames = newTrapInterface.GetFrames(spriteMap),
                    rectangle = new Rectangle(suitableLocation.x, suitableLocation.y, tileSize, tileSize),
                    objectBehaviour = newTrap
                };

                room.activeObjects.Add(newRenderObject);
            }
        }
    }
}
