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
            () => { return new Saw(2); },
            () => { return new Turret(2, 0.5f); },
            () => { return new Spike(); }
        };

        private const int trapsPerRoom = 5;

        private static Random rnd = new Random();

        public static void GenerateTraps(Room room, SpriteMap spriteMap, int tileSize)
        {
            for(int i = 0; i < trapsPerRoom; i++)
            {
                var newTrap = traps[rnd.Next(0, traps.Length)]();
                var newTrapInterface = (ITrap)newTrap;
                Vector2 suitableLocation = newTrapInterface.GetSuitableLocation(room, tileSize, rnd);

                var newRenderObject = new RenderObject()
                {
                    frames = newTrapInterface.GetFrames(spriteMap),
                    rectangle = new Rectangle(suitableLocation.x, suitableLocation.y, tileSize, tileSize),
                    objectBehaviour = newTrap,
                    animationTime = 0.1f
                };

                room.activeObjects.Add(newRenderObject);
            }
        }
    }
}
