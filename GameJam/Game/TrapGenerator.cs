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
    public class TrapGenerator
    {
        /*private readonly Dictionary<TrapType, Func<float, ObjectBehaviour>> traps = new Dictionary<TrapType, Func<float, ObjectBehaviour>>()
        {
            {TrapType.Trap, (time) => { return new Trap(time); } }
        };*/

        private Func<ObjectBehaviour>[] traps = {
            () => { return new Trap(2); }
        };

        private const int trapsPerRoom = 5;

        public void GenerateTraps(Room room, GameContext gameContext)
        {
            Random rnd = new Random();
            var l = traps.Length;
            for(int i = 0; i < l; i++)
            {
                var newTrap = traps[i]();
                ITrap newTrapInterface = (ITrap)newTrap;
                Vector2 suitableLocation = newTrapInterface.GetSuitableLocation(room);

                var newRenderObject = new RenderObject()
                {
                    frames = newTrapInterface.GetFrames(gameContext),
                    rectangle = new Rectangle(2 * gameContext.tileSize, 2 * gameContext.tileSize, gameContext.tileSize, gameContext.tileSize),
                    objectBehaviour = newTrap
                };
            }
        }
    }
}
