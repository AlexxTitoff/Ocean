using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class CannotGetIndexByCoordinateException : OceanException
    {
        #region --- Properties ---

        public Coordinate Coord { get; private set; }

        #endregion

        #region --- ctor ---

        public CannotGetIndexByCoordinateException() : base()
        {
        }

        public CannotGetIndexByCoordinateException(string message) : base(message)
        {
        }

        public CannotGetIndexByCoordinateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CannotGetIndexByCoordinateException(string message, Coordinate coord) : base(message)
        {
            Coord = coord;
        }

        #endregion


    }
}
