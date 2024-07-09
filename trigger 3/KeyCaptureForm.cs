using System;
using System.Windows.Forms;

namespace trigger_3
{
    public partial class KeyCaptureForm : Form
    {
        public Keys SelectedKey { get; private set; }

        public KeyCaptureForm()
        {
            InitializeComponent();
        }

        private void KeyCaptureForm_KeyDown(object sender, KeyEventArgs e)
        {
            SelectedKey = e.KeyCode;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void KeyCaptureForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += KeyCaptureForm_KeyDown;
        }
    }
}
