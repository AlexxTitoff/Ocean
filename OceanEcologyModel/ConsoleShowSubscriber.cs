using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL_Library;

namespace BL_Library
{
    class ConsoleShowSubscriber
    {
        #region ---ctor---

        public ConsoleShowSubscriber(ICellLifeTimePublisher publisher)
        {
            publisher.AddedCell += ShowAdditionMethods;
            publisher.RemovedCell += ShowRemovalMethods;
        }

        #endregion

        public void ShowAdditionMethods(object sender, ChangedCellEventArgs args)
        {
            OceanConcoleViewer.AddImage(args.Target.Coord, args.Target.Image);
        }

        public void ShowRemovalMethods(object sender, ChangedCellEventArgs args)
        {
            OceanConcoleViewer.RemoveImage(args.Target.Coord);
        }
    }
}
