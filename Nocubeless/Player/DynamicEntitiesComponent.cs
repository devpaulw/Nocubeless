using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class DynamicEntitiesComponent : NocubelessComponent
	{
		private List<DynamicEntity> dynamicEntities;

		public DynamicEntitiesComponent(Nocubeless nocubeless) : base(nocubeless)
		{
			dynamicEntities = new List<DynamicEntity>();
			Add(Nocubeless.Player);

			// convert the settings speeds into worldSpeeds
			Nocubeless.Player.Settings.FlyingSpeed *= Nocubeless.CubeWorld.GetGraphicsCubeRatio();
			Nocubeless.Player.Settings.RunningSpeed *= Nocubeless.CubeWorld.GetGraphicsCubeRatio();
			Nocubeless.Player.Settings.WalkingSpeed *= Nocubeless.CubeWorld.GetGraphicsCubeRatio();
			Nocubeless.Player.Speed = Nocubeless.Player.Settings.WalkingSpeed;
		}

		public void Add(DynamicEntity entity)
		{
			dynamicEntities.Add(entity);
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var dynamicEntity in dynamicEntities)
			{
				dynamicEntity.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
			}
		}
	}
}
