using GameJam.Game;

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
