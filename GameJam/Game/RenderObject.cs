using System;
using System.Drawing;
using GameJam.TileEvents;

namespace GameJam.Game
{
    public class RenderObject
    {
        internal RectangleF rectangle;
        internal float frame;
        internal float animationSpeed = 10;
        internal TileBehaviour objectBehaviour;

        internal Rectangle[] frames;

        internal void MoveFrame(float frametime)
        {
            frame += frametime * animationSpeed;
            if (frame >= frames.Length)
            {
                frame = 0;
            }
        }
    }
}



