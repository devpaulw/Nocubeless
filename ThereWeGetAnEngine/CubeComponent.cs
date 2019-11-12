using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThereWeGetAnEngine
{
    public class ModelComponent : GameComponent
    {
        public ModelComponent()
        {

        }

        public override void Add(GameComponent c)
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Remove(GameComponent c)
        {
            throw new NotImplementedException();
        }
    }

    public class CubeComponent : GameComponent
    {
        public override void Add(GameComponent c)
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Remove(GameComponent c)
        {
            throw new NotImplementedException();
        }
    }
}
