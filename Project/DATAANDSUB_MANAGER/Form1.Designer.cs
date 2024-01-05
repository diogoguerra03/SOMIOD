namespace DATAANDSUB_MANAGER
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
            this.containerTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.getDataAndSubButton = new System.Windows.Forms.Button();
            this.dataListBox = new System.Windows.Forms.ListBox();
            this.subListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.deleteDataButton = new System.Windows.Forms.Button();
            this.deleteSubscriptionButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.createDataButton = new System.Windows.Forms.Button();
            this.dataContentTextBox = new System.Windows.Forms.TextBox();
            this.dataNameTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.subNameTextBox = new System.Windows.Forms.TextBox();
            this.subEventTextBox = new System.Windows.Forms.TextBox();
            this.subEndpointTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.createSubButton = new System.Windows.Forms.Button();
            this.getDataDetails = new System.Windows.Forms.Button();
            this.getSubDetails = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // appTextBox
            // 
            this.appTextBox.Location = new System.Drawing.Point(33, 54);
            this.appTextBox.Name = "appTextBox";
            this.appTextBox.Size = new System.Drawing.Size(100, 22);
            this.appTextBox.TabIndex = 0;
            // 
            // containerTextBox
            // 
            this.containerTextBox.Location = new System.Drawing.Point(161, 54);
            this.containerTextBox.Name = "containerTextBox";
            this.containerTextBox.Size = new System.Drawing.Size(100, 22);
            this.containerTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Application";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Container";
            // 
            // getDataAndSubButton
            // 
            this.getDataAndSubButton.Location = new System.Drawing.Point(277, 40);
            this.getDataAndSubButton.Name = "getDataAndSubButton";
            this.getDataAndSubButton.Size = new System.Drawing.Size(149, 50);
            this.getDataAndSubButton.TabIndex = 4;
            this.getDataAndSubButton.Text = "Get Data and Sub";
            this.getDataAndSubButton.UseVisualStyleBackColor = true;
            this.getDataAndSubButton.Click += new System.EventHandler(this.getDataAndSubButton_Click);
            // 
            // dataListBox
            // 
            this.dataListBox.FormattingEnabled = true;
            this.dataListBox.ItemHeight = 16;
            this.dataListBox.Location = new System.Drawing.Point(571, 32);
            this.dataListBox.Name = "dataListBox";
            this.dataListBox.Size = new System.Drawing.Size(356, 260);
            this.dataListBox.TabIndex = 5;
            // 
            // subListBox
            // 
            this.subListBox.FormattingEnabled = true;
            this.subListBox.ItemHeight = 16;
            this.subListBox.Location = new System.Drawing.Point(571, 323);
            this.subListBox.Name = "subListBox";
            this.subListBox.Size = new System.Drawing.Size(370, 260);
            this.subListBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(568, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Datas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(568, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Subscriptions";
            // 
            // deleteDataButton
            // 
            this.deleteDataButton.BackColor = System.Drawing.Color.Red;
            this.deleteDataButton.ForeColor = System.Drawing.Color.White;
            this.deleteDataButton.Location = new System.Drawing.Point(571, 660);
            this.deleteDataButton.Name = "deleteDataButton";
            this.deleteDataButton.Size = new System.Drawing.Size(172, 65);
            this.deleteDataButton.TabIndex = 9;
            this.deleteDataButton.Text = "DELETE DATA";
            this.deleteDataButton.UseVisualStyleBackColor = false;
            this.deleteDataButton.Click += new System.EventHandler(this.deleteDataButton_Click);
            // 
            // deleteSubscriptionButton
            // 
            this.deleteSubscriptionButton.BackColor = System.Drawing.Color.Red;
            this.deleteSubscriptionButton.ForeColor = System.Drawing.Color.White;
            this.deleteSubscriptionButton.Location = new System.Drawing.Point(779, 660);
            this.deleteSubscriptionButton.Name = "deleteSubscriptionButton";
            this.deleteSubscriptionButton.Size = new System.Drawing.Size(162, 65);
            this.deleteSubscriptionButton.TabIndex = 10;
            this.deleteSubscriptionButton.Text = "DELETE SUBSCRIPTION";
            this.deleteSubscriptionButton.UseVisualStyleBackColor = false;
            this.deleteSubscriptionButton.Click += new System.EventHandler(this.deleteSubscriptionButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Content:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.createDataButton);
            this.groupBox1.Controls.Add(this.dataContentTextBox);
            this.groupBox1.Controls.Add(this.dataNameTextBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(6, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 179);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Data";
            // 
            // createDataButton
            // 
            this.createDataButton.Location = new System.Drawing.Point(329, 63);
            this.createDataButton.Name = "createDataButton";
            this.createDataButton.Size = new System.Drawing.Size(139, 68);
            this.createDataButton.TabIndex = 15;
            this.createDataButton.Text = "Create Data";
            this.createDataButton.UseVisualStyleBackColor = true;
            this.createDataButton.Click += new System.EventHandler(this.createDataButton_Click);
            // 
            // dataContentTextBox
            // 
            this.dataContentTextBox.Location = new System.Drawing.Point(86, 73);
            this.dataContentTextBox.Multiline = true;
            this.dataContentTextBox.Name = "dataContentTextBox";
            this.dataContentTextBox.Size = new System.Drawing.Size(188, 100);
            this.dataContentTextBox.TabIndex = 14;
            // 
            // dataNameTextBox
            // 
            this.dataNameTextBox.Location = new System.Drawing.Point(78, 37);
            this.dataNameTextBox.Name = "dataNameTextBox";
            this.dataNameTextBox.Size = new System.Drawing.Size(108, 22);
            this.dataNameTextBox.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Event:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 16);
            this.label9.TabIndex = 18;
            this.label9.Text = "Endpoint";
            // 
            // subNameTextBox
            // 
            this.subNameTextBox.Location = new System.Drawing.Point(73, 23);
            this.subNameTextBox.Name = "subNameTextBox";
            this.subNameTextBox.Size = new System.Drawing.Size(100, 22);
            this.subNameTextBox.TabIndex = 19;
            // 
            // subEventTextBox
            // 
            this.subEventTextBox.Location = new System.Drawing.Point(69, 67);
            this.subEventTextBox.Name = "subEventTextBox";
            this.subEventTextBox.Size = new System.Drawing.Size(100, 22);
            this.subEventTextBox.TabIndex = 20;
            // 
            // subEndpointTextBox
            // 
            this.subEndpointTextBox.Location = new System.Drawing.Point(85, 105);
            this.subEndpointTextBox.Name = "subEndpointTextBox";
            this.subEndpointTextBox.Size = new System.Drawing.Size(189, 22);
            this.subEndpointTextBox.TabIndex = 21;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.createSubButton);
            this.groupBox2.Controls.Add(this.subEndpointTextBox);
            this.groupBox2.Controls.Add(this.subEventTextBox);
            this.groupBox2.Controls.Add(this.subNameTextBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(6, 347);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(535, 161);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create Subscription";
            // 
            // createSubButton
            // 
            this.createSubButton.Location = new System.Drawing.Point(329, 47);
            this.createSubButton.Name = "createSubButton";
            this.createSubButton.Size = new System.Drawing.Size(139, 59);
            this.createSubButton.TabIndex = 22;
            this.createSubButton.Text = "Create Subscription";
            this.createSubButton.UseVisualStyleBackColor = true;
            this.createSubButton.Click += new System.EventHandler(this.createSubButton_Click);
            // 
            // getDataDetails
            // 
            this.getDataDetails.Location = new System.Drawing.Point(571, 589);
            this.getDataDetails.Name = "getDataDetails";
            this.getDataDetails.Size = new System.Drawing.Size(172, 65);
            this.getDataDetails.TabIndex = 23;
            this.getDataDetails.Text = "Get Data Details";
            this.getDataDetails.UseVisualStyleBackColor = true;
            this.getDataDetails.Click += new System.EventHandler(this.getDataDetails_Click);
            // 
            // getSubDetails
            // 
            this.getSubDetails.Location = new System.Drawing.Point(779, 589);
            this.getSubDetails.Name = "getSubDetails";
            this.getSubDetails.Size = new System.Drawing.Size(162, 65);
            this.getSubDetails.TabIndex = 24;
            this.getSubDetails.Text = "Get Subscription Details";
            this.getSubDetails.UseVisualStyleBackColor = true;
            this.getSubDetails.Click += new System.EventHandler(this.getSubDetails_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 748);
            this.Controls.Add(this.getSubDetails);
            this.Controls.Add(this.getDataDetails);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.deleteSubscriptionButton);
            this.Controls.Add(this.deleteDataButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.subListBox);
            this.Controls.Add(this.dataListBox);
            this.Controls.Add(this.getDataAndSubButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.containerTextBox);
            this.Controls.Add(this.appTextBox);
            this.Name = "Form1";
            this.Text = "DATA AND SUB MANAGER";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox appTextBox;
        private System.Windows.Forms.TextBox containerTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button getDataAndSubButton;
        private System.Windows.Forms.ListBox dataListBox;
        private System.Windows.Forms.ListBox subListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deleteDataButton;
        private System.Windows.Forms.Button deleteSubscriptionButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createDataButton;
        private System.Windows.Forms.TextBox dataContentTextBox;
        private System.Windows.Forms.TextBox dataNameTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox subNameTextBox;
        private System.Windows.Forms.TextBox subEventTextBox;
        private System.Windows.Forms.TextBox subEndpointTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button createSubButton;
        private System.Windows.Forms.Button getDataDetails;
        private System.Windows.Forms.Button getSubDetails;
    }
}

