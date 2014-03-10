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
            
            PicrossPuzzle.CellValue[] flushedLeft = new PicrossPuzzle.CellValue[puzzle.Size.Width];
            PicrossPuzzle.CellValue[] flushedRight = new PicrossPuzzle.CellValue[puzzle.Size.Width];
            int idx = 0;
            foreach (var runLength in puzzle.LinesValues[lineNo])
            {
                for (int i = 0; i < runLength; i++)
                {
                    flushedLeft[idx] = PicrossPuzzle.CellValue.Filled;
                    idx++;
                }
                idx++;
            }
            idx -= 1;
            Array.Copy(flushedLeft, 0, flushedRight, puzzle.Size.Width-idx, idx);

            for (int i = 0; i < puzzle.Size.Width && flushedLeft[i] == PicrossPuzzle.CellValue.Filled; i++)
            {
                if (flushedRight[i] == PicrossPuzzle.CellValue.Filled)
                {
                    puzzle.Cells[lineNo, i] = PicrossPuzzle.CellValue.Filled;
                }
            }

            for (int i = puzzle.Size.Width - 1; i >= 0 && flushedRight[i] == PicrossPuzzle.CellValue.Filled; i--)
            {
                if (flushedLeft[i] == PicrossPuzzle.CellValue.Filled)
                {
                    puzzle.Cells[lineNo, i] = PicrossPuzzle.CellValue.Filled;
                }
            }



            
            return false;
        }

        public bool SolveColumn(PicrossPuzzle puzzle, int columnNo)
        {
            
            PicrossPuzzle.CellValue[] flushedLeft = new PicrossPuzzle.CellValue[puzzle.Size.Height];
            PicrossPuzzle.CellValue[] flushedRight = new PicrossPuzzle.CellValue[puzzle.Size.Height];
            int idx = 0;
            foreach (var runLength in puzzle.ColumnsValues[columnNo])
            {
                for (int i = 0; i < runLength; i++)
                {
                    flushedLeft[idx] = PicrossPuzzle.CellValue.Filled;
                    idx++;
                }
                idx++;
            }
            idx -= 1;
            Array.Copy(flushedLeft, 0, flushedRight, puzzle.Size.Height - idx, idx);

            for (int i = 0; i < puzzle.Size.Height && flushedLeft[i] == PicrossPuzzle.CellValue.Filled; i++)
            {
                if (flushedRight[i] == PicrossPuzzle.CellValue.Filled)
                {
                    puzzle.Cells[i, columnNo] = PicrossPuzzle.CellValue.Filled;
                }
            }

            for (int i = puzzle.Size.Height - 1; i >= 0 && flushedRight[i] == PicrossPuzzle.CellValue.Filled; i--)
            {
                if (flushedLeft[i] == PicrossPuzzle.CellValue.Filled)
                {
                    puzzle.Cells[i, columnNo] = PicrossPuzzle.CellValue.Filled;
                }
            }
            
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


        private bool Backtrack(PicrossPuzzle puzzle, int currentX = 0, int currentY = 0)
        {

            //TODO: don't recheck past line
            //algo:
            // backtracking, remplir ce dont est sur en fonction de l'état de la grille entre chaque appel recursif

            //base case en fonction de puzzle.getgamestate

            PicrossPuzzle.PuzzleState puzzleState = puzzle.GetPuzzleState(currentY);
            if (puzzleState == PicrossPuzzle.PuzzleState.Finished)
                return true;
            else if (puzzleState == PicrossPuzzle.PuzzleState.Incorrect)
                return false;

            Size s = puzzle.Size;
            int w = s.Width;
            int h = s.Height;

            // pour chaque cellule avec des doutes:
            // essayer remplir la cellule, essayer de résoudre (appel recursif à solve)
            // si resolutuion ok -> fini
            // sinon -> cellule crossed, essayer de remplir la prochiane, etc.
            int x = currentX;
            for (int y = currentY; y < h; y++)
            {
                for (; x < w; x++)
                {
                    if (puzzle.Cells[y, x] == PicrossPuzzle.CellValue.Empty)
                    {
                        puzzle.Cells[y, x] = PicrossPuzzle.CellValue.Filled;
                        PicrossPuzzle tmpPuzzle = new PicrossPuzzle(puzzle);
                        if (Backtrack(tmpPuzzle, x, y))
                        {
                            puzzle.Cells = tmpPuzzle.Cells;
                            return true;
                        }
                        puzzle.Cells[y, x] = PicrossPuzzle.CellValue.Crossed;
                    }
                }
                x = 0;
            }

            return false;
        }

         public bool Solve(PicrossPuzzle puzzle)
        {
            //algo:
            // backtracking, remplir ce dont est sur en fonction de l'état de la grille entre chaque appel recursif

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
              Parallel.For(0, h, i => { progressLines[i] = SolveLine(puzzle, i); });
              Parallel.For(0, w, i => { progressColumn[i] = SolveColumn(puzzle, i); });
            } while (Any(progressLines) || Any(progressColumn));

            return Backtrack(puzzle);
        }
    }
}
