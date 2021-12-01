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
        int prevCordX = -110;
        int prevCordY = 0;

        Point[] initCord = new Point[9];  // holds the original tiles locations (only need 9, but want to start at 1)
        Rectangle[] rect = new Rectangle[9];
        Boolean[] chk = new Boolean[9];

        bool clicked = false;
        bool genFlag = false;

        public Form1()
        {
            InitializeComponent();
            
            this.BackColor = Color.Aqua;

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
                        e.Graphics.FillRectangle(Brushes.Black, rect[i]);
                    }

                    initCord[i] = rect[i].Location;
                    prevCordX = rect[i].Location.X;
                    prevCordY = rect[i].Location.Y;

                    if (i == 8)
                    {
                        genFlag = true;
                    }

                    if (genFlag == true)
                    {
                        foreach (Rectangle Value in rect)
                        {
                            if (chk[i] == false)
                            {
                                e.Graphics.FillRectangle(Brushes.Black, rect[i]);
                            }
                            else if (chk[i] == true)
                            {
                                if (i >= 0 && i <= 5 && (chk[i + 3] == true || chk[i + 1] == true))
                                    e.Graphics.FillRectangle(Brushes.White, rect[i]);
                                else if (i >= 3 && i <= 8 && (chk[i - 3] == true || chk[i - 1] == true))
                                    e.Graphics.FillRectangle(Brushes.White, rect[i]);
                                else if (i >= 1 && chk[i - 1] == true)
                                    e.Graphics.FillRectangle(Brushes.White, rect[i]);
                                else if (i <= 8 && chk[i + 1] == true)
                                    e.Graphics.FillRectangle(Brushes.White, rect[i]);
                            }
                        }
                    }
                }

                
        }

        private void RectMouseDown(object sender, MouseEventArgs e)
        {
            Point relativeMouse = this.PointToClient(Cursor.Position);
            int i = 0;

            foreach (Rectangle Value in rect)
            {
                if ((relativeMouse.X <= (initCord[i].X + rect[i].Width) && relativeMouse.X >= initCord[i].X) && (relativeMouse.Y <= (initCord[i].Y + rect[i].Height) && relativeMouse.Y >= initCord[i].Y) && (chk.Count(c => c) < 2))
                {
                    chk[i] = true;
                }
                else
                {
                    i++;
                }
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
