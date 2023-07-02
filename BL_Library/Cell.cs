using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public abstract class Cell
    {
        #region ---Fields & Properties---

        protected readonly ICellContainer _owner;
        protected Coordinate _coord;

        public Coordinate Coord
        {
            get
            {
                return _coord;
            }
            set // private set ???
            {
                _coord = value;
            }
        }

        public virtual PositionViewType Image
        {
            get
            {
                return PositionViewType.Empty;
            }
        }

        #endregion

        #region ---ctor---

        public Cell(ICellContainer owner, Coordinate coord)
        {
            _owner = owner;
            _coord = coord;
        }

        #endregion

        public abstract void Process(/*ref int numberOfProcessedItems, ref int index*/);

        
    }
}
