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
            this.deleteApp = new System.Windows.Forms.Button();
            this.deleteContainer = new System.Windows.Forms.Button();
            this.DeleteSubscription = new System.Windows.Forms.Button();
            this.Delete_Data = new System.Windows.Forms.Button();
            this.buttonUpdateAppName = new System.Windows.Forms.Button();
            this.buttonUpdateContainerName = new System.Windows.Forms.Button();
            this.pictureBoxLamp = new System.Windows.Forms.PictureBox();
            this.listBoxApps = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLamp)).BeginInit();
            this.SuspendLayout();
            // 
            // deleteApp
            // 
            this.deleteApp.Location = new System.Drawing.Point(12, 12);
            this.deleteApp.Name = "deleteApp";
            this.deleteApp.Size = new System.Drawing.Size(232, 59);
            this.deleteApp.TabIndex = 0;
            this.deleteApp.Text = "DELETE APP";
            this.deleteApp.UseVisualStyleBackColor = true;
            this.deleteApp.Click += new System.EventHandler(this.deleteApp_Click);
            // 
            // deleteContainer
            // 
            this.deleteContainer.Location = new System.Drawing.Point(12, 77);
            this.deleteContainer.Name = "deleteContainer";
            this.deleteContainer.Size = new System.Drawing.Size(232, 66);
            this.deleteContainer.TabIndex = 1;
            this.deleteContainer.Text = "DELETE CONTAINER";
            this.deleteContainer.UseVisualStyleBackColor = true;
            this.deleteContainer.Click += new System.EventHandler(this.deleteContainer_Click);
            // 
            // DeleteSubscription
            // 
            this.DeleteSubscription.Location = new System.Drawing.Point(12, 149);
            this.DeleteSubscription.Name = "DeleteSubscription";
            this.DeleteSubscription.Size = new System.Drawing.Size(232, 61);
            this.DeleteSubscription.TabIndex = 2;
            this.DeleteSubscription.Text = "Delete Sub";
            this.DeleteSubscription.UseVisualStyleBackColor = true;
            this.DeleteSubscription.Click += new System.EventHandler(this.DeleteSubscription_Click);
            // 
            // Delete_Data
            // 
            this.Delete_Data.Location = new System.Drawing.Point(12, 216);
            this.Delete_Data.Name = "Delete_Data";
            this.Delete_Data.Size = new System.Drawing.Size(232, 58);
            this.Delete_Data.TabIndex = 3;
            this.Delete_Data.Text = "Delete Data";
            this.Delete_Data.UseVisualStyleBackColor = true;
            this.Delete_Data.Click += new System.EventHandler(this.Delete_Data_Click);
            // 
            // buttonUpdateAppName
            // 
            this.buttonUpdateAppName.Location = new System.Drawing.Point(12, 280);
            this.buttonUpdateAppName.Name = "buttonUpdateAppName";
            this.buttonUpdateAppName.Size = new System.Drawing.Size(232, 62);
            this.buttonUpdateAppName.TabIndex = 4;
            this.buttonUpdateAppName.Text = "Update app name";
            this.buttonUpdateAppName.UseVisualStyleBackColor = true;
            this.buttonUpdateAppName.Click += new System.EventHandler(this.buttonUpdateAppName_Click);
            // 
            // buttonUpdateContainerName
            // 
            this.buttonUpdateContainerName.Location = new System.Drawing.Point(12, 348);
            this.buttonUpdateContainerName.Name = "buttonUpdateContainerName";
            this.buttonUpdateContainerName.Size = new System.Drawing.Size(232, 61);
            this.buttonUpdateContainerName.TabIndex = 5;
            this.buttonUpdateContainerName.Text = "Update container Name";
            this.buttonUpdateContainerName.UseVisualStyleBackColor = true;
            this.buttonUpdateContainerName.Click += new System.EventHandler(this.buttonUpdateContainerName_Click);
            // 
            // pictureBoxLamp
            // 
            this.pictureBoxLamp.Image = global::APP_A.Properties.Resources.lampadaDesligada;
            this.pictureBoxLamp.Location = new System.Drawing.Point(681, 12);
            this.pictureBoxLamp.Name = "pictureBoxLamp";
            this.pictureBoxLamp.Size = new System.Drawing.Size(243, 243);
            this.pictureBoxLamp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLamp.TabIndex = 6;
            this.pictureBoxLamp.TabStop = false;
            // 
            // listBoxApps
            // 
            this.listBoxApps.FormattingEnabled = true;
            this.listBoxApps.ItemHeight = 16;
            this.listBoxApps.Location = new System.Drawing.Point(266, 12);
            this.listBoxApps.Name = "listBoxApps";
            this.listBoxApps.Size = new System.Drawing.Size(390, 260);
            this.listBoxApps.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 554);
            this.Controls.Add(this.listBoxApps);
            this.Controls.Add(this.pictureBoxLamp);
            this.Controls.Add(this.buttonUpdateContainerName);
            this.Controls.Add(this.buttonUpdateAppName);
            this.Controls.Add(this.Delete_Data);
            this.Controls.Add(this.DeleteSubscription);
            this.Controls.Add(this.deleteContainer);
            this.Controls.Add(this.deleteApp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "APP A";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLamp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deleteApp;
        private System.Windows.Forms.Button deleteContainer;
        private System.Windows.Forms.Button DeleteSubscription;
        private System.Windows.Forms.Button Delete_Data;
        private System.Windows.Forms.Button buttonUpdateAppName;
        private System.Windows.Forms.Button buttonUpdateContainerName;
        private System.Windows.Forms.PictureBox pictureBoxLamp;
        private System.Windows.Forms.ListBox listBoxApps;
    }
}

