using GameJam.Enums;
using GameJam.TileEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Game
{
    public class TrapGenerator
    {
        private readonly Dictionary<TrapType, Func<float, ObjectBehaviour>> traps = new Dictionary<TrapType, Func<float, ObjectBehaviour>>()
        {
            {TrapType.Trap, (time) => { return new Trap(time); } }
        };

        private const int trapsPerRoom = 5;

        public void GenerateTraps(Room room)
        {

        }
    }
}
