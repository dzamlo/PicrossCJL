using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace PicrossCJL
{
    public class PicrossSolver
    {
        public PicrossSolver()
        {

        }

        /// <summary>
        /// Fill all it can fill deterministicaly
        /// </summary>
        /// <param name="puzzle"></param>
        /// <param name="lineNo"></param>
        /// <returns>True if it make any progress</returns>
        public bool SolveLine(PicrossPuzzle puzzle, int lineNo)
        {
            //Console.WriteLine(lineNo);
            return false;
        }

        public bool SolveColumn(PicrossPuzzle puzzle, int columnNo)
        {
            return false;
        }

        public bool Any(IEnumerable<bool> bools)
        {
            foreach (var b in bools)
            {
                if(b) return true;
            }
            return false;
        }

        public bool Solve(PicrossPuzzle puzzle, int currentX = 0, int currentY = 0)
        {
            //algo:
            //  backtracking, remplir ce dont est sur en fonction de l'état de la grille entre chaque appel recursif

            //base case en fonction de puzzle.getgamestate

            PicrossPuzzle.PuzzleState puzzleState = puzzle.GetPuzzleState();
            if (puzzleState == PicrossPuzzle.PuzzleState.Finished)
                return true;
            else if (puzzleState == PicrossPuzzle.PuzzleState.Incorrect)
                return false;

            Size s = puzzle.Size;
            int w = s.Width;
            int h = s.Height;

            // remplir ce dont on n'est sur

            bool[] progressLines = new bool[h];
            bool[] progressColumn = new bool[w];


            do
            {
              Parallel.For(currentX, h, i => { progressLines[i] = SolveLine(puzzle, i); });
              Parallel.For(0, w, i => { progressColumn[i] = SolveColumn(puzzle, i); });
            } while (Any(progressLines) || Any(progressColumn));


            // pour chaque cellule avec des doutes:
            // essayer remplir la cellule, essayer de résoudre (appel recursif à solve)
            // si resolutuion ok -> fini
            // sinon  -> cellule crossed, essayer de remplir la prochiane, etc.
            int y = currentY;
            for (int x = currentX; x < w; x++)
            {
                for (; y < h; y++)
                {
                    if (puzzle.Cells[x, y] == PicrossPuzzle.CellValue.Empty)
                    {
                        puzzle.Cells[x, y] = PicrossPuzzle.CellValue.Filled;
                        PicrossPuzzle tmpPuzzle = new PicrossPuzzle(puzzle);
                        if (Solve(tmpPuzzle, x, y))
                        {
                            puzzle.Cells = tmpPuzzle.Cells;
                            return true;
                        }
                        puzzle.Cells[x, y] = PicrossPuzzle.CellValue.Crossed;
                    }
                }
                y = 0;
            }

            return false;
        }
    }
}
