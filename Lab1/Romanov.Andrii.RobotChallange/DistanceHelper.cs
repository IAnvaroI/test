using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romanov.Andrii.RobotChallenge
{
    public class DistanceHelper
    {
        public static int FindDistanceCost(Position a, Position b)
        {
            return (int)(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}
