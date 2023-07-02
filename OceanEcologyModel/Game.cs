using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL_Library;

namespace OceanEcologyModel
{
    class Game
    {
        #region ---Constants---

        private const int NUMBER_OF_ITERATIONS = 30;

        #endregion

        #region --- Fields & Properties --- // TODO: separate

        private int _iterationsCount;
        private readonly Ocean _ocean;
        private readonly IGameFieldSize _view;
        private readonly CellCountSubscriber _cellCountSubscriber;
        private readonly ConsoleShowSubscriber _cellShowSubscriber;

        public int IterationsCount
        {
            get
            {
                return _iterationsCount;
            }
        }

        #endregion

        #region --- ctor ---

        public Game(IGameFieldSize view, int numberOfColumns, int numberOfRows,
            int numberOfObstacles =
            OceanRandomInitializer.DEFAULT_NUMBER_OF_OBSTACLES,
            int numberOfPreys =
            OceanRandomInitializer.DEFAULT_INITIAL_NUMBER_OF_PREYS,
            int numberOfPredators =
            OceanRandomInitializer.DEFAULT_INITIAL_NUMBER_OF_PREDATORS)
        {
            _view = view;
            _ocean = new Ocean(view, numberOfColumns, numberOfRows);
            _iterationsCount = 0;
            _cellCountSubscriber = new CellCountSubscriber(_ocean);
            _cellShowSubscriber = new ConsoleShowSubscriber(_ocean);
        }

        #endregion

        public void Initialize()
        {
            OceanRandomInitializer source = new OceanRandomInitializer();
            source.AddItems(_ocean);
        }

        public void ShowConsoleStartPosition()
        {
            OceanConcoleViewer.PrintOceanBorders(_ocean);
        }

        public void Run(int numberOfIterations = NUMBER_OF_ITERATIONS)
        {
            for (int i = 0; i < numberOfIterations; i++)
            {
                _ocean.Process();
                _iterationsCount++;
            }
        }
    }
}
