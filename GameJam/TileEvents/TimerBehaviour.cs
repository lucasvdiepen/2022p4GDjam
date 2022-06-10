using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public abstract class TimerBehaviour : ObjectBehaviour
    {
        private float _timeElapsed;
        private float _time;

        public TimerBehaviour(bool isMoveBlocked, float time) : base(isMoveBlocked)
        {
            _time = time;
        }

        public override void Update(UpdateEvent updateEvent)
        {
            _timeElapsed += updateEvent.FrameTime;

            if (_timeElapsed >= _time)
            {
                _timeElapsed = 0;
                TimerTick(updateEvent);
            }
        }

        public abstract void TimerTick(UpdateEvent updateEvent);
    }
}
