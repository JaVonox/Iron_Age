namespace Exp2
{
    partial class Observe
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
            this.ImagePic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePic)).BeginInit();
            this.SuspendLayout();
            // 
            // ImagePic
            // 
            this.ImagePic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImagePic.Location = new System.Drawing.Point(0, 0);
            this.ImagePic.Name = "ImagePic";
            this.ImagePic.Size = new System.Drawing.Size(1384, 861);
            this.ImagePic.TabIndex = 0;
            this.ImagePic.TabStop = false;
            this.ImagePic.Click += new System.EventHandler(this.ImagePic_Click);
            // 
            // Observe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Salmon;
            this.ClientSize = new System.Drawing.Size(1384, 861);
            this.Controls.Add(this.ImagePic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Observe";
            this.Text = "Observe";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Observe_FormClosed);
            this.Load += new System.EventHandler(this.Observe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImagePic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ImagePic;
    }
}