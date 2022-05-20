using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Game
{
    public abstract class TileBehaviour
    {
        public float x;
        public float y;

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
