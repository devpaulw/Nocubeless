using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class CubeEffect : Effect
    {
        #region Effect Parameters
        EffectParameter worldParam;
        EffectParameter viewParam;
        EffectParameter projectionParam;
        EffectParameter colorParam;
        EffectParameter alphaParam;
        #endregion

        #region Fields
        private Matrix world;
        private Matrix view;
        private Matrix projection;
        private Vector3 color;
        private float alpha;
        #endregion

        #region Public Properties
        public Matrix World {
            get => world;
            set {
                worldParam.SetValue(value);
                world = value;
            }
        }
        public Matrix View {
            get => view;
            set {
                viewParam.SetValue(value);
                view = value;
            }
        }
        public Matrix Projection {
            get => projection;
            set {
                projectionParam.SetValue(value);
                projection = value;
            }
        }
        public Vector3 Color {
            get => color;
            set {
                colorParam.SetValue(value); // Is converted to a Vector3, because the effect don't know Color struct
                color = value;
            }
        }
        public float Alpha {
            get => alpha;
            set {
                alphaParam.SetValue(value);
                alpha = value;
            }
        }
        #endregion

        #region Methods
        public CubeEffect(GraphicsDevice graphicsDevice) 
            : base(graphicsDevice, 
            File.ReadAllBytes(@"MGContent/CubeEffect.mgfx") /*URGENT is not correct*/ /*Is not design correct x)*/)
        {
            CacheEffectParameters();
        }

        private void CacheEffectParameters()
        {
            const string worldParamName = "World";
            const string viewParamName = "View";
            const string projectionParamName = "Projection";
            const string colorParamName = "CubeColor";
            const string alphaParamName = "CubeAlpha";

            worldParam = Parameters[worldParamName];
            viewParam = Parameters[viewParamName];
            projectionParam = Parameters[projectionParamName];
            colorParam = Parameters[colorParamName];
            alphaParam = Parameters[alphaParamName];
        }
        #endregion
    }
}
