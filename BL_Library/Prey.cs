using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class Prey : Cell
    {
        public const int MAX_TIME_TO_REPRODUCE_PREY = 12;

        #region ---Fields & Properties---

        protected int _timeToReproduce;

        public int TimeToReproduce
        {
            get
            {
                return _timeToReproduce;
            }
        }

        public override PositionViewType Image
        {
            get
            {
                return PositionViewType.Prey;
            }
        }

        #endregion

        #region ---ctor---

        public Prey(ICellContainer owner, Coordinate coord, int timeToReproduce = MAX_TIME_TO_REPRODUCE_PREY) : base(owner, coord)
        {
            _timeToReproduce = timeToReproduce;
        }

        #endregion

        protected bool TryMoveToEmptyCell()
        {
            bool result = false;

            if (_owner.TryFindRandomEmptyNextCell(_coord, 
                out Coordinate nextCellCoord))
            {
                Coordinate oldCoord = _coord;
                _owner.MoveCell(oldCoord, nextCellCoord);
                result = true;
            }

            return result;
        }

        protected virtual bool TryReproduce()
        {
            bool result = false;

            if (_timeToReproduce == 0 
                && _owner.TryFindRandomEmptyNextCell(_coord,
                out Coordinate nextCellCoord))
            {
                _owner.Add(new Prey(_owner, nextCellCoord,
                    MAX_TIME_TO_REPRODUCE_PREY));
                _timeToReproduce = MAX_TIME_TO_REPRODUCE_PREY;
                result = true;
            }
            else
            {
                _timeToReproduce--;
            }

            return result;
        }

        public override void Process(/*ref int numberOfProcessedItems, ref int index*/)
        {
            if (TryMoveToEmptyCell())
            {
                TryReproduce();
            }
        }

    }
}
