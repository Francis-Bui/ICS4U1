namespace Sprite___Interactive_CS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tmrProf = new System.Windows.Forms.Timer(this.components);
            this.tmrExplode = new System.Windows.Forms.Timer(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            //
            // Mine Timer
            //
            this.timer.Interval = 40;
            // 
            // tmrProf
            // 
            this.tmrProf.Interval = 40;
            this.tmrProf.Tick += new System.EventHandler(this.tmrProf_Tick);
            // 
            // tmrExplode
            // 
            this.tmrExplode.Enabled = true;
            this.tmrExplode.Interval = 20;
            this.tmrExplode.Tick += new System.EventHandler(this.tmrExplode_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            //this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(984, 711);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            //this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Load += new System.EventHandler(this.Form1_Load);
            //
            // Other
            //
            this.pnlMayinlar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTimeShower = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Timer tmrProf;
        internal System.Windows.Forms.Timer tmrExplode;
        internal System.Windows.Forms.Panel pnlMayinlar;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label lblTimeShower;
        internal System.Windows.Forms.Button button;
    }
}

