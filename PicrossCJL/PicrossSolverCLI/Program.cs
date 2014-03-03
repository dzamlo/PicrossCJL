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
            /*int[][] linesValue = new int[2][];
            linesValue[0] = new int[] {1};
            linesValue[1] = new int[] {1};

            int[][] columnsValue = new int[2][];
            columnsValue[0] = new int[] {1};
            columnsValue[1] = new int[] {1};

            PicrossPuzzle.CellValue[,] cellsValue = new PicrossPuzzle.CellValue[,] {};

            PicrossPuzzle puzzle = new PicrossPuzzle(cellsValue, linesValue, columnsValue);

            PicrossSolver solver = new PicrossSolver();

            solver.Solve(puzzle);
            Console.WriteLine(puzzle);*/

            PicrossPuzzle puzzle = new PicrossPuzzle(new PicrossPuzzle.CellValue[4, 4], new int[4][], new int[4][]);
            
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                    puzzle.Cells[x, y] = PicrossPuzzle.CellValue.Empty;

            puzzle.Cells[0, 0] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[1, 0] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[3, 0] = PicrossPuzzle.CellValue.Filled;

            puzzle.Cells[0, 1] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[1, 1] = PicrossPuzzle.CellValue.Crossed;
            puzzle.Cells[2, 1] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[3, 1] = PicrossPuzzle.CellValue.Filled;

            puzzle.Cells[1, 2] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[2, 2] = PicrossPuzzle.CellValue.Crossed;
            puzzle.Cells[3, 2] = PicrossPuzzle.CellValue.Crossed;

            puzzle.Cells[0, 3] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[1, 3] = PicrossPuzzle.CellValue.Filled;
            puzzle.Cells[2, 3] = PicrossPuzzle.CellValue.Filled;

            int[][] linesValues = puzzle.BitmapToLinesValue(puzzle.Cells);
            int[][] columnValues = puzzle.BitmapToColumnsValue(puzzle.Cells);

            Console.WriteLine(puzzle.ToString());
        }
    }
}
