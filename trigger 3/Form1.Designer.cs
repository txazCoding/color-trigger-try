namespace trigger_3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblAltStatus;
        private System.Windows.Forms.Label lblSelectedColor;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPurple;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.TrackBar trackBarRadius;
        private System.Windows.Forms.Label lblRadiusValue;
        private System.Windows.Forms.Button btnSelectTriggerKey;
        private System.Windows.Forms.Label lblSelectedKey;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            trackBarTolerance = new TrackBar();
            lblToleranceValue = new Label();
            btnSelectTriggerKey = new Button();
            lblSelectedKey = new Label();
            trackBarRadius = new TrackBar();
            lblRadiusValue = new Label();
            lblStatus = new Label();
            lblAltStatus = new Label();
            lblSelectedColor = new Label();
            btnStart = new Button();
            btnPurple = new Button();
            btnYellow = new Button();
            button1 = new Button();
            panel1 = new Panel();
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            circleRadius = new TrackBar();
            btnRed = new Button();
            button2 = new Button();
            button3 = new Button();
            panel3 = new Panel();
            panel4 = new Panel();
            ((System.ComponentModel.ISupportInitialize)trackBarTolerance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRadius).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)circleRadius).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // trackBarTolerance
            // 
            trackBarTolerance.Location = new Point(17, 238);
            trackBarTolerance.Maximum = 255;
            trackBarTolerance.Name = "trackBarTolerance";
            trackBarTolerance.Size = new Size(92, 45);
            trackBarTolerance.TabIndex = 12;
            trackBarTolerance.TickStyle = TickStyle.None;
            trackBarTolerance.Value = 163;
            trackBarTolerance.Scroll += trackBarTolerance_Scroll;
            // 
            // lblToleranceValue
            // 
            lblToleranceValue.AutoSize = true;
            lblToleranceValue.Font = new Font("Tahoma", 9F);
            lblToleranceValue.Location = new Point(18, 260);
            lblToleranceValue.Name = "lblToleranceValue";
            lblToleranceValue.Size = new Size(38, 14);
            lblToleranceValue.TabIndex = 11;
            lblToleranceValue.Text = "label1";
            // 
            // btnSelectTriggerKey
            // 
            btnSelectTriggerKey.BackColor = Color.Transparent;
            btnSelectTriggerKey.FlatStyle = FlatStyle.Flat;
            btnSelectTriggerKey.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSelectTriggerKey.ForeColor = Color.Black;
            btnSelectTriggerKey.Location = new Point(119, 103);
            btnSelectTriggerKey.Name = "btnSelectTriggerKey";
            btnSelectTriggerKey.Size = new Size(86, 80);
            btnSelectTriggerKey.TabIndex = 0;
            btnSelectTriggerKey.Text = "Select Trigger Key";
            btnSelectTriggerKey.UseVisualStyleBackColor = false;
            btnSelectTriggerKey.Click += btnSelectTriggerKey_Click;
            // 
            // lblSelectedKey
            // 
            lblSelectedKey.AutoSize = true;
            lblSelectedKey.FlatStyle = FlatStyle.Flat;
            lblSelectedKey.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelectedKey.ForeColor = Color.Black;
            lblSelectedKey.Location = new Point(120, 190);
            lblSelectedKey.Name = "lblSelectedKey";
            lblSelectedKey.Size = new Size(59, 14);
            lblSelectedKey.TabIndex = 1;
            lblSelectedKey.Text = "Selected:";
            // 
            // trackBarRadius
            // 
            trackBarRadius.BackColor = Color.White;
            trackBarRadius.Location = new Point(16, 190);
            trackBarRadius.Maximum = 100;
            trackBarRadius.Minimum = 2;
            trackBarRadius.Name = "trackBarRadius";
            trackBarRadius.Size = new Size(93, 45);
            trackBarRadius.TabIndex = 0;
            trackBarRadius.TickStyle = TickStyle.None;
            trackBarRadius.Value = 25;
            trackBarRadius.Scroll += trackBarRadius_Scroll;
            // 
            // lblRadiusValue
            // 
            lblRadiusValue.AutoSize = true;
            lblRadiusValue.BackColor = Color.Transparent;
            lblRadiusValue.Font = new Font("Tahoma", 9F);
            lblRadiusValue.ForeColor = Color.Black;
            lblRadiusValue.Location = new Point(17, 214);
            lblRadiusValue.Name = "lblRadiusValue";
            lblRadiusValue.Size = new Size(63, 14);
            lblRadiusValue.TabIndex = 1;
            lblRadiusValue.Text = "Radius: 25";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Black;
            lblStatus.Location = new Point(120, 217);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(42, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Status:";
            // 
            // lblAltStatus
            // 
            lblAltStatus.AutoSize = true;
            lblAltStatus.ForeColor = Color.Black;
            lblAltStatus.Location = new Point(120, 239);
            lblAltStatus.Name = "lblAltStatus";
            lblAltStatus.Size = new Size(52, 15);
            lblAltStatus.TabIndex = 1;
            lblAltStatus.Text = "no Input";
            lblAltStatus.Click += lblAltStatus_Click;
            // 
            // lblSelectedColor
            // 
            lblSelectedColor.AutoSize = true;
            lblSelectedColor.BackColor = Color.Transparent;
            lblSelectedColor.BorderStyle = BorderStyle.FixedSingle;
            lblSelectedColor.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelectedColor.ForeColor = Color.Black;
            lblSelectedColor.Location = new Point(144, 15);
            lblSelectedColor.Name = "lblSelectedColor";
            lblSelectedColor.Size = new Size(39, 18);
            lblSelectedColor.TabIndex = 2;
            lblSelectedColor.Text = "Color";
            lblSelectedColor.TextAlign = ContentAlignment.MiddleCenter;
            lblSelectedColor.Click += lblSelectedColor_Click;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.Transparent;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStart.ForeColor = Color.Black;
            btnStart.Location = new Point(12, 50);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(193, 47);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            btnStart.MouseEnter += HoverButton_MouseEnter;
            btnStart.MouseLeave += HoverButton_MouseLeave;
            // 
            // btnPurple
            // 
            btnPurple.BackColor = Color.Transparent;
            btnPurple.FlatStyle = FlatStyle.Flat;
            btnPurple.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPurple.ForeColor = Color.Black;
            btnPurple.Location = new Point(12, 160);
            btnPurple.Name = "btnPurple";
            btnPurple.Size = new Size(101, 23);
            btnPurple.TabIndex = 6;
            btnPurple.Text = "Purple";
            btnPurple.UseVisualStyleBackColor = false;
            btnPurple.Click += btnPurple_Click;
            btnPurple.MouseEnter += HoverButton_MouseEnterPurple;
            btnPurple.MouseLeave += HoverButton_MouseLeavePurple;
            // 
            // btnYellow
            // 
            btnYellow.BackColor = Color.Transparent;
            btnYellow.FlatStyle = FlatStyle.Flat;
            btnYellow.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnYellow.ForeColor = Color.Black;
            btnYellow.Location = new Point(12, 131);
            btnYellow.Name = "btnYellow";
            btnYellow.Size = new Size(101, 23);
            btnYellow.TabIndex = 7;
            btnYellow.Text = "Yellow";
            btnYellow.UseVisualStyleBackColor = false;
            btnYellow.Click += btnYellow_Click;
            btnYellow.MouseEnter += HoverButton_MouseEnterYellow;
            btnYellow.MouseLeave += HoverButton_MouseLeaveYellow;
            // 
            // button1
            // 
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
            button1.Location = new Point(185, 12);
            button1.Name = "button1";
            button1.Size = new Size(24, 23);
            button1.TabIndex = 8;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(lblSelectedColor);
            panel1.Location = new Point(-5, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(230, 44);
            panel1.TabIndex = 9;
            panel1.Paint += panel1_Paint;
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseMove += panel1_MouseMove;
            panel1.MouseUp += panel1_MouseUp;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Viner Hand ITC", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(16, 12);
            label1.Name = "label1";
            label1.Size = new Size(65, 26);
            label1.TabIndex = 9;
            label1.Text = "Zethon";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // circleRadius
            // 
            circleRadius.Location = new Point(0, 0);
            circleRadius.Name = "circleRadius";
            circleRadius.Size = new Size(104, 45);
            circleRadius.TabIndex = 0;
            // 
            // btnRed
            // 
            btnRed.BackColor = Color.Transparent;
            btnRed.FlatStyle = FlatStyle.Flat;
            btnRed.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRed.ForeColor = Color.Black;
            btnRed.Location = new Point(12, 103);
            btnRed.Name = "btnRed";
            btnRed.Size = new Size(101, 23);
            btnRed.TabIndex = 10;
            btnRed.Text = "Red";
            btnRed.UseVisualStyleBackColor = false;
            btnRed.Click += btnRed_Click;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Tahoma", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            button2.Location = new Point(119, 260);
            button2.Name = "button2";
            button2.Size = new Size(42, 23);
            button2.TabIndex = 14;
            button2.Text = "UI";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Tahoma", 6.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            button3.Location = new Point(43, 46);
            button3.Name = "button3";
            button3.Size = new Size(42, 23);
            button3.TabIndex = 15;
            button3.Text = "Game";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(button3);
            panel3.Location = new Point(119, 213);
            panel3.Name = "panel3";
            panel3.Size = new Size(87, 71);
            panel3.TabIndex = 14;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Location = new Point(12, 186);
            panel4.Name = "panel4";
            panel4.Size = new Size(101, 98);
            panel4.TabIndex = 16;
            // 
            // Form1
            // 
            BackColor = Color.White;
            ClientSize = new Size(219, 295);
            Controls.Add(button2);
            Controls.Add(lblStatus);
            Controls.Add(lblAltStatus);
            Controls.Add(lblToleranceValue);
            Controls.Add(btnRed);
            Controls.Add(lblSelectedKey);
            Controls.Add(btnSelectTriggerKey);
            Controls.Add(panel1);
            Controls.Add(btnYellow);
            Controls.Add(btnPurple);
            Controls.Add(btnStart);
            Controls.Add(lblRadiusValue);
            Controls.Add(trackBarRadius);
            Controls.Add(trackBarTolerance);
            Controls.Add(panel3);
            Controls.Add(panel4);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Opacity = 0.6D;
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            Load += Form1_Load_1;
            ((System.ComponentModel.ISupportInitialize)trackBarTolerance).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRadius).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)circleRadius).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Button button1;
        private Panel panel1;
        private ContextMenuStrip contextMenuStrip1;
        private TrackBar circleRadius;
        private TrackBar TrackBar_ValueChanged;
        private Button btnRed;
        private Label lblToleranceValue;
        private TrackBar trackBarTolerance;
        private Label label1;
        private Button button2;
        private Button button3;
        private Panel panel3;
        private Panel panel4;
    }
}
