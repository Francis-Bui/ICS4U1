using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using proj = WindowsFormsApplication1.Properties;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {

    WMPLib.WindowsMediaPlayer Player;

    //private Bitmap animatedImage = Image.FromFile(@"C:\Users\Francis Bui\Documents\Visual Studio 2010\Projects\GPong Francis\GPong Francis\Resources\explosion.gif");

    // Physics
    private int ballxVel = 30, ballyVel = 30, padyVel = 20, lazerVel = 70;
    private int pdlInc = 10, velInc = 3;

    // Flags
    private bool padLUp = false;
    private bool padLDown = false;
    private bool padRUp = false;
    private bool padRDown = false;
    private bool shootL = false;
    private bool shootR = false;
    private bool start = true;
    private bool laserLClear = true;
    private bool laserRClear = true;
    private bool endRoundL = false;
    private bool endRoundR = false;

    // scoring
    private int scoreL = 0, scoreR = 0, padLHeight = 150, padRHeight = 150;

    private Random rnd = new Random();

    // Rectangles
    private Rectangle rBall, rPadL, rPadR, rTitle, rWinner, rStart, rQuit, rScoreL, rScoreR, rExplosion, rLaserL, rLaserR, rBackHPL, rBackHPR, rHPL, rHPR, rBackShotL, rShotL, rBackShotR, rShotR;

    public Form1()
    {
        InitializeComponent();
        // info

        rBackHPL = new Rectangle(104, 27, 460, 67);
        rBackHPR = new Rectangle(1000, 27, 460, 67);

        rBackShotL = new Rectangle(12, 600, 99, 53);
        rShotL = new Rectangle(13, 605, 99, 53);
        rBackShotR = new Rectangle(1463, 600, 99, 53);
        rShotR = new Rectangle(1464, 605, 99, 53);

        // Ball
        rBall = new Rectangle(50, 50, 50, 50);

        // Ships
        rPadL = new Rectangle(60, pbCanvas.Height / 2 - 150, 60, 150);
        rPadR = new Rectangle(pbCanvas.Width - 120, 150, 60, 150);

        // title images

        rTitle = new Rectangle(530, 50, 490, 115);
        rWinner = new Rectangle(510, 170, 530, 125);
        rStart = new Rectangle(400, 400, 203, 94);
        rQuit = new Rectangle(980, 400, 203, 94);
        rScoreL = new Rectangle(31, 25, 48, 56);
        rScoreR = new Rectangle(1504, 25, 48, 56);
    }

    private void CanvasPaint(object sender, PaintEventArgs e)
    {
        if (start == false)
        {

            // Create info'

            e.Graphics.FillRectangle(Brushes.DarkRed, rBackHPL);
            e.Graphics.FillRectangle(Brushes.Red, rHPL);

            e.Graphics.FillRectangle(Brushes.DarkRed, rBackHPR);
            e.Graphics.FillRectangle(Brushes.Red, rHPR);

            if (laserLClear == true)
            {
                e.Graphics.DrawImage(proj.Resources.Loaded, rBackShotL);
                e.Graphics.DrawImage(proj.Resources.LoadedRed, rShotL);
            }
            else if (laserLClear == false)
                e.Graphics.DrawImage(proj.Resources.Empty, rShotL);

            if (laserRClear == true)
            {
                e.Graphics.DrawImage(proj.Resources.LoadedRed, rShotR);
                e.Graphics.DrawImage(proj.Resources.Loaded, rBackShotR);
            }
            else if (laserRClear == false)
                e.Graphics.DrawImage(proj.Resources.Empty, rShotR);

            // Create ball'
            e.Graphics.DrawImage(proj.Resources.fireball, rBall);

            // Create Ships'
            if (padLHeight > 30)
                e.Graphics.DrawImage(proj.Resources.pongShipBlue, rPadL);

            if (padRHeight > 30)
                e.Graphics.DrawImage(proj.Resources.pongShipRed, rPadR);

            // Scoring'
            if (scoreL == 0)
                e.Graphics.DrawImage(proj.Resources._0, rScoreL);
            else if (scoreL == 1)
                e.Graphics.DrawImage(proj.Resources._1, rScoreL);
            else if (scoreL == 2)
                e.Graphics.DrawImage(proj.Resources._2, rScoreL);
            else if (scoreL == 3)
                e.Graphics.DrawImage(proj.Resources._3, rScoreL);

            if (scoreR == 0)
                e.Graphics.DrawImage(proj.Resources._0, rScoreR);
            else if (scoreR == 1)
                e.Graphics.DrawImage(proj.Resources._1, rScoreR);
            else if (scoreR == 2)
                e.Graphics.DrawImage(proj.Resources._2, rScoreR);
            else if (scoreR == 3)
                e.Graphics.DrawImage(proj.Resources._3, rScoreR);

            if (scoreL > 3)
            {
                start = true;
                endRoundL = true;
            }

            if (scoreR > 3)
            {
                start = true;
                endRoundR = true;
            }

            e.Graphics.DrawImage(proj.Resources.explosion, rExplosion);

            // check if shot'
            if (shootL == true)
            {
                e.Graphics.DrawRectangle(Pens.Blue, rLaserL);
                e.Graphics.FillRectangle(Brushes.White, rLaserL);
            }

            if (shootR == true)
            {
                e.Graphics.DrawRectangle(Pens.Red, rLaserR);
                e.Graphics.FillRectangle(Brushes.White, rLaserR);
            }
        }

        // display when start = true'
        if (start == true)
        {
            e.Graphics.DrawImage(proj.Resources.Title, rTitle);
            e.Graphics.DrawImage(proj.Resources.Start, rStart);
            e.Graphics.DrawImage(proj.Resources.Quit, rQuit);

            if (endRoundL == true)
                e.Graphics.DrawImage(proj.Resources.Blue_Wins, rWinner);

            if (endRoundR == true)
                e.Graphics.DrawImage(proj.Resources.Red_Wins, rWinner);
        }
    }

    private void TimerTick(object sender, EventArgs e)
    {
        rBall.X += ballxVel;
        rBall.Y += ballyVel;

        rLaserL.X += lazerVel;
        rLaserR.X -= lazerVel;

        // Bottom'
        if (rBall.Bottom >= pbCanvas.Bottom)
            ballyVel *= -1;

        // Top'
        if (rBall.Top <= pbCanvas.Top)
            ballyVel *= -1;

        // Right'
        if (rBall.Right >= pbCanvas.Right)
        {
            CentreBall();
            scoreL += 1;
        }

        // Left'
        if (rBall.Left <= pbCanvas.Left)
        {
            CentreBall();
            scoreR += 1;
        }

        // check if the laser is off screen'
        if (rLaserL.X >= pbCanvas.Right)
            laserLClear = true;

        if (rLaserR.X <= pbCanvas.Left)
            laserRClear = true;


        // Paddle code'

        if (padLUp == true & rPadL.Top > pbCanvas.Top)
            rPadL.Y -= padyVel;

        if (padLDown == true & rPadL.Bottom < pbCanvas.Bottom)
            rPadL.Y += padyVel;

        // Right Paddle'
        if (padRUp == true & rPadR.Top > pbCanvas.Top)
            rPadR.Y -= padyVel;

        if (padRDown == true & rPadR.Bottom < pbCanvas.Bottom)
            rPadR.Y += padyVel;

        // Check if pad is dead'
        if (padRHeight < 30)
        {
            scoreR += 1;
            CentreBall();
        }

        if (padLHeight < 30)
        {
            scoreR += 1;
            CentreBall();
        }

        BlockCollide(rPadL);
        BlockCollide(rPadR);

        pbCanvas.Refresh();
    }

    private void KbDown(object sender, KeyEventArgs e)
    {

        // Up W // Left Table'
        if (e.KeyData == Keys.W)
        {
            padLUp = true;
            padLDown = false;
        }

        // Down S// Left Table'
        if (e.KeyData == Keys.S)
        {
            padLDown = true;
            padLUp = false;
        }

        // Up Arrow // Right Table'
        if (e.KeyData == Keys.Up)
        {
            padRUp = true;
            padRDown = false;
        }

        // Down Arrow // Right Table'
        if (e.KeyData == Keys.Down)
        {
            padRDown = true;
            padRUp = false;
        }

        // Blue Laser Shoot'
        if (e.KeyData == Keys.Space & laserLClear == true)
        {
            shootL = true;
            PlayFile(@"C:\Users\Francis\Documents\Visual Studio 2010\Projects\PongConversionBui\PongConversionBui\Resources\laser.wav");
            rLaserL = new Rectangle(rPadL.X + 20, rPadL.Y + 60, 25, 10);
            laserLClear = false;
        }

        // Right Laser Shoot'
        if (e.KeyData == Keys.NumPad0 & laserRClear == true)
        {
            shootR = true;
            PlayFile(@"C:\Users\Francis\Documents\Visual Studio 2010\Projects\PongConversionBui\PongConversionBui\Resources\laser.wav");
            rLaserR = new Rectangle(rPadR.X + 20, rPadR.Y + 60, 25, 10);
            laserRClear = false;
        }
    }

    private void KbUp(object sender, KeyEventArgs e)
    {

        // Up W // Left Table'
        if (e.KeyData == Keys.W)
            padLUp = false;

        // Down S // Left Table'
        if (e.KeyData == Keys.S)
            padLDown = false;

        // Up Arrow // Right Table'
        if (e.KeyData == Keys.Up)
            padRUp = false;

        // Down Arrow // Right Table'
        if (e.KeyData == Keys.Down)
            padRDown = false;
    }

    private void CentreBall()
    {

        // Centres ball and generates random direction'
        rBall.Location = new Point(pbCanvas.Width / 2, pbCanvas.Height / 2);
        ballxVel = rnd.Next(-25, 25);
        ballyVel = rnd.Next(-10, 10);

        padLHeight = 150;
        padRHeight = 150;
        rPadL = new Rectangle(rPadL.X, rPadL.Y, 60, padLHeight);
        rPadR = new Rectangle(rPadR.X, rPadR.Y, 60, padRHeight);

        rHPL = new Rectangle(115, 30, padLHeight * 3, 60);
        rHPR = new Rectangle(1000, 30, padRHeight * 3, 60);

        // ensures ball isnt too slow
        while (ballxVel >= -12 & ballxVel <= 13 | ballyVel >= -7 & ballyVel <= 8)
        {
            ballxVel = rnd.Next(-25, 25);
            ballyVel = rnd.Next(-10, 10);
        }
    }

    private void BlockCollide(Rectangle rect)
    {

        // Where to create explosion'
        if (rBall.IntersectsWith(rect))
        {
            rExplosion = new Rectangle(rBall.X, rBall.Y, 70, 70);
        }
        if (rLaserL.IntersectsWith(rect))
        {
            laserLClear = true;
            rExplosion = new Rectangle(rect.X, rect.Y, 60, 60);
        }

        if (rLaserR.IntersectsWith(rect))
        {
            laserLClear = true;
            rExplosion = new Rectangle(rect.X, rect.Y, 60, 60);
        }


        // check for a collision first.  If yes, then...
        if (rBall.IntersectsWith(rect) | rLaserL.IntersectsWith(rect) | rLaserR.IntersectsWith(rect))
        {


            // from the left?
            if (rBall.Right >= rect.Left & rBall.Left < rect.Left)
                ballxVel *= -1;

            // from the right?
            if (rBall.Left <= rect.Right & rBall.Right > rect.Right)
                ballxVel *= -1;

            // from the top
            if (rBall.Bottom >= rect.Top & rBall.Top < rect.Top)
                ballyVel *= -1;

            // from the bottom
            if (rBall.Top <= rect.Bottom & rBall.Bottom > rect.Bottom)
                ballyVel *= -1;

            //proj.Computer.Audio.Play(proj.Resources.Explosion1, AudioPlayMode.Background);

            // Health Degrading'
            // Speed Increment'
            if (rPadL.IntersectsWith(rBall))
            {
                ballxVel += velInc;
                padLHeight -= pdlInc;
            }

            if (rPadR.IntersectsWith(rBall))
            {
                ballxVel -= velInc;
                padRHeight -= pdlInc;
            }

            if (rPadL.IntersectsWith(rLaserR))
                padLHeight -= pdlInc;

            if (rPadR.IntersectsWith(rLaserL))
                padRHeight -= pdlInc;

            // Give shield health'
            // shieldHpL = rPadL.Height
            // shieldHpR = rPadR.Height

            // tbShieldL.Text = shieldHpL
            // tbShieldR.Text = shieldHpR

            rPadL = new Rectangle(rPadL.X, rPadL.Y, 60, padLHeight);
            rPadR = new Rectangle(rPadR.X, rPadR.Y, 60, padRHeight);

            rHPL = new Rectangle(115, 30, (padLHeight - 30) * 3, 60);
            rHPR = new Rectangle(1000, 30, (padRHeight - 30) * 3, 60);
        }
    }

    private void Reset()
    {
        start = false;
        scoreL = 0;
        scoreR = 0;
        endRoundL = false;
        endRoundR = false;
        CentreBall();
    }

    private void CanvasDown(object sender, MouseEventArgs e)
    {
        if (rStart.Contains(e.Location))
        {
            Reset();
            timer1.Enabled = true;
        }

        if (rQuit.Contains(e.Location))
            System.Environment.Exit(0);
    }

    private void PlayFile(String url)
    {
        Player = new WMPLib.WindowsMediaPlayer();
        Player.MediaError +=
            new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
        Player.URL = url;
    }

    private void Player_MediaError(object pMediaObject)
    {
        MessageBox.Show("Cannot play media file.");
    }

    }
}
