using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Trap : ObjectBehaviour
    {
        public override void OnEnter(MoveEvent moveEvent)
        {
            //Deal damage to player
            Debug.WriteLine("Deal damage to player");
        }
    }
}
