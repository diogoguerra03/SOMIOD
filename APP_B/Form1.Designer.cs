namespace APP_B
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnLightOn = new System.Windows.Forms.Button();
            this.btnLightOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLightOn
            // 
            this.btnLightOn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLightOn.Location = new System.Drawing.Point(12, 73);
            this.btnLightOn.Name = "btnLightOn";
            this.btnLightOn.Size = new System.Drawing.Size(311, 107);
            this.btnLightOn.TabIndex = 0;
            this.btnLightOn.Text = "Light ON";
            this.btnLightOn.UseVisualStyleBackColor = true;
            this.btnLightOn.Click += new System.EventHandler(this.btnLightOn_Click);
            // 
            // btnLightOff
            // 
            this.btnLightOff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLightOff.Location = new System.Drawing.Point(12, 248);
            this.btnLightOff.Name = "btnLightOff";
            this.btnLightOff.Size = new System.Drawing.Size(311, 107);
            this.btnLightOff.TabIndex = 1;
            this.btnLightOff.Text = "Light Off";
            this.btnLightOff.UseVisualStyleBackColor = true;
            this.btnLightOff.Click += new System.EventHandler(this.btnLightOff_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 450);
            this.Controls.Add(this.btnLightOff);
            this.Controls.Add(this.btnLightOn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "APP B";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLightOn;
        private System.Windows.Forms.Button btnLightOff;
    }
}

