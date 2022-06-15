using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Spike : ObjectBehaviour
    {
        public override void OnEnter(MoveEvent moveEvent)
        {
            moveEvent.GameContext.playerHealth.RemoveHealth(1);
        }
    }
}
