﻿/*
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

        public PuzzleState GetPuzzleState()
        {
            return PuzzleState.Incomplete;
        }
        #endregion
    }
}
