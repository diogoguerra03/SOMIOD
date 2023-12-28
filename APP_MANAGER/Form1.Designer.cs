﻿namespace APP_MANAGER
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
            this.listBoxApps = new System.Windows.Forms.ListBox();
            this.buttonUpdateAppName = new System.Windows.Forms.Button();
            this.btnDeleteApp = new System.Windows.Forms.Button();
            this.txtNameToUpdate = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCreateApp = new System.Windows.Forms.Button();
            this.txtNameToCreate = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxApps
            // 
            this.listBoxApps.FormattingEnabled = true;
            this.listBoxApps.Location = new System.Drawing.Point(295, 11);
            this.listBoxApps.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxApps.Name = "listBoxApps";
            this.listBoxApps.Size = new System.Drawing.Size(232, 212);
            this.listBoxApps.TabIndex = 10;
            // 
            // buttonUpdateAppName
            // 
            this.buttonUpdateAppName.Location = new System.Drawing.Point(179, 18);
            this.buttonUpdateAppName.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUpdateAppName.Name = "buttonUpdateAppName";
            this.buttonUpdateAppName.Size = new System.Drawing.Size(133, 29);
            this.buttonUpdateAppName.TabIndex = 9;
            this.buttonUpdateAppName.Text = "Update app name";
            this.buttonUpdateAppName.UseVisualStyleBackColor = true;
            this.buttonUpdateAppName.Click += new System.EventHandler(this.buttonUpdateAppName_Click);
            // 
            // btnDeleteApp
            // 
            this.btnDeleteApp.BackColor = System.Drawing.Color.Red;
            this.btnDeleteApp.ForeColor = System.Drawing.Color.White;
            this.btnDeleteApp.Location = new System.Drawing.Point(331, 20);
            this.btnDeleteApp.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteApp.Name = "btnDeleteApp";
            this.btnDeleteApp.Size = new System.Drawing.Size(162, 43);
            this.btnDeleteApp.TabIndex = 8;
            this.btnDeleteApp.Text = "DELETE APP";
            this.btnDeleteApp.UseVisualStyleBackColor = false;
            this.btnDeleteApp.Click += new System.EventHandler(this.btnDeleteApp_Click);
            // 
            // txtNameToUpdate
            // 
            this.txtNameToUpdate.Location = new System.Drawing.Point(48, 19);
            this.txtNameToUpdate.Name = "txtNameToUpdate";
            this.txtNameToUpdate.Size = new System.Drawing.Size(126, 20);
            this.txtNameToUpdate.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnDeleteApp);
            this.groupBox1.Controls.Add(this.txtNameToUpdate);
            this.groupBox1.Controls.Add(this.buttonUpdateAppName);
            this.groupBox1.Location = new System.Drawing.Point(13, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 84);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCreateApp);
            this.groupBox2.Controls.Add(this.txtNameToCreate);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 247);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Creating";
            // 
            // btnCreateApp
            // 
            this.btnCreateApp.Location = new System.Drawing.Point(154, 19);
            this.btnCreateApp.Name = "btnCreateApp";
            this.btnCreateApp.Size = new System.Drawing.Size(75, 23);
            this.btnCreateApp.TabIndex = 15;
            this.btnCreateApp.Text = "Create App";
            this.btnCreateApp.UseVisualStyleBackColor = true;
            this.btnCreateApp.Click += new System.EventHandler(this.btnCreateApp_Click);
            // 
            // txtNameToCreate
            // 
            this.txtNameToCreate.Location = new System.Drawing.Point(48, 21);
            this.txtNameToCreate.Name = "txtNameToCreate";
            this.txtNameToCreate.Size = new System.Drawing.Size(100, 20);
            this.txtNameToCreate.TabIndex = 14;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(295, 228);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(232, 30);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 355);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBoxApps);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "App Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxApps;
        private System.Windows.Forms.Button buttonUpdateAppName;
        private System.Windows.Forms.Button btnDeleteApp;
        private System.Windows.Forms.TextBox txtNameToUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNameToCreate;
        private System.Windows.Forms.Button btnCreateApp;
        private System.Windows.Forms.Button btnRefresh;
    }
}
