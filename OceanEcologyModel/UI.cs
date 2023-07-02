using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL_Library;

namespace OceanEcologyModel
{
    class UI
    {
        public static void InputNumberOfColumns(IGameFieldSize source, out int numberOfColumns)
        {
            Console.WriteLine("Input Number of Columns:");
            string numberOfColumnsString = Console.ReadLine();
            int.TryParse(numberOfColumnsString, out numberOfColumns);

            while (!int.TryParse(numberOfColumnsString, out numberOfColumns)
                || !ConsoleValidator.IsValidNumberOfColumns(source, numberOfColumns))
            {
                Console.WriteLine("Error number! Let's try again...");
                numberOfColumnsString = Console.ReadLine();
                int.TryParse(numberOfColumnsString, out numberOfColumns);
            }

            Console.Clear();
        }

        public static void InputNumberOfRows(IGameFieldSize source, out int numberOfRows)
        {
            Console.WriteLine("Input Number of Rows:");
            string numberOfRowsString = Console.ReadLine();
            int.TryParse(numberOfRowsString, out numberOfRows);

            while (!int.TryParse(numberOfRowsString, out numberOfRows)
                || !ConsoleValidator.IsValidNumberOfRows(source, numberOfRows))
            {
                Console.WriteLine("Error number! Let's try again...");
                numberOfRowsString = Console.ReadLine();
                int.TryParse(numberOfRowsString, out numberOfRows);
            }

            Console.Clear();
        }

    }
}
