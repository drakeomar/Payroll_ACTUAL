namespace Payroll
{
    partial class newEmployee
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
            this.button1 = new System.Windows.Forms.Button();
            this.newEmployeeIdTextBox = new System.Windows.Forms.TextBox();
            this.newfullNameTextBox = new System.Windows.Forms.TextBox();
            this.newRegHoursTextBox = new System.Windows.Forms.TextBox();
            this.newSSNTextBox = new System.Windows.Forms.TextBox();
            this.newOTHoursTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(82, 399);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Employee";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.clickAddNew);
            // 
            // newEmployeeIdTextBox
            // 
            this.newEmployeeIdTextBox.Location = new System.Drawing.Point(30, 82);
            this.newEmployeeIdTextBox.Margin = new System.Windows.Forms.Padding(15);
            this.newEmployeeIdTextBox.Name = "newEmployeeIdTextBox";
            this.newEmployeeIdTextBox.Size = new System.Drawing.Size(156, 22);
            this.newEmployeeIdTextBox.TabIndex = 1;
            // 
            // newfullNameTextBox
            // 
            this.newfullNameTextBox.Location = new System.Drawing.Point(30, 30);
            this.newfullNameTextBox.Margin = new System.Windows.Forms.Padding(15);
            this.newfullNameTextBox.Name = "newfullNameTextBox";
            this.newfullNameTextBox.Size = new System.Drawing.Size(156, 22);
            this.newfullNameTextBox.TabIndex = 2;
            // 
            // newRegHoursTextBox
            // 
            this.newRegHoursTextBox.Location = new System.Drawing.Point(30, 186);
            this.newRegHoursTextBox.Margin = new System.Windows.Forms.Padding(15);
            this.newRegHoursTextBox.Name = "newRegHoursTextBox";
            this.newRegHoursTextBox.Size = new System.Drawing.Size(156, 22);
            this.newRegHoursTextBox.TabIndex = 3;
            // 
            // newSSNTextBox
            // 
            this.newSSNTextBox.Location = new System.Drawing.Point(30, 134);
            this.newSSNTextBox.Margin = new System.Windows.Forms.Padding(15);
            this.newSSNTextBox.Name = "newSSNTextBox";
            this.newSSNTextBox.Size = new System.Drawing.Size(156, 22);
            this.newSSNTextBox.TabIndex = 4;
            // 
            // newOTHoursTextBox
            // 
            this.newOTHoursTextBox.Location = new System.Drawing.Point(30, 238);
            this.newOTHoursTextBox.Margin = new System.Windows.Forms.Padding(15);
            this.newOTHoursTextBox.Name = "newOTHoursTextBox";
            this.newOTHoursTextBox.Size = new System.Drawing.Size(156, 22);
            this.newOTHoursTextBox.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.newfullNameTextBox);
            this.flowLayoutPanel1.Controls.Add(this.newEmployeeIdTextBox);
            this.flowLayoutPanel1.Controls.Add(this.newSSNTextBox);
            this.flowLayoutPanel1.Controls.Add(this.newRegHoursTextBox);
            this.flowLayoutPanel1.Controls.Add(this.newOTHoursTextBox);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(82, 59);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(15);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(217, 327);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Full Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Employee ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "SSN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Regular Hours";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 300);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "OT Hours";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(195, 399);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 49);
            this.button2.TabIndex = 12;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // newEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 479);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Name = "newEmployee";
            this.Text = "newEmployee";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox newEmployeeIdTextBox;
        private System.Windows.Forms.TextBox newfullNameTextBox;
        private System.Windows.Forms.TextBox newRegHoursTextBox;
        private System.Windows.Forms.TextBox newSSNTextBox;
        private System.Windows.Forms.TextBox newOTHoursTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
    }
}