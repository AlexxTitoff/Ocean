using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class OceanConcoleViewer : IGameFieldSize
    {
        #region Fields and Properties

        public int MaxNumberOfRows
        {
            get
            {
                return (Console.WindowHeight - 2);
            }
        }

        public int MaxNumberOfColumns
        {
            get
            {
                return (Console.WindowWidth - 2);
            }
        }

        #endregion

        public static void PrintOceanBorders(IConsoleView ocean)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < ocean.NumberOfColumns + 2; i++)
            {
                Console.Write("{0}", (char)PositionViewType.HorizontalBorder);
            }

            for (int j = 1; j < ocean.NumberOfRows + 1; j++)
            {
                Console.SetCursorPosition(0, j);
                Console.Write("{0}", (char)PositionViewType.VerticalBorder);

                Console.SetCursorPosition(ocean.NumberOfColumns + 1, j);
                Console.Write("{0}", (char)PositionViewType.VerticalBorder);
            }
            Console.SetCursorPosition(0, 0);

            Console.SetCursorPosition(0, ocean.NumberOfRows + 1);
            for (int i = 0; i < ocean.NumberOfColumns + 2; i++)
            {
                Console.Write("{0}", (char)PositionViewType.HorizontalBorder);
            }
        }

        public static void AddImage(Coordinate coord, PositionViewType image)
        {
            Console.SetCursorPosition(coord.X + 1, coord.Y + 1);
            Console.Write("{0}", (char)image);
        }

        public static void RemoveImage(Coordinate coord)
        {
            Console.SetCursorPosition(coord.X + 1, coord.Y + 1);
            Console.Write("{0}", (char)PositionViewType.Empty);
        }
    }
}
