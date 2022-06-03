using GameJam.Events;
using GameJam.Game;
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
        private float _moveTime;
        private float _timeElapsed;

        public Trap(float moveTime)
        {
            _moveTime = moveTime;
        }

        public override void OnEnter(MoveEvent moveEvent)
        {
            //Deal damage to player
            Debug.WriteLine("Deal damage to player");
        }

        public override void Update(float frameTime, RenderObject renderObject)
        {
            _timeElapsed += frameTime;

            if(_timeElapsed >= _moveTime)
            {
                _timeElapsed = 0;
                Move(renderObject);
            }
        }

        private void Move(RenderObject renderObject)
        {
            Debug.WriteLine("trap move");

            renderObject.rectangle.X += 1 * 16;
        }
    }
}
