namespace APP_A
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
            this.deleteContainer = new System.Windows.Forms.Button();
            this.DeleteSubscription = new System.Windows.Forms.Button();
            this.Delete_Data = new System.Windows.Forms.Button();
            this.buttonUpdateContainerName = new System.Windows.Forms.Button();
            this.pictureBoxLamp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLamp)).BeginInit();
            this.SuspendLayout();
            // 
            // deleteContainer
            // 
            this.deleteContainer.Location = new System.Drawing.Point(9, 11);
            this.deleteContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deleteContainer.Name = "deleteContainer";
            this.deleteContainer.Size = new System.Drawing.Size(149, 33);
            this.deleteContainer.TabIndex = 1;
            this.deleteContainer.Text = "DELETE CONTAINER";
            this.deleteContainer.UseVisualStyleBackColor = true;
            this.deleteContainer.Click += new System.EventHandler(this.deleteContainer_Click);
            // 
            // DeleteSubscription
            // 
            this.DeleteSubscription.Location = new System.Drawing.Point(9, 48);
            this.DeleteSubscription.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DeleteSubscription.Name = "DeleteSubscription";
            this.DeleteSubscription.Size = new System.Drawing.Size(149, 31);
            this.DeleteSubscription.TabIndex = 2;
            this.DeleteSubscription.Text = "Delete Sub";
            this.DeleteSubscription.UseVisualStyleBackColor = true;
            this.DeleteSubscription.Click += new System.EventHandler(this.DeleteSubscription_Click);
            // 
            // Delete_Data
            // 
            this.Delete_Data.Location = new System.Drawing.Point(9, 83);
            this.Delete_Data.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Delete_Data.Name = "Delete_Data";
            this.Delete_Data.Size = new System.Drawing.Size(149, 30);
            this.Delete_Data.TabIndex = 3;
            this.Delete_Data.Text = "Delete Data";
            this.Delete_Data.UseVisualStyleBackColor = true;
            this.Delete_Data.Click += new System.EventHandler(this.Delete_Data_Click);
            // 
            // buttonUpdateContainerName
            // 
            this.buttonUpdateContainerName.Location = new System.Drawing.Point(11, 117);
            this.buttonUpdateContainerName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonUpdateContainerName.Name = "buttonUpdateContainerName";
            this.buttonUpdateContainerName.Size = new System.Drawing.Size(147, 38);
            this.buttonUpdateContainerName.TabIndex = 5;
            this.buttonUpdateContainerName.Text = "Update container Name";
            this.buttonUpdateContainerName.UseVisualStyleBackColor = true;
            this.buttonUpdateContainerName.Click += new System.EventHandler(this.buttonUpdateContainerName_Click);
            // 
            // pictureBoxLamp
            // 
            this.pictureBoxLamp.Image = global::APP_A.Properties.Resources.lampadaDesligada;
            this.pictureBoxLamp.Location = new System.Drawing.Point(180, 11);
            this.pictureBoxLamp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxLamp.Name = "pictureBoxLamp";
            this.pictureBoxLamp.Size = new System.Drawing.Size(182, 197);
            this.pictureBoxLamp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLamp.TabIndex = 6;
            this.pictureBoxLamp.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 234);
            this.Controls.Add(this.pictureBoxLamp);
            this.Controls.Add(this.buttonUpdateContainerName);
            this.Controls.Add(this.Delete_Data);
            this.Controls.Add(this.DeleteSubscription);
            this.Controls.Add(this.deleteContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "APP A";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLamp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button deleteContainer;
        private System.Windows.Forms.Button DeleteSubscription;
        private System.Windows.Forms.Button Delete_Data;
        private System.Windows.Forms.Button buttonUpdateContainerName;
        private System.Windows.Forms.PictureBox pictureBoxLamp;
    }
}

