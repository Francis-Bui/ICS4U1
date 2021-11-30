using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BuiArray
{
    public partial class Form1 : Form
    {

        int spc = 110;  // space between each tile
        int initPosX;
        int prevCordX = -110;
        int prevCordY = 0;

        Point tempCord;   // temporary SWAP location 
        Point[] initCord = new Point[9];  // holds the original tiles locations (only need 9, but want to start at 1)
        Rectangle[] rect = new Rectangle[9];


        bool pass = false;
        bool clicked = false;
        bool genFlag = false;

        private Rectangle main;

        public Form1()
        {
            main = new Rectangle(0, 0, this.Width, this.Height);
            InitializeComponent();
            
            this.BackColor = Color.Aqua;

            //store INITIAL locations of buttons -- use for checkWin()

        }

        private void timer_Tick(object sender, EventArgs e)
        {
        }


        private void CanvasPaint(object sender, PaintEventArgs e)
        {

                for (int i = 0; i < 9; i++)
                {
                    if (i == 3 || i == 6)
                    {
                        prevCordX = -110;
                        prevCordY = prevCordY + spc;
                    }

                    if (genFlag == false)
                    {
                        rect[i].Location = new Point(prevCordX + spc, prevCordY);

                        rect[i].Size = new Size(100, 100);
                    }

                    if (clicked == false)
                        e.Graphics.FillRectangle(Brushes.Black, rect[i]);
                    else
                        e.Graphics.FillRectangle(Brushes.White, rect[i]);

                    initCord[i] = rect[i].Location;

                    prevCordX = rect[i].Location.X;
                    prevCordY = rect[i].Location.Y;

                    if (i == 8)
                    {
                        genFlag = true;
                    }
   
            }
        }

        private void RectMouseDown(object sender, MouseEventArgs e)
        {
            int i = 0;

            while(i < 9)
            {
                Point relativeMouse = this.PointToClient(Cursor.Position);

                if ((relativeMouse.X <= (initCord[i].X + rect[i].Width) && relativeMouse.X >= initCord[i].X) && (relativeMouse.Y <= (initCord[i].Y + rect[i].Height) && relativeMouse.Y >= initCord[i].Y))
                {
                    clicked = true;
                }
                else 
                {
                    clicked = false;
                }

                i++;

            }

            this.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Point relativeMouse = this.PointToClient(Cursor.Position);

            e.Graphics.DrawString(relativeMouse.ToString(), DefaultFont, Brushes.Black, new Point(2, 2));
        }


    }
}
