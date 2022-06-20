using GameJam.Enums;
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
    public class Saw : TimerBehaviour, ITrap
    {
        // todo: moet bepaald worden door trap generator
        private Direction2D _direction2D;
        private int _direction;

        private int _invertCount;

        public Saw(float moveTime) : base(false, false, moveTime)
        {   

        }

        public override void OnEnter(MoveEvent moveEvent)
        {
            //Deal damage to player
            moveEvent.GameContext.playerHealth.RemoveHealth(1);
        }

        public override void TimerTick(UpdateEvent updateEvent, float frameTimeLeft)
        {
            Move(updateEvent.GameContext, updateEvent.RenderObject);
        }

        private void Move(GameContext gameContext, RenderObject renderObject)
        {
            if (_invertCount >= 2) return;

            int newX = (int)renderObject.rectangle.X;
            int newY = (int)renderObject.rectangle.Y;

            switch(_direction2D)
            {
                case Direction2D.Horizontal:
                    newX += _direction * gameContext.tileSize;
                    break;
                case Direction2D.Vertical:
                    newY += _direction * gameContext.tileSize;
                    break;
            }

            if (gameContext.room.IsMoveBlocked(newX, newY))
            {
                InvertMove(gameContext, renderObject);
                return;
            }
            else _invertCount = 0;

            renderObject.rectangle.X = newX;
            renderObject.rectangle.Y = newY;

            //Deal damage to player
            if(CollisionUtility.HasCollision(renderObject.rectangle, gameContext.player.rectangle)) gameContext.playerHealth.RemoveHealth(1);
        }

        private void InvertMove(GameContext gameContext, RenderObject renderObject)
        {
            _invertCount++;
            _direction *= -1;
            Move(gameContext, renderObject);
        }

        public Vector2 GetSuitableLocation(Room room, int tileSize, Random rnd)
        {
            var newLocation = room.GetRandomBuildableTile(rnd);

            _direction = rnd.Next(0, 2) == 1 ? 1 : -1;
            _direction2D = (Direction2D)rnd.Next(0, Enum.GetValues(typeof(Direction2D)).Length);

            return newLocation;
        }

        public Rectangle[] GetFrames(SpriteMap spriteMap) => spriteMap.GetSawFrames();
    }
}
