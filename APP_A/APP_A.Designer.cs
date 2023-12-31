namespace APP_A
{
    partial class APP_A
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APP_A));
            this.pictureBoxLamp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLamp)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLamp
            // 
            this.pictureBoxLamp.Image = global::APP_A.Properties.Resources.lampadaDesligada;
            this.pictureBoxLamp.Location = new System.Drawing.Point(138, 20);
            this.pictureBoxLamp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxLamp.Name = "pictureBoxLamp";
            this.pictureBoxLamp.Size = new System.Drawing.Size(243, 242);
            this.pictureBoxLamp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLamp.TabIndex = 6;
            this.pictureBoxLamp.TabStop = false;
            // 
            // APP_A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 288);
            this.Controls.Add(this.pictureBoxLamp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "APP_A";
            this.Text = "APP A";
            this.Load += new System.EventHandler(this.APP_A_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLamp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxLamp;
    }
}

