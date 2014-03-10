/*******************
 * Author : J. Peiry
 * Date   : 10.02.2014
 * Desc   : GUI for the PicrossPuzzle application
 * *****************/

using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Drawing;

namespace PicrossCJLGUI
{
    public partial class Form1 : Form
    {
        const int PIXEL_PER_DIGIT = 20;
        const int MARGIN_TOP_LEFT = 2;
        const string FONT_NAME = "Comic Sans Ms";
        Rectangle[,] _cells;


        Graphics g;
        PicrossController _controller;

        public Rectangle[,] CellsRectangle
        {
            get {
                if (_cells == null)
                    _cells = new Rectangle[12, 12];
                return _cells;
            }
            set { _cells = value; }
        }

        internal PicrossController Controller
        {
            get
            {
                if (_controller == null)
                    _controller = new PicrossController();
                return _controller;
            }
            set { _controller = value; }
        }
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.Draw();

        }


        /// <summary>
        /// On click on the "Save file" button in the menu. Save the file.
        /// </summary>
        private void sauvegardezToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //save file - Call Puzzle.Save()

            }
        }

        /// <summary>
        /// On click on the "Close" button in the menu. Exit the application
        /// </summary>
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Save data ? Confirmation before exit window ?
            Application.Exit();
        }

        /// <summary>
        /// On click on the "Load file" button in the menu. Open an xml file
        /// </summary>
        private void chargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //load file and send it to the constructor of the PicrossPuzzle
                //Call Puzzle.Load()



            }
        }

        /// <summary>
        /// On click on the "about" button in the menu
        /// </summary>
        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Kris, Loic and Jordan");

        }

        public void UpdateView()
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Draw()
        {
            panel1.Invalidate();

        }

        private void DrawGrid(int lines, int columns, Graphics g)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int LineSize = 4;
            int ColumnSize = 4;
            int ColumnsCount = 12;
            int LineCount = 12;
            int CellLineCount = LineCount;
            int CellColumnCount = ColumnsCount;

            Random rd = new Random();

            //Draw columns rectangles & values
            for (int i = LineSize; i < ColumnsCount + LineSize; i++)
            {
                for (int j = 0; j < ColumnSize; j++)
                {
                    g.DrawRectangle(Pens.Black, new Rectangle(i * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, j * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT));

                    g.DrawString(rd.Next(0, 10).ToString(),
                        new Font(FONT_NAME, PIXEL_PER_DIGIT / 2),
                        Brushes.Black,
                        new RectangleF(i * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, j * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT));
                }
            }

            //Draw lines rectangles & values
            for (int i = ColumnSize; i < LineCount + ColumnSize; i++)
            {
                for (int j = 0; j < LineSize; j++)
                {
                    g.DrawRectangle(Pens.Blue, new Rectangle(j * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, i * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT));

                    g.DrawString(rd.Next(0, 10).ToString(),
                        new Font(FONT_NAME, PIXEL_PER_DIGIT / 2),
                        Brushes.Black,
                        new RectangleF(j * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, i * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT));
                }
            }
            
            //Draw cells rectangles
            //for (int i = LineSize; i < LineSize + CellLineCount; i++) 
            for(int i = 0; i < CellLineCount; i++)
            {

                for (int j = 0; j < CellColumnCount; j++)
                {
                    Rectangle r = new Rectangle(i * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT + LineSize*PIXEL_PER_DIGIT, j * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT + ColumnSize*PIXEL_PER_DIGIT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT);
                    CellsRectangle[i, j] = r;
                    g.DrawRectangle(Pens.Red, r);
                }
            }

                    

         

            //g.DrawLine(Pens.Black, new Point(0, 0), new Point(panel1.Width, panel1.Height));

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < CellsRectangle.GetLength(0); i++)
            {
                for (int j = 0; j < CellsRectangle.GetLength(1); j++)
                {
                    if (CellsRectangle[i, j].Contains(e.Location))
                    {
                        //MessageBox.Show("Clicked in the cells' space");
                        //Get state of the current rectangle

                        //Change state

                        //(Draw a cross)
                        g.DrawLine(Pens.Black , new Point(CellsRectangle[i, j].X, CellsRectangle[i, j].Y), new Point(CellsRectangle[i, j].X + PIXEL_PER_DIGIT, CellsRectangle[i, j].Y + PIXEL_PER_DIGIT));
                        g.DrawLine(Pens.Black, new Point(CellsRectangle[i, j].X+ PIXEL_PER_DIGIT, CellsRectangle[i, j].Y), new Point(CellsRectangle[i, j].X, CellsRectangle[i, j].Y + PIXEL_PER_DIGIT));
                    }
                }
            }
        }

    }
}
