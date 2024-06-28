using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace trigger_3
{
    public partial class Form2 : Form
    {

        private int centerX;
        private int centerY;
        private int circleRadius;
        private bool drawCircle = false;

        public Form2(int radius)
        {
            this.circleRadius = radius;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Magenta; // Use a color that is not used in your circle
            this.TransparencyKey = this.BackColor;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Maximized;
            this.circleRadius = radius;
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
                using (Graphics g = e.Graphics)
                {
                    Pen pen = new Pen(Color.Blue, 1); // Blue circle with 2 pixel thickness
                    g.DrawEllipse(pen, centerX - circleRadius, centerY - circleRadius, circleRadius * 2, circleRadius * 2);
                }
            }
        }

        public bool IsWithinCircle(int x, int y)
        {
            // Check if the point (x, y) is within the circle
            double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
            return distance <= circleRadius;
        }

    }
}
