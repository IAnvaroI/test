using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;

namespace Romanov.Andrii.RobotChallenge.Test
{
    [TestClass]
    public class TestDistanceHelper
    {
        [TestMethod]
        public void TestDistance()
        {
            var position = new Position(1, 1);
            var position2 = new Position(6, 6);
            Assert.AreEqual(50, DistanceHelper.FindDistanceCost(position, position2));
        }
    }
}
