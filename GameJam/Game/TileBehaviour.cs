using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Game
{
    public class TileBehaviour
    {
        public float X { get; private set; }
        public float Y {get; private set; }
        public bool IsMoveBlocked { get; set; }

        public TileBehaviour(bool isMoveBlocked)
        {
            IsMoveBlocked = isMoveBlocked;
        }

        public virtual void OnEnter(MoveEvent moveEvent)
        {
            
        }

        public virtual void OnExit()
        {

        }

        public virtual void Update(float frameTime)
        {

        }
    }
}
