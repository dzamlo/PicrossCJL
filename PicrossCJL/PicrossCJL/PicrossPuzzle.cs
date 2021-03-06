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
using System.Xml;

namespace PicrossCJL
{
    [Serializable]
    public class PicrossPuzzle
    {
        #region Enums
        public enum CellValue : byte
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
        private int[] _nbFilledByLines;
        private int[][] _columnsValues;
        protected int[] _nbFilledByColumns;
        private int _state;

        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        public CellValue[,] Cells
        {
            get { return this._cells; }
            set { this._cells = value; }
        }

        public int[][] LinesValues
        {
            get { return this._linesValues; }
            set { this._linesValues = value; }
        }


        public int[][] ColumnsValues
        {
            get { return this._columnsValues; }
            set { this._columnsValues = value; }
        }

        public int Width
        {
            get
            {
                return ColumnsValues.GetLength(0);
            }
        }

        public int Height
        {
            get
            {
                return LinesValues.GetLength(0);
            }
        }

        public Size Size
        {
            get
            {
                // GetLength(0) -> width, GetLenght(1) -> height
                return new Size(Width, Height);
            }
        }

        public int[] NbFilledByLines
        {
            get { return _nbFilledByLines; }
        }

        public int[] NbFilledByColumns
        {
            get { return _nbFilledByColumns; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// PicrossPuzzle Constructor
        /// </summary>
        /// <param name="puzzle"></param>
        public PicrossPuzzle(PicrossPuzzle puzzle)
            : this(puzzle.Cells, puzzle.LinesValues, puzzle.ColumnsValues, puzzle._nbFilledByLines, puzzle._nbFilledByColumns)
        { } // No code

        /// <summary>
        /// PicrossPuzzle Constructor
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="linesValues"></param>
        /// <param name="columnsValues"></param>
        public PicrossPuzzle(CellValue[,] cells, int[][] linesValues, int[][] columnsValues, int[] nbCrossedByLines = null, int[] nbCrossedByColumns = null)
        {
            if(cells == null)
                this.Cells = new CellValue[linesValues.GetLength(0), columnsValues.GetLength(0)];
            else
               this.Cells = (CellValue[,])cells.Clone();
            this.LinesValues = linesValues;
            this.ColumnsValues = columnsValues;
            

            if (nbCrossedByLines != null)
                _nbFilledByLines = nbCrossedByLines;
            else
            {
                _nbFilledByLines = new int[LinesValues.Length];
                for (int i = 0; i < LinesValues.Length; i++)
                {
                    _nbFilledByLines[i] = LinesValues[i].Sum();
                }
            }

            if (nbCrossedByColumns != null)
                _nbFilledByColumns = nbCrossedByColumns;
            else
            {
                _nbFilledByColumns = new int[ColumnsValues.Length];
                for (int i = 0; i < ColumnsValues.Length; i++)
                {
                    _nbFilledByColumns[i] = ColumnsValues[i].Sum();
                }
            }
        }

        /// <summary>
        /// PicrossPuzzle Constructor
        /// </summary>
        /// <param name="img"></param>
        /// <param name="size"></param>
        public PicrossPuzzle(Bitmap img, Size? size = null, int threshold = 128, int[] nbCrossedByLines = null, int[] nbCrossedByColumns = null)
        {
            this.Cells = new CellValue[img.Width, img.Height];
            this.LinesValues = BitmapToLinesValue((size.HasValue) ? new Bitmap(img, (Size)size) : img, threshold);
            this.ColumnsValues = BitmapToColumnsValue((size.HasValue) ? new Bitmap(img, (Size)size) : img, threshold);

            if (nbCrossedByLines != null)
                _nbFilledByLines = nbCrossedByLines;
            else
            {
                _nbFilledByLines = new int[LinesValues.Length];
                for (int i = 0; i < LinesValues.Length; i++)
                {
                    _nbFilledByLines[i] = LinesValues[i].Sum();
                }
            }

            if (nbCrossedByColumns != null)
                _nbFilledByColumns = nbCrossedByColumns;
            else
            {
                _nbFilledByColumns = new int[ColumnsValues.Length];
                for (int i = 0; i < ColumnsValues.Length; i++)
                {
                    _nbFilledByColumns[i] = ColumnsValues[i].Sum();
                }
            }
        }

        public static PicrossPuzzle FromBitmap(Bitmap img, Size? size = null, int threshold = 128)
        {
            img = (size.HasValue) ? new Bitmap(img, (Size)size) : img;
            int [][] LinesValues = BitmapToLinesValue((size.HasValue) ? new Bitmap(img, (Size)size) : img, threshold);
            int [][] ColumnsValues = BitmapToColumnsValue((size.HasValue) ? new Bitmap(img, (Size)size) : img, threshold);

            return new PicrossPuzzle(null, LinesValues, ColumnsValues);

        }

        /// <summary>
        /// Create an empty puzzle with a specified size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static PicrossPuzzle Empty(int width, int height)
        {
            int[][] linesValues = new int[height][];
            int[][] columnsValues = new int[height][];

            for (int x = 0; x < width; x++)
            {
                linesValues[x] = new int[0];
            }

            for (int y = 0; y < height; y++)
            {
                columnsValues[y] = new int[0];
            }

            return new PicrossPuzzle(null, linesValues, columnsValues);
        }

        #endregion

        #region Methods

        /// <summary>
        /// To String method
        /// </summary>
        /// <returns>String value</returns>
        public override string ToString()
        {
            string str = string.Empty;

            // Draw the cells value
            for (int y = 0; y < this.Size.Height; y++)
            {
                for (int x = 0; x < this.Size.Width; x++)
                {
                    switch (this.Cells[y, x])
                    {
                        case CellValue.Filled:  str += "#";
                            break;
                        case CellValue.Empty:   str += "?";
                            break;
                        case CellValue.Crossed: str += ".";
                            break;
                    }
                }
                str += "\n";
            }

            // Show lines values
            str += "\n";
            for (int i = 0; i < this.Size.Height; i++)
            {
                str += string.Format("Line {0}: ", i);
                foreach (int v in this.LinesValues[i])
                    str += string.Format("{0} ", v);
                str += "\n";
            }

            // Show columns values
            str += "\n";
            for (int i = 0; i < this.Size.Width; i++)
            {
                str += string.Format("Column {0}: ", i);
                foreach (int v in this.ColumnsValues[i])
                    str += string.Format("{0} ", v);
                str += "\n";
            }

            // Show puzzle state

            str += string.Format("State: {0} ", GetPuzzleState()); ;
            str += "\n";

            return str;
        }

        /// <summary>
        /// Method to convert a bitmap image into an array of CellValue
        /// </summary>
        /// <param name="bmp">Bitmap image</param>
        /// <param name="threshold">Threashold value (integer) to determine if the value is Filled or not</param>
        /// <returns>2D CellValue array</returns>
        private static CellValue[,] BitmapToCellsValueArray(Bitmap bmp, int threshold)
        {
            int average = 0;
            Color c;
            CellValue[,] cells = new CellValue[bmp.Height, bmp.Width];
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    c = bmp.GetPixel(x, y);
                    average = (c.R + c.G + c.B) / 3;
                    cells[y, x] = (average <= threshold) ? CellValue.Filled : CellValue.Empty;
                }
            }
            return cells;
        }



