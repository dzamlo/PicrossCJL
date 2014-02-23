using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PicrossCJL
{
    public class PicrossSolver
    {
        public PicrossSolver()
        {

        }

        public bool Solve(PicrossPuzzle puzzle, int currentX = 0, int currentY  = 0)
        {
            //algo:
            //  backtracking, remplir ce dont est sur en fonction de l'état de la grille entre chauqe appel recursif

            //base case en fonction de puzzle.getgamestate
            PicrossPuzzle.PuzzleState puzzleState = puzzle.GetPuzzleState();
            if (puzzleState == PicrossPuzzle.PuzzleState.Finished)
                return true;
            else if (puzzleState == PicrossPuzzle.PuzzleState.Incorrect)
                return true;

            Size s = puzzle.Size;
            int w = s.Width;
            int h = s.Height;
            // remplir ce dont on n'est sur

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
