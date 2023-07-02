using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL_Library;

namespace OceanEcologyModel
{
    class ConsoleValidator
    {
        public static bool IsValidNumberOfColumns(IGameFieldSize source, int numberOfColumns)
        {
            return (numberOfColumns >= 0 && numberOfColumns < source.MaxNumberOfColumns);
        }

        public static bool IsValidNumberOfRows(IGameFieldSize source, int numberOfRows)
        {
            return (numberOfRows >= 0 && numberOfRows < source.MaxNumberOfRows);
        }

    }
}
