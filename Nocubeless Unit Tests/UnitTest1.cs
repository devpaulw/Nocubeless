using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Nocubeless;

namespace Nocubeless_Unit_Tests
{
	[TestClass]
	public class WorldCoordinatesUnit
	{
		[TestMethod]
		public void TestAdd()
		{
			var coords = new WorldCoordinates(new CubeCoordinates(0, 5, 3), new Vector3(0, 0, 0.5f));
			var res = new WorldCoordinates(new CubeCoordinates(0, 4, 4), new Vector3(0.3f, 0.8f, 0.6f));

			Assert.AreEqual(res, coords + new Vector3(0.3f, -0.2f, 1.1f));
		}
	}
}
