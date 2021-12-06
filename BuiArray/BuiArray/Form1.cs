using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace BuiArray {
  public partial class Form1: Form {
    int spc = 110, k, prevCordX = -110, prevCordY;
    Point[] initCord = new Point[9];
    Point prevLoc = new Point();
    Rectangle[] rect = new Rectangle[9];
    Boolean[] chk = new Boolean[9];
    bool genFlag = false;
    Image[] img = new Image[10], imgArray = new Image[9];
    Image P = Properties.Resources.C;
    public Form1() {
      InitializeComponent();
      this.BackColor = Color.Black;
      int tileW = P.Width / 3, tileH = P.Height / 3;
      for (int j = 0; j < 3; j++) {
        for (int i = 0; i < 3; i++) {
          int index = j * 3 + i;
          imgArray[index] = new Bitmap(tileW, tileH);
          var g = Graphics.FromImage(imgArray[index]);
          g.DrawImage(P, new Rectangle(0, 0, tileW, tileH), new Rectangle(i * tileW, j * tileH, tileW, tileH), GraphicsUnit.Pixel);
        }
      }
      for (int x = 0; x <= 8; x++) {
        img[x] = imgArray[x];
      }
    }
    private void CanvasPaint(object sender, PaintEventArgs e) {
      for (int i = 0; i < 9; i++) {
        if (i == 3 || i == 6) {
          prevCordX = -110;
          prevCordY = prevCordY + spc;
        }
        if (genFlag == false) {
          rect[i].Location = new Point(prevCordX + spc, prevCordY);
          rect[i].Size = new Size(100, 100);
          e.Graphics.DrawImage(img[i], rect[i]);
          initCord[i] = rect[i].Location;
        }
        prevCordX = rect[i].Location.X;
        prevCordY = rect[i].Location.Y;
        e.Graphics.DrawImage(img[i], rect[i]);
        if (rect[i].Location == initCord[i]) {}
        if (i == 8) {
          genFlag = true;
        }
        if (genFlag == true) {
          foreach(Rectangle Value in rect) if (chk[i] == true) {
            if (chk.Count(c => c) == 1) {
              prevLoc = rect[i].Location;
              k = i;
            }
            if ((chk.Count(c => c) == 2) && (((rect[k].X == (rect[i].X - spc)) || (rect[k].Y == (rect[i].Y - spc)) || (rect[k].X == (rect[i].X + spc)) || (rect[k].Y == (rect[i].Y + spc))))) {
              rect[k].Location = rect[i].Location;
              rect[i].Location = prevLoc;
              for (int c = 0; c < chk.Length; c++) {
                chk[c] = false;
              }
              this.Refresh();
            }
            if (chk.Count(c => c) != 0) e.Graphics.DrawRectangle(Pens.White, rect[k]);
          }
        }
      }
    }
    private void RectMouseDown(object sender, MouseEventArgs e) {
      Point relativeMouse = this.PointToClient(Cursor.Position);
      int i = 0;
      foreach(Rectangle Value in rect) {
        if ((relativeMouse.X <= (rect[i].X + rect[i].Width) && relativeMouse.X >= rect[i].X) && (relativeMouse.Y <= (rect[i].Y + rect[i].Height) && relativeMouse.Y >= rect[i].Y)) {
          chk[i] = true;
        } else i++;
      }
      this.Refresh();
    }
  }
}