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
            this.SuspendLayout();
            // 
            // deleteApp
            // 
            this.deleteApp.Location = new System.Drawing.Point(120, 33);
            this.deleteApp.Name = "deleteApp";
            this.deleteApp.Size = new System.Drawing.Size(232, 59);
            this.deleteApp.TabIndex = 0;
            this.deleteApp.Text = "DELETE APP";
            this.deleteApp.UseVisualStyleBackColor = true;
            this.deleteApp.Click += new System.EventHandler(this.deleteApp_Click);
            // 
            // deleteContainer
            // 
            this.deleteContainer.Location = new System.Drawing.Point(214, 180);
            this.deleteContainer.Name = "deleteContainer";
            this.deleteContainer.Size = new System.Drawing.Size(205, 147);
            this.deleteContainer.TabIndex = 1;
            this.deleteContainer.Text = "DELETE CONTAINER";
            this.deleteContainer.UseVisualStyleBackColor = true;
            this.deleteContainer.Click += new System.EventHandler(this.deleteContainer_Click);
            // 
            // DeleteSubscription
            // 
            this.DeleteSubscription.Location = new System.Drawing.Point(120, 402);
            this.DeleteSubscription.Name = "DeleteSubscription";
            this.DeleteSubscription.Size = new System.Drawing.Size(126, 58);
            this.DeleteSubscription.TabIndex = 2;
            this.DeleteSubscription.Text = "Delete Sub";
            this.DeleteSubscription.UseVisualStyleBackColor = true;
            this.DeleteSubscription.Click += new System.EventHandler(this.DeleteSubscription_Click);
            // 
            // Delete_Data
            // 
            this.Delete_Data.Location = new System.Drawing.Point(395, 391);
            this.Delete_Data.Name = "Delete_Data";
            this.Delete_Data.Size = new System.Drawing.Size(202, 111);
            this.Delete_Data.TabIndex = 3;
            this.Delete_Data.Text = "Delete Data";
            this.Delete_Data.UseVisualStyleBackColor = true;
            this.Delete_Data.Click += new System.EventHandler(this.Delete_Data_Click);
            // 
            // buttonUpdateAppName
            // 
            this.buttonUpdateAppName.Location = new System.Drawing.Point(517, 68);
            this.buttonUpdateAppName.Name = "buttonUpdateAppName";
            this.buttonUpdateAppName.Size = new System.Drawing.Size(171, 110);
            this.buttonUpdateAppName.TabIndex = 4;
            this.buttonUpdateAppName.Text = "Update app name";
            this.buttonUpdateAppName.UseVisualStyleBackColor = true;
            this.buttonUpdateAppName.Click += new System.EventHandler(this.buttonUpdateAppName_Click);
            // 
            // buttonUpdateContainerName
            // 
            this.buttonUpdateContainerName.Location = new System.Drawing.Point(517, 223);
            this.buttonUpdateContainerName.Name = "buttonUpdateContainerName";
            this.buttonUpdateContainerName.Size = new System.Drawing.Size(171, 104);
            this.buttonUpdateContainerName.TabIndex = 5;
            this.buttonUpdateContainerName.Text = "Update container Name";
            this.buttonUpdateContainerName.UseVisualStyleBackColor = true;
            this.buttonUpdateContainerName.Click += new System.EventHandler(this.buttonUpdateContainerName_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 554);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deleteApp;
        private System.Windows.Forms.Button deleteContainer;
        private System.Windows.Forms.Button DeleteSubscription;
        private System.Windows.Forms.Button Delete_Data;
        private System.Windows.Forms.Button buttonUpdateAppName;
        private System.Windows.Forms.Button buttonUpdateContainerName;
    }
}

