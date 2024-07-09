using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace trigger_3
{
    public partial class Form1 : Form
    {
        // Raw Input constants
        private const int RIM_TYPEMOUSE = 0;
        private const int RID_INPUT = 0x10000003;

        [StructLayout(LayoutKind.Sequential)]
        struct RAWINPUTHEADER
        {
            public int dwType;
            public int dwSize;
            public IntPtr hDevice;
            public IntPtr wParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct RAWMOUSE
        {
            public ushort usFlags;
            public uint ulButtons;
            public ushort usButtonFlags;
            public ushort usButtonData;
            public uint ulRawButtons;
            public int lLastX;
            public int lLastY;
            public uint ulExtraInformation;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct RAWINPUT
        {
            [FieldOffset(0)]
            public RAWINPUTHEADER header;
            [FieldOffset(16)]
            public RAWMOUSE mouse;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public int dwFlags;
            public IntPtr hwndTarget;
        }

        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private const int INPUT_MOUSE = 0;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        private Thread detectionThread;
        private bool detecting;

        private Overlayform overlayForm;
        private int centerX;
        private int centerY;

        private Color targetColor = Color.Red; // Default target color
        private int colorTolerance = 163; // Adjust as needed for sensitivity

        bool mouseDown;
        private Point offset;





        public Form1()
        {
            InitializeComponent();
            overlayForm = new Overlayform(25); // Default radius
            overlayForm.Show();

            // Initialize center coordinates
            centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            centerY = Screen.PrimaryScreen.Bounds.Height / 2;

            string message = "\r\nPress Start to Activate the Detection\r\n\r\nChoose Color (yellow recommended)\r\n\r\nHold Down 'alt' to activate triggerbot\r\n\r\nDraggable window, put anywhere you want\r\n\r\nHave fun!";
            string title = "Instructions";
            MessageBox.Show(message, title);
        }






        private void trackBarRadius_Scroll(object sender, EventArgs e)
        {
            int radiusValue = trackBarRadius.Value; // Get the current radius from the trackBar
            lblRadiusValue.Text = $"Radius: {radiusValue}";

            // Update the circle radius in overlayForm
            overlayForm.SetCircleRadius(radiusValue);
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
            int circleRadius = 0;

            while (detecting)
            {
                Invoke(new Action(() => circleRadius = trackBarRadius.Value)); // Get the current radius from the trackBar safely

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
                    int step = 1; // Skip pixels to reduce CPU usage


                    // Iterate through pixels within the screenshot
                    for (int x = 0; x < screenshot.Width; x += step)
                    {
                        for (int y = 0; y < screenshot.Height; y += step)
                        {
                            // Calculate local coordinates
                            int localX = x - screenshot.Width / 2;
                            int localY = y - screenshot.Height / 2;

                            // Check if the pixel is within the circle
                            if (localX * localX + localY * localY <= circleRadius * circleRadius)
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
                        // Bring the game window to the foreground
                        IntPtr gameWindow = GetForegroundWindow(); // Assuming the game window is already in the foreground
                        if (gameWindow != IntPtr.Zero)
                        {
                            SetForegroundWindow(gameWindow);
                            Thread.Sleep(50); // Give some time for the window to come to the foreground

                            // Perform a mouse click at the center of the screen
                            Cursor.Position = new Point(centerX, centerY);
                            SimulateMouseClick();
                        }
                    }

                     // Update the circle position on the overlay form
                    Invoke(new Action(() => overlayForm.SetCirclePosition(centerX, centerY)));

                    // Sleep for a short duration to reduce CPU usage
                    Thread.Sleep(1); // Shorter sleep duration for quicker detection
                }
                else
                {
                    Invoke(new Action(() => lblAltStatus.Text = "No Input"));
                    Thread.Sleep(10); // Longer sleep duration when not active
                }
            }

            // Ensure the label is cleared when detection stops
            Invoke(new Action(() => lblAltStatus.Text = string.Empty));
        }




        private void SimulateMouseClick()
        {
            INPUT[] inputs = new INPUT[2];

            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi = new MOUSEINPUT
            {
                dwFlags = MOUSEEVENTF_LEFTDOWN
            };

            inputs[1].type = INPUT_MOUSE;
            inputs[1].mi = new MOUSEINPUT
            {
                dwFlags = MOUSEEVENTF_LEFTUP
            };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
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

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public int type;
            public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
    }
}
