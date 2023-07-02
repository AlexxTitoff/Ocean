using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public interface ICellLifeTimePublisher
    {
        event ChangedCellDelegate AddedCell;
        event ChangedCellDelegate RemovedCell;
    }
}
