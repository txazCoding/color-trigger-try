using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace trigger_3
{
    public partial class Form1 : Form
    {
        private const int RIM_TYPEMOUSE = 0;
        private const int RID_INPUT = 0x10000003;
        private System.Windows.Forms.Timer fadeTimer;
        private const int FadeDuration = 40; // Duration in milliseconds per step
        private const double FadeStep = 0.05; // Step for increasing opacity

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

        private Color targetColor = Color.Red; // default color thing
        private int colorTolerance = 158; // default sens

        private bool mouseDown;
        private Point offset;

        private Keys triggerKey = Keys.Menu;

        private static readonly Color LightRed = Color.FromArgb(255, 200, 200);
        private static readonly Color LightYellow = Color.FromArgb(255, 255, 200);
        private static readonly Color LightPurple = Color.FromArgb(230, 200, 255);

        private System.Windows.Forms.Timer hoverTimer;
        private Button currentHoveredButton;
        private Color targetHoverColor;
        private int animationStep;
        private const int AnimationSteps = 10;

        public Form1()
        {
            InitializeComponent();
            overlayForm = new Overlayform(25); // Default radius
            overlayForm.Show();

            centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            centerY = Screen.PrimaryScreen.Bounds.Height / 2;

            btnStart.MouseEnter += HoverButton_MouseEnter;
            btnStart.MouseLeave += HoverButton_MouseLeave;

            btnRed.MouseEnter += HoverButton_MouseEnterRed;
            btnRed.MouseLeave += HoverButton_MouseLeaveRed;

            btnYellow.MouseEnter += HoverButton_MouseEnterYellow;
            btnYellow.MouseLeave += HoverButton_MouseLeaveYellow;

            btnPurple.MouseEnter += HoverButton_MouseEnterPurple;
            btnPurple.MouseLeave += HoverButton_MouseLeavePurple;

            lblToleranceValue.Text = $"tolerance: {colorTolerance}";

            hoverTimer = new System.Windows.Forms.Timer();
            hoverTimer.Interval = 45;
            hoverTimer.Tick += HoverTimer_Tick;

            // Set initial opacity and start the fade-in effect
            this.Opacity = 0;
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = FadeDuration;
            fadeTimer.Tick += FadeTimer_Tick;
            fadeTimer.Start();
        }

        private const double MaxOpacity = 0.7; // Maximum opacity

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < MaxOpacity)
            {
                this.Opacity += FadeStep;
            }
            else
            {
                fadeTimer.Stop();
            }
        }

        private void HoverTimer_Tick(object sender, EventArgs e)
        {
            if (currentHoveredButton == null) return;

            int r = (targetHoverColor.R - currentHoveredButton.BackColor.R) / AnimationSteps;
            int g = (targetHoverColor.G - currentHoveredButton.BackColor.G) / AnimationSteps;
            int b = (targetHoverColor.B - currentHoveredButton.BackColor.B) / AnimationSteps;

            currentHoveredButton.BackColor = Color.FromArgb(
                currentHoveredButton.BackColor.R + r,
                currentHoveredButton.BackColor.G + g,
                currentHoveredButton.BackColor.B + b);

            animationStep++;
            if (animationStep >= AnimationSteps)
            {
                hoverTimer.Stop();
                animationStep = 0;
            }
        }

        private void HoverButton_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, Color.LightBlue);
            }
        }

        private void HoverButton_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, SystemColors.Control);
                btn.BackColor = Color.White;
            }
        }

        private void HoverButton_MouseEnterRed(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, LightRed);
            }
        }

        private void HoverButton_MouseLeaveRed(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, SystemColors.Control);
                btn.BackColor = Color.White;
            }
        }

        private void HoverButton_MouseEnterYellow(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, LightYellow);
            }
        }

        private void HoverButton_MouseLeaveYellow(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, SystemColors.Control);
                btn.BackColor = Color.White;
            }
        }

        private void HoverButton_MouseEnterPurple(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, LightPurple);
            }
        }

        private void HoverButton_MouseLeavePurple(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, SystemColors.Control);
                btn.BackColor = Color.White;
            }
        }

        private void StartHoverAnimation(Button btn, Color targetColor)
        {
            currentHoveredButton = btn;
            targetHoverColor = targetColor;
            animationStep = 0;
            hoverTimer.Start();
        }

        private void trackBarRadius_Scroll(object sender, EventArgs e)
        {
            int radiusValue = trackBarRadius.Value;
            lblRadiusValue.Text = $"Radius: {radiusValue}";

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

        public void btnSelectColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                targetColor = colorDialog.Color;

                lblSelectedColor.BackColor = targetColor;
            }
        }

        public void btnSelectTriggerKey_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Text = "Press a key...";

            KeyCaptureForm keyCaptureForm = new KeyCaptureForm();
            if (keyCaptureForm.ShowDialog() == DialogResult.OK)
            {
                triggerKey = keyCaptureForm.SelectedKey;
                lblSelectedKey.Text = $"Key: {triggerKey}";
            }

            btn.Text = "Select Trigger Key";
        }

        private void DetectColorChange()
        {
            bool triggerKeyPressed = false;
            int circleRadius = 25;

            while (detecting)
            {
                Invoke(new Action(() => circleRadius = trackBarRadius.Value));

                bool triggerKeyCurrentlyPressed = GetAsyncKeyState(triggerKey) < 0;

                if (triggerKeyCurrentlyPressed != triggerKeyPressed)
                {
                    triggerKeyPressed = triggerKeyCurrentlyPressed;
                    Invoke(new Action(() => lblAltStatus.Text = triggerKeyPressed ? "Ready" : "No Input"));
                }

                if (triggerKeyPressed)
                {
                    Rectangle captureRect = new Rectangle(centerX - circleRadius, centerY - circleRadius, circleRadius * 2, circleRadius * 2);
                    Bitmap screenshot = new Bitmap(captureRect.Width, captureRect.Height);
                    using (Graphics g = Graphics.FromImage(screenshot))
                    {
                        g.CopyFromScreen(captureRect.Location, Point.Empty, captureRect.Size);
                    }

                    bool isTargetColorDetected = false;
                    int step = 1; // Skip pixels to reduce CPU usage

                    for (int x = 0; x < screenshot.Width; x += step)
                    {
                        for (int y = 0; y < screenshot.Height; y += step)
                        {
                            int localX = x - screenshot.Width / 2;
                            int localY = y - screenshot.Height / 2;

                            // Check if the pixel is within the circle
                            if (localX * localX + localY * localY <= circleRadius * circleRadius)
                            {
                                Color pixelColor = screenshot.GetPixel(x, y);

                                if (ColorWithinTolerance(pixelColor, targetColor, colorTolerance))
                                {
                                    isTargetColorDetected = true;
                                    break;
                                }
                            }
                        }
                        if (isTargetColorDetected)
                            break;
                    }

                    if (isTargetColorDetected)
                    {
                        // Bring the game window to the foreground
                        IntPtr gameWindow = GetForegroundWindow();
                        if (gameWindow != IntPtr.Zero)
                        {
                            SetForegroundWindow(gameWindow);
                            Thread.Sleep(50);

                            //mouse click at the center of the screen
                            Cursor.Position = new Point(centerX, centerY);
                            SimulateMouseClick();
                        }
                    }

                    Invoke(new Action(() => overlayForm.SetCirclePosition(centerX, centerY)));

                    Thread.Sleep(1); // Shorter sleep duration for quicker detection
                }
                else
                {
                    Invoke(new Action(() => lblAltStatus.Text = "No Input"));
                    Thread.Sleep(10); // Longer sleep duration when not active
                }
            }

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
            int deltaR = Math.Abs(color1.R - color2.R);
            int deltaG = Math.Abs(color1.G - color2.G);
            int deltaB = Math.Abs(color1.B - color2.B);

            double distance = Math.Sqrt(deltaR * deltaR + deltaG * deltaG + deltaB * deltaB);

            return distance <= tolerance;
        }

        private bool IsWithinCircle(int x, int y, int centerX, int centerY, int radius)
        {
            int dx = x - centerX;
            int dy = y - centerY;
            return (dx * dx + dy * dy) <= (radius * radius);
        }

        public void btnRed_Click(object sender, EventArgs e)
        {
            targetColor = Color.Red;
            lblSelectedColor.BackColor = Color.IndianRed;
            lblSelectedColor.Text = "Color: Red";
        }

        public void btnPurple_Click(object sender, EventArgs e)
        {
            targetColor = Color.Purple;
            lblSelectedColor.BackColor = Color.MediumPurple;
            lblSelectedColor.ForeColor = Color.WhiteSmoke;
            lblSelectedColor.Text = "Color: Purple";
        }

        public void btnYellow_Click(object sender, EventArgs e)
        {
            targetColor = Color.Yellow;
            lblSelectedColor.BackColor = LightYellow;
            lblSelectedColor.ForeColor = Color.Black;
            lblSelectedColor.Text = "Color: Yellow";
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
            this.TopMost = true; // the form stays on top
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
        }

        private void trackBarTolerance_Scroll(object sender, EventArgs e)
        {
            colorTolerance = trackBarTolerance.Value; // Update  tolerance value
            lblToleranceValue.Text = $"Tolerance: {colorTolerance}";
        }

        private void lblSelectedColor_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string message = "Press Start to Activate the Detection\r\n\r\nChoose Color (yellow recommended)\r\n\r\nhold down the set button to activate detection\r\n\r\nDraggable window, put anywhere you want\r\n\r\nHave fun!";
            string title = "UI Settings";
            MessageBox.Show(message, title);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string message2 = "Fullscreen recomended because game might lag when in windowed(working on a fix rn)\r\n\r\nTurn off RawInputBuffer\r\n\r\ncircle is not visible ingame when fullscreened\r\n\r\nHave fun!";
            string title2 = "VALORANT settings";
            MessageBox.Show(message2, title2);
        }


        private void UIHelp_MouseEnter_1(object sender, EventArgs e)
        {
            UIHelp.Text = "Help";

        }

        private void UIHelp_MouseLeave(object sender, EventArgs e)
        {
            UIHelp.Text = "UI";

        }

        private void GameHelp_MouseEnter(object sender, EventArgs e)
        {
            GameHelp.Text = "Help";
        }


        private void GameHelp_MouseLeave(object sender, EventArgs e)
        {
            GameHelp.Text = "Game";

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, Color.Red);
            }
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                StartHoverAnimation(btn, SystemColors.Control);
                btn.BackColor = Color.LightPink;
            }
        }

        private void lblSelectedKey_Click(object sender, EventArgs e)
        {

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
