using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Sprite___Interactive_CS
{
    public partial class Form1 : Form
    {
        Color buttonBackColor;
        int buttonSize;
        int buttonNumberX;
        int buttonNumberY;
        int mineNumber;
        int opacity;
        Game game;
        Timer timer;
        ElapsedTime elapsedTime;
        Button[,] allButtons;
        Dictionary<Button, Squares> squaresInButtons;
        Dictionary<GameStatus, string> gameResultText;
        Dictionary<GameStatus, Color> gameResultColor;
        Image imgProf = Properties.Resources.prof;
        Image imgBeaker = Properties.Resources.beaker;
        Image imgExplode = Properties.Resources.explosion;

        // variables to keep track of which frame/sprite to display - prof
        int count, row, col, totalRows, totalCols, profWidth, profHeight, profVel;

        // variables to keep track of which frame/sprite to display - explosion
        int count2, row2, col2, totalRows2, totalCols2, explodeWidth, explodeHeight;

        Boolean goUp, goDown, goLeft, goRight, explode;
        Rectangle rProf, rBeaker;

        public Form1()
        {

            buttonSize = 35;
            buttonNumberX = 38;
            buttonNumberY = 17;
            mineNumber = (buttonNumberX * buttonNumberY) / 9;
            buttonBackColor = Color.FromArgb(160, 90, 250);

            gameResultText = new Dictionary<GameStatus, string>
        {
            { GameStatus.Won, "- - - - - WON - - - - - -" },
            { GameStatus.Lost, "- - - - - LOST - - - - - -" }
        };

            gameResultColor = new Dictionary<GameStatus, Color>
        {
            { GameStatus.Won, Color.Green },
            { GameStatus.Lost, Color.Red }
        };

            // prof
            totalCols = 9;
            totalRows = 4;

            // explosion
            totalCols2 = 9;
            totalRows2 = 9;

            profWidth = imgProf.Width / totalCols;
            profHeight = imgProf.Height / totalRows;

            explodeWidth = imgExplode.Width / totalCols2;
            explodeHeight = imgExplode.Height / totalRows2;

            rProf = new Rectangle(100, 100, profWidth, profHeight);
            rBeaker = new Rectangle(200, 200, imgBeaker.Width, imgBeaker.Height);
            Rectangle[] rExplode = new Rectangle[100];
            profVel = 10;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int panelWidth = buttonNumberX * buttonSize;
            int panelHeight = buttonNumberY * buttonSize;
            this.CenterToScreen();
            this.Size = new Size(panelWidth + 50, panelHeight + 150);

            pnlMayinlar.Size = new Size(panelWidth, panelHeight);
            pnlMayinlar.Left = 20;
            pnlMayinlar.Top = 85;
            pnlMayinlar.BackColor = Color.Black;

            InitializeGame();

            int lblTop = 40;
            label2.Top = lblTop;
            lblTimeShower.Top = lblTop;

            label1.Text = "Remaining Squares: " + game.NumberOfNotOpenedSafetySquare().ToString();
            label1.Location = new Point(panelWidth - label1.Width, lblTop);
            pnlMayinlar.Show();
        }

        private void tmrExplode_Tick(object sender, EventArgs e)
        {
            // explosion animation code
            if (explode == true)
            {
                tmrProf.Stop();

                if (count2 >= (totalRows2 * totalCols2))
                {
                    count2 = 0;

                    tmrProf.Start();
                }

                // do some math on "count" to give you row and column on the spritesheet
                row2 = count2 / totalCols2;     // row is chosen by key
                col2 = count2 % totalCols2;    // returns the remainder only (no integer)

                // increment the counter
                count2 += 1;

                this.Refresh();
            }
        }

        private void tmrProf_Tick(object sender, EventArgs e)
        {
            if (goDown == true)
            {
                rProf.Y += profVel;
            }

            if (goUp == true)
            {
                rProf.Y -= profVel;
            }

            if (goLeft == true)
            {
                rProf.X -= profVel;
            }

            if (goRight == true)
            {
                rProf.X += profVel;
            }


            // prof sprite animation code========================================
            // after the last row, start over
            if (count >= (totalRows * totalCols))
            {
                count = 0;
            }

            // do some math on "count" to give you row and column on the spritesheet
            // row = count \ totalCols     //row is chosen by key
            col = count % totalCols;      //returns the remainder only (no integer)

            // increment the counter
            count += 1;

            //====================================================================


            // collision.............................
            if (rProf.IntersectsWith(rBeaker))
            {
                explode = true;
                rProf.Location = new Point(100, 100);
            }


            this.Refresh();
        }

        void InitializeGame()
        {
            squaresInButtons = new Dictionary<Button, Squares>();
            game = new Game(buttonNumberX, buttonNumberY, mineNumber);
            allButtons = new Button[buttonNumberY, buttonNumberX];
            pnlMayinlar.Enabled = true;

            for (int i = 0; i < game.Squares.GetLength(0); i++)
            {
                for (int j = 0; j < game.Squares.GetLength(1); j++)
                {
                    Button button = CreateButton(j, i);
                    squaresInButtons.Add(button, game.Square(j, i));
                    pnlMayinlar.Controls.Add(button);

                }
            }

            label2.Hide();
            label1.Show();
            SetLabelText(game.NumberOfNotOpenedSafetySquare());
            elapsedTime = new ElapsedTime();
            timer = new Timer
            {
                Interval = 1000,

            };
            timer.Tick += DrawElapsedTime;
            timer.Start();
        }

        Button CreateButton(int x, int y)
        {
            Button button = new Button()
            {
                Size = new Size(buttonSize, buttonSize),
                Top = y * buttonSize,
                Left = x * buttonSize,
                BackColor = buttonBackColor,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            button.MouseDown += ClickingOnSquares;
            allButtons[y, x] = button;

            return button;
        }

        private void menuOyunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlMayinlar.Controls.Clear();
            InitializeGame();
        }

        void ClickingOnSquares(object sender, MouseEventArgs e)
        {
            Button clicked = sender as Button;
            Squares square = squaresInButtons[clicked];

            if (e.Button == MouseButtons.Right)
            {

                Actions actions = game.ClickSquare(Clicks.RightClick, square);

                if (actions == Actions.DoNothing)
                {
                    return;
                }

                if (actions == Actions.PutFlag)
                {
                    clicked.BackgroundImage = Properties.Resources.flagIcon;
                }
                else if (actions == Actions.RemoveFlag)
                {
                    clicked.BackgroundImage = null;
                    clicked.BackColor = buttonBackColor;
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                Actions actions = game.ClickSquare(Clicks.LeftClick, square);

                if (actions == Actions.DoNothing)
                {
                    return;
                }
                // open left clicked square that has at least one neighborhood mine
                else if (actions == Actions.OpenSquare)
                {
                    OpenMineFreeSquare(square);
                }
                // open square that has no mine neighborhood and its neighborhoods at once
                else if (actions == Actions.OpenSquaresRecursively)
                {
                    IEnumerable<Squares> squareList = game.SquaresWillBeOpened(square);
                    foreach (Squares item in squareList)
                    {
                        OpenMineFreeSquare(item);
                    }
                }
                else if (actions == Actions.ExplodeAllMines)
                {
                    // show where all mines are after any mine is clicked
                    IEnumerable<Squares> allMines = game.MinesToShow();
                    ShowMines(allMines);
                    System.Threading.Thread.Sleep(1000);
                    // put exploded mine image on every mine 
                    //in order to their distance first clicked mine
                    IEnumerable<Squares> inLineMines = game.MinesToExplode(square);
                    ExplodeAllMines(inLineMines);
                    explode = true;
                }


                SetLabelText(game.NumberOfNotOpenedSafetySquare());

                // getting game situation for checking if there is a win or lose
                GameStatus gameState = game.GameSituation();

                // if game should be continue then leave method 
                // else check there is a win or lose and do necessary things
                if (gameState == GameStatus.NotFinished | gameState == GameStatus.Default)
                {
                    return;
                }
                else
                {
                    // stop counting time and write resulting text above map
                    timer.Stop();
                    label1.Hide();

                    label2.Show();
                    label2.ForeColor = gameResultColor[gameState];
                    label2.Text = gameResultText[gameState];
                    label2.Left = (this.Width - label2.Width) / 2;

                    if (gameState == GameStatus.Won)
                    {
                        IEnumerable<Squares> notDetonetedMines = game.MinesToShow();
                        ShowMines(notDetonetedMines);
                    }
                    else
                    {
                        // opening all not opened non-mine squares after all mines exploded
                        IEnumerable<Squares> NotOpenedSquares = game.NotOpenedSquare();
                        foreach (Squares item in NotOpenedSquares)
                        {
                            OpenMineFreeSquare(item);
                            System.Threading.Thread.Sleep(10);
                        }
                    }

                    pnlMayinlar.Enabled = false;
                }
            }

        }

        // when a no-mine square is clicked, number of neighborhood mine is wrote 
        // on it and colored depending on that number
        void OpenMineFreeSquare(Squares square)
        {
            Button clicked = allButtons[square.Location.Y, square.Location.X];
            if (square.NumberOfAdjacentMines > 0)
            {
                clicked.Text = square.NumberOfAdjacentMines.ToString();
            }
            clicked.BackColor = SquareTextColor(square.NumberOfAdjacentMines);
            clicked.Enabled = false;
        }

        // put a detoneted mine image on squares after any mine is clicked
        void ExplodeAllMines(IEnumerable<Squares> inLineMines)
        {
            foreach (Squares item in inLineMines)
            {
                int i = 0;
                Button willBeDetoneted = allButtons[item.Location.Y, item.Location.X];
                willBeDetoneted.Paint += new System.Windows.Forms.PaintEventHandler(this.buttonPaint);
                //rExplode[i].Location = willBeDetoneted.Location;
                willBeDetoneted.BackgroundImage = Properties.Resources.detonatedmine;
                explode = true;
                willBeDetoneted.Enabled = false;
                willBeDetoneted.Update();
                System.Threading.Thread.Sleep(50);
                i++;
            }
        }

        // put a not-detoneted mine image on squares before detoneted mine image is put
        // for making exploding animation
        void ShowMines(IEnumerable<Squares> inLineMines)
        {
            foreach (Squares item in inLineMines)
            {
                Button willBeDetoneted = allButtons[item.Location.Y, item.Location.X];
                willBeDetoneted.BackgroundImage = Properties.Resources.notDetonatedMine;
                willBeDetoneted.Enabled = false;
            }
        }

        // start when map is loaded and showing elapsed time at the left upper corner 
        void DrawElapsedTime(object source, EventArgs e)
        {
            lblTimeShower.Text = elapsedTime.TimeInHourFormat();
        }

        // write number of how many more square must be clicked for to win
        void SetLabelText(int score)
        {
            label1.Text = "Remaining Squares: " + score.ToString();
        }

        // color list for squares those have neighborhood mine at least one
        Color SquareTextColor(int mineNumber)
        {
            Color[] colors = {
             Color.FromArgb(180, 180, 180),
             Color.FromArgb(20, 110, 250),
             Color.FromArgb(10, 220, 20),
             Color.FromArgb(250, 20, 20),
             Color.FromArgb(150, 20, 60),
             Color.FromArgb(180, 40, 170),
             Color.FromArgb(90, 20, 20),
             Color.FromArgb(80, 30, 60),
             Color.FromArgb(50, 10, 40)
        };

            return colors[mineNumber];
        }

        private void buttonPaint(object sender, PaintEventArgs e)
        {
            if (explode == true)
            {
                //e.Graphics.DrawImage(imgExplode, rExplode[1], new RectangleF(col2 * explodeWidth, row2 * explodeHeight, explodeWidth, explodeHeight), GraphicsUnit.Pixel);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.S)
            {
                goDown = true;
                row = 2;
                tmrProf.Start();

            }

            if (e.KeyData == Keys.W)
            {
                goUp = true;
                row = 0;
                tmrProf.Start();
            }

            if (e.KeyData == Keys.A)
            {
                goLeft = true;
                row = 1;
                tmrProf.Start();
            }

            if (e.KeyData == Keys.D)
            {
                goRight = true;
                row = 3;
                tmrProf.Start();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.S)
            {
                goDown = false;
                tmrProf.Stop();
            }

            if (e.KeyData == Keys.W)
            {
                goUp = false;
                tmrProf.Stop();
            }

            if (e.KeyData == Keys.A)
            {
                goLeft = false;
                tmrProf.Stop();
            }

            if (e.KeyData == Keys.D)
            {
                goRight = false;
                tmrProf.Stop();
            }
        }
    }
}
