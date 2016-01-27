using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 表示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = true;
        }

        private void 非表示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
