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

        int p, spc = 110;

        List<int> ListA = new List<int>();
        Random r = new Random();

        Rectangle[] rect = new Rectangle[10];

        public Form1() {


            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Font drawFont = new Font("Arial", 50);

            for (int x = 0; x < 5; x++)
            {
                int k = r.Next(1, 6);
                ListA.Add(k);
            }


                for (int i = 0; i < 10; i++)
                {
                    ListA.AddRange(ListA); 
                    rect[i].Location = new Point(rect[p].X + spc, 100);
                    rect[i].Size = new Size(100, 100);
                    p = i;
                    e.Graphics.FillRectangle(Brushes.Black, rect[i]);
                    e.Graphics.DrawString(ListA.Last().ToString(), drawFont, Brushes.Blue, rect[i]);

                }
        }
    }

    static class MixIt
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random r = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
