namespace Exp2
{
    partial class MainMenu
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
            this.MenuImg = new System.Windows.Forms.PictureBox();
            this.Timera = new System.Windows.Forms.Timer(this.components);
            this.Datainputtmr = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MenuImg)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuImg
            // 
            this.MenuImg.BackColor = System.Drawing.Color.LightCoral;
            this.MenuImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MenuImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuImg.Location = new System.Drawing.Point(0, 0);
            this.MenuImg.Name = "MenuImg";
            this.MenuImg.Size = new System.Drawing.Size(1384, 861);
            this.MenuImg.TabIndex = 0;
            this.MenuImg.TabStop = false;
            this.MenuImg.Click += new System.EventHandler(this.MenuImg_Click);
            // 
            // Timera
            // 
            this.Timera.Interval = 33;
            this.Timera.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // Datainputtmr
            // 
            this.Datainputtmr.Interval = 1;
            this.Datainputtmr.Tick += new System.EventHandler(this.Datainputtmr_Tick);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.MenuImg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MenuImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MenuImg;
        public System.Windows.Forms.Timer Timera;
        public System.Windows.Forms.Timer Datainputtmr;
    }
}