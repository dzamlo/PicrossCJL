using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

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
        public bool QuickSolveLine(PicrossPuzzle puzzle, int lineNo)
        {

            if (puzzle.LinesValues[lineNo].Length > 0)
            {
                PicrossPuzzle.CellValue[] flushedLeft = new PicrossPuzzle.CellValue[puzzle.Width];
                PicrossPuzzle.CellValue[] flushedRight = new PicrossPuzzle.CellValue[puzzle.Width];
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
                Array.Copy(flushedLeft, 0, flushedRight, puzzle.Width - idx, idx);

                for (int i = 0; i < puzzle.Width && flushedLeft[i] == PicrossPuzzle.CellValue.Filled; i++)
                {
                    if (flushedRight[i] == PicrossPuzzle.CellValue.Filled)
                    {
                        puzzle.Cells[lineNo, i] = PicrossPuzzle.CellValue.Filled;
                    }
                }

                for (int i = puzzle.Width - 1; i >= 0 && flushedRight[i] == PicrossPuzzle.CellValue.Filled; i--)
                {
                    if (flushedLeft[i] == PicrossPuzzle.CellValue.Filled)
                    {
                        puzzle.Cells[lineNo, i] = PicrossPuzzle.CellValue.Filled;
                    }
                }
            }
            else
            {
                for (int i = 0; i < puzzle.Width; i++)
                {
                    puzzle.Cells[lineNo, i] = PicrossPuzzle.CellValue.Crossed;
                }
            }



            return false;
        }

        public bool QuickSolveColumn(PicrossPuzzle puzzle, int columnNo)
        {
            if (puzzle.ColumnsValues[columnNo].Length > 0)
            {
                PicrossPuzzle.CellValue[] flushedUp = new PicrossPuzzle.CellValue[puzzle.Height];
                PicrossPuzzle.CellValue[] flushedDown = new PicrossPuzzle.CellValue[puzzle.Height];
                int idx = 0;
                foreach (var runLength in puzzle.ColumnsValues[columnNo])
                {
                    for (int i = 0; i < runLength; i++)
                    {
                        flushedUp[idx] = PicrossPuzzle.CellValue.Filled;
                        idx++;
                    }
                    idx++;
                }
                idx -= 1;
                Array.Copy(flushedUp, 0, flushedDown, puzzle.Height - idx, idx);

                for (int i = 0; i < puzzle.Height && flushedUp[i] == PicrossPuzzle.CellValue.Filled; i++)
                {
                    if (flushedDown[i] == PicrossPuzzle.CellValue.Filled)
                    {
                        puzzle.Cells[i, columnNo] = PicrossPuzzle.CellValue.Filled;
                    }
                }

                for (int i = puzzle.Height - 1; i >= 0 && flushedDown[i] == PicrossPuzzle.CellValue.Filled; i--)
                {
                    if (flushedUp[i] == PicrossPuzzle.CellValue.Filled)
                    {
                        puzzle.Cells[i, columnNo] = PicrossPuzzle.CellValue.Filled;
                    }
                }
            }
            else
            {
                for (int i = 0; i < puzzle.Height; i++)
                {
                    puzzle.Cells[i, columnNo] = PicrossPuzzle.CellValue.Crossed;
                }
            }
            return false;
        }



        public bool Any(IEnumerable<bool> bools)
        {
            foreach (var b in bools)
            {
                if (b) return true;
            }
            return false;
        }

        private IEnumerable<IEnumerable<int>> GenPossiblesGapLenghts(int nbGap, int nbCrossed, bool first = true)
        {
            if (nbGap == 0)
            {
                yield return new int[0];
            }
            else
            {
                int maxGapLength = nbCrossed - nbGap + 1;
                for (int i = first ? 0 : 1; i <= maxGapLength; i++)
                {
                    int[] e = new int[] { i };
                    foreach (var item in GenPossiblesGapLenghts(nbGap - 1, nbCrossed - i, false))
                    {
                        //yield return [i] + item
                        yield return e.Concat(item);
                    }
                }
            }
        }


        private List<PicrossPuzzle.CellValue[]> GenPossiblesLines(int width, int[] lineValues, int nbCrossed)
        {
            if (lineValues.Length == 0)
            {
                PicrossPuzzle.CellValue[] line = new PicrossPuzzle.CellValue[width];
                for (int i = 0; i < width; i++)
                {
                    line[i] = PicrossPuzzle.CellValue.Crossed;
                }
                List<PicrossPuzzle.CellValue[]> result = new List<PicrossPuzzle.CellValue[]>(capacity: 1);
                result.Add(line);
                return result;
            }
            else
            {
                List<PicrossPuzzle.CellValue[]> result = new List<PicrossPuzzle.CellValue[]>();
                foreach (var item in GenPossiblesGapLenghts(lineValues.Length, nbCrossed))
                {
                    PicrossPuzzle.CellValue[] line = new PicrossPuzzle.CellValue[width];
                    int idx = 0;
                    int lineValuesIdx = 0;
                    foreach (var gapLength in item)
                    {
                        int blockLength = lineValues[lineValuesIdx];
                        for (int i = 0; i < gapLength; i++)
                        {
                            line[idx] = PicrossPuzzle.CellValue.Crossed;
                            idx++;
                        }
                        for (int i = 0; i < blockLength; i++)
                        {
                            line[idx] = PicrossPuzzle.CellValue.Filled;
                            idx++;
                        }
                        lineValuesIdx++;

                    }
                    for (; idx < width; idx++)
                    {
                        line[idx] = PicrossPuzzle.CellValue.Crossed;
                    }
                    result.Add(line);
                }
                return result;
                // GenPossiblesGapLenghts(lineValues.Length, nbCrossed)
            }
        }


        private void SolveSteps(PicrossPuzzle puzzle, int w, int h, List<PicrossPuzzle.CellValue[]>[] possibleLinesValues, List<PicrossPuzzle.CellValue[]>[] possiblesColumnsValues)
        {
            //TODO: work only on affected row/column, by using conccurentBag
            bool[] progressLines = new bool[h];
            bool[] progressColumn = new bool[w];
            do
            {
                Parallel.For(0, h, i => { progressLines[i] = SolveLine(puzzle, w, i, possibleLinesValues[i]); });
                Parallel.For(0, w, i => { progressColumn[i] = SolveColumn(puzzle, h, i, possiblesColumnsValues[i]); });
            } while (Any(progressLines) || Any(progressColumn));
        }


        private bool Backtrack(PicrossPuzzle puzzle, int w, int h, List<PicrossPuzzle.CellValue[]>[] possibleLinesValues, List<PicrossPuzzle.CellValue[]>[] possiblesColumnsValues, int currentX = 0, int currentY = 0)
        {

            //TODO: don't recheck past line
            //TODO: TODO:eliminate call to GetPuzzleState by only checking affected row and column
            //algo:
            // backtracking, remplir ce dont est sur en fonction de l'état de la grille entre chaque appel recursif

            //base case en fonction de puzzle.GetPuzzleState

            PicrossPuzzle.PuzzleState puzzleState = puzzle.GetPuzzleState(currentX, currentY);
            //PicrossPuzzle.PuzzleState puzzleState = puzzle.GetPuzzleState();
            if (puzzleState == PicrossPuzzle.PuzzleState.Finished)
                return true;
            else if (puzzleState == PicrossPuzzle.PuzzleState.Incorrect)
                return false;


            SolveSteps(puzzle, w, h, possibleLinesValues, possiblesColumnsValues);

            // pour chaque cellule avec des doutes:
            // essayer remplir la cellule, essayer de résoudre (appel recursif à solve)
            // si resolutuion ok -> fini
            // sinon -> cellule crossed, essayer de remplir la prochaine, etc.
            int x = currentX;
            for (int y = currentY; y < h; y++)
            {
                for (; x < w; x++)
                {
                    if (puzzle.Cells[y, x] == PicrossPuzzle.CellValue.Empty)
                    {
                        puzzle.Cells[y, x] = PicrossPuzzle.CellValue.Filled;

                        PicrossPuzzle.PuzzleState lineState = puzzle.CheckPuzzleLine(y);
                        if (lineState == PicrossPuzzle.PuzzleState.Incorrect)
                            puzzle.Cells[y, x] = PicrossPuzzle.CellValue.Crossed;
                        else
                        {
                            PicrossPuzzle.PuzzleState columnState = puzzle.CheckPuzzleColumn(x);
                            if (columnState == PicrossPuzzle.PuzzleState.Incorrect)
                                puzzle.Cells[y, x] = PicrossPuzzle.CellValue.Crossed;
                            else
                            {
                                // lineState and columnState is either incomplete or finished

                                //if(y == height-1 && x = == height -1 && lineState == PicrossPuzzle.PuzzleState.Finished && columnState == PicrossPuzzle.PuzzleState.Finished)

                                PicrossPuzzle tmpPuzzle = new PicrossPuzzle(puzzle);
                                if (Backtrack(tmpPuzzle, w, h, PossibilitiesDeepCopy(possibleLinesValues), PossibilitiesDeepCopy(possiblesColumnsValues), x, y))
                                {
                                    puzzle.Cells = tmpPuzzle.Cells;
                                    return true;
                                }
                                puzzle.Cells[y, x] = PicrossPuzzle.CellValue.Crossed;
                            }
                        }

                    }
                }
                x = 0;
            }
            
            return puzzle.GetPuzzleState() == PicrossPuzzle.PuzzleState.Finished;
        }

        private bool SolveLine(PicrossPuzzle puzzle, int width, int lineNo, List<PicrossPuzzle.CellValue[]> possibilities/*, ConcurrentBag<int> affectedColumns*/)
        {
            bool progress = false;
            for (int i = 0; i < width; i++)
            {
                PicrossPuzzle.CellValue value = puzzle.Cells[lineNo, i];
                if (value != PicrossPuzzle.CellValue.Empty)
                {
                    //if possiblity is different in a cell we are sure of, this possiblity is wrong, we remove it
                    for (int j = 0; j < possibilities.Count; j++)
                    {
                        if (possibilities[j][i] != value)
                        {
                            possibilities.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }

            for (int i = 0; i < width; i++)
            {
                PicrossPuzzle.CellValue value = puzzle.Cells[lineNo, i];
                if (value == PicrossPuzzle.CellValue.Empty)
                {
                    //if a cell value is the same in every possibiliy it is the corrct one
                    foreach (var possiblity in possibilities)
                    {
                        PicrossPuzzle.CellValue possiblityValue = possiblity[i];
                        if (value == PicrossPuzzle.CellValue.Empty)
                            value = possiblityValue;
                        else
                        {
                            if (possiblityValue != value)
                            {
                                value = PicrossPuzzle.CellValue.Empty;
                                break;
                            }
                        }
                    }
                    if (value != PicrossPuzzle.CellValue.Empty)
                    {
                        puzzle.Cells[lineNo, i] = value;
                        //affectedColumns.Add(i);
                        progress = true;
                    }
                }
            }
            return progress;
        }

        private bool SolveColumn(PicrossPuzzle puzzle, int height, int columnNo, List<PicrossPuzzle.CellValue[]> possibilities/*, ConcurrentBag<int> affectedLines*/)
        {
            bool progress = false;
            for (int i = 0; i < height; i++)
            {
                PicrossPuzzle.CellValue value = puzzle.Cells[i, columnNo];
                if (value != PicrossPuzzle.CellValue.Empty)
                {
                    //if possiblity is different in a cell we are sure of, this possiblity is wrong, we remove it
                    for (int j = 0; j < possibilities.Count; j++)
                    {
                        if (possibilities[j][i] != value)
                        {
                            possibilities.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }

            for (int i = 0; i < height; i++)
            {
                PicrossPuzzle.CellValue value = puzzle.Cells[i, columnNo];
                if (value == PicrossPuzzle.CellValue.Empty)
                {
                    //if a cell value is the same in every possibiliy it is the corrct one
                    foreach (var possiblity in possibilities)
                    {
                        PicrossPuzzle.CellValue possiblityValue = possiblity[i];
                        if (value == PicrossPuzzle.CellValue.Empty)
                            value = possiblityValue;
                        else
                        {
                            if (possiblityValue != value)
                            {
                                value = PicrossPuzzle.CellValue.Empty;
                                break;
                            }
                        }
                    }
                    if (value != PicrossPuzzle.CellValue.Empty)
                    {
                        puzzle.Cells[i, columnNo] = value;
                        //affectedLines.Add(i);
                        progress = true;
                    }
                }
            }
            return progress;
        }

        private List<PicrossPuzzle.CellValue[]>[] PossibilitiesDeepCopy(List<PicrossPuzzle.CellValue[]>[] possiblities)
        {
            List<PicrossPuzzle.CellValue[]>[] copy = new List<PicrossPuzzle.CellValue[]>[possiblities.Length];
            for (int i = 0; i < possiblities.Length; i++)
            {
                copy[i] = new List<PicrossPuzzle.CellValue[]>(possiblities[i].Select(line => (PicrossPuzzle.CellValue[])line.Clone()));
            }
            return copy;
        }

        /// <summary>
        /// Solve a puzzle.
        /// </summary>
        /// <param name="puzzle">The puzzle to solve</param>
        /// <returns></returns>
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

            int w = puzzle.Width;
            int h = puzzle.Height;

            Parallel.For(0, h, i => QuickSolveLine(puzzle, i));
            Parallel.For(0, w, i => QuickSolveColumn(puzzle, i));

            Stopwatch sw  = new Stopwatch();
            sw.Start();
            List<PicrossPuzzle.CellValue[]>[] possibleLinesValues = new List<PicrossPuzzle.CellValue[]>[h];
            List<PicrossPuzzle.CellValue[]>[] possiblesColumnsValues = new List<PicrossPuzzle.CellValue[]>[w];
            Parallel.For(0, h, i => possibleLinesValues[i] = GenPossiblesLines(w, puzzle.LinesValues[i], w - puzzle.NbFilledByLines[i]));
            Parallel.For(0, h, i => possiblesColumnsValues[i] = GenPossiblesLines(h, puzzle.ColumnsValues[i], h - puzzle.NbFilledByColumns[i]));
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            //SolveSteps(puzzle, w, h, possibleLinesValues, possiblesColumnsValues);

            if (Backtrack(puzzle, w, h, PossibilitiesDeepCopy(possibleLinesValues), PossibilitiesDeepCopy(possiblesColumnsValues)))
            {

                Parallel.For(0, h, y =>
                {
                    for (int x = 0; x < w; x++)
                    {
                        if (puzzle.Cells[y, x] == PicrossPuzzle.CellValue.Empty)
                            puzzle.Cells[y, x] = PicrossPuzzle.CellValue.Crossed;
                    }
                });
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
