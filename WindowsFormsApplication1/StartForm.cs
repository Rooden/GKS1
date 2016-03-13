using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class StartForm : Form
    {
        public int numOfCond;
        public int numOfX;

        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            XBox.Select();
            XBox.Text = string.Empty;
            CondBox.Text = string.Empty;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                numOfCond = int.Parse(CondBox.Text);
                numOfX = int.Parse(XBox.Text);

                if (numOfX == 0 || numOfX > 7)
                    throw new OverflowException();

                Hide();
            }
            catch (FormatException)
            {
                MessageBox.Show("Enter both numbers!", "Error!");
            }
            catch (OverflowException)
            {
                MessageBox.Show("X number must be between 1 and 7", "Error!");
            }
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                Application.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}