using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	// DESIGN PATTERN: composite
	// update the speed of all dynamic elements to be synchronised with the framerate
	class DynamicEntitiesComponent : NocubelessComponent
	{
		private List<DynamicEntity> dynamicEntities;

		public DynamicEntitiesComponent(Nocubeless nocubeless) : base(nocubeless)
		{
			dynamicEntities = new List<DynamicEntity>();
			Add(Nocubeless.Player);
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
