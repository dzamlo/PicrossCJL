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
        #region Fields & Properties
        private PicrossPuzzle _puzzle;

        public PicrossPuzzle Puzzle
        {
            get { return _puzzle; }
            set { _puzzle = value; }
        }
        #endregion

        #region Ctor
        public PicrossController()
        {
            this.Puzzle = PicrossPuzzle.Empty(4,4);
        }
        #endregion

        #region Methods
        public int GetNbMaxColumnsValues()
        {
            int maxValue = 0;
            for (int i = 0; i < this.Puzzle.ColumnsValues.GetLength(0); i++)
                maxValue = (maxValue < this.Puzzle.ColumnsValues[i].Length) ? this.Puzzle.ColumnsValues[i].Length : maxValue;
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
            return this.Puzzle.Cells[y, x];
        }

        public void LoadFromFile(string filename)
        {
            if (filename.EndsWith(".non") || filename.EndsWith(".txt"))
                this.Puzzle = PicrossPuzzle.LoadFromNonFile(filename);
            else
                this.Puzzle = PicrossPuzzle.LoadXmlFile(filename);

        }

        internal void Solve()
        {
            PicrossSolver solver = new PicrossSolver();
            solver.Solve(this.Puzzle);
        }
        #endregion
    }
}
