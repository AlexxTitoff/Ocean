using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class ChangedCellEventArgs : EventArgs
    {
        public Cell Target { get; private set; }

        public ChangedCellEventArgs(Cell target)
        {
            Target = target;
        }
    }
}
