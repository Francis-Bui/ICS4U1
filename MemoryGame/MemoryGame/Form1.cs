using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int p, k, w, f, spc = 110;
        List<int> ListA = new List<int>();
        Random r = new Random();
        Rectangle[] rect = new Rectangle[10];
        Boolean[] chk = new Boolean[10];
        Boolean gen, dye;
        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
        }
        private void Form1_Paint(object sender, PaintEventArgs e) {
        Generate(e);
        }
        private void Generate(PaintEventArgs e) {
            Font drawFont = new Font("Arial", 50);
            if (gen == false) {
            for (int x = 0; x < 5; x++) {
                int k = r.Next(1, 6);
                ListA.Add(k);
            } 
                ListA.AddRange(ListA); 
            }
                for (int i = 0; i < 10; i++) {   
                    if (gen == false) {
                    rect[i].Location = new Point(rect[p].X + spc, 100);
                    rect[i].Size = new Size(100, 100);
                    p = i; 
                    }
                    e.Graphics.FillRectangle(Brushes.Black, rect[i]);
                    e.Graphics.DrawString(ListA.ElementAt(i).ToString(), drawFont, Brushes.Blue, rect[i]);
                    foreach(Rectangle Value in rect) if (chk[i] == true){


                        if (chk.Count(c => c) == 1) {
                        w = i;
                        e.Graphics.DrawRectangle(Pens.Blue, rect[w]);
                        }

                        if ((chk.Count(c => c) == 2) && (ListA.ElementAt(i) == ListA.ElementAt(w))) {
                            e.Graphics.DrawRectangle(Pens.Green, rect[i]);
                            e.Graphics.DrawRectangle(Pens.Green, rect[w]);
                            dye = true;
                            // PUT DELAY HERE
                        }

                        if ((chk.Count(c => c) >= 3)) {
                            for (int c = 0; c < chk.Length; c++) {
                            chk[c] = false;
                            }
                        }

                        //if (chk.Count(c => c) != 0) {e.Graphics.DrawRectangle(Pens.Blue, rect[w]);}
                }
            }
            gen = true;
        }

        private void Sense(object sender, MouseEventArgs e) {
            Point relativeMouse = this.PointToClient(Cursor.Position);
            int i = 0;
            foreach(Rectangle Value in rect) {
                if ((relativeMouse.X <= (rect[i].X + rect[i].Width) && relativeMouse.X >= rect[i].X) && (relativeMouse.Y <= (rect[i].Y + rect[i].Height) && relativeMouse.Y >= rect[i].Y)) {
            chk[i] = true;
        } else i++; this.Refresh();
      }
    }

    }
}
