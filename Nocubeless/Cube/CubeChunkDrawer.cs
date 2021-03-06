﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class CubeChunkDrawer : GameComponent
    {
        private readonly ModelMeshPart cubeMeshPart; // store rendering attributes
        private readonly CubeEffect cubeEffect;
        private readonly Matrix cubeScale;

        public float CubeHeight { get; set; }

        public CubeChunkDrawer(Game game, float heightOfCubes) : base(game)
        {
            CubeHeight = heightOfCubes;

            cubeMeshPart = Cube.LoadModel(Game.GraphicsDevice);
            cubeEffect = new CubeEffect(Game);
            cubeScale = Matrix.CreateScale(CubeHeight);
        }

        public void Draw(ref CubeChunk chunk, Vector3 position, float gap, EffectMatrices effectMatrices) // TO-OPTIMIZE
        {
            cubeEffect.View = effectMatrices.View;
            cubeEffect.Projection = effectMatrices.Projection;

            Game.GraphicsDevice.SetVertexBuffer(cubeMeshPart.VertexBuffer);
            Game.GraphicsDevice.Indices = cubeMeshPart.IndexBuffer;

            for (int x = 0; x < CubeChunk.Size; x++)
            {
                for (int y = 0; y < CubeChunk.Size; y++)
                {
                    for (int z = 0; z < CubeChunk.Size; z++)
                    {
                        if (chunk[x + (y * CubeChunk.Size) + (z * CubeChunk.Size * CubeChunk.Size)] == null)
                            continue;

                        Vector3 cubePosition = new Vector3(position.X + (x * gap), position.Y + (y * gap), position.Z + (z * gap));

                        Matrix translation = Matrix.CreateTranslation(cubePosition);
                        Matrix world = cubeScale * translation * effectMatrices.World;

                        cubeEffect.World = world;
                        cubeEffect.Alpha = 1; // actually, it's always full alpha.

                        cubeEffect.Color = chunk[x + (y * CubeChunk.Size) + (z * CubeChunk.Size * CubeChunk.Size)].ToVector3();

                        foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            Game.GraphicsDevice.DrawIndexedPrimitives(
                            PrimitiveType.TriangleList,
                            0,
                            cubeMeshPart.StartIndex,
                            cubeMeshPart.PrimitiveCount);
                        }
                    }
                }
            }

        }
    }
}
