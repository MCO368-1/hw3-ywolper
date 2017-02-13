using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Board gameOfLifeBoard = new Board();
            gameOfLifeBoard.SetBoard(BeginningLayout.Pulsar);
            Console.WriteLine(gameOfLifeBoard.ToString());
            Console.WriteLine();

            Console.ReadKey();

            Console.WriteLine(gameOfLifeBoard.NextState().ToString());
            Console.WriteLine();

            Console.ReadKey();

            Console.WriteLine(gameOfLifeBoard.NextState().ToString());
            Console.WriteLine();

            Console.ReadKey();

            Console.WriteLine(gameOfLifeBoard.NextState().ToString());
            Console.WriteLine();

            Console.ReadKey();

            Console.WriteLine(gameOfLifeBoard.NextState().ToString());
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
