using GameJam.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameJam.Game
{
    public class KeyPress
    {
        public Keys keyCode;
        private float startingDelay;
        public float delay;
        public Vector2 direction;

        public void SetKey(Keys keyCode)
        {
            this.keyCode = keyCode;
        }

        public void SetDelay(float delay)
        {
            startingDelay = delay;
            this.delay = 0;
        }

        public void ResetDelay()
        {
            delay += startingDelay;
        }

        public void SetDirection(int x, int y)
        {
            this.direction = new Vector2(x, y);
        }

        public void Reset()
        {
            keyCode = Keys.None;
        }
    }

    public class PlayerMovement
    {
        private Dictionary<Keys, Action<KeyPress>> _keyActions = new Dictionary<Keys, Action<KeyPress>>()
        {
            {Keys.W, (keyPress) => keyPress.SetDirection(0, -1) },
            {Keys.S, (keyPress) => keyPress.SetDirection(0, 1) },
            {Keys.A, (keyPress) => keyPress.SetDirection(-1, 0) },
            {Keys.D, (keyPress) => keyPress.SetDirection(1, 0) },
        };

        private KeyPress _currentMoveKey = new KeyPress();

        private static Tile _previousTile;

        public void OnKeyDown(Keys keyCode)
        {
            if (!_keyActions.ContainsKey(keyCode) || _currentMoveKey.keyCode != Keys.None ||  _currentMoveKey.keyCode == keyCode) return;

            _currentMoveKey.SetKey(keyCode);
            _currentMoveKey.SetDelay(0.5f);

            _keyActions[keyCode](_currentMoveKey);
        }

        public void OnKeyUp(Keys keyCode)
        {
            _currentMoveKey.Reset();
        }

        public void Update(UpdateEvent updateEvent)
        {
            if (_currentMoveKey.keyCode == Keys.None) return;

            _currentMoveKey.delay -= updateEvent.FrameTime;
            while(true)
            {
                if (_currentMoveKey.delay <= 0)
                {
                    _currentMoveKey.ResetDelay();

                    MovePlayer(_currentMoveKey.direction.x, _currentMoveKey.direction.y, updateEvent.GameContext, updateEvent.LevelLoader, updateEvent.GameRenderer);
                }
                else break;
            }
        }

        private static void MovePlayer(int x, int y, GameContext gameContext, LevelLoader levelLoader, GameRenderer gameRenderer)
        {
            RenderObject player = gameContext.player;
            float newx = player.rectangle.X + (x * gameContext.tileSize);
            float newy = player.rectangle.Y + (y * gameContext.tileSize);

            Tile next = gameContext.room.GetTile((int)newx, (int)newy);

            if (next != null)
            {
                MoveEvent newMoveEvent = new MoveEvent()
                {
                    GameContext = gameContext,
                    GameRenderer = gameRenderer,
                    LevelLoader = levelLoader,
                    PlayerRenderer = player,
                    Direction = new Vector2(x, y)
                };

                CanEnterEvent canEnterEvent = next.tileBehaviour?.CanEnter(newMoveEvent);

                //Move the player
                if ((canEnterEvent == null || !canEnterEvent.BlockMovement) && !gameContext.room.IsActiveRenderObjectBlocking((int)newx, (int)newy))
                {
                    player.rectangle.X = newx;
                    player.rectangle.Y = newy;

                    RenderObject[] activeRenderObjects = gameContext.room.GetActiveObjects((int)newx, (int)newy);

                    //Call render object events
                    foreach (RenderObject renderObject in activeRenderObjects)
                    {
                        renderObject.objectBehaviour?.OnEnter(newMoveEvent);
                    }
                }

                //Call tile object events
                if (canEnterEvent == null || !canEnterEvent.BlockEvents)
                {
                    _previousTile?.tileBehaviour?.OnExit(newMoveEvent);
                    next.tileBehaviour?.OnEnter(newMoveEvent);
                }

                if (canEnterEvent == null || !canEnterEvent.BlockMovement || !canEnterEvent.BlockEvents)
                {
                    _previousTile = next;
                }
            }
        }

        /*public int KeyPressedIndex(Keys keyCode)
        {
            var l = keyPressed.Count;
            for(int i = 0; i < l; i++)
            {
                if (keyPressed[i].keyCode == keyCode) return i;
            }

            return -1;
        }*/
    }
}