        /// <summary>
        /// Method to get values of each lines on the bitmap
        /// </summary>
        /// <param name="bmp">Bitmap image</param>
        /// <param name="threshold">Threashold value (integer) to determine if the value is Filled or not</param>
        /// <returns>2D integer array</returns>
        public static  int[][] BitmapToLinesValue(Bitmap bmp, int threshold)
        {
            CellValue[,] cells = BitmapToCellsValueArray(bmp, threshold);
            int currentValue = 0;
            int[][] linesValues = new int[cells.GetLength(0)][];
            List<int> values = new List<int>();

            for (int y = 0; y < cells.GetLength(0); y++)
            {
                for (int x = 0; x < cells.GetLength(1); x++)
                {
                    if (cells[y,x] != CellValue.Filled)
                    {
                        if (currentValue > 0) values.Add(currentValue);
                        currentValue = 0;
                    }
                    else
                        currentValue += (cells[y, x] == CellValue.Filled) ? 1 : 0;

                }
                if (currentValue > 0) values.Add(currentValue);
                currentValue = 0;
                linesValues[y] = values.ToArray();
                values.Clear();
            }

            return linesValues;
        }

        /// <summary>
        /// Method to get values of each lines on the bitmap
        /// </summary>
        /// <param name="cells">2D CellValue array</param>
        /// <returns>2D integer array</returns>
        public int[][] BitmapToLinesValue(CellValue[,] cells)
        {
            int currentValue = 0;
            int[][] linesValues = new int[cells.GetLength(0)][];
            List<int> values = new List<int>();
            
            for (int y = 0; y < cells.GetLength(0); y++)
            {
                for (int x = 0; x < cells.GetLength(1); x++)
                {
                    if (cells[y, x] != CellValue.Filled)
                    {
                        if(currentValue > 0) values.Add(currentValue);
                        currentValue = 0;
                    }
                    else
                        currentValue += (cells[y, x] == CellValue.Filled) ? 1 : 0;

                }
                if (currentValue > 0) values.Add(currentValue);
                currentValue = 0;
                linesValues[y] = values.ToArray();
                values.Clear();
            }

            return linesValues;
        }

