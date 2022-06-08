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
            var newX = renderObject.rectangle.X + gameContext.tileSize * _direction.x;
            var newY = renderObject.rectangle.Y + gameContext.tileSize * _direction.y;

            if (gameContext.room.IsMoveBlocked((int)newX, (int)newY))
            {
                gameContext.room.activeObjects.Remove(renderObject);
                return;
            }

            renderObject.rectangle.X = newX;
            renderObject.rectangle.Y = newY;

            if ((int)renderObject.rectangle.X == (int)gameContext.player.rectangle.X && (int)renderObject.rectangle.Y == (int)gameContext.player.rectangle.Y)
            {
                Debug.WriteLine("Deal damage to player");
                gameContext.playerHealth.RemoveHealth(1);
            }
        }
    }
}
