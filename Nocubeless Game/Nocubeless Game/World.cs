using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public class World : DrawableGameComponent
    {
        private List<Cube> cubes;
        private CubeRenderer cubeRenderer;

        public Camera Camera { get; set; }
        private CubeEffect CubeEffect { get; } // Idem than behind
        public float CubesHeight { get; set; } // Store later in the collection


        public World(GameApp game, Camera camera, CubeEffect cubeEffect, float cubesHeight) : base(game)
        {
            cubes = new List<Cube>();
            Camera = camera;
            CubeEffect = cubeEffect;
            CubesHeight = cubesHeight;

            cubeRenderer = new CubeRenderer(Game.GraphicsDevice, CubeEffect, CubesHeight);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Cube cube in cubes)
            {
                cubeRenderer.Draw(Camera, cube);
            }

            base.Draw(gameTime);
        }

        public void LayCube(Cube cube)
        {
            cubes.Add(cube);
        }
    }
}
