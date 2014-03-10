/*
 * Project : PicrossCJL
 * Authors : Damien L., Rejas C.,  Peiry J.
 * Date    : 2014-02-10
 * Desc.   : ---
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;

namespace PicrossCJL
{
    [Serializable]
    public class PicrossPuzzle
    {
        #region Enums
        public enum CellValue
        {
            Empty,
            Filled,
            Crossed
        }

        public enum PuzzleState
        {
            Incorrect,
            Finished,
            Incomplete
        }
        #endregion

        #region Fields & Properties
        private CellValue[,] _cells;
        private int[][] _linesValues;
        protected int[] _nbCrossedByLines;
        private int[][] _columnsValues;
        protected int[] _nbCrossedByColumns;

        public CellValue[,] Cells
        {
            get { return this._cells; }
            set { this._cells = value; }
        }

        public int[][] LinesValues
        {
            get { return this._linesValues; }
            private set { this._linesValues = value; }
        }


        public int[][] ColumnsValues
        {
            get { return this._columnsValues; }
            private set { this._columnsValues = value; }
        }

        public Size Size
        {
            get
            {
                // GetLength(0) -> width, GetLenght(1) -> height
                return new Size(this.Cells.GetLength(0), this.Cells.GetLength(1));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// PicrossPuzzle Constructor
        /// </summary>
        /// <param name="puzzle"></param>
        public PicrossPuzzle(PicrossPuzzle puzzle)
            : this(puzzle.Cells, puzzle.LinesValues, puzzle.ColumnsValues, puzzle._nbCrossedByLines, puzzle._nbCrossedByColumns)
        { } // No code

        /// <summary>
        /// PicrossPuzzle Constructor
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="linesValues"></param>
        /// <param name="columnsValues"></param>
        public PicrossPuzzle(CellValue[,] cells, int[][] linesValues, int[][] columnsValues, int[] nbCrossedByLines = null, int[] nbCrossedByColumns = null)
        {
            this.Cells = (CellValue[,])cells.Clone();
            this.LinesValues = linesValues;
            this.ColumnsValues = columnsValues;

            if (nbCrossedByLines != null)
                _nbCrossedByLines = nbCrossedByLines;
            else
            {
                _nbCrossedByLines = new int[LinesValues.Length];
                for (int i = 0; i < LinesValues.Length; i++)
                {
                    _nbCrossedByLines[i] = LinesValues[i].Sum();
                }
            }

            if (nbCrossedByColumns != null)
                _nbCrossedByColumns = nbCrossedByColumns;
            else
            {
                _nbCrossedByColumns = new int[ColumnsValues.Length];
                for (int i = 0; i < ColumnsValues.Length; i++)
                {
                    _nbCrossedByColumns[i] = ColumnsValues[i].Sum();
                }
            }

        }

        /// <summary>
        /// PicrossPuzzle Constructor
        /// </summary>
        /// <param name="filename"></param>
        public PicrossPuzzle(string filename)
        {

        }

        /// <summary>
        /// PicrossPuzzle Constructor
        /// </summary>
        /// <param name="img"></param>
        /// <param name="size"></param>
        public PicrossPuzzle(Bitmap img, Size? size = null, int threshold = 128)
        {
            this.LinesValues = this.BitmapToLinesValue((size.HasValue) ? new Bitmap(img, (Size)size) : img, threshold);
            this.ColumnsValues = this.BitmapToColumnsValue((size.HasValue) ? new Bitmap(img, (Size)size) : img, threshold);
        }
        #endregion

        #region Methods

        unsafe private CellValue[,] BitmapToCellsValueArray(Bitmap bmp, int threshold)
        {
            CellValue[,] cells = new CellValue[bmp.Width, bmp.Height];
            Rectangle r = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(r, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr px = bmpData.Scan0;

            int stride = bmpData.Stride;
            int offset = stride - (bmp.Width * 4);
            int average = 0;
            byte* ptrPx = (byte*)px;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    average = (ptrPx[0] + ptrPx[1] + ptrPx[2]) / 3;
                    ptrPx += 4;
                    cells[x, y] = (average >= threshold) ? CellValue.Filled : CellValue.Empty;
                }
                ptrPx += offset;
            }

            bmp.UnlockBits(bmpData);

            return cells;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        unsafe private int[][] BitmapToLinesValue(Bitmap bmp, int threshold)
        {
            CellValue[,] cells = this.BitmapToCellsValueArray(bmp, threshold);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {

                }
            }


            return new int[0][];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        private int[][] BitmapToColumnsValue(Bitmap bmp, int threshold)
        {
            return new int[0][];
        }

        /// <summary>
        /// Load from a xml serialization file
        /// </summary>
        /// <param name="filename">Xml filename</param>
        public void LoadFromFile(string filename)
        {
            // Reference : http://msdn.microsoft.com/en-us/library/4abbf6k0(v=vs.110).aspx
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            PicrossPuzzle puzzleTmp = (PicrossPuzzle)formatter.Deserialize(stream);
            this.Cells = puzzleTmp.Cells;
            this.LinesValues = puzzleTmp.LinesValues;
            this.ColumnsValues = puzzleTmp.ColumnsValues;
            stream.Close();
        }

        /// <summary>
        /// Save the current PicrossPuzzle to a xml serialization file.
        /// </summary>
        /// <param name="filename"></param>
        public void SaveToFile(string filename)
        {
            // Reference : http://msdn.microsoft.com/en-us/library/4abbf6k0(v=vs.110).aspx
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public PuzzleState CheckPuzzleLine(int lineNo)
        {
            Size s = Size;
            int w = s.Width;

            int[] values = LinesValues[lineNo];
            int nbValues = values.Length;

            List<int> runLengths = new List<int>();

            int currentRunLength = 0;
            int nbFilled = 0;
            int nbCrossed = 0;

            for (int x = 0; x < w; x++)
            {
                CellValue currentCellValue = Cells[lineNo, x];
                if (currentCellValue == CellValue.Filled)
                {
                    currentRunLength += 1;
                    nbFilled += 1;
                }
                else
                {
                    if (currentCellValue == CellValue.Crossed)
                    {
                        nbCrossed += 1;
                    }
                    if (currentRunLength > 0)
                    {
                        runLengths.Add(currentRunLength);
                        currentRunLength = 0;
                    }
                }
            }

            if (nbFilled > _nbCrossedByLines[lineNo] || nbCrossed > w - _nbCrossedByLines[lineNo])
                return PuzzleState.Incorrect;


            if (currentRunLength > 0)
            {
                runLengths.Add(currentRunLength);
                currentRunLength = 0;
            }

            if (runLengths.Count == values.Length)
            {
                for (int i = 0; i < runLengths.Count; i++)
                {
                    if (runLengths[i] != values[i])
                    {
                        // PuzzleState.Incomplete || PuzzleState.Incorrect
                        // TODO: determine when PuzzleState is Incorrect
                        return PuzzleState.Incomplete;
                    }
                }
                return PuzzleState.Finished;
            }
            else
            {
                // PuzzleState.Incomplete || PuzzleState.Incorrect
                // TODO: determine when PuzzleState is Incorrect
                return PuzzleState.Incomplete;
            }
        }

        public PuzzleState CheckPuzzleColumn(int columnNo)
        {
            Size s = Size;
            int h = s.Height;

            int[] values = ColumnsValues[columnNo];
            int nbValues = values.Length;

            List<int> runLengths = new List<int>();

            int currentRunLength = 0;

            int nbFilled = 0;
            int nbCrossed = 0;

            for (int y = 0; y < h; y++)
            {
                CellValue currentCellValue = Cells[y, columnNo];
                if (currentCellValue == CellValue.Filled)
                {
                    currentRunLength += 1;
                    nbFilled += 1;
                }
                else
                {
                    if (currentCellValue == CellValue.Crossed)
                    {
                        nbCrossed += 1;
                    }
                    if (currentRunLength > 0)
                    {
                        runLengths.Add(currentRunLength);
                        currentRunLength = 0;
                    }
                }
            }

            if (nbFilled > _nbCrossedByColumns[columnNo] || nbCrossed > h - _nbCrossedByColumns[columnNo])
                return PuzzleState.Incorrect;

            if (currentRunLength > 0)
            {
                runLengths.Add(currentRunLength);
                currentRunLength = 0;
            }

            if (runLengths.Count == values.Length)
            {
                for (int i = 0; i < runLengths.Count; i++)
                {
                    if (runLengths[i] != values[i])
                    {
                        // PuzzleState.Incomplete || PuzzleState.Incorrect
                        // TODO: determine when PuzzleState is Incorrect
                        return PuzzleState.Incomplete;
                    }
                }
                return PuzzleState.Finished;
            }
            else
            {
                // PuzzleState.Incomplete || PuzzleState.Incorrect
                // TODO: determine when PuzzleState is Incorrect
                return PuzzleState.Incomplete;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PuzzleState GetPuzzleState()
        {
            Size s = Size;
            int w = s.Width;
            int h = s.Height;

            bool incorrect = false;
            bool finished = true;

            // first check each line
            for (int y = 0; y < h && !incorrect && finished; y++)
            {
                switch (CheckPuzzleLine(y))
                {
                    case PuzzleState.Incorrect:
                        incorrect = true;
                        finished = false;
                        break;
                    case PuzzleState.Finished:
                        break;
                    case PuzzleState.Incomplete:
                        finished = false;
                        break;
                    default:
                        break;
                }

            }

            // then check each column
            for (int x = 0; x < w && !incorrect && finished; x++)
            {
                switch (CheckPuzzleColumn(x))
                {
                    case PuzzleState.Incorrect:
                        incorrect = true;
                        finished = false;
                        break;
                    case PuzzleState.Finished:
                        break;
                    case PuzzleState.Incomplete:
                        finished = false;
                        break;
                    default:
                        break;
                }

            }

            return incorrect ? PuzzleState.Incorrect : finished ? PuzzleState.Finished : PuzzleState.Incomplete;
        }
        #endregion
    }
}
