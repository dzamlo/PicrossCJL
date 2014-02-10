using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PicrossCJL
{
    public class PicrossPuzzle
    {
        enum CellValue
        {
            Empty,
            Filled,
            Crossed
        }

        enum PuzzleState
        {
            Incorrect,
            Finished,
            Incomplete
        }

        public CellValue[,] Cells { get; set; }
        public int[][] LinesValues { get; private set; }
        public int[][] ColumnsValues { get; private set; }

        public Size Size
        {
            get
            {
                return new Size();
            }
        }

        public PicrossPuzzle(CellValue[,] cells, int[][] linesValues, int[][] columnsValues)
        {

        }

        public PicrossPuzzle(string filename)
        {

        }

        public PicrossPuzzle(PicrossPuzzle puzzle)
        {

        }

        public PicrossPuzzle(Bitmap img, Size? size = null)
        {

        }

        public void LoadFromFile(string filename)
        {
        }

        public void SaveToFile(string filename)
        {
        }

        public PuzzleState GetPuzzleState()
        {
            return PuzzleState.Incomplete;
        }
    }
}
