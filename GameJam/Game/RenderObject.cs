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

        private float timeElapsed;

        internal void MoveFrame(float frametime)
        {
            if (frames.Length <= 1) return;

            timeElapsed += frametime;

            if (timeElapsed >= animationTime)
            {
                frame += (int)Math.Floor(timeElapsed / animationTime);
                timeElapsed %= animationTime;
                if (frame >= frames.Length)
                {
                    frame %= frames.Length;
                }
            }
        }
    }
}



