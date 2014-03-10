using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicrossCJL;
using System.Drawing;

namespace PicrossCJLGUI
{
    class PicrossController
    {
        private PicrossPuzzle _puzzle;

        public PicrossPuzzle Puzzle
        {
            get { return _puzzle; }
            set { _puzzle = value; }
        }

        public PicrossController()
        {
            this.Puzzle = new PicrossPuzzle(new PicrossPuzzle.CellValue[4, 4], new int[4][], new int[4][]);
        }

        public int GetNbMaxColumnsValues()
        {
            int maxValue = 0;
            for (int i = 0; i < this.Puzzle.ColumnsValues.GetLength(0); i++)
                maxValue = (maxValue < this.Puzzle.ColumnsValues[i].Length) ?this.Puzzle.ColumnsValues[i].Length : maxValue;
            return maxValue;
        }

        public int GetNbMaxLinesValues()
        {
            int maxValue = 0;
            for (int i = 0; i < this.Puzzle.LinesValues.GetLength(0); i++)
                maxValue = (maxValue < this.Puzzle.LinesValues[i].Length) ? this.Puzzle.LinesValues[i].Length : maxValue;
            return maxValue;
        }

        public Size GetCellSize()
        {
            return new Size(this.Puzzle.Cells.GetLength(0), this.Puzzle.Cells.GetLength(1));
        }

        public PicrossPuzzle.CellValue GetCellState(int x, int y)
        {
            return this.Puzzle.Cells[x, y];
        }
    }
}
