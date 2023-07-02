using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public interface IConsoleView : IField
    {
        PositionViewType this[Coordinate coord] { get;  }
    }
}
