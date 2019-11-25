using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class WorldInputComponent : GameComponent
    {
        public InputKeySettings InputKeys { get; set; }
        public World World { get; set; }
        public Camera Camera { get; set; }

        public WorldInputComponent(IGameApp game, World world) : base(game.Instance)
        {
            InputKeys = game.Settings.InputKeys;
            Camera = ;
            World = game.World;

            Camera = new Camera(game);
        }

        public override void Initialize()
        {
            // Settings
            // Max laying distance: where?
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }

     
}
