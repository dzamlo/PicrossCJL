using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicrossCJL;
using System.Diagnostics;

namespace PicrossSolverCLI
{
    class Program
    {
        static char CellValue2Char(PicrossPuzzle.CellValue cellvalue)
        {
            switch (cellvalue)
            {
                case PicrossPuzzle.CellValue.Empty:
                    return ' ';
                case PicrossPuzzle.CellValue.Filled:
                    return '#';
                case PicrossPuzzle.CellValue.Crossed:
                    return 'x';
                default:
                    return ' ';
            }
        }

        static void Main(string[] args)
        {
            int[][] linesValue = new int[5][];
            linesValue[0] = new int[] { 3 };
            linesValue[1] = new int[] { 4 };
            linesValue[2] = new int[] { 1 };
            linesValue[3] = new int[] { 1, 3 };
            linesValue[4] = new int[] { 2 };

            int[][] columnsValue = new int[5][];
            columnsValue[0] = new int[] { 1, 1 };
            columnsValue[1] = new int[] { 2 };
            columnsValue[2] = new int[] { 4 };
            columnsValue[3] = new int[] { 2, 2 };
            columnsValue[4] = new int[] { 2 };

            PicrossPuzzle.CellValue[,] cellsValue = new PicrossPuzzle.CellValue[5, 5];

            PicrossPuzzle puzzle = new PicrossPuzzle(cellsValue, linesValue, columnsValue);

            PicrossSolver solver = new PicrossSolver();

            Stopwatch sw = new Stopwatch();

            sw.Restart();
            solver.Solve(puzzle);
            sw.Stop();
            Console.WriteLine("Time to solve 5x5: {0} ms", sw.ElapsedMilliseconds);

            for (int y = 0; y < puzzle.Size.Height; y++)
            {
                for (int x = 0; x < puzzle.Size.Width; x++)
                {
                    Console.Write(CellValue2Char(puzzle.Cells[y, x]));
                }
                Console.WriteLine();
            }

            linesValue = new int[7][];
            linesValue[0] = new int[] { 3 };
            linesValue[1] = new int[] { 1, 2, 1 };
            linesValue[2] = new int[] { 2, 3 };
            linesValue[3] = new int[] { 2, 2 };
            linesValue[4] = new int[] { 2, 1 };
            linesValue[5] = new int[] { 1, 2, 1 };
            linesValue[6] = new int[] { 3, 1 };

            columnsValue = new int[7][];
            columnsValue[0] = new int[] { 2, 3 };
            columnsValue[1] = new int[] { 1, 3, 1 };
            columnsValue[2] = new int[] { 1, 1, 2 };
            columnsValue[3] = new int[] { 2, 2 };
            columnsValue[4] = new int[] { 2, 1 };
            columnsValue[5] = new int[] { 2, 1 };
            columnsValue[6] = new int[] { 3 };

            cellsValue = new PicrossPuzzle.CellValue[7, 7];

            puzzle = new PicrossPuzzle(cellsValue, linesValue, columnsValue);

            sw.Restart();
            solver.Solve(puzzle);
            sw.Stop();
            Console.WriteLine("Time to solve 7x7: {0} ms", sw.ElapsedMilliseconds);

            for (int y = 0; y < puzzle.Size.Height; y++)
            {
                for (int x = 0; x < puzzle.Size.Width; x++)
                {
                    Console.Write(CellValue2Char(puzzle.Cells[y, x]));
                }
                Console.WriteLine();
            }
        }
    }
}
