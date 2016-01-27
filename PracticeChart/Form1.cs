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

namespace PracticeChart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.chart1.Series.Clear();
            this.chart1.Titles.Clear();
            this.chart1.Series.Add("a");
            this.chart1.Series["a"].ChartType = SeriesChartType.Doughnut;
            this.chart1.Series["a"].Label = "てすとらべる";
            this.chart1.Series["a"]["PieStartAngle"] = "270";

            int idx = this.chart1.Series["a"].Points.AddXY(0, 10);
            this.chart1.Series["a"].Points[idx].Name = "ねーむ";
            this.chart1.Series["a"].Points[idx].Label = "家賃 77,000円";

            idx = this.chart1.Series["a"].Points.AddXY(0, 30);
            this.chart1.Series["a"].Points[idx].Name = "ねーむ2";
            this.chart1.Series["a"].Points[idx].Label = "食料品 25,000円";

            idx = this.chart1.Series["a"].Points.AddXY(0, 40);
            this.chart1.Series["a"].Points[idx].Name = "ねーむ3";
            this.chart1.Series["a"].Points[idx].Label = "外食 15,000円";

            this.chart1.Titles.Add("たいとる");
            this.chart1.Legends.Clear();
            this.chart1.ChartAreas[0].BackColor = SystemColors.Control;
        }
    }
}
