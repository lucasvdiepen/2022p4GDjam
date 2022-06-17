using System;
using System.Drawing;
using GameJam.TileEvents;

namespace GameJam.Game
{
    public class RenderObject
    {
        internal RectangleF rectangle;
        internal int frame;
        internal float animationTime = 1;
        internal ObjectBehaviour objectBehaviour;

        internal Rectangle[] frames;

        private float _timeElapsed;

        public RenderObject(float frameTimeLeft = 0)
        {
            _timeElapsed = frameTimeLeft;
        }

        internal void MoveFrame(float frametime)
        {
            if (frames.Length <= 1) return;

            _timeElapsed += frametime;

            if (_timeElapsed >= animationTime)
            {
                frame += (int)Math.Floor(_timeElapsed / animationTime);
                _timeElapsed %= animationTime;
                if (frame >= frames.Length)
                {
                    frame %= frames.Length;
                }
            }
        }
    }
}



