using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class BulletExplosion : TimerBehaviour
    {
        public BulletExplosion(float time, float frameTimeLeft) : base(false, true, true, time, frameTimeLeft)
        {

        }

        public override void TimerTick(UpdateEvent updateEvent, float frameTimeLeft)
        {
            updateEvent.GameContext.room.activeObjects.Remove(updateEvent.RenderObject);
        }
    }
}
