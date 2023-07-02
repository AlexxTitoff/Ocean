using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public interface IGameFieldSize
    {
        int MaxNumberOfRows { get; }

        int MaxNumberOfColumns { get; }
    }
}
