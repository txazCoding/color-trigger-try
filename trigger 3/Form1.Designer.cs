namespace trigger_3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblStatus = new Label();
            btnstart = new Button();
            button1 = new Button();
            lblAltStatus = new Label();
            btnSelectColor = new Button();
            colorDialog1 = new ColorDialog();
            lblSelectedColor = new Label();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 161);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(90, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Detecting Color";
            // 
            // btnstart
            // 
            btnstart.FlatStyle = FlatStyle.Popup;
            btnstart.Location = new Point(12, 12);
            btnstart.Name = "btnstart";
            btnstart.Size = new Size(75, 23);
            btnstart.TabIndex = 1;
            btnstart.Text = "button1";
            btnstart.UseVisualStyleBackColor = true;
            btnstart.Click += btnStart_Click;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(119, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnStop_Click;
            // 
            // lblAltStatus
            // 
            lblAltStatus.AutoSize = true;
            lblAltStatus.Location = new Point(111, 161);
            lblAltStatus.Name = "lblAltStatus";
            lblAltStatus.Size = new Size(89, 15);
            lblAltStatus.TabIndex = 3;
            lblAltStatus.Text = "Detecting input";
            // 
            // btnSelectColor
            // 
            btnSelectColor.Location = new Point(12, 87);
            btnSelectColor.Name = "btnSelectColor";
            btnSelectColor.Size = new Size(75, 23);
            btnSelectColor.TabIndex = 4;
            btnSelectColor.Text = "button2";
            btnSelectColor.UseVisualStyleBackColor = true;
            btnSelectColor.Click += btnSelectColor_Click;
            // 
            // lblSelectedColor
            // 
            lblSelectedColor.AutoSize = true;
            lblSelectedColor.Location = new Point(12, 113);
            lblSelectedColor.Name = "lblSelectedColor";
            lblSelectedColor.Size = new Size(38, 15);
            lblSelectedColor.TabIndex = 5;
            lblSelectedColor.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(206, 211);
            Controls.Add(lblSelectedColor);
            Controls.Add(btnSelectColor);
            Controls.Add(lblAltStatus);
            Controls.Add(button1);
            Controls.Add(btnstart);
            Controls.Add(lblStatus);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximumSize = new Size(222, 250);
            MinimumSize = new Size(222, 250);
            Name = "Form1";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Zethon SCT";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private Button btnstart;
        private Button button1;
        private Label lblAltStatus;
        private Button btnSelectColor;
        private ColorDialog colorDialog1;
        private Label lblSelectedColor;
    }
}
