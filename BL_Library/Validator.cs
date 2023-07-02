using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class Validator
    {
        public static bool IsInsideOcean(IField source, Coordinate coord)
        {
            return (coord.X >= 0 && coord.X < source.NumberOfColumns
                && coord.Y >= 0 && coord.Y < source.NumberOfRows);
        }

        public static bool IsValidTimeToReproducePrey(int timeToReproduce)
        {
            return (timeToReproduce > 0 && timeToReproduce <= Prey.MAX_TIME_TO_REPRODUCE_PREY);
        }

        public static bool IsValidTimeToReproducePredator(int timeToReproduce)
        {
            return (timeToReproduce > 0 && timeToReproduce <= Predator.MAX_TIME_TO_REPRODUCE_PREDATOR);
        }

        public static bool IsValidTimeToFeed(int timeToFeed)
        {
            return (timeToFeed > 0 && timeToFeed <= Predator.MAX_TIME_TO_FEED);
        }

        public static bool IsValidCell(IField source, Cell item)
        {
            bool result = false;

            if (item != null)
            {
                if (item is Obstacle)
                {
                    result = IsInsideOcean(source, item.Coord);
                }
                else if (item is Prey)
                {
                    result = IsInsideOcean(source, item.Coord)
                        && IsValidTimeToReproducePrey(((Prey)item).TimeToReproduce);
                }
                else if (item is Predator)
                {
                    result = IsInsideOcean(source, item.Coord)
                        && IsValidTimeToReproducePredator(((Predator)item).TimeToReproduce)
                        && IsValidTimeToFeed((item as Predator).TimeToFeed);
                }
            }

            return result;
        }



    }
}
