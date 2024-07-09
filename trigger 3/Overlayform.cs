using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace trigger_3
{
    public partial class Overlayform : Form
    {
        private int radius;
        private int centerX;
        private int centerY;
        private int circleRadius;
        private bool drawCircle = false;

        public Overlayform(int initialRadius)
        {
            this.radius = initialRadius;
            this.circleRadius = radius;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Turquoise; // Use a color that is not used in your circle
            this.TransparencyKey = this.BackColor;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Maximized;
            this.circleRadius = radius;
            circleRadius = initialRadius;

            InitializeComponent();
            circleRadius = initialRadius;

            // Set the form to be click-through
            SetFormTransparent();
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;

        private void SetFormTransparent()
        {
            int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            exStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT;
            SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle);
        }

        public void SetCircleRadius(int radius)
        {
            circleRadius = radius;
            Invalidate(); // This will force the form to redraw, including the circle with the new radius
        }

        public void SetCirclePosition(int x, int y)
        {
            centerX = x;
            centerY = y;
            drawCircle = true;
            this.Invalidate();
        }

        public void ClearCircle()
        {
            drawCircle = false;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (drawCircle)
            {
                // Draw the circle with the updated radius
                using (Pen pen = new Pen(Color.Blue, 1))
                {
                    e.Graphics.DrawEllipse(pen, centerX - circleRadius, centerY - circleRadius, circleRadius * 2, circleRadius * 2);
                }
            }
        }

        public bool IsWithinCircle(int x, int y)
        {
            // Check if the point (x, y) is within the circle
            double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
            return distance <= circleRadius;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
