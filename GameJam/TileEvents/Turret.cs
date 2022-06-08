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
    public class Turret : TimerBehaviour
    {
        Vector2 _direction = new Vector2(1, 0);

        public Turret(float shootDelay) : base(true, shootDelay)
        {

        }

        public override void TimerTick(UpdateEvent updateEvent)
        {
            Shoot(updateEvent.GameContext, updateEvent.RenderObject);
        }

        private void Shoot(GameContext gameContext, RenderObject renderObject)
        {
            Vector2 spawnPosition = new Vector2(renderObject.rectangle.X + gameContext.tileSize * _direction.x, renderObject.rectangle.Y + gameContext.tileSize * _direction.y);

            if (gameContext.room.IsMoveBlocked((int)spawnPosition.x, (int)spawnPosition.y)) return;

            var newBullet = new RenderObject()
            {
                frames = gameContext.spriteMap.GetPlayerFrames(),
                rectangle = new Rectangle((int)spawnPosition.x, (int)spawnPosition.y, gameContext.tileSize, gameContext.tileSize),
                objectBehaviour = new Bullet(_direction, 0.5f)
            };

            gameContext.room.activeObjects.Add(newBullet);

            //Deal damage to player
            if (CollisionUtility.HasCollision(newBullet.rectangle, gameContext.player.rectangle)) gameContext.playerHealth.RemoveHealth(1);
        }
    }
}
