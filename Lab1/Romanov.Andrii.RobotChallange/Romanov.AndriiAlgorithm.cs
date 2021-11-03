using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romanov.Andrii.RobotChallenge
{
    public class RomanovAndriiAlgorithm : IRobotAlgorithm
    {
        public string Author => "Andrii Romanov";
        private int OptimalCellsCountToMove = 1;
        private const int myRobotsStartCount = 10;
        private const int EnergyFarmDistance = 2;

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            Robot.Common.Robot movingRobot = robots[robotToMoveIndex];
            IList<Robot.Common.Robot> myRobots = myRobotsList(robots);
            if ((movingRobot.Energy > 500) && (myRobots.Count < myRobotsStartCount * 4))
            {
                return new CreateNewRobotCommand();
            }

            SetOptimalCellsCountToMove(movingRobot.Energy);

            if(movingRobot.Position.X > 96 || movingRobot.Position.Y > 96 || movingRobot.Position.X < 4 || movingRobot.Position.Y < 4)
            {
                OptimalCellsCountToMove = 1;
            }

            Position stationPosition = FindNearestFreeStation(robots[robotToMoveIndex], map, robots);

            if (stationPosition == null)
                return null;

            if (stopCondition(movingRobot.Position, stationPosition))
                return new CollectEnergyCommand();
            else
            {
                int distanceCost = DistanceHelper.FindDistanceCost(movingRobot.Position, stationPosition);

                if (distanceCost < OptimalCellsCountToMove * OptimalCellsCountToMove * 2)
                {
                    return new MoveCommand() { NewPosition = stationPosition };
                }
                else
                {
                    return new MoveCommand() { NewPosition = OptimalPositionToMove(movingRobot.Position, stationPosition) };
                }
                
            }
        }
        public bool isStationFree(EnergyStation station, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            if (!IsCellFree(station.Position, movingRobot, robots))
            {
                return false;
            };

            for (int i = 1; i <= EnergyFarmDistance; i++)
            {
                for (int j = 0; j < 1 + i * 2; j++)
                {
                    Position testPosition = new Position() { X = station.Position.X + i, Y = station.Position.Y + i - j};
                    if (!IsCellFree(testPosition, movingRobot, robots))
                    {
                        return false;
                    };
                }
                for (int j = 0; j < 1 + i * 2; j++)
                {
                    Position testPosition = new Position() { X = station.Position.X + i - j, Y = station.Position.Y - i };
                    if (!IsCellFree(testPosition, movingRobot, robots))
                    {
                        return false;
                    };
                }
                for (int j = 0; j < 1 + i * 2; j++)
                {
                    Position testPosition = new Position() { X = station.Position.X - i, Y = station.Position.Y - i + j };
                    if (!IsCellFree(testPosition, movingRobot, robots))
                    {
                        return false;
                    };
                }
                for (int j = 0; j < 1 + i * 2; j++)
                {
                    Position testPosition = new Position() { X = station.Position.X - i + j, Y = station.Position.Y + i};
                    if (!IsCellFree(testPosition, movingRobot, robots))
                    {
                        return false;
                    };
                }
            }

            return true;
        }
        public IList<Robot.Common.Robot> myRobotsList(IList<Robot.Common.Robot> allRobots)
        {
            IList<Robot.Common.Robot> MyRobots = new List<Robot.Common.Robot>();
            foreach (var robot in allRobots)
            {
                if (robot.OwnerName == Author)
                {
                    MyRobots.Add(robot);
                }
            }
            return MyRobots;
        }
        public bool stopCondition(Position robotPosition, Position stationPosition)
        {
            int XMin = stationPosition.X - EnergyFarmDistance;
            int XMax = stationPosition.X + EnergyFarmDistance;
            int YMin = stationPosition.Y - EnergyFarmDistance;
            int YMax = stationPosition.Y + EnergyFarmDistance;

            if (robotPosition.X >= XMin && robotPosition.X <= XMax && robotPosition.Y >= YMin && robotPosition.Y <= YMax){
                return true;
            }
            return false;
        }
        public int SetOptimalCellsCountToMove(int energy)
        {
            if (energy > 100)
            {
                OptimalCellsCountToMove = 5;
            }
            else if (energy <= 100 && energy > 67)
            {
                OptimalCellsCountToMove = 4;
            }
            else if (energy <= 67 && energy > 48)
            {
                OptimalCellsCountToMove = 3;
            }
            else if (energy <= 48 && energy > 10)
            {
                OptimalCellsCountToMove = 2;
            }
            else
            {
                OptimalCellsCountToMove = 1;
            }
            return OptimalCellsCountToMove;
        }
        public Position OptimalPositionToMove(Position robotPosition, Position stationPosition)
        {
            if(robotPosition.X == stationPosition.X)
            {
                if (robotPosition.Y < stationPosition.Y)
                {
                    return new Position() { X = robotPosition.X, Y = robotPosition.Y + OptimalCellsCountToMove };
                }
                else
                {
                    return new Position() { X = robotPosition.X, Y = robotPosition.Y - OptimalCellsCountToMove };
                }

            }
            else if(robotPosition.Y == stationPosition.Y)
            {
                if (robotPosition.X < stationPosition.X)
                {
                    return new Position() { Y = robotPosition.Y, X = robotPosition.X + OptimalCellsCountToMove };
                }
                else
                {
                    return new Position() { Y = robotPosition.Y, X = robotPosition.X - OptimalCellsCountToMove };
                }
            }
            else if(robotPosition.Y < stationPosition.Y)
            {
                if(robotPosition.X < stationPosition.X) // robot in quarter 3, station in quarter 1
                {
                    return new Position() { 
                        Y = robotPosition.Y + OptimalCellsCountToMove,
                        X = robotPosition.X + OptimalCellsCountToMove 
                    };
                }
                else// robot in quarter 4, station in quarter 2
                {
                    return new Position() { 
                        Y = robotPosition.Y + OptimalCellsCountToMove,
                        X = robotPosition.X - OptimalCellsCountToMove 
                    };
                }
            }
            else if (robotPosition.Y > stationPosition.Y)
            {
                if (robotPosition.X < stationPosition.X)// robot in quarter 2, station in quarter 4
                {
                    return new Position() { 
                        Y = robotPosition.Y - OptimalCellsCountToMove,
                        X = robotPosition.X + OptimalCellsCountToMove 
                    };
                }
                else// robot in quarter 1, station in quarter 3
                {
                    return new Position() { 
                        Y = robotPosition.Y - OptimalCellsCountToMove, 
                        X = robotPosition.X - OptimalCellsCountToMove
                    };
                }
            }
            return null;
        }
        // copypaste
        public Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots)
        {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            foreach (var station in map.Stations)
            {
                if (isStationFree(station, movingRobot, robots))
                {
                    int d = DistanceHelper.FindDistanceCost(station.Position, movingRobot.Position);

                    if (d < minDistance)
                    {
                        minDistance = d;
                        nearest = station;
                    }
                }
            }
            return nearest == null ? null : nearest.Position;
        }

        public bool IsCellFree(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            foreach (var robot in robots)
            {
                if (robot != movingRobot)
                {
                    if (robot.Position == cell)
                        return false;
                }
            }
            return true;
        }

    }
}

