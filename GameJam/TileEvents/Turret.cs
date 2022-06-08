using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Turret : TimerBehaviour
    {
        public Turret(float shootDelay) : base(true, shootDelay)
        {

        }

        public override void TimerTick(UpdateEvent updateEvent)
        {
            
        }
    }
}
