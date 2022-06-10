using GameJam.Events;
using GameJam.Game;
using GameJam.TileEvents;
using GameJam.Tools;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GameJam
{
    public partial class RenderForm : Form
    {
        private LevelLoader levelLoader;
        private float frametime;
        private GameRenderer renderer;
        private Audio audio;
        private readonly GameContext gc = new GameContext();
        private Tile _previousTile;

        public RenderForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            ResizeRedraw = true;

            //audio = new Audio();
            KeyDown += RenderForm_KeyDown;
            FormClosing += Form1_FormClosing;
            Load += RenderForm_Load;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            renderer.Dispose();
            audio.Dispose();
        }
        private void RenderForm_Load(object sender, EventArgs e)
        {
            levelLoader = new LevelLoader(gc.tileSize, new FileLevelDataSource());
            levelLoader.LoadRooms(gc.spriteMap);

            renderer = new GameRenderer(gc);

            gc.room = levelLoader.GetRoom(0, 0, 0);

            gc.player = new RenderObject()
            {
                frames = gc.spriteMap.GetPlayerFrames(),
                rectangle = new Rectangle(2 * gc.tileSize, 2 * gc.tileSize, gc.tileSize, gc.tileSize)
            };

            RenderObject testTrap = new RenderObject()
            {
                frames = gc.spriteMap.GetPlayerFrames(),
                rectangle = new Rectangle(7 * gc.tileSize, 2 * gc.tileSize, gc.tileSize, gc.tileSize),
                objectBehaviour = new Trap(2)
            };

            gc.room.activeObjects.Add(testTrap);

            ClientSize =
             new Size(

                (gc.tileSize * gc.room.tiles[0].Length) * gc.scaleunit,
                (gc.tileSize * gc.room.tiles.Length) * gc.scaleunit
                );
        }

        private void RenderForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                MovePlayer(0, -1);
            }
            else if (e.KeyCode == Keys.S)
            {
                MovePlayer(0, 1);
            }
            else if (e.KeyCode == Keys.A)
            {
                MovePlayer(-1, 0);
            }
            else if (e.KeyCode == Keys.D)
            {
                MovePlayer(1, 0);
            }
        }

        private void MovePlayer(int x, int y)
        {
            RenderObject player = gc.player;
            float newx = player.rectangle.X + (x * gc.tileSize);
            float newy = player.rectangle.Y + (y * gc.tileSize);

            Tile next = gc.room.GetTile((int)newx, (int)newy);

            if (next != null)
            {
                MoveEvent newMoveEvent = new MoveEvent()
                {
                    GameContext = gc,
                    GameRenderer = renderer,
                    LevelLoader = levelLoader,
                    PlayerRenderer = player,
                    Direction = new Vector2(x, y)
                };

                CanEnterEvent canEnterEvent = next.tileBehaviour?.CanEnter(newMoveEvent);

                if (canEnterEvent == null || !canEnterEvent.BlockMovement)
                {
                    player.rectangle.X = newx;
                    player.rectangle.Y = newy;

                    List<RenderObject> activeObjects = gc.room.activeObjects;

                    int c = activeObjects.Count;
                    for (int i = 0; i < c; i++)
                    {
                        RenderObject currentObject = activeObjects[i];

                        if ((int)currentObject.rectangle.X == newx && (int)currentObject.rectangle.Y == newy)
                        {
                            currentObject.objectBehaviour?.OnEnter(newMoveEvent);
                        }
                    }
                }

                if(canEnterEvent == null || !canEnterEvent.BlockEvents)
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

        public void Logic(float frametime)
        {
            this.frametime = frametime;

            var newUpdateEvent = new UpdateEvent()
            {
                FrameTime = frametime,
                GameContext = gc
            };

            //Update all tiles
            Tile[] allTiles = gc.room.GetAllTiles();

            int c = allTiles.Length;
            for (int i = 0; i < c; i++)
            {
                allTiles[i].tileBehaviour?.Update(newUpdateEvent);
            }

            //Update active objects
            List<RenderObject> activeObjects = gc.room.activeObjects;

            int l = activeObjects.Count;
            for(int i = 0; i < l; i++)
            {
                newUpdateEvent.RenderObject = activeObjects[i];
                activeObjects[i].objectBehaviour?.Update(newUpdateEvent);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            renderer.Render(e, frametime);
        }
    }

}


