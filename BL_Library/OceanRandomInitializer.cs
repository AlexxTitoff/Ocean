using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class OceanRandomInitializer // Вынести из библиотеки в отдельную библиотеку (в DATALAYER)
    {
        public static Random rnd = new Random();

        #region ---Constants---

        public const int DEFAULT_INITIAL_NUMBER_OF_PREYS = 150;
        public const int DEFAULT_INITIAL_NUMBER_OF_PREDATORS = 20;
        public const int DEFAULT_NUMBER_OF_OBSTACLES = 75;

        private const double MAX_INITIAL_OCEAN_FILLING = 0.5;

        #endregion

        private Ocean _destination;

        public void AddItems(Ocean destination, 
            int numberOfObstacles = DEFAULT_NUMBER_OF_OBSTACLES,
            int numberOfPreys = DEFAULT_INITIAL_NUMBER_OF_PREYS, 
            int numberOfPredators = DEFAULT_INITIAL_NUMBER_OF_PREDATORS)
        {
            if (numberOfObstacles + numberOfPreys + numberOfPredators >
                (int)(destination.Size * MAX_INITIAL_OCEAN_FILLING))
            {
                throw new NotImplementedException(); // TODO: TooManyItemsException !!!!!
            }

            _destination = destination;
            AddObstacles(numberOfObstacles);
            AddPreys(numberOfPreys);
            AddPredators(numberOfPredators);
        }

        private Coordinate GetEmptyCellCoordinates()
        {
            Coordinate coord = new Coordinate();

            do
            {
                coord.X = rnd.Next(0, _destination.NumberOfColumns); 
                coord.Y = rnd.Next(0, _destination.NumberOfRows); 
            } while (_destination.HasCell(coord));

            return coord;
        }

        private void AddObstacles(int numberObstacles)
        {
            for (int i = 0; i < numberObstacles; i++)
            {
                Coordinate coord = GetEmptyCellCoordinates();

                _destination.Add(new Obstacle(_destination, coord));
            }
        }

        private void AddPreys(int numberOfPreys)
        {
            for (int i = 0; i < DEFAULT_INITIAL_NUMBER_OF_PREYS; i++)
            {
                Coordinate coord = GetEmptyCellCoordinates();

                _destination.Add(new Prey(_destination, coord, 
                    rnd.Next(1, Prey.MAX_TIME_TO_REPRODUCE_PREY + 1)));
            }
        }

        private void AddPredators(int numberOfPredators)
        {
            for (int i = 0; i < DEFAULT_INITIAL_NUMBER_OF_PREDATORS; i++)
            {
                Coordinate coord = GetEmptyCellCoordinates();

                _destination.Add(new Predator(_destination, coord,
                    rnd.Next(1, Predator.MAX_TIME_TO_REPRODUCE_PREDATOR + 1),
                    rnd.Next(1, Predator.MAX_TIME_TO_FEED + 1)));
            }
        }
    }
}
