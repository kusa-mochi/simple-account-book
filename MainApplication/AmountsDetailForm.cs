using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApplication
{
    /// <summary>
    /// 使うか分からない。
    /// </summary>
    public partial class AmountsDetailForm : Form
    {
        public AmountsDetailForm()
        {
            InitializeComponent();
        }

        private void AmountsDetailForm_Load(object sender, EventArgs e)
        {
            this.Capture = true;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            int x = 0;
            int y = 0;

            if(m.Msg == 0xca)
            {
                x = Cursor.Position.X;
                y = Cursor.Position.Y;

                if(
                    (x < this.Location.X) ||
                    (this.Location.X + this.Width < x) ||
                    (y < this.Location.Y) ||
                    (this.Location.Y + this.Height < y)
                    )
                {
                    this.Close();
                    return;
                }

                this.Capture = true;
            }
        }
    }
}
