using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class Predator : Prey
    {
        public const int MAX_TIME_TO_FEED = 9;
        public const int MAX_TIME_TO_REPRODUCE_PREDATOR = 6;

        #region ---Fields & Properties---

        private int _timeToFeed;

        internal int TimeToFeed
        {
            get
            {
                return _timeToFeed;
            }
        }

        public override PositionViewType Image
        {
            get
            {
                return PositionViewType.Predator;
            }
        }

        #endregion

        #region ---ctor---

        internal Predator(ICellContainer owner, Coordinate coord, 
            int timeToReproduce = MAX_TIME_TO_REPRODUCE_PREDATOR,
            int timeToFeed = MAX_TIME_TO_FEED) : base(owner, coord)
        {
            _timeToReproduce = timeToReproduce;
            _timeToFeed = timeToFeed;
        }

        #endregion

        private bool IsPrey(Coordinate coord)
        {
            bool result = false;

            if (_owner.TryGetCellByCoordinate(coord, out Cell foundCell))
            {
                result = (foundCell is Prey) && !(foundCell is Predator);
            }

            return result;
        }

        public bool TryFindRandomNextPrey(Coordinate coord,
            out Coordinate nextCellCoord)
        {
            nextCellCoord.X = -1;
            nextCellCoord.Y = -1;
            bool result = false;

            List<Coordinate> foundNextPreys = new List<Coordinate>();

            if (_owner.TryFindNorthCell(coord, out Coordinate northCoord)
                && IsPrey(northCoord))
            {
                foundNextPreys.Add(northCoord);
            }
            if (_owner.TryFindSouthCell(coord, out Coordinate southCoord)
                && IsPrey(southCoord))
            {
                foundNextPreys.Add(southCoord);
            }
            if (_owner.TryFindWestCell(coord, out Coordinate westCoord)
                && IsPrey(westCoord))
            {
                foundNextPreys.Add(westCoord);
            }
            if (_owner.TryFindEastCell(coord, out Coordinate eastCoord)
                && IsPrey(eastCoord))
            {
                foundNextPreys.Add(eastCoord);
            }

            if (foundNextPreys.Count != 0)
            {
                int randonNumber = OceanRandomInitializer.rnd.Next(0,
                    foundNextPreys.Count);
                nextCellCoord = foundNextPreys[randonNumber];
                result = true;
            }

            return result;
        }

        protected override bool TryReproduce()
        {
            bool result = false;

            if (_timeToReproduce == 0 &&
                _owner.TryFindRandomEmptyNextCell(_coord, 
                out Coordinate nextCellCoord))
            {
                _owner.Add(new Predator(_owner, nextCellCoord,
                    MAX_TIME_TO_REPRODUCE_PREDATOR, MAX_TIME_TO_FEED));
                _timeToReproduce = MAX_TIME_TO_REPRODUCE_PREDATOR;
                result = true;
            }
            else
            {
                _timeToReproduce--;
            }

            return result;
        }

        public bool IsStarvedToDeath()
        {
            if(_timeToFeed == 0)
            {
                _owner.SetNullCell(_coord);
            }

            return (_timeToFeed == 0);
        }

        public bool TryFeed()
        {
            bool result = false;

            if (TryFindRandomNextPrey(_coord,
                    out Coordinate nextCellCoord))
            {
                _owner.SetNullCell(nextCellCoord);
                Coordinate oldCoord = _coord;
                _owner.MoveCell(oldCoord, nextCellCoord); // TODO: Попадает на еще не удаленную ячейку ?????
                _timeToFeed = MAX_TIME_TO_FEED;
                result = true;
            }
            else
            {
                _timeToFeed--;
            }

            return result;
        }

        public override void Process(/*int numberOfProcessedItems, ref int index*/)
        {
            if (!IsStarvedToDeath())
            {
                if (!TryFeed())
                {
                    if (TryMoveToEmptyCell())
                    {
                        TryReproduce();
                    }
                }
            }
            //else
            //{
            //    numberOfProcessedItems--;
            //    index--;
            //}
        }

    }
}
