using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class TileBehaviour
    {
        public float X { get; private set; }
        public float Y {get; private set; }
        private bool isMoveBlocked;

        public TileBehaviour()
        {

        }

        public TileBehaviour(bool isMoveBlocked)
        {
            this.isMoveBlocked = isMoveBlocked;
        }

        public virtual CanEnterEvent CanEnter(MoveEvent moveEvent)
        {
            return new CanEnterEvent() { BlockMovement = isMoveBlocked, BlockEvents = true };
        }

        public virtual void OnEnter(MoveEvent moveEvent)
        {
            
        }

        public virtual void OnExit(MoveEvent moveEvent)
        {
            
        }

        public virtual void Update(float frameTime)
        {
            
        }
    }
}
