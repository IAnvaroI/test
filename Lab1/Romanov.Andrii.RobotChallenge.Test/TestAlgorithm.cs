using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;
using System.Collections.Generic;

namespace Romanov.Andrii.RobotChallenge.Test
{
    [TestClass]
    public class TestAlgorithm
    {
        [TestMethod]
        public void TestMoveCommandToStationArea()
        {
            var algorithm = new RomanovAndriiAlgorithm();
            Map map = new Map();
            Position position = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Position = position, Energy = 1000, RecoveryRate = 2 });
            List<Robot.Common.Robot> robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() { Position = new Position(2, 4), Energy = 200 }
            };
            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is MoveCommand);
            Assert.AreEqual(new Position(1, 3), ((MoveCommand)command).NewPosition);
        }

        [TestMethod]
        public void TestMoveCommandToStationCell()
        {
            var algorithm = new RomanovAndriiAlgorithm();
            Map map = new Map();
            Position position = new Position(4, 4);
            map.Stations.Add(new EnergyStation() { Position = position, Energy = 1000, RecoveryRate = 2 });
            List<Robot.Common.Robot> robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() { Position = new Position(7, 8), Energy = 200 }
            };
            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is MoveCommand);
            Assert.AreEqual(position, ((MoveCommand)command).NewPosition);
        }

        [TestMethod]
        public void TestCollectCommand()
        {
            var algorithm = new RomanovAndriiAlgorithm();
            Map map = new Map();
            Position position = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Position = position, Energy = 1000, RecoveryRate = 2 });
            List<Robot.Common.Robot> robots = new List<Robot.Common.Robot>() { 
                new Robot.Common.Robot() { Position = new Position(1, 1), Energy = 200 }
            };
            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is CollectEnergyCommand);
        }

        [TestMethod]
        public void TestCreateNewRobotCommand()
        {
            var algorithm = new RomanovAndriiAlgorithm();
            Map map = new Map();
            Position position = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Position = position, Energy = 1000, RecoveryRate = 2 });
            List<Robot.Common.Robot> robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() { Position = new Position(1, 1), Energy = 600 }
            };
            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is CreateNewRobotCommand);
        }

        [TestMethod]
        public void TestIsStationFree()
        {
            var algorithm = new RomanovAndriiAlgorithm();
            Position position = new Position(1, 1);
            var station = new EnergyStation() { Position = position, Energy = 1000, RecoveryRate = 2 };
            List<Robot.Common.Robot> robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() { Position = new Position(3, 3), Energy = 600 },
                new Robot.Common.Robot() { Position = new Position(4, 4), Energy = 600 }
            };

            bool isFree = algorithm.isStationFree(station, robots[1], robots);

            Assert.IsFalse(isFree);
        }

        [TestMethod]
        public void TestMyRobotsList()
        {
            var algorithm = new RomanovAndriiAlgorithm();
            List<Robot.Common.Robot> robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() { Position = new Position(1, 1), OwnerName = algorithm.Author, Energy = 600 },
                new Robot.Common.Robot() { Position = new Position(3, 3), OwnerName = "Harry", Energy = 600 }
            };

            var myRobots = algorithm.myRobotsList(robots);

            Assert.AreEqual(1, myRobots.Count);
            Assert.AreEqual(robots[0], myRobots[0]);
        }

        [TestMethod]
        public void TestStopCondition()
        {
            var algorithm = new RomanovAndriiAlgorithm();
            Position stationPosition = new Position(1, 1);
            Position robotPosition = new Position(3, 3);
            
            var stopConditionResult = algorithm.stopCondition(robotPosition, stationPosition);

            Assert.IsTrue(stopConditionResult);
        }

        [TestMethod]
        public void TestSetOptimalCellsCountToMove()
        {
            var algorithm = new RomanovAndriiAlgorithm();
            int energy = 150;
            
            var cellsCountToMove = algorithm.SetOptimalCellsCountToMove(energy);

            Assert.AreEqual(5, cellsCountToMove);
        }

        [TestMethod]
        public void TestOptimalPositionToMove()
        {
            var algorithm = new RomanovAndriiAlgorithm();
            Position stationPosition = new Position(1, 1);
            Position robotPosition = new Position(3, 3);

            var actualPosition = algorithm.OptimalPositionToMove(robotPosition, stationPosition);
            var expectedPosition = new Position() { X = 2, Y = 2 };

            Assert.AreEqual(expectedPosition, actualPosition);
        }
    }
}
