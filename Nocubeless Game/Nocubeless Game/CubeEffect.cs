using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        #endregion

        #region Fields
        private Matrix world;
        private Matrix view;
        private Matrix projection;
        private Color color;
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
        public Color Color {
            get => color;
            set {
                colorParam.SetValue(value.ToVector3()); // Is converted to a Vector3, because the effect don't know Color struct
                color = value;
            }
        }
        #endregion

        #region Methods
        public CubeEffect(Effect source) : base(source)
        {
            CacheEffectParameters();
        }

        private void CacheEffectParameters()
        {
            const string worldParamName = "World";
            const string viewParamName = "View";
            const string projectionParamName = "Projection";
            const string colorParamName = "Color";

            worldParam = Parameters[worldParamName];
            viewParam = Parameters[viewParamName];
            projectionParam = Parameters[projectionParamName];
            colorParam = Parameters[colorParamName];
        }
        #endregion
    }
}
