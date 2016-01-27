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

namespace PracticeLineOnChart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.chart1.Series.Clear();

            DataTable dt = new DataTable();
            dt.Columns.Add("col1", 1.GetType());
            dt.Columns.Add("col2", "".GetType());
            dt.Columns.Add("col3", "".GetType());

            for (int i = 0; i < 10; i++)
            {
                
                dt.Rows.Add(10 * i, "2015/09", (10 * i).ToString());
                dt.Rows.Add(140 / (i + 1), "2015/10", (10 * i).ToString());
                dt.Rows.Add(7 * i, "2015/11", (10 * i).ToString());
            }

            DataView dv = new DataView(dt);
            dv.Sort = "col2 asc";
            this.chart1.DataManipulator.FilterSetEmptyPoints = true;
            this.chart1.DataManipulator.FilterMatchedPoints = true;
            this.chart1.DataBindCrossTable(dv, "col3", "col2", "col1", "Label=col1");

            foreach (Series s in this.chart1.Series)
            {
                this.chart1.DataManipulator.Filter(CompareMethod.EqualTo, 0, s);
                s.ChartType = SeriesChartType.StackedColumn;
                s.Font = new Font("メイリオ", 11, FontStyle.Regular);
            }

            // ここから目標金額ラインの描画処理
            Series ser = new Series();
            ser.LegendText = "目標金額";
            ser.ChartType = SeriesChartType.Line;
            ser.Color = Color.Red;
            ser.BorderDashStyle = ChartDashStyle.Dot;
            ser.BorderWidth = 5;
            ser.XAxisType = AxisType.Secondary;
            ser.YAxisType = AxisType.Primary;
            ser.Points.AddXY(0, 300);
            ser.Points.AddXY(1, 300);
            this.chart1.Series.Add(ser);
            this.chart1.ChartAreas[0].AxisX2.Minimum = 0;
            this.chart1.ChartAreas[0].AxisX2.Maximum = 1;
            this.chart1.ChartAreas[0].AxisX2.LabelStyle.Enabled = false;
            // ここまで目標金額ラインの描画処理
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }
    }
}
