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
        private int[][] _columnsValues;

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
        public PicrossPuzzle(PicrossPuzzle puzzle) : this(puzzle.Cells, puzzle.LinesValues, puzzle.ColumnsValues)
        { } // No code

        /// <summary>
        /// PicrossPuzzle Constructor
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="linesValues"></param>
        /// <param name="columnsValues"></param>
        public PicrossPuzzle(CellValue[,] cells, int[][] linesValues, int[][] columnsValues)
        {
            this.Cells = cells;
            this.LinesValues = linesValues;
            this.ColumnsValues = columnsValues;
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
        public PicrossPuzzle(Bitmap img, Size? size = null)
        {
            this.LinesValues = this.BitmapToLinesValue((size.HasValue) ? new Bitmap(img, (Size)size) : img);
            this.ColumnsValues = this.BitmapToColumnsValue((size.HasValue) ? new Bitmap(img, (Size)size) : img);
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        unsafe private int[][] BitmapToLinesValue(Bitmap bmp)
        {
            Rectangle r = new Rectangle();
            
            return new int[0][];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        private int[][] BitmapToColumnsValue(Bitmap bmp)
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

        public PuzzleState GetPuzzleState()
        {
            Size s = Size;
            int w = s.Width;
            int h = s.Height;

            // check each line
            for (int y = 0; y < h; y++)
            {
                int[] value = LinesValues[y];
                int currentRunIdx = 0;
                int currentRunLength = 0;
                for (int x = 0; x < w; x++)
                {

                }

            }


            // check each column

            return PuzzleState.Incomplete;
        }
        #endregion
    }
}
