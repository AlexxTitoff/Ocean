using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class CoordinateIsOutOfOceanException : OceanException
    {
        //private const string ERROR_MESSAGE = "Coordinate is out of Ocean!";

        #region --- Properties ---

        public Coordinate Coord { get; private set; }

        #endregion

        #region --- ctor ---

        public CoordinateIsOutOfOceanException() : base(/*ERROR_MESSAGE*/)
        {
        }

        public CoordinateIsOutOfOceanException(string message) : base(message)
        {
        }

        public CoordinateIsOutOfOceanException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CoordinateIsOutOfOceanException(string message, Coordinate coord) : base(message)
        {
            Coord = coord;
        }

        #endregion
    }
}
