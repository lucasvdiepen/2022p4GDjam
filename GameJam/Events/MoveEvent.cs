using GameJam.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Events
{
    public class MoveEvent
    {
        public GameContext GameContext { get; set; }
        public GameRenderer GameRenderer { get; set; }
        public LevelLoader LevelLoader { get; set; }
        public RenderObject PlayerRenderer { get; set; }
        public Vector2 Direction { get; set; }
    }
}
