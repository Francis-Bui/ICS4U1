using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class varcache
    {
        public int ballxVel = 30, ballyVel = 30, padyVel = 20, lazerVel = 70;
        public int pdlInc = 10, velInc = 3;

        // Flags
        public bool padLUp = false;
        public bool padLDown = false;
        public bool padRUp = false;
        public bool padRDown = false;
        public bool shootL = false;
        public bool shootR = false;
        public bool start = true;
        public bool laserLClear = true;
        public bool laserRClear = true;
        public bool endRoundL = false;
        public bool endRoundR = false;

        // scoring
        public int scoreL = 0, scoreR = 0, padLHeight = 150, padRHeight = 150;

        public Random rnd = new Random();

        // Rectangles
        public Rectangle rBall, rPadL, rPadR, rTitle, rWinner, rStart, rQuit, rScoreL, rScoreR, rExplosion, rLaserL, rLaserR, rBackHPL, rBackHPR, rHPL, rHPR, rBackShotL, rShotL, rBackShotR, rShotR;
    }
}
