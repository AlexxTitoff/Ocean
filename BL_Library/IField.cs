using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public interface IField
    {
        int NumberOfRows { get; }
        int NumberOfColumns { get; }
        Coordinate this[int index] { get; }
    }
}
