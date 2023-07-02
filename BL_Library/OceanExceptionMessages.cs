using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class OceanExceptionMessages
    {
        public static string IsOutOfOceanCoordinate(Coordinate coord, int numberOfColumns, int numberOfRows)
        {
            return String.Format("Inputed Coordinate (X = {0}, Y = {1}) " +
                "is out of Ocean range, " +
                "X must be in range from 0 and less than {2}," +
                "Y must be in range from 0 and less than {3}",
                coord.X, coord.Y, numberOfColumns, numberOfRows);
        }

        public static string IsNotIndexedCoordinate(Coordinate coord)
        {
            return String.Format("Inputed Coordinate (X = {0}, Y = {1}) " +
                "doesn't correspond to indexed Cell", coord.X, coord.Y);
        }
    }
}
