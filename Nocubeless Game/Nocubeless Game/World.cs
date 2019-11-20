using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class World : DrawableGameComponent
    {
        private readonly DrawableCubes cubes;

        public WorldSettings Settings { get; }

        public World(GameApp game, WorldSettings settings) : base(game)
        {
            Settings = settings;

            cubes = new DrawableCubes(game, settings.CubesHeight);
        }

        public override void Draw(GameTime gameTime)
        {
            cubes.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void LayCube(Cube cube)
        {
            cubes.Add(cube);
        }

        public static World LoadFromTest(GameApp game)
        {
            WorldSettings worldSettings = new WorldSettings { CubesHeight = 0.1f };

            World world = new World(game, worldSettings); // TO-DISPOSE

            world.LayCube(new Cube(
                    new Color(0f, 1f, 0f, 1f),
                    new CubeCoordinate(-1, -1, -1)));
            world.LayCube(new Cube(
                new Color(1f, 0.2f, 0f, 1f),
                new CubeCoordinate(0, 0, 0)));
            world.LayCube(new Cube(
                new Color(0f, 0.2f, 1f, 1f),
                new CubeCoordinate(2, 0, 0)));
            world.LayCube(new Cube(
                new Color(0f, 1f, 1f, 1f),
                new CubeCoordinate(-3, 2, 1)));
            world.LayCube(new Cube(
                new Color(1f, 1f, 0f, 1f),
                new CubeCoordinate(2, 3, 4)));

            return world;
        }
    }
}
