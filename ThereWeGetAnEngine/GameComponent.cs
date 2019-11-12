using OpenTK;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThereWeGetAnEngine
{
    public abstract class GameComponent
    {
        public Vector3 Position { get; set; }

        // Constructor
        public GameComponent(Vector3 position)
        {
            Position = position;
        }

        public abstract void Add(GameComponent c);
        public abstract void Remove(GameComponent c);
        public abstract void Draw();
    }
}
