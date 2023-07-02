using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class Obstacle : Cell
    {
        public  override PositionViewType Image
        {
            get
            {
                return PositionViewType.Obstacle;
            }
        }

        #region ---ctor---

        public Obstacle(Ocean owner, Coordinate coord) : base(owner, coord)
        {
        }

        #endregion

        public override void Process()
        {
        }

    }
}
