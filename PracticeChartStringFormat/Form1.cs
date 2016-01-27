using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PracticeChartStringFormat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.chart1.Series.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("col1", 1.GetType());
            dt.Columns.Add("col2", 1.GetType());
            dt.Columns.Add("col3", "".GetType());
            dt.Rows.Add(1000, 1, "label 1");
            dt.Rows.Add(3000, 1, "label 2");
            dt.Rows.Add(1000, 1, "label 3");
            dt.Rows.Add(1000, 2, "label 1");
            dt.Rows.Add(0, 2, "label 2");
            dt.Rows.Add(5000, 2, "label 3");
            dt.Rows.Add(1000, 3, "label 1");
            dt.Rows.Add(1000, 3, "label 2");
            dt.Rows.Add(2000, 3, "label 3");
            dt.Rows.Add(1000, 4, "label 1");
            dt.Rows.Add(3000, 4, "label 2");
            dt.Rows.Add(7000, 4, "label 3");
            DataView dv = new DataView(dt);
            dv.Sort = "col2 asc";
            this.chart1.DataManipulator.FilterSetEmptyPoints = true;
            this.chart1.DataManipulator.FilterMatchedPoints = true;
            this.chart1.DataBindCrossTable(dv, "col3", "col2", "col1", "Label=col1");
            foreach (ChartArea a in this.chart1.ChartAreas)
            {
                a.AxisY.LabelStyle.Format = "C";
            }
            foreach (Series s in this.chart1.Series)
            {
                this.chart1.DataManipulator.Filter(CompareMethod.EqualTo, 0, s);
                s.ChartType = SeriesChartType.StackedColumn;
                s.Font = new Font("メイリオ", 11, FontStyle.Regular);
                s.Label = "Y = #VALY{0:#,#}";
            }
        }

        private void chart1_FormatNumber(object sender, FormatNumberEventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            foreach (Series s in this.chart1.Series)
            {
                s.Label = "Y = #VALY{0:#,#}";
            }
        }
    }
}