        /// <summary>
        /// Method to get values of each columns on the bitmap
        /// </summary>
        /// <param name="bmp">Bitmap image</param>
        /// <param name="threshold">Threashold value (integer) to determine if the value is Filled or not</param>
        /// <returns>2D integer array</returns>
        public static int[][] BitmapToColumnsValue(Bitmap bmp, int threshold)
        {
            CellValue[,] cells = BitmapToCellsValueArray(bmp, threshold);
            int currentValue = 0;
            int[][] columnsValues = new int[cells.GetLength(1)][];
            List<int> values = new List<int>();

            for (int x = 0; x < cells.GetLength(1); x++)
            {
                for (int y = 0; y < cells.GetLength(0); y++)
                {
                    if (cells[y, x] != CellValue.Filled)
                    {
                        if (currentValue > 0) values.Add(currentValue);
                        currentValue = 0;
                    }
                    else
                        currentValue += (cells[y, x] == CellValue.Filled) ? 1 : 0;

                }
                if (currentValue > 0) values.Add(currentValue);
                currentValue = 0;
                columnsValues[x] = values.ToArray();
                values.Clear();
            }

            return columnsValues;
        }

        /// <summary>
        /// Method to get values of each column on the bitmap
        /// </summary>
        /// <param name="cells">2D CellValue array</param>
        /// <returns>2D integer array</returns>
        public int[][] BitmapToColumnsValue(CellValue[,] cells)
        {
            int currentValue = 0;
            int[][] columnsValues = new int[cells.GetLength(0)][];
            List<int> values = new List<int>();

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    if (cells[y, x] != CellValue.Filled)
                    {
                        if (currentValue > 0) values.Add(currentValue);
                        currentValue = 0;
                    }
                    else
                        currentValue += (cells[y, x] == CellValue.Filled) ? 1 : 0;

                }
                if (currentValue > 0) values.Add(currentValue);
                currentValue = 0;
                columnsValues[x] = values.ToArray();
                values.Clear();
            }

