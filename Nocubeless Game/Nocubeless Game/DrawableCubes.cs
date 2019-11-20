using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class DrawableCubes : DrawableGameComponent
    {
        private readonly List<Cube> toDraw;
        private readonly ModelMeshPart cubeMeshPart; // Store rendering attributes
        private readonly CubeEffect cubeEffect;
        private Matrix scale;

        public Camera Camera { get; set; }
        public float Height { get; }

        public DrawableCubes(GameApp game, float cubesHeight) : base(game)
        {
            Height = cubesHeight;
            Camera = game.Camera;

            toDraw = new List<Cube>();
            cubeMeshPart = Cube.LoadModel(GraphicsDevice);
            cubeEffect = new CubeEffect(Game.Content.Load<Effect>("CubeEffect")); // DOLATER: deprecate and use bytecode instead.
            scale = Matrix.CreateScale(Height);
        }

        public void Add(Cube cube)
        {
            toDraw.Add(cube);
        }

        public override void Draw(GameTime gameTime)
        {
            // there, do later the light!
            //BasicEffect eff = new BasicEffect(Game.GraphicsDevice);
            //eff.LightingEnabled = true;
            //eff.EnableDefaultLighting();

            foreach (Cube cube in toDraw)
            {
                Matrix translation = cube.Position.CreateWorldTranslation(Height);

                //eff.World = scale * translation;
                //eff.View = Camera.ViewMatrix;
                //eff.Projection = Camera.ProjectionMatrix;
                //eff.AmbientLightColor = cube.Color.ToVector3();
                //eff.Alpha = 1.0f;

                cubeEffect.World = scale * translation;
                cubeEffect.View = Camera.ViewMatrix;
                cubeEffect.Projection = Camera.ProjectionMatrix;
                cubeEffect.Color = cube.Color;

                GraphicsDevice.SetVertexBuffer(cubeMeshPart.VertexBuffer);
                GraphicsDevice.Indices = cubeMeshPart.IndexBuffer;

                foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    GraphicsDevice.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    cubeMeshPart.StartIndex,
                    cubeMeshPart.PrimitiveCount);
                }

                //foreach (EffectPass pass in eff.CurrentTechnique.Passes)
                //{
                //    pass.Apply();
                //    GraphicsDevice.DrawIndexedPrimitives(
                //    PrimitiveType.TriangleList,
                //    0,
                //    cubeMeshPart.StartIndex,
                //    cubeMeshPart.PrimitiveCount);
                //}
            }

            base.Draw(gameTime);
        }
    }
}
