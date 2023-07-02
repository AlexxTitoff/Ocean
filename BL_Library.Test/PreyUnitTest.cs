using Moq;

namespace BL_Library.Test
{
    [TestClass]
    public class PreyUnitTest
    {
        //[TestMethod]
        //[DataRow(0, 1)]
        //[DataRow(1, 0)]
        //[DataRow(1, 2)]
        //[DataRow(2, 1)]

        //public void TryMoveToEmptyCellTest(int x, int y)
        //{
        //    bool result = false;

        //    Coordinate coord = new Coordinate(1, 1);
        //    Coordinate nextCellCoord = new Coordinate(x, y);

        //    Mock<ICellContainer> c = new Mock<ICellContainer>();
        //    c.Setup(c => c.TryFindRandomEmptyNextCell(coord, out nextCellCoord)).Returns(true);
        //    c.Setup(c => c.MoveCell(coord, nextCellCoord));

        //    Prey p = new Prey(c.Object, coord);

        //    Assert.IsTrue(p.TryMoveToEmptyCell());
        //}

        //[TestMethod]
        //[DataRow(0, -1)]
        //[DataRow(-1, 0)]
        //public void TryMoveToEmptyCellNegativeTest(int x, int y)
        //{
        //    bool result = false;

        //    Coordinate coord = new Coordinate(0, 0);
        //    Coordinate nextCellCoord = new Coordinate(x, y);

        //    Mock<ICellContainer> c = new Mock<ICellContainer>();
        //    c.Setup(c => c.TryFindRandomEmptyNextCell(coord, out nextCellCoord)).Returns(false);
        //    c.Setup(c => c.MoveCell(coord, nextCellCoord));

        //    Prey p = new Prey(c.Object, coord);

        //    Assert.IsFalse(p.TryMoveToEmptyCell());
        //}
    }
}