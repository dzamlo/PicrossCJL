/*******************
 * Author : J. Peiry, C. Rejas, L. Damien
 * Date   : 10.02.2014
 * Desc   : GUI for the PicrossPuzzle application
 * *****************/

using System;
using System.Windows.Forms;
using System.Drawing;
using PicrossCJL;

namespace PicrossCJLGUI
{
    /*TODO:
     * indiquer ligne/colomne (in)correcte et/ou puzzle terminé
     * intégrer solver dans GUI, backgroudworker ?
     * pour l'instant un seul chiffre des linesValue et columnsValue sont afficher, par example 2 au lieu de 20 su l'exemple 20x20
     *     edit: en fait sa depand les chiffre, surement un problème de taiile du rectangle dans lequel on affiche les valeurs
     * s'inspirer de http://www.puzzle-nonograms.com/
     * verfifier ligne colone inverser partout
     * integrer from bmp dans la gui
     * implémenter save
     * Ceratin calculs/instatiation sont fait plusisuers fois de manière inutiles
     * + checker sur moodle fonctionalité manquante
     * */

    public partial class Form1 : Form
    {
        #region Constants
        const int PIXEL_PER_DIGIT = 15;
        const int MARGIN_TOP_LEFT = 2;
        const string FONT_NAME = "Comic Sans Ms"; // RC: Comic Sans MS... really ?
        #endregion

        #region Fields & Properties
        private Rectangle[,] _cells;
        //private Graphics g;
        private PicrossController _controller;
        

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
        #endregion

        #region Ctor
        public Form1()
        {
            InitializeComponent();
            //g = panel1.CreateGraphics();
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            this.Controller = new PicrossController();
            this.Draw();
        }
        #endregion

        #region Methods
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
            this.ofd.Filter = "Fichiers XML|*.xml|Fichiers non|*.non;*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
                this.Controller.LoadFromFile(ofd.FileName);

            this.UpdateView();

            // Auto resize the view
            this.Width = (this.Controller.GetCellSize().Width + this.Controller.GetNbMaxLinesValues()) * PIXEL_PER_DIGIT + 60;
            this.Height = (this.Controller.GetCellSize().Height + this.Controller.GetNbMaxColumnsValues()) * PIXEL_PER_DIGIT + 100;
        }

        /// <summary>
        /// On click on the "about" button in the menu
        /// </summary>
        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Picross Puzzle v1.0 - CFPT-I\nby Loïc.D, Rejas.C and Peiry.J", "About");
        }

        /// <summary>
        /// Method to update the view
        /// </summary>
        public void UpdateView()
        {
            this.Draw();
        }

        /// <summary>
        /// Draw the Picross
        /// </summary>
        private void Draw()
        {
            panel1.Invalidate();
        }

        /// <summary>
        /// Paint panel method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.White);
            CellsRectangle = new Rectangle[this.Controller.GetCellSize().Width, this.Controller.GetCellSize().Height];

            #region Draw columns values
            // Draw columns rectangles & values
            for (int i = 0; i < this.Controller.Puzzle.ColumnsValues.GetLength(0); i++)
            {
                for (int j = 0; j < this.Controller.Puzzle.ColumnsValues[i].Length; j++)
                {
                    g.DrawRectangle(Pens.Black, new Rectangle((i + this.Controller.GetNbMaxLinesValues()) * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, j * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT));

                    g.DrawString(this.Controller.Puzzle.ColumnsValues[i][j].ToString(),
                        new Font(FONT_NAME, PIXEL_PER_DIGIT / 2),
                        Brushes.Black,
                        new RectangleF((i + this.Controller.GetNbMaxLinesValues()) * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, j * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT));
                }
            }
            #endregion

            #region Draw lines values
            // Draw lines rectangles & values
            for (int i = 0; i < this.Controller.Puzzle.LinesValues.GetLength(0); i++)
            {
                for (int j = 0; j < this.Controller.Puzzle.LinesValues[i].Length; j++)
                {
                    g.DrawRectangle(Pens.Blue, new Rectangle(j * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, (i + this.Controller.GetNbMaxColumnsValues()) * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT));

                    g.DrawString(this.Controller.Puzzle.LinesValues[i][j].ToString(),
                        new Font(FONT_NAME, PIXEL_PER_DIGIT / 2),
                        Brushes.Black,
                        new RectangleF(j * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, (i + this.Controller.GetNbMaxColumnsValues()) * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT));
                }
            }
            #endregion

            #region Draw cells grid
            // Draw cells grid
            for (int x = 0; x < this.Controller.GetCellSize().Width; x++)
            {
                for (int y = 0; y < this.Controller.GetCellSize().Height; y++)
                {
                    Rectangle r = new Rectangle(x * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT + this.Controller.GetNbMaxLinesValues() * PIXEL_PER_DIGIT, y * PIXEL_PER_DIGIT + MARGIN_TOP_LEFT + this.Controller.GetNbMaxColumnsValues() * PIXEL_PER_DIGIT, PIXEL_PER_DIGIT, PIXEL_PER_DIGIT);
                    CellsRectangle[x, y] = r;
                    switch (this.Controller.GetCellState(x, y))
                    {
                        case PicrossPuzzle.CellValue.Empty:
                            g.DrawRectangle(Pens.Red, r);
                            break;
                        case PicrossPuzzle.CellValue.Crossed:
                            g.DrawRectangle(Pens.Red, r);
                            g.DrawLine(Pens.Black, new Point(r.Left, r.Top), new Point(r.Right, r.Bottom));
                            g.DrawLine(Pens.Black, new Point(r.Right, r.Top), new Point(r.Left, r.Bottom));
                            break;
                        case PicrossPuzzle.CellValue.Filled:
                            g.FillRectangle(Brushes.Red, r);
                            break;
                        default: break;
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Mouse Click on Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
           for (int x = 0; x < this.Controller.GetCellSize().Width; x++)
                for (int y = 0; y < this.Controller.GetCellSize().Height; y++)
                    if (CellsRectangle[x, y].Contains(e.Location))
                        this.Controller.Puzzle.Cells[x, y] = (PicrossPuzzle.CellValue)(((int)this.Controller.Puzzle.Cells[x, y] + 1) % 3);

           this.UpdateView();
        }
        

        /// <summary>
        /// Method to solve the picross
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.Solve();
            this.UpdateView();
        }

        /// <summary>
        /// On click on the "Load file" button in the menu. Open a bmp file.
        /// </summary>
        private void loadFormBitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Fichiers Bitmap|*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
                this.Controller.LoadFromBitmap(ofd.FileName);

            this.UpdateView();

            // Auto resize the view
            this.Width = (this.Controller.GetCellSize().Width + this.Controller.GetNbMaxLinesValues()) * PIXEL_PER_DIGIT + 60;
            this.Height = (this.Controller.GetCellSize().Height + this.Controller.GetNbMaxColumnsValues()) * PIXEL_PER_DIGIT + 100;
        }
        #endregion
    }
}
