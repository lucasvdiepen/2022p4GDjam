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
        private Vector2 _direction = new Vector2(1, 0);
        private float _bulletSpeed;

        public Turret(float shootDelay, float bulletSpeed) : base(true, false, shootDelay)
        {
            _bulletSpeed = bulletSpeed;
        }

        public override void TimerTick(UpdateEvent updateEvent, float frameTimeLeft)
        {
            Shoot(updateEvent.GameContext, updateEvent.RenderObject, frameTimeLeft);
        }

        private void Shoot(GameContext gameContext, RenderObject renderObject, float frameTimeLeft)
        {
            Vector2 spawnPosition = new Vector2(renderObject.rectangle.X + gameContext.tileSize * _direction.x, renderObject.rectangle.Y + gameContext.tileSize * _direction.y);

            if (gameContext.room.IsMoveBlocked(spawnPosition.x, spawnPosition.y)) return;

            var bulletFrames = gameContext.spriteMap.GetBulletFrames(_direction);

            var newBullet = new RenderObject(frameTimeLeft)
            {
                frames = bulletFrames,
                rectangle = new Rectangle(spawnPosition.x, spawnPosition.y, gameContext.tileSize, gameContext.tileSize),
                objectBehaviour = new Bullet(_direction, _bulletSpeed, frameTimeLeft),
                animationTime = _bulletSpeed / bulletFrames.Length
            };

            gameContext.room.activeObjects.Add(newBullet);

            //Deal damage to player
            if (CollisionUtility.HasCollision(newBullet.rectangle, gameContext.player.rectangle)) gameContext.playerHealth.RemoveHealth(1);
        }

        public Rectangle[] GetFrames(SpriteMap spriteMap) => spriteMap.GetTurretFrames(_direction);

        public Vector2 GetSuitableLocation(Room room, int tileSize, Random rnd)
        {
            var newLocation = room.GetRandomBuildableTile(rnd);

            Vector2[] directions = new Vector2[]
            {
                new Vector2(-1, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(0, -1)
            };

            int[] freespace = new int[directions.Length];

            var l = directions.Length;
            for(int i = 0; i < l; i++)
            {
                freespace[i] = FreeSpaceCount(room, newLocation, directions[i]);
            }

            _direction = directions[Array.IndexOf(freespace, freespace.Max())];

            return newLocation;
        }

        private int FreeSpaceCount(Room room, Vector2 startPosition, Vector2 direction)
        {
            int c = 0;
            Vector2 newPosition = startPosition;
            while(true)
            {
                newPosition += direction;
                Tile tile = room.GetTile(newPosition.x, newPosition.y);
                if (tile == null) break;

                if (tile.tileBehaviour != null && tile.tileBehaviour.IsMoveBlocked) break;

                c++;
            }

            return c;
        }
    }
}
