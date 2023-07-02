using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public interface ICellContainer : IField
    {
        bool TryGetCellByCoordinate(Coordinate coord, out Cell foundCell);
        bool TryFindNorthCell(Coordinate coord, out Coordinate northCoord);
        bool TryFindSouthCell(Coordinate coord, out Coordinate southCoord);
        bool TryFindWestCell(Coordinate coord, out Coordinate westCoord);
        bool TryFindEastCell(Coordinate coord, out Coordinate eastCoord);
        bool TryFindRandomEmptyNextCell(Coordinate coord, out Coordinate nextCellCoord);
        void Add(Cell item);
        void MoveCell(Coordinate oldCoord, Coordinate newCoord);
        void SetNullCell(Coordinate coord);
        //void RemoveCell(Coordinate coord);

    }
}
