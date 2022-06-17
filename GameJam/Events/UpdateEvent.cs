using GameJam.Game;

namespace GameJam.Events
{
    public class UpdateEvent
    {
        public float FrameTime { get; set; }
        public GameContext GameContext { get; set; }
        public RenderObject RenderObject { get; set; }
    }
}
