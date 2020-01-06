using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	abstract class GameInputProcessor : NocubelessComponent
	{
		protected List<NocubelessComponent> inputProcessors;

		public GameInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
			inputProcessors = new List<NocubelessComponent>();
			Nocubeless.Player.NextColorToLay = new CubeColor(7, 7, 7); // TODO: Manage
		}

		public void Add(NocubelessComponent inputProcessor)
		{
			inputProcessors.Add(inputProcessor);
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var inputProcessor in inputProcessors)
			{
				inputProcessor.Update(gameTime);
			}
		}
	}
}
