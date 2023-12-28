namespace APP_CONTAINER_MANAGER
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
            this.appTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.containersListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.getContainerButton = new System.Windows.Forms.Button();
            this.deleteContainerButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.containerNameTextBox = new System.Windows.Forms.TextBox();
            this.createContainerButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.updateContainerTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.updateContainer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // appTextBox
            // 
            this.appTextBox.Location = new System.Drawing.Point(108, 48);
            this.appTextBox.Name = "appTextBox";
            this.appTextBox.Size = new System.Drawing.Size(100, 22);
            this.appTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Aplication: ";
            // 
            // containersListBox
            // 
            this.containersListBox.FormattingEnabled = true;
            this.containersListBox.ItemHeight = 16;
            this.containersListBox.Location = new System.Drawing.Point(456, 51);
            this.containersListBox.Name = "containersListBox";
            this.containersListBox.Size = new System.Drawing.Size(309, 292);
            this.containersListBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(456, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Containers:";
            // 
            // getContainerButton
            // 
            this.getContainerButton.Location = new System.Drawing.Point(229, 48);
            this.getContainerButton.Name = "getContainerButton";
            this.getContainerButton.Size = new System.Drawing.Size(150, 23);
            this.getContainerButton.TabIndex = 4;
            this.getContainerButton.Text = "Get Containers";
            this.getContainerButton.UseVisualStyleBackColor = true;
            this.getContainerButton.Click += new System.EventHandler(this.getContainerButton_Click);
            // 
            // deleteContainerButton
            // 
            this.deleteContainerButton.BackColor = System.Drawing.Color.Red;
            this.deleteContainerButton.ForeColor = System.Drawing.Color.White;
            this.deleteContainerButton.Location = new System.Drawing.Point(494, 365);
            this.deleteContainerButton.Name = "deleteContainerButton";
            this.deleteContainerButton.Size = new System.Drawing.Size(235, 53);
            this.deleteContainerButton.TabIndex = 5;
            this.deleteContainerButton.Text = "DELETE CONTAINER";
            this.deleteContainerButton.UseVisualStyleBackColor = false;
            this.deleteContainerButton.Click += new System.EventHandler(this.deleteContainerButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name:";
            // 
            // containerNameTextBox
            // 
            this.containerNameTextBox.Location = new System.Drawing.Point(79, 29);
            this.containerNameTextBox.Name = "containerNameTextBox";
            this.containerNameTextBox.Size = new System.Drawing.Size(100, 22);
            this.containerNameTextBox.TabIndex = 7;
            // 
            // createContainerButton
            // 
            this.createContainerButton.Location = new System.Drawing.Point(202, 18);
            this.createContainerButton.Name = "createContainerButton";
            this.createContainerButton.Size = new System.Drawing.Size(165, 44);
            this.createContainerButton.TabIndex = 8;
            this.createContainerButton.Text = "Create Container";
            this.createContainerButton.UseVisualStyleBackColor = true;
            this.createContainerButton.Click += new System.EventHandler(this.createContainerButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.createContainerButton);
            this.groupBox1.Controls.Add(this.containerNameTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 246);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Creating";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Name:";
            // 
            // updateContainerTextBox
            // 
            this.updateContainerTextBox.Location = new System.Drawing.Point(80, 35);
            this.updateContainerTextBox.Name = "updateContainerTextBox";
            this.updateContainerTextBox.Size = new System.Drawing.Size(100, 22);
            this.updateContainerTextBox.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.updateContainer);
            this.groupBox2.Controls.Add(this.updateContainerTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(11, 361);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 75);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Editing";
            // 
            // updateContainer
            // 
            this.updateContainer.Location = new System.Drawing.Point(203, 29);
            this.updateContainer.Name = "updateContainer";
            this.updateContainer.Size = new System.Drawing.Size(165, 34);
            this.updateContainer.TabIndex = 10;
            this.updateContainer.Text = "Update Container";
            this.updateContainer.UseVisualStyleBackColor = true;
            this.updateContainer.Click += new System.EventHandler(this.updateContainer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.deleteContainerButton);
            this.Controls.Add(this.getContainerButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.containersListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.appTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox appTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox containersListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button getContainerButton;
        private System.Windows.Forms.Button deleteContainerButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox containerNameTextBox;
        private System.Windows.Forms.Button createContainerButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox updateContainerTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button updateContainer;
    }
}

