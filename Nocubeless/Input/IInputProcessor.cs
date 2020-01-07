using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	interface IInputProcessor : IDisposable // SDNMSG: Why?
	{
		void Update(GameTime gameTime); // SDNMSG: It's conflict with NocubelessComponent <- GameComponent <- Update(GameTime)
	}
}
