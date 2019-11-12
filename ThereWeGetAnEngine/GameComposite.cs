using OpenTK;
using System;
using System.Collections.Generic;

namespace ThereWeGetAnEngine
{
    public class GameComposite : IComparable<GameComposite>
    {
        private List<GameComponent> gameComponents = new List<GameComponent>();

        //public GameComposite(Vector3 position) : base(position) { }

        public override void Add(GameComponent gameComponent)
        {
            gameComponents.Add(gameComponent);
        }

        public override void Remove(GameComponent gameComponent)
        {
            gameComponents.Remove(gameComponent);
        }

        public override void Draw()
        {
            foreach (GameComponent gameComponent in gameComponents)
            {
                gameComponent.Draw();
            }
        }

        public int CompareTo(GameComposite other)
        {
            throw new NotImplementedException();
        }
    }
}
