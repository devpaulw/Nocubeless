using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class DynamicEntityComponent : NocubelessComponent
	{
		private List<DynamicEntity> dynamicEntities;

		public DynamicEntityComponent(Nocubeless nocubeless) : base(nocubeless)
		{
			dynamicEntities = new List<DynamicEntity>();
			Add(Nocubeless.Player);
			Nocubeless.Player.ratio = Nocubeless.CubeWorld.GetGraphicsCubeRatio();
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
