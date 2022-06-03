using GameJam.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Events
{
    public class UpdateEvent
    {
        public float FrameTime { get; set; }
        public GameContext GameContext { get; set; }
        public RenderObject RenderObject { get; set; }
    }
}
