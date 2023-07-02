using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class Ocean : IConsoleView, ICellContainer, ICellLifeTimePublisher
    {
        #region ---Fields & Properties---

        private int _numberOfColumns;
        private int _numberOfRows;
        private List<Cell> _cells;

        public int NumberOfRows
        {
            get
            {
                return _numberOfRows;
            }
            private set
            {
                _numberOfRows = value;
            }
        }

        public int NumberOfColumns
        {
            get
            {
                return _numberOfColumns;
            }
            private set
            {
                _numberOfColumns = value;
            }
        }

        public int Size
        {
            get
            {
                return NumberOfRows * NumberOfColumns;
            }
        }

        #endregion

        #region ---Delegates and Events---

        private ChangedCellDelegate _addedCell; // Цепочка методов
        private ChangedCellDelegate _removedCell; // Цепочка методов

        public event ChangedCellDelegate AddedCell
        {
            add
            {
                _addedCell += value;
            }
            remove
            {
                if (_addedCell != null)
                {
                    _addedCell -= value;
                }
            }
        }

        public event ChangedCellDelegate RemovedCell
        {
            add
            {
                _removedCell += value;
            }
            remove
            {
                if (_removedCell != null)
                {
                    _removedCell -= value;
                }
            }
        }

        #endregion

        #region ---ctor---

        public Ocean(IGameFieldSize source, int numOfColumns, int numOfRows)
        {
            _numberOfRows = numOfRows;
            _numberOfColumns = numOfColumns;
            _cells = new List<Cell>();
        }

        #endregion

        public bool IsInsideOcean(Coordinate coord)
        {
            return (coord.X >= 0 && coord.X < NumberOfColumns
                && coord.Y >= 0 && coord.Y < NumberOfRows);
        }

        public bool HasCell(Coordinate coord)
        {
            bool result = false;

            for (int i = 0; i < _cells.Count; i++)
            {
                if (_cells[i] != null && _cells[i].Coord.Equals(coord))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public bool TryGetCellByCoordinate(Coordinate coord, out Cell foundCell)
        {
            bool result = false;
            foundCell = null;

            for (int i = 0; i < _cells.Count; i++)
            {
                if (_cells[i] != null && _cells[i].Coord.Equals(coord))
                {
                    foundCell = _cells[i];
                    result = true;
                    break;
                }
            }

            return result;
        }

        public bool TryGetIndexByCoordinate(Coordinate coord, out int index)
        {
            bool result = false;
            index = -1;

            for (int i = 0; i < _cells.Count; i++)
            {
                if (_cells[i] != null && _cells[i].Coord.Equals(coord)) // Переделка: добавлено != null
                {
                    index = i;
                    result = true;
                    break;
                }
            }

            return result;
        }

        public Coordinate this[int index]
        {
            get
            {
                Coordinate result = new Coordinate();

                if(_cells.Count > 0 
                    && index >= 0 && index < _cells.Count)
                {
                    result = _cells[index].Coord;
                }

                return result;
            }
        }

        public PositionViewType this[Coordinate coord]
        {
            get
            {
                PositionViewType result = PositionViewType.Empty;

                if (_cells.Count > 0
                    && TryGetCellByCoordinate(coord, out Cell foundCell))
                {
                    result = foundCell.Image;
                }

                return result;
            }
        }

        #region ---FindCellMethods---

        public bool TryFindNorthCell(Coordinate coord, out Coordinate northCoord)
        {
            northCoord.X = -1;
            northCoord.Y = -1;
            bool result = false;

            if (IsInsideOcean(coord) && coord.Y > 0)
            {
                    northCoord.X = coord.X;
                    northCoord.Y = coord.Y - 1;
                    result = true;
            }

            return result;
        }

        public bool TryFindSouthCell(Coordinate coord, out Coordinate southCoord)
        {
            southCoord.X = -1;
            southCoord.Y = -1;
            bool result = false;

            if (IsInsideOcean(coord) && coord.Y < NumberOfRows - 1)
            {
                southCoord.X = coord.X;
                southCoord.Y = coord.Y + 1;
                result = true;
            }

            return result;
        }

        public bool TryFindWestCell(Coordinate coord, out Coordinate westCoord)
        {
            westCoord.X = -1;
            westCoord.Y = -1;
            bool result = false;

            if (IsInsideOcean(coord) && coord.X > 0)
            {
                westCoord.X = coord.X - 1;
                westCoord.Y = coord.Y;
                result = true;
            }

            return result;
        }

        public bool TryFindEastCell(Coordinate coord, out Coordinate eastCoord)
        {
            eastCoord.X = -1;
            eastCoord.Y = -1;
            bool result = false;

            if (IsInsideOcean(coord) && coord.X < NumberOfColumns - 1)
            {
                eastCoord.X = coord.X + 1;
                eastCoord.Y = coord.Y;
                result = true;
            }

            return result;
        }

        public bool TryFindRandomEmptyNextCell(Coordinate coord,
            out Coordinate nextCellCoord)
        {
            nextCellCoord.X = -1;
            nextCellCoord.Y = -1;
            bool result = false;

            if (IsInsideOcean(coord))
            {
                List<Coordinate> foundEmptyNextCells = new List<Coordinate>();

                if (TryFindNorthCell(coord, out Coordinate northCoord)
                    && !HasCell(northCoord))
                {
                    foundEmptyNextCells.Add(northCoord);
                }
                if (TryFindSouthCell(coord, out Coordinate southCoord)
                    && !HasCell(southCoord))
                {
                    foundEmptyNextCells.Add(southCoord);
                }
                if (TryFindWestCell(coord, out Coordinate westCoord)
                    && !HasCell(westCoord))
                {
                    foundEmptyNextCells.Add(westCoord);
                }
                if (TryFindEastCell(coord, out Coordinate eastCoord)
                    && !HasCell(eastCoord))
                {
                    foundEmptyNextCells.Add(eastCoord);
                }

                if (foundEmptyNextCells.Count != 0)
                {
                    int randomNumber = OceanRandomInitializer.rnd.Next(0,
                        foundEmptyNextCells.Count);
                    nextCellCoord = foundEmptyNextCells[randomNumber];
                    result = true;
                }
            }

            return result;
        }

        #endregion

        #region ---OperationMethods---

        public void Add(Cell item)
        {
            if (!Validator.IsInsideOcean(this, item.Coord)) // TODO: ??? (!Validator.IsValidCell(this, item))
            {
                string message = OceanExceptionMessages.IsOutOfOceanCoordinate(
                    item.Coord, _numberOfColumns, _numberOfRows);
                throw new CoordinateIsOutOfOceanException(message, item.Coord);
            }

            _cells.Add(item);
            _addedCell?.Invoke(this, new ChangedCellEventArgs(item)); // ?.Invoke = Проверка (!= null)
        }

        public void MoveCell(Coordinate oldCoord, Coordinate newCoord)
        {
            if (!TryGetIndexByCoordinate(oldCoord, out int index))
            {
                string message = 
                    OceanExceptionMessages.IsNotIndexedCoordinate(oldCoord);
                throw new CannotGetIndexByCoordinateException(message, oldCoord);
            }

            if (!IsInsideOcean(newCoord))
            {
                string message = OceanExceptionMessages.IsOutOfOceanCoordinate(
                    newCoord, _numberOfColumns, _numberOfRows);
                throw new CannotGetIndexByCoordinateException(message, newCoord);
            }

            _removedCell?.Invoke(this, new ChangedCellEventArgs(_cells[index])); // ?.Invoke = Проверка (!= null)
            _cells[index].Coord = newCoord;
            _addedCell?.Invoke(this, new ChangedCellEventArgs(_cells[index])); // ?.Invoke = Проверка (!= null)
        }

        public void SetNullCell(Coordinate coord)
        {
            if (!TryGetIndexByCoordinate(coord, out int index))
            {
                string message =
                    OceanExceptionMessages.IsNotIndexedCoordinate(coord);
                throw new CannotGetIndexByCoordinateException(message, coord);
            }

            _removedCell?.Invoke(this, new ChangedCellEventArgs(_cells[index])); // ?.Invoke = Проверка (!= null)
            _cells[index] = null;
        }

        private void RemoveNullCells()
        {
            for (int i = _cells.Count - 1; i >= 0 ; i--)
            {
                if (_cells[i] == null)
                {
                    _cells.Remove(_cells[i]);
                }
            }
        }

        //public void RemoveCell(Coordinate coord)
        //{
        //    if (!TryGetIndexByCoordinate(coord, out int index))
        //    {
        //        string message = 
        //            OceanExceptionMessages.IsNotIndexedCoordinate(coord);
        //        throw new CannotGetIndexByCoordinateException(message, coord);
        //    }

        //    _removedCell?.Invoke(this, new ChangedCellEventArgs(_cells[index])); // ?.Invoke = Проверка (!= null)
        //    _cells.Remove(_cells[index]);
        //}

        public void Process()
        {
            for (int i = _cells.Count - 1; i >= 0; i--)
            {
                //if (_cells[i] != null)
                //{
                //    _cells[i].Process();
                //}

                _cells[i]?.Process();
            }

            RemoveNullCells();
        }

        #endregion


    }
}
