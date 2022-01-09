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
            this.tmrProf = new System.Windows.Forms.Timer(this.components);
            this.tmrExplode = new System.Windows.Forms.Timer(this.components);
            this.pnlMayinlar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTimeShower = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuOyunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1362, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pnlMayinlar
            // 
            this.pnlMayinlar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.pnlMayinlar.ForeColor = System.Drawing.Color.Gold;
            this.pnlMayinlar.Location = new System.Drawing.Point(87, 174);
            this.pnlMayinlar.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pnlMayinlar.Name = "pnlMayınlar";
            this.pnlMayinlar.Size = new System.Drawing.Size(433, 192);
            this.pnlMayinlar.TabIndex = 1;
            // 
            // MenuToolStripMenuItem
            // 
            this.MenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOyunToolStripMenuItem});
            this.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem";
            this.MenuToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.MenuToolStripMenuItem.Text = "Menu";
            // 
            // menuOyunToolStripMenuItem
            // 
            this.menuOyunToolStripMenuItem.Name = "menuOyunToolStripMenuItem";
            this.menuOyunToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.menuOyunToolStripMenuItem.Text = "Restart";
            this.menuOyunToolStripMenuItem.Click += new System.EventHandler(this.menuOyunToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Highlight;
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(1235, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(765, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 4;
            // 
            // lblTimeShower
            // 
            this.lblTimeShower.AutoSize = true;
            this.lblTimeShower.BackColor = System.Drawing.Color.Black;
            this.lblTimeShower.ForeColor = System.Drawing.Color.Red;
            this.lblTimeShower.Location = new System.Drawing.Point(41, 28);
            this.lblTimeShower.Name = "lblTimeShower";
            this.lblTimeShower.Size = new System.Drawing.Size(0, 25);
            this.lblTimeShower.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 1500);
            this.DoubleBuffered = true;
            this.Controls.Add(this.lblTimeShower);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlMayinlar);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Mine Hero";
            this.Text = "Mine Hero";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrProf;
        private System.Windows.Forms.Timer tmrExplode;
        private System.Windows.Forms.Panel pnlMayinlar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuOyunToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTimeShower;
    }
}

