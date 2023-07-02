using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL_Library;


namespace BL_Library
{
    class CellCountSubscriber
    {
        #region ---Fields & Properties---

        private int _numberOfObstacles;
        private int _numberOfPreys;
        private int _numberOfPredators;

        public int NumberOfObstacles
        {
            get
            {
                return _numberOfObstacles;
            }
        }

        public int NumberOfPreys
        {
            get
            {
                return _numberOfPreys;
            }
        }

        public int NumberOfPredators
        {
            get
            {
                return _numberOfPredators;
            }
        }

        #endregion

        #region ---ctor---

        public CellCountSubscriber(ICellLifeTimePublisher publisher)
        {
            publisher.AddedCell += CountAdditionMethods;
            publisher.RemovedCell += CountRemovalMethods;
        }

        #endregion

        private void CountAdditionMethods(object sender, ChangedCellEventArgs args)
        {
            if(args.Target is Obstacle)
            {
                _numberOfObstacles++;
            }

            if (args.Target is Predator)
            {
                _numberOfPredators++;
            }
            else if (args.Target is Prey)
            {
                _numberOfPreys++;
            }
        }

        private void CountRemovalMethods(object sender, ChangedCellEventArgs args)
        {
            if (args.Target is Predator)
            {
                _numberOfPredators--;
            }
            else if (args.Target is Prey)
            {
                _numberOfPreys--;
            }
        }

    }
}
