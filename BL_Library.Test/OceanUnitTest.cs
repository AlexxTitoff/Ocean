using Moq;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace BL_Library.Test
{
    [TestClass]
    public class OceanUnitTest
    {
        #region Constants

        const int TEST_NUMBER_OF_COLUMNS = 4;
        const int TEST_NUMBER_OF_ROWS = 4;

        const int MIDDLE_X1 = 1;
        const int MIDDLE_Y1 = 1;
        const int MIDDLE_X2 = 2;
        const int MIDDLE_Y2 = 2;

        const int BORDER_X1 = 0;
        const int BORDER_Y1 = 1;
        const int BORDER_X2 = TEST_NUMBER_OF_COLUMNS - 1;
        const int BORDER_Y2 = 2;
        const int BORDER_X3 = 2;
        const int BORDER_Y3 = 0;
        const int BORDER_X4 = 1;
        const int BORDER_Y4 = TEST_NUMBER_OF_ROWS - 1;

        #endregion

        #region Coordinates

        Coordinate cornerUL = new Coordinate(0, 0);
        Coordinate cornerUR = new Coordinate(TEST_NUMBER_OF_COLUMNS - 1, 0);
        Coordinate cornerLL = new Coordinate(0, TEST_NUMBER_OF_ROWS - 1);
        Coordinate cornerLR = new Coordinate(
           TEST_NUMBER_OF_COLUMNS - 1, TEST_NUMBER_OF_ROWS - 1);

        Coordinate middle1 = new Coordinate(MIDDLE_X1, MIDDLE_Y1);
        Coordinate middle2 = new Coordinate(MIDDLE_X2, MIDDLE_Y2);

        Coordinate border1 = new Coordinate(BORDER_X1, BORDER_Y1);
        Coordinate border2 = new Coordinate(BORDER_X2, BORDER_Y2);
        Coordinate border3 = new Coordinate(BORDER_X3, BORDER_Y3);
        Coordinate border4 = new Coordinate(BORDER_X4, BORDER_Y4);

        Coordinate offCornerUL = new Coordinate(-1, 0);
        Coordinate offCornerLR = new Coordinate(
            TEST_NUMBER_OF_COLUMNS - 1, TEST_NUMBER_OF_ROWS);

        #endregion

        Mock<IGameFieldSize> size = new Mock<IGameFieldSize>();

        Ocean testOcean;

        List<Coordinate> inOceanCoords1 = new List<Coordinate>();
        List<Coordinate> inOceanCoords2 = new List<Coordinate>();
        List<Coordinate> offOceanCoords = new List<Coordinate>();

        public OceanUnitTest()
        {
            AddInOceanCoords1();
            AddInOceanCoords2();
            AddOffOceanCoords();

            size.Setup(c => c.MaxNumberOfRows).Returns(TEST_NUMBER_OF_ROWS);
            size.Setup(c => c.MaxNumberOfColumns).Returns(TEST_NUMBER_OF_COLUMNS);

            testOcean = new Ocean(size.Object, TEST_NUMBER_OF_COLUMNS, TEST_NUMBER_OF_ROWS);
        }

        #region HelpMethods

        private void AddInOceanCoords1()
        {
            inOceanCoords1.Add(cornerUL);
            inOceanCoords1.Add(cornerLR);
            inOceanCoords1.Add(border1);
            inOceanCoords1.Add(border2);
            inOceanCoords1.Add(middle1);

        }

        private void AddInOceanCoords2()
        {
            inOceanCoords2.Add(cornerUR);
            inOceanCoords2.Add(cornerLL);
            inOceanCoords2.Add(border3);
            inOceanCoords2.Add(border4);
            inOceanCoords2.Add(middle1);
        }

        private void AddOffOceanCoords()
        {
            offOceanCoords.Add(offCornerUL);
            offOceanCoords.Add(offCornerLR);
        }


        private void AddMockCellsInOceanCoord1()
        {
            foreach (var c in inOceanCoords1)
            {
                var cell1 = new Mock<Cell>(testOcean, c);
                testOcean.Add(cell1.Object);
            }
        }

        private void AddMockCellInheritorsInOceanCoord1(out List<Type> inheritorTypes)
        {
            var o1 = new Mock<Obstacle>(testOcean, cornerUL);
            var prey1 = new Mock<Prey>(testOcean, cornerLR);
            var prey2 = new Mock<Prey>(testOcean, border1);
            var predator1 = new Mock<Predator>(testOcean, border2);
            var predator2 = new Mock<Predator>(testOcean, middle1);

            testOcean.Add(o1.Object);
            testOcean.Add(prey1.Object);
            testOcean.Add(prey2.Object);
            testOcean.Add(predator1.Object);
            testOcean.Add(predator2.Object);

            inheritorTypes = new List<Type>()
            {
                prey1.Object.GetType(),
                prey1.Object.GetType(),
                prey2.Object.GetType(),
                predator1.Object.GetType(),
                predator2.Object.GetType()
            };
        }

        #endregion

        #region IsInsideOceanTest
        //public bool IsInsideOcean(Coordinate coord)
        //{
        //    return (coord.X >= 0 && coord.X < NumberOfColumns
        //        && coord.Y >= 0 && coord.Y < NumberOfRows);
        //}

        [TestMethod]
        public void IsInsideOceanTrueTest()
        {
            int count = 0;
            foreach (var c in inOceanCoords1)
            {
                if (testOcean.IsInsideOcean(c))
                {
                    count++;
                }
            }

            Assert.AreEqual(count, 5);
        }

        [TestMethod]
        public void IsInsideOceanFalseTest()
        {
            int count = 0;
            foreach (var c in offOceanCoords)
            {
                if (testOcean.IsInsideOcean(c))
                {
                    count++;
                }
            }

            Assert.AreEqual(count, 0);
        }

        #endregion

        #region ContainsCoordinateTest

        //public bool HasCell(Coordinate coord)
        //{
        //    bool result = false;

        //    for (int i = 0; i < _cells.Count; i++)
        //    {
        //        if (_cells[i] != null && _cells[i].Coord.Equals(coord))
        //        {
        //            result = true;
        //            break;
        //        }
        //    }

        //    return result;
        //}

        [TestMethod]
        // _cells[i] = null
        public void HasCellInEmptyOceanTest() 
        {
            int count = 0;
            foreach (var c in inOceanCoords1)
            {
                if (testOcean.HasCell(c))
                {
                    count++;
                }
            }

            Assert.AreEqual(count, 0);
        }

        [TestMethod]
        // _cells[i] != null && _cells[i].Coord.Equals(coord)
        public void HasCellTrueCoordTest() 
        {
            AddMockCellsInOceanCoord1();

            int count = 0;
            foreach (var c in inOceanCoords1)
            {
                if (testOcean.HasCell(c))
                {
                    count++;
                }
            }

            Assert.AreEqual(count, inOceanCoords1.Count);
        }

        [TestMethod]
        public void HasCellFalseCoordTest()
        {
            AddMockCellsInOceanCoord1();

            int count = 0;
            foreach (var c in inOceanCoords2.Union(offOceanCoords).ToList())
            {
                if (testOcean.HasCell(c))
                {
                    count++;
                }
            }

            Assert.AreEqual(count, 0);
        }

        #endregion

        #region TryGetCellByCoordinateTest

        //public bool TryGetCellByCoordinate(Coordinate coord, out Cell foundCell)
        //{
        //    bool result = false;
        //    foundCell = null;

        //    for (int i = 0; i < _cells.Count; i++)
        //    {
        //        if (_cells[i] != null && _cells[i].Coord.Equals(coord))
        //        {
        //            foundCell = _cells[i];
        //            result = true;
        //            break;
        //        }
        //    }

        //    return result;
        //}

        [TestMethod]
        // _cells[i] = null
        public void TryGetCellByCoordinateInEmptyOceanTest() 
        {
            int count = 0;
            foreach (var c in inOceanCoords1)
            {
                if (testOcean.TryGetCellByCoordinate(c, out Cell foundCell))
                {
                    count++;
                }
            }

            Assert.AreEqual(count, 0);
        }

        [TestMethod]
        // _cells[i] != null && _cells[i].Coord.Equals(coord)
        public void TryGetCellByCoordinateFalseTest()
        {
            AddMockCellsInOceanCoord1();

            int count = 0;
            foreach (var c in inOceanCoords2.Union(offOceanCoords))
            {
                if (testOcean.TryGetCellByCoordinate(c, out Cell foundCell))
                {
                    count++;
                }
            }

            Assert.AreEqual(count, 0);
        }

        [TestMethod]
        public void TryGetCellByCoordinateTrueTest()
        {
            AddMockCellsInOceanCoord1();

            int count = 0;
            foreach (var c in inOceanCoords1)
            {
                if (testOcean.TryGetCellByCoordinate(c, out Cell foundCell))
                {
                   //Type t1 = foundCell.GetType();
                    count++;
                }
            }

            Assert.AreEqual(count, inOceanCoords1.Count);
        }

        [TestMethod]
        public void TryGetCellByCoordinateTrueTypeTest() 
        {
            AddMockCellInheritorsInOceanCoord1(out List<Type> inheritorTypes);

            var foundCellTypes = new List<Type>(); 
            
            foreach (var c in inOceanCoords1)
            {
                if (testOcean.TryGetCellByCoordinate(c, out Cell foundCell))
                {
                    foundCellTypes.Add(foundCell.GetType());
                }
            }

            CollectionAssert.AreEqual(foundCellTypes, inheritorTypes);
        }

        #endregion


    }
}
