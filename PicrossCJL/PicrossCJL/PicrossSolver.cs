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

        bool Solve(PicrossPuzzle puzzle)
        {
            //algo:
            //  backtracking, remplir ce dont est sur en fonction de l'état de la grille entre chauqe appel recursif

            //base case en fonction de puzzle.getgamestate


            Size s = puzzle.Size;
            int w = s.Width;
            int h = s.Height;
            // remplir ce dont on n'est sur

            // pour chaque cellule avec des doutes:
            // essayer remplir la cellule, essayer de résoudre (appel recursif à solve)
            // si resolutuion ok -> fini
            // sinon  -> cellule crossed, essayer de remplir la prochiane, etc.
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (puzzle.Cells[x, y] == PicrossPuzzle.CellValue.Empty)
                    {
                        puzzle.Cells[x, y] = PicrossPuzzle.CellValue.Filled;
                        PicrossPuzzle tmpPuzzle = new PicrossPuzzle(puzzle);
                        if (Solve(tmpPuzzle))
                        {
                            puzzle.Cells = tmpPuzzle.Cells;
                            return true;
                        }
                        puzzle.Cells[x, y] = PicrossPuzzle.CellValue.Crossed;
                    }
                }
            }

            return false;
        }
    }
}
