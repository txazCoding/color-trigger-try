namespace trigger_3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblAltStatus;
        private System.Windows.Forms.Label lblSelectedColor;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnPurple;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.TrackBar trackBarRadius;
        private System.Windows.Forms.Label lblRadiusValue;

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
            trackBarRadius = new TrackBar();
            lblRadiusValue = new Label();
            lblStatus = new Label();
            lblAltStatus = new Label();
            lblSelectedColor = new Label();
            btnStart = new Button();
            btnRed = new Button();
            btnPurple = new Button();
            btnYellow = new Button();
            button1 = new Button();
            panel1 = new Panel();
            contextMenuStrip1 = new ContextMenuStrip(components);
            circleRadius = new TrackBar();
            label1 = new Label();
            btnStop = new Button();
            ((System.ComponentModel.ISupportInitialize)trackBarRadius).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)circleRadius).BeginInit();
            SuspendLayout();
            // 
            // trackBarRadius
            // 
            trackBarRadius.BackColor = Color.White;
            trackBarRadius.Location = new Point(9, 217);
            trackBarRadius.Maximum = 100;
            trackBarRadius.Minimum = 2;
            trackBarRadius.Name = "trackBarRadius";
            trackBarRadius.Size = new Size(200, 45);
            trackBarRadius.TabIndex = 0;
            trackBarRadius.TickStyle = TickStyle.None;
            trackBarRadius.Value = 25;
            trackBarRadius.Scroll += trackBarRadius_Scroll;
            // 
            // lblRadiusValue
            // 
            lblRadiusValue.AutoSize = true;
            lblRadiusValue.Location = new Point(12, 230);
            lblRadiusValue.Name = "lblRadiusValue";
            lblRadiusValue.Size = new Size(60, 15);
            lblRadiusValue.TabIndex = 1;
            lblRadiusValue.Text = "Radius: 25";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(3, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(42, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Status:";
            // 
            // lblAltStatus
            // 
            lblAltStatus.AutoSize = true;
            lblAltStatus.ForeColor = Color.White;
            lblAltStatus.Location = new Point(3, 24);
            lblAltStatus.Name = "lblAltStatus";
            lblAltStatus.Size = new Size(52, 15);
            lblAltStatus.TabIndex = 1;
            lblAltStatus.Text = "no Input";
            lblAltStatus.Click += lblAltStatus_Click;
            // 
            // lblSelectedColor
            // 
            lblSelectedColor.AutoSize = true;
            lblSelectedColor.BackColor = Color.White;
            lblSelectedColor.BorderStyle = BorderStyle.FixedSingle;
            lblSelectedColor.Location = new Point(93, 82);
            lblSelectedColor.Name = "lblSelectedColor";
            lblSelectedColor.Size = new Size(38, 17);
            lblSelectedColor.TabIndex = 2;
            lblSelectedColor.Text = "Color";
            lblSelectedColor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Location = new Point(12, 50);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(96, 29);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnRed
            // 
            btnRed.BackColor = Color.Transparent;
            btnRed.FlatStyle = FlatStyle.Flat;
            btnRed.ForeColor = Color.Black;
            btnRed.Location = new Point(12, 102);
            btnRed.Name = "btnRed";
            btnRed.Size = new Size(193, 23);
            btnRed.TabIndex = 5;
            btnRed.Text = "Red";
            btnRed.UseVisualStyleBackColor = false;
            btnRed.Click += btnRed_Click;
            // 
            // btnPurple
            // 
            btnPurple.BackColor = Color.Transparent;
            btnPurple.FlatStyle = FlatStyle.Flat;
            btnPurple.ForeColor = Color.Black;
            btnPurple.Location = new Point(12, 160);
            btnPurple.Name = "btnPurple";
            btnPurple.Size = new Size(193, 23);
            btnPurple.TabIndex = 6;
            btnPurple.Text = "Purple";
            btnPurple.UseVisualStyleBackColor = false;
            btnPurple.Click += btnPurple_Click;
            // 
            // btnYellow
            // 
            btnYellow.BackColor = Color.Transparent;
            btnYellow.FlatStyle = FlatStyle.Flat;
            btnYellow.Location = new Point(12, 131);
            btnYellow.Name = "btnYellow";
            btnYellow.Size = new Size(193, 23);
            btnYellow.TabIndex = 7;
            btnYellow.Text = "Yellow (recommended)";
            btnYellow.UseVisualStyleBackColor = false;
            btnYellow.Click += btnYellow_Click;
            // 
            // button1
            // 
            button1.ForeColor = Color.FromArgb(64, 64, 64);
            button1.Location = new Point(182, 12);
            button1.Name = "button1";
            button1.Size = new Size(24, 23);
            button1.TabIndex = 8;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(64, 64, 64);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(lblStatus);
            panel1.Controls.Add(lblAltStatus);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(219, 44);
            panel1.TabIndex = 9;
            panel1.Paint += panel1_Paint;
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseMove += panel1_MouseMove;
            panel1.MouseUp += panel1_MouseUp;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 197);
            label1.Name = "label1";
            label1.Size = new Size(164, 15);
            label1.TabIndex = 10;
            label1.Text = "hold 'alt' to activate detection";
            // 
            // btnStop
            // 
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Location = new Point(109, 50);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(96, 29);
            btnStop.TabIndex = 11;
            btnStop.Text = "Start";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // Form1
            // 
            BackColor = Color.White;
            ClientSize = new Size(219, 269);
            Controls.Add(btnStop);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(btnYellow);
            Controls.Add(btnPurple);
            Controls.Add(btnRed);
            Controls.Add(btnStart);
            Controls.Add(lblSelectedColor);
            Controls.Add(trackBarRadius);
            Controls.Add(lblRadiusValue);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Opacity = 0.6D;
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)trackBarRadius).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)circleRadius).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button button1;
        private Panel panel1;
        private ContextMenuStrip contextMenuStrip1;
        private TrackBar circleRadius;
        private TrackBar TrackBar_ValueChanged;
        private Label label1;
        private Button btnStop;
    }
}
