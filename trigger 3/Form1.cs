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

        private System.Windows.Forms.Timer animationTimer;
        private string targetText;
        private int animationStep;

        private Form2 overlayForm;
        private int centerX;
        private int centerY;

        Form2 form2; // Declare overlayForm as a field in your Form1 class

        private Color targetColor = Color.Red; // Default target color
        private int colorTolerance = 100; // Adjust as needed for sensitivity


        public Form1()
        {
            InitializeComponent();
            overlayForm = new Form2(20); // Pass the radius parameter (e.g., 50) here
            overlayForm.Show();

            // Initialize center coordinates
            centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            centerY = Screen.PrimaryScreen.Bounds.Height / 2;
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
            int circleRadius = 50; // Adjust as needed for the radius of the detection circle

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
                    // Capture the screen
                    Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    using (Graphics g = Graphics.FromImage(screenshot))
                    {
                        g.CopyFromScreen(0, 0, 0, 0, screenshot.Size);
                    }

                    // Flag to indicate if target color is detected
                    bool isTargetColorDetected = false;

                    // Iterate through all pixels within the screen bounds
                    for (int x = Math.Max(0, centerX - circleRadius); x < Math.Min(screenshot.Width, centerX + circleRadius); x++)
                    {
                        for (int y = Math.Max(0, centerY - circleRadius); y < Math.Min(screenshot.Height, centerY + circleRadius); y++)
                        {
                            // Check if the pixel is within the circle
                            if (overlayForm.IsWithinCircle(x, y))
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
                    Thread.Sleep(100);
                }
                else
                {
                    Invoke(new Action(() => lblAltStatus.Text = "No Input"));
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

    }
}
