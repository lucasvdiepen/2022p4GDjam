using GameJam.Events;
using GameJam.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Bullet : TimerBehaviour
    {
        private Vector2 _direction;

        public Bullet(Vector2 direction, float moveTime) : base(false, moveTime)
        {
            _direction = direction;
        }

        public override void TimerTick(UpdateEvent updateEvent)
        {
            Move(updateEvent.GameContext, updateEvent.RenderObject);
        }

        private void Move(GameContext gameContext, RenderObject renderObject)
        {

        }
    }
}
