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
    public class Turret : TimerBehaviour, ITrap
    {
        // todo: moet bepaald worden door trap generator
        private Vector2 _direction = new Vector2(1, 0);
        private float _shootDelay;
        private float _bulletSpeed;

        public Turret(float shootDelay, float bulletSpeed) : base(true, false, shootDelay)
        {
            _shootDelay = shootDelay;
            _bulletSpeed = bulletSpeed;
        }

        public override void TimerTick(UpdateEvent updateEvent)
        {
            Shoot(updateEvent.GameContext, updateEvent.RenderObject);
        }

        private void Shoot(GameContext gameContext, RenderObject renderObject)
        {
            Vector2 spawnPosition = new Vector2(renderObject.rectangle.X + gameContext.tileSize * _direction.x, renderObject.rectangle.Y + gameContext.tileSize * _direction.y);

            if (gameContext.room.IsMoveBlocked(spawnPosition.x, spawnPosition.y)) return;

            var bulletFrames = gameContext.spriteMap.GetBulletFrames(_direction);

            var newBullet = new RenderObject()
            {
                frames = bulletFrames,
                rectangle = new Rectangle(spawnPosition.x, spawnPosition.y, gameContext.tileSize, gameContext.tileSize),
                objectBehaviour = new Bullet(_direction, _bulletSpeed),
                animationTime = _bulletSpeed / bulletFrames.Length
            };

            gameContext.room.activeObjects.Add(newBullet);

            //Deal damage to player
            if (CollisionUtility.HasCollision(newBullet.rectangle, gameContext.player.rectangle)) gameContext.playerHealth.RemoveHealth(1);
        }

        public Rectangle[] GetFrames(SpriteMap spriteMap) => spriteMap.GetTurretFrames(_direction);

        public Vector2 GetSuitableLocation(Room room, int tileSize, Random rnd)
        {
            Tile[] buildableTiles = room.GetBuildableTiles();

            if (buildableTiles.Length == 0) return null;

            var rect = buildableTiles[rnd.Next(0, buildableTiles.Length)].rectangle;

            return new Vector2(rect.X, rect.Y);
        }
    }
}
