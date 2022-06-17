using GameJam.Events;
using GameJam.Game;
using GameJam.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.TileEvents
{
    public class Bullet : TimerBehaviour
    {
        private Vector2 _direction;

        public Bullet(Vector2 direction, float moveTime, float frameTimeLeft) : base(false, true, moveTime, frameTimeLeft)
        {
            _direction = direction;
        }

        public override void TimerTick(UpdateEvent updateEvent, float frameTimeLeft)
        {
            Move(updateEvent.GameContext, updateEvent.RenderObject, frameTimeLeft);
        }

        public override void OnEnter(MoveEvent moveEvent)
        {
            moveEvent.GameContext.playerHealth.RemoveHealth(1);
        }

        private void Move(GameContext gameContext, RenderObject renderObject, float frameTimeLeft)
        {
            var newX = renderObject.rectangle.X + gameContext.tileSize * _direction.x;
            var newY = renderObject.rectangle.Y + gameContext.tileSize * _direction.y;

            if (gameContext.room.IsMoveBlocked((int)newX, (int)newY))
            {
                //Spawn bullet explosion
                var newBulletExplosion = new RenderObject(frameTimeLeft)
                {
                    frames = gameContext.spriteMap.GetBulletExplosionFrames(),
                    rectangle = new Rectangle((int)renderObject.rectangle.X, (int)renderObject.rectangle.Y, gameContext.tileSize, gameContext.tileSize),
                    objectBehaviour = new BulletExplosion(1, frameTimeLeft),
                };

                gameContext.room.activeObjects.Add(newBulletExplosion);

                gameContext.room.activeObjects.Remove(renderObject);
                return;
            }

            renderObject.rectangle.X = newX;
            renderObject.rectangle.Y = newY;

            //Deal damage to player
            if (CollisionUtility.HasCollision(renderObject.rectangle, gameContext.player.rectangle)) gameContext.playerHealth.RemoveHealth(1);
        }
    }
}
