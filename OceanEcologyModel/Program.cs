using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL_Library;

namespace OceanEcologyModel
{
    class Program
    {
        static void Main(string[] args)
        {
            OceanConcoleViewer view = new OceanConcoleViewer();

            try
            {
                RunGame1(view);
            }
            catch (OceanException target)
            {
                Console.WriteLine(target.Message);
            }

            Console.ReadKey();
        }

        public static void RunGame1(OceanConcoleViewer view)
        {
            UI.InputNumberOfColumns(view, out int numberOfColumns);
            UI.InputNumberOfRows(view, out int numberOfRows);

            Game game1 = new Game(view, numberOfColumns, numberOfRows);
            game1.ShowConsoleStartPosition();
            game1.Initialize();
            game1.Run(50);

        }
    }
}
