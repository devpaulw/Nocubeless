using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class CubeWorld
    {
        public List<Cube> XCHEATGETCUBESDIRECTLY { get { return cubes; } } // not allowed, but before chunk

        private List<Cube> cubes;

        public CubeWorldSettings Settings { get; set; }

        public CubeWorld(CubeWorldSettings settings)
        {
            Settings = settings; // Is not correct for the long term

            cubes = new List<Cube>();

            /*TEST*/ LayCube(new Cube(Color.DarkBlue.ToVector3(), new CubeWorldCoordinates(0, 0, -21))); // TestCube
        }

        public void LayCube(Cube cube)
        {
            cubes.Add(cube);
        }

        public void BreakCube(CubeWorldCoordinates position)
        {
            if (position == null)
                return;

            for (int i = 0; i < cubes.Count; i++)
                if (cubes[i].Position.Equals(position))
                    cubes.RemoveAt(i);
        }

        public Cube GetCube(CubeWorldCoordinates position)
        {
            var requestedCube = (from cube in cubes
                                 where cube.Position.Equals(position)
                                 select cube)
                                 .FirstOrDefault();

            return requestedCube;
        }

        #region Static
        public static Vector3 GetGraphicsCubePosition(CubeWorldCoordinates cubePosition, float heightOfCubes) // cube position in graphics representation.
        {
            return cubePosition.ToVector3() / GetGraphicsCubeRatio(heightOfCubes);
        }

        public static float GetGraphicsCubeRatio(float heightOfCubes) // how much is a cube smaller/bigger in the graphics representation?
        {
            return 1.0f / heightOfCubes / 2.0f;
        }
        #endregion
    }
}
