using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            numOfCond = int.Parse(CondBox.Text);
            numOfX = int.Parse(XBox.Text);
            Hide();
        }
    }
}
