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
    internal class CubeWorld
    {
        public WorldSettings Settings { get; set; }

        public List<Cube> LoadedCubes { get; }

        public float SceneCubeRatio { 
            get {
                return 1.0f / Settings.HeightOfCubes / 2.0f;
            }
        }

        public CubeWorld(WorldSettings settings)
        {
            Settings = settings; // Is not correct for the long term
            LoadedCubes = new List<Cube>();
            
            /*TEST*/ LayCube(new Cube(Color.DarkBlue, new CubeWorldCoordinates(0, 0, -21))); // TestCube
        }

        public void LayCube(Cube cube)
        {
            LoadedCubes.Add(cube);
        }

        public void LayPreviewedCube()
        {
            LayCube(previewableCube);
        }

        public void BreakCube(CubeWorldCoordinates position)
        {
            if (position == null)
                return;

            for (int i = 0; i < LoadedCubes.Count; i++)
                if (LoadedCubes[i].Position.Equals(position))
                    LoadedCubes.RemoveAt(i);
        }

        public void PreviewCube(Cube cube)
        {
            previewableCube = cube;
        }

        public bool IsFreeSpace(CubeWorldCoordinates position)
        {
            foreach (Cube cube in LoadedCubes)
                if (cube.Position.Equals(position))
                    return false;

            return true;
        }

        public Vector3 GetCubeScenePosition(CubeWorldCoordinates cubePosition) // DESIGN: To Move Place
        {
            return cubePosition.ToVector3() * 2.0f * Settings.HeightOfCubes;
        }

        
    }
}
