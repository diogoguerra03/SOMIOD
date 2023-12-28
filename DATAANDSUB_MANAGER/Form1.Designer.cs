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
            this.dataListBox.Size = new System.Drawing.Size(317, 260);
            this.dataListBox.TabIndex = 5;
            // 
            // subListBox
            // 
            this.subListBox.FormattingEnabled = true;
            this.subListBox.ItemHeight = 16;
            this.subListBox.Location = new System.Drawing.Point(571, 323);
            this.subListBox.Name = "subListBox";
            this.subListBox.Size = new System.Drawing.Size(317, 260);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 663);
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
            this.Text = "Form1";
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
    }
}

