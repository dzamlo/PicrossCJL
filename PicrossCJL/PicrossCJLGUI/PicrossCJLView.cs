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

        Graphics g;

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
            g.DrawLine(Pens.Black, new Point(0, 0), new Point(panel1.Width, panel1.Height));
        }

    }
}