            return columnsValues;
        }

        /// <summary>
        /// Load from a xml serialization file
        /// </summary>
        /// <param name="filename">Xml filename</param>
        public static  PicrossPuzzle LoadFromFile(string filename)
        {
            // Reference : http://msdn.microsoft.com/en-us/library/4abbf6k0(v=vs.110).aspx
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            PicrossPuzzle puzzleTmp = (PicrossPuzzle)formatter.Deserialize(stream);
            stream.Close();
            return puzzleTmp;
        }

        /// <summary>
        /// Save the current PicrossPuzzle to a xml serialization file.
        /// </summary>
        /// <param name="filename">Xml filename</param>
        public void SaveToFile(string filename)
        {
            // Reference : http://msdn.microsoft.com/en-us/library/4abbf6k0(v=vs.110).aspx
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public static PicrossPuzzle LoadXmlFile(string xmlPath)
        {
            int nbLines = 0;
            int nbColumns = 0;
            int index = 0;
            string xmlString;
            List<int> values;
            StringBuilder sb = new StringBuilder();
            int[][] linesValues;
            int[][] columnsValues;

            using (StreamReader sr = new StreamReader(xmlPath))
            {
                xmlString = sr.ReadToEnd();
            }

            sb = new StringBuilder();
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("NbLines");
                reader.MoveToFirstAttribute();
                nbLines = Convert.ToInt16(reader.Value);
                sb.AppendLine("Number of lines: " + nbLines);

                reader.ReadToFollowing("NbRows");
                reader.MoveToFirstAttribute();
                nbColumns = Convert.ToInt16(reader.Value);
                sb.AppendLine("Number of lines: " + nbColumns);

                linesValues = new int[nbLines][];
                columnsValues = new int[nbColumns][];


                while (reader.ReadToFollowing("Line"))
                {
                    reader.MoveToFirstAttribute();
                    index = Convert.ToInt16(reader.Value);
                    reader.ReadToFollowing("indices_string");
                    reader.MoveToFirstAttribute();

                    string indices = reader.ReadElementContentAsString();
                    if (indices != string.Empty)
                    {
                        values = new List<int>();
                        foreach (string s in indices.Split(' '))
                            values.Add(Convert.ToInt16(s));
                        linesValues[index] = values.ToArray();
                    }
                    else
                    {
                        linesValues[index] = new int[0];
                    }
                }
            }

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                while (reader.ReadToFollowing("Row"))
                {
                    reader.MoveToFirstAttribute();
                    index = Convert.ToInt16(reader.Value);

                    reader.ReadToFollowing("indices_string");
                    reader.MoveToFirstAttribute();

                    string indices = reader.ReadElementContentAsString();
                    if (indices != string.Empty)
                    {
                        values = new List<int>();
                        foreach (string s in indices.Split(' '))
                            values.Add(Convert.ToInt16(s));

                        columnsValues[index] = values.ToArray();
                    }
                    else
                    {
                        columnsValues[index] = new int[0];
                    }
                }
            }
            return new PicrossPuzzle(null, linesValues, columnsValues);
        }


        static int[][] ReadValuesFromNonFile(StreamReader sr, int size)
        {
            int[][] values = new int[size][];

            for (int i = 0; i < size; i++)
            {
                string line = sr.ReadLine();
                if (line == string.Empty)
                    i--;
                else
                    values[i] = line.Split(',').Select(int.Parse).Where(n => n>0).ToArray();
            }

            return values;
        }

        /// <summary>
        /// Load a puzzle from a Non file. The format is specified on http://www.comp.lancs.ac.uk/~ss/nonogram/fmt2
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static PicrossPuzzle LoadFromNonFile(string filepath)
        {
            int[][] linesValues = null;
            int[][] columnsValues = null;
            int width = 0;
            int height = 0;

            using (StreamReader sr = new StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.StartsWith("width"))
                        width = int.Parse(line.Substring(6));
                    else if (line.StartsWith("height"))
                        height = int.Parse(line.Substring(7));
                    else if (line.StartsWith("rows"))
                        linesValues = ReadValuesFromNonFile(sr, height);
                    else if (line.StartsWith("columns"))
                        columnsValues = ReadValuesFromNonFile(sr, width);
                }
            }

            return new PicrossPuzzle(null, linesValues, columnsValues);

        }

        /// <summary>
        /// Method to check a line
        /// </summary>
        /// <param name="columnNo">Number of the line (integer)</param>
        /// <returns>PuzzleState enum</returns>
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

            if (nbFilled > _nbFilledByLines[lineNo] || nbCrossed > w - _nbFilledByLines[lineNo])
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
                        if (nbFilled + nbCrossed >= w)
                            return PuzzleState.Incorrect;
                        return PuzzleState.Incomplete;
                    }
                }
                return PuzzleState.Finished;
            }
            else
            {
                // PuzzleState.Incomplete || PuzzleState.Incorrect
                // TODO: determine when PuzzleState is Incorrect
                if (nbFilled + nbCrossed >= w)
                    return PuzzleState.Incorrect;
                return PuzzleState.Incomplete;
            }
        }

        /// <summary>
        /// Method to check a column
        /// </summary>
        /// <param name="columnNo">Number of the column</param>
        /// <returns>PuzzleState enum</returns>
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

            if (nbFilled > _nbFilledByColumns[columnNo] || nbCrossed > h - _nbFilledByColumns[columnNo])
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
                        if (nbFilled + nbCrossed >= h)
                            return PuzzleState.Incorrect;
                        return PuzzleState.Incomplete;
                    }
                }
                return PuzzleState.Finished;
            }
            else
            {
                // PuzzleState.Incomplete || PuzzleState.Incorrect
                // TODO: determine when PuzzleState is Incorrect
                if (nbFilled + nbCrossed >= h)
                    return PuzzleState.Incorrect;
                return PuzzleState.Incomplete;
            }
        }

        /// <summary>
        /// Method to get the current puzzle state
        /// </summary>
        /// <returns>PuzzleState enum</returns>
        public PuzzleState GetPuzzleState(int currentX = 0, int currentY = 0)
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
