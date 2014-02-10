/*******************
 * Author : J. Peiry
 * Date   : 10.02.2014
 * Desc   : GUI for the PicrossPuzzle application
 * *****************/

using System;
using System.Windows.Forms;

namespace PicrossCJLGUI
{
    public partial class Form1 : Form
    {   

        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// On click on the "sauvegarder" button in the menu. Save the file.
        /// </summary>
        private void sauvegardezToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //save file
            }
        }

        /// <summary>
        /// On click on the "Quitter" button in the menu. Exit the application
        /// </summary>
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Save data ? Confirmation before exit window ?
            Application.Exit();
        }

        /// <summary>
        /// On click on the "charger" button in the menu. Open an xml file
        /// </summary>
        private void chargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //load file
            }
        }

        /// <summary>
        /// On click on the "A propos" button in the menu
        /// </summary>
        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Kris, Loic and Jordan");
        }

        

    }
}
