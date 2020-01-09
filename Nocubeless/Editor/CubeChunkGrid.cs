using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class CubeChunkGrid : NocubelessDrawableComponent
    {
        private readonly CubeEffect cubeEffect;

        public CubeChunkGrid(Nocubeless nocubeless) : base(nocubeless)
        {
            cubeEffect = new CubeEffect(Game);
        }

        public override void Draw(GameTime gameTime)
        {
            //cubeEffect.View = Nocubeless.Camera.ViewMatrix;
            //cubeEffect.Projection = Nocubeless.Camera.ProjectionMatrix;
            //cubeEffect.World = Nocubeless.Camera.WorldMatrix;

            //Game.GraphicsDevice.SetVertexBuffer(cubeMeshPart.VertexBuffer);
            //Game.GraphicsDevice.Indices = cubeMeshPart.IndexBuffer;

            //foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //    Game.GraphicsDevice.DrawIndexedPrimitives(
            //    PrimitiveType.LineList,
            //    0,
            //    cubeMeshPart.StartIndex,
            //    cubeMeshPart.PrimitiveCount);
            //}

            base.Draw(gameTime);
        }
    }
}
