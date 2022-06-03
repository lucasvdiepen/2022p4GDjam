using GameJam.Enums;
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
        private Direction2D _direction2D;
        private int _direction = 1;

        public Trap(float moveTime)
        {
            _moveTime = moveTime;
            _direction2D = Direction2D.Horizontal;
        }

        public override void OnEnter(MoveEvent moveEvent)
        {
            //Deal damage to player
            Debug.WriteLine("Deal damage to player");
        }

        public override void Update(UpdateEvent updateEvent)
        {
            _timeElapsed += updateEvent.FrameTime;

            if(_timeElapsed >= _moveTime)
            {
                _timeElapsed = 0;
                Move(updateEvent.GameContext, updateEvent.RenderObject);
            }
        }

        private void Move(GameContext gameContext, RenderObject renderObject)
        {
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

            Tile nextTile = gameContext.room.GetTile(newX, newY);

            if(nextTile != null)
            {
                if(nextTile.tileBehaviour != null && nextTile.tileBehaviour.IsMoveBlocked())
                {
                    _direction *= -1;
                    Move(gameContext, renderObject);
                    return;
                }

                renderObject.rectangle.X = newX;
                renderObject.rectangle.Y = newY;
            }
            else
            {
                _direction *= -1;
                Move(gameContext, renderObject);
                return;
            }
        }
    }
}
