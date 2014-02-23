using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicrossCJL;

namespace PicrossSolverCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] linesValue = new int[2][];
            linesValue[0] = new int[] {1};
            linesValue[1] = new int[] {1};

            int[][] columnsValue = new int[2][];
            columnsValue[0] = new int[] {1};
            columnsValue[1] = new int[] {1};

            PicrossPuzzle.CellValue[,] cellsValue = new PicrossPuzzle.CellValue[,] {};

            PicrossPuzzle puzzle = new PicrossPuzzle(cellsValue, linesValue, columnsValue);

            PicrossSolver solver = new PicrossSolver();

            solver.Solve(puzzle);
            Console.WriteLine(puzzle);
        }
    }
}
