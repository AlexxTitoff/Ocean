using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public enum PositionViewType
    {
        Empty = ' ',
        Obstacle = '#',
        Prey = 'f',
        Predator = 'S',
        HorizontalBorder = '-',
        VerticalBorder = '|'
    }
}
