using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace trigger_3
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        private Thread detectionThread;
        private bool detecting;

        private Form2 overlayForm;
        private int centerX;
        private int centerY;

        private Color targetColor = Color.Red; // Default target color
        private int colorTolerance = 100; // Adjust as needed for sensitivity

        bool mouseDown;
        private Point offset;

        public Form1()
        {
            InitializeComponent();
            overlayForm = new Form2(20); // Pass the radius parameter (e.g., 50) here
            overlayForm.Show();

            // Initialize center coordinates
            centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            centerY = Screen.PrimaryScreen.Bounds.Height / 2;

            string message = "\r\nPress Start to Activate the Detection\r\n\r\nChoose Color (yellow recommended)\r\n\r\nHold Down 'alt' to activate triggerbot\r\n\r\nDraggable window, put anywhere you want\r\n\r\nHave fun!";
            string title = "Instructions";
            MessageBox.Show(message, title);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!detecting)
            {
                detecting = true;
                detectionThread = new Thread(DetectColorChange);
                detectionThread.Start();
                lblStatus.Text = "Detecting.";
            }
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                targetColor = colorDialog.Color;
                // Optionally update UI to reflect selected color
                // For example, change the background color of a label to show the selected color
                lblSelectedColor.BackColor = targetColor;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            detecting = false;
            detectionThread?.Join();
            overlayForm.ClearCircle();
            lblStatus.Text = "Done";
        }

        private void DetectColorChange()
        {
            bool altPressed = false;
            int circleRadius = 25; // Adjust as needed for the radius of the detection circle

            while (detecting)
            {
                bool altCurrentlyPressed = GetAsyncKeyState(Keys.Menu) < 0;

                if (altCurrentlyPressed != altPressed)
                {
                    altPressed = altCurrentlyPressed;
                    Invoke(new Action(() => lblAltStatus.Text = altPressed ? "Ready" : "No Input"));
                }

                if (altPressed)
                {
                    // Capture only the region around the detection circle
                    Rectangle captureRect = new Rectangle(centerX - circleRadius, centerY - circleRadius, circleRadius * 2, circleRadius * 2);
                    Bitmap screenshot = new Bitmap(captureRect.Width, captureRect.Height);
                    using (Graphics g = Graphics.FromImage(screenshot))
                    {
                        g.CopyFromScreen(captureRect.Location, Point.Empty, captureRect.Size);
                    }

                    bool isTargetColorDetected = false;
                    int step = 3; // Skip pixels to reduce CPU usage

                    // Iterate through pixels within the circle
                    for (int x = 0; x < screenshot.Width; x += step)
                    {
                        for (int y = 0; y < screenshot.Height; y += step)
                        {
                            // Convert x, y to global coordinates
                            int globalX = x + centerX - circleRadius;
                            int globalY = y + centerY - circleRadius;

                            // Check if the pixel is within the circle
                            if (IsWithinCircle(globalX, globalY, centerX, centerY, circleRadius))
                            {
                                Color pixelColor = screenshot.GetPixel(x, y);

                                // Check if the pixel color is within the tolerance range of the target color
                                if (ColorWithinTolerance(pixelColor, targetColor, colorTolerance))
                                {
                                    isTargetColorDetected = true;
                                    break; // Break out of inner loop
                                }
                            }
                        }
                        if (isTargetColorDetected)
                            break; // Break out of outer loop
                    }

                    if (isTargetColorDetected)
                    {
                        // Perform a mouse click at the center of the screen
                        Cursor.Position = new Point(centerX, centerY);
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    }

                    // Update the circle position on the overlay form
                    Invoke(new Action(() => overlayForm.SetCirclePosition(centerX, centerY)));

                    // Sleep for a short duration to reduce CPU usage
                    Thread.Sleep(20); // Shorter sleep duration for quicker detection
                }
                else
                {
                    Invoke(new Action(() => lblAltStatus.Text = "No Input"));
                    Thread.Sleep(100); // Longer sleep duration when not active
                }
            }

            // Ensure the label is cleared when detection stops
            Invoke(new Action(() => lblAltStatus.Text = string.Empty));
        }

        private bool ColorWithinTolerance(Color color1, Color color2, int tolerance)
        {
            // Calculate the Euclidean distance between the colors
            int deltaR = Math.Abs(color1.R - color2.R);
            int deltaG = Math.Abs(color1.G - color2.G);
            int deltaB = Math.Abs(color1.B - color2.B);

            double distance = Math.Sqrt(deltaR * deltaR + deltaG * deltaG + deltaB * deltaB);

            // Check if the distance is within the tolerance range
            return distance <= tolerance;
        }

        private bool IsWithinCircle(int x, int y, int centerX, int centerY, int radius)
        {
            int dx = x - centerX;
            int dy = y - centerY;
            return (dx * dx + dy * dy) <= (radius * radius);
        }

        // Event handlers for preset color buttons
        private void btnRed_Click(object sender, EventArgs e)
        {
            targetColor = Color.Red;
            lblSelectedColor.BackColor = targetColor;
        }

        private void btnPurple_Click(object sender, EventArgs e)
        {
            targetColor = Color.Purple;
            lblSelectedColor.BackColor = targetColor;
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            targetColor = Color.Yellow;
            lblSelectedColor.BackColor = targetColor;
        }

        private void lblAltStatus_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.TopMost = true; // Ensure the form stays on top
        }
    }
}
