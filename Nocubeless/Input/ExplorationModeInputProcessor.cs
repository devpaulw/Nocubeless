using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class ExplorationModeInputProcessor : GameInputProcessor
	{
		public ExplorationModeInputProcessor(Nocubeless nocubeless) : base(nocubeless)
		{
			inputProcessors.Add(new CameraInputProcessor(nocubeless));
			inputProcessors.Add(new PlayerEntityInputProcessor(nocubeless));
			inputProcessors.Add(new CubeInteractionsInputProcessor(nocubeless));
		}

		public override void Update(GameTime gameTime)
		{
			if (Nocubeless.CurrentState == NocubelessState.Playing)
			{
				base.Update(gameTime);
			}
		}
	}
}
