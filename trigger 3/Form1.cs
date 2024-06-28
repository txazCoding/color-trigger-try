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

                    // Define the target red sensitivity
                    int redThreshold = 100; // Adjust as needed for sensitivity
                    int redDifferenceThreshold = 50; // Minimum difference between red and green/blue

                    bool isRedDetected = false;

                    // Iterate through all pixels within the screen bounds
                    for (int x = 0; x < screenshot.Width; x++)
                    {
                        for (int y = 0; y < screenshot.Height; y++)
                        {
                            // Check if the pixel is within the circle
                            if (overlayForm.IsWithinCircle(x, y))
                            {
                                Color pixelColor = screenshot.GetPixel(x, y);

                                // Check if the color is some kind of red
                                bool isRed = pixelColor.R > redThreshold &&
                                             (pixelColor.R - pixelColor.G) > redDifferenceThreshold &&
                                             (pixelColor.R - pixelColor.B) > redDifferenceThreshold;

                                if (isRed)
                                {
                                    isRedDetected = true;
                                    break;
                                }
                            }
                        }
                        if (isRedDetected)
                            break;
                    }

                    // Debugging: Print if red is detected
                    Console.WriteLine($"Red Detected: {isRedDetected}");

                    if (isRedDetected)
                    {
                        // Perform a mouse click at the center of the screen
                        int centerX = Screen.PrimaryScreen.Bounds.Width / 2;
                        int centerY = Screen.PrimaryScreen.Bounds.Height / 2;
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

    }
}
