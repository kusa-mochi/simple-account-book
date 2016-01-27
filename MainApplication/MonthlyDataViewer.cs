using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MainApplication
{
    public partial class MonthlyDataViewer : Form
    {
        private MonthlyDataViewerSettings _settings;
        private DataManager _dataManager;

        public MonthlyDataViewer(DataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("data");
            }

            InitializeComponent();

            _dataManager = dataManager;

            // DBから初期設定情報を読み込む。
            _settings = _dataManager.GetMonthlyDataViewerSettings();

            this.RemoveValueChangedEventHandler();

            this.comboBox_Month.SelectedIndex = _settings.month.Month - 1;

            this.chart_Income.Controls.Add(this.label_Income);
            this.label_Income.Top = this.chart_Income.Top + ((this.chart_Income.Height - this.label_Income.Height) / 2) - 45;
            this.label_Income.Left = this.chart_Income.Left + ((this.chart_Income.Width - this.label_Income.Width) / 2) - 23;
            this.chart_Spending.Controls.Add(this.label_Spending);
            this.label_Spending.Top = this.label_Income.Top;

            this.button_PrevMonth.BackColor = CommonConst.PrevNextMonthButtonColor;
            this.button_NextMonth.BackColor = CommonConst.PrevNextMonthButtonColor;
            this.AdjustButtonShape();

            this.numericUpDown_Year.BackColor = CommonConst.MonthControlButtonColor;
            this.comboBox_Month.BackColor = CommonConst.MonthControlButtonColor;
        }

        #region プライベートメソッド

        private void AdjustButtonShape()
        {
            this.AdjustPrevButtonShape();
            this.AdjustNextButtonShape();
        }

        private void AdjustPrevButtonShape()
        {
            GraphicsPath gPath = new GraphicsPath(
                new PointF[]
                {
                    new PointF(30.0f, 5.0f),
                    new PointF(5.0f, 17.5f),
                    new PointF(30.0f, 30.0f)
                },
                new byte[]
                {
                    (byte)PathPointType.Line,
                    (byte)PathPointType.Line,
                    (byte)PathPointType.Line
                },
                FillMode.Winding
            );

            this.button_PrevMonth.Width = 100;
            this.button_PrevMonth.Height = 100;
            this.button_PrevMonth.Region = new Region(gPath);
        }

        private void AdjustNextButtonShape()
        {
            GraphicsPath gPath = new GraphicsPath(
                new PointF[]
                {
                    new PointF(5.0f, 5.0f),
                    new PointF(30.0f, 17.5f),
                    new PointF(5.0f, 30.0f)
                },
                new byte[]
                {
                    (byte)PathPointType.Line,
                    (byte)PathPointType.Line,
                    (byte)PathPointType.Line
                },
                FillMode.Winding
            );

            this.button_NextMonth.Width = 100;
            this.button_NextMonth.Height = 100;
            this.button_NextMonth.Region = new Region(gPath);
        }

        /// <summary>
        /// 渡された1ヶ月分の家計データをドーナツグラフに描画する。
        /// </summary>
        /// <param name="data"></param>
        private void DrawMonthlyData(DateTime month)
        {
            MonthlyData data = _dataManager.GetMonthlyData(month);

            this.label_Income.Visible = data.existIncomeData;
            this.label_Spending.Visible = data.existSpendingData;

            this.chart_Income.Series.Clear();
            this.chart_Spending.Series.Clear();

            this.chart_Income.Series.Add("chart");
            this.chart_Spending.Series.Add("chart");

            Series incomeSeries = this.chart_Income.Series["chart"];
            Series spendingSeries = this.chart_Spending.Series["chart"];

            incomeSeries.ChartType = SeriesChartType.Doughnut;
            spendingSeries.ChartType = SeriesChartType.Doughnut;

            incomeSeries.Font = new Font("メイリオ", 10, FontStyle.Regular);
            spendingSeries.Font = new Font("メイリオ", 10, FontStyle.Regular);

            incomeSeries["PieStartAngle"] = "270";
            spendingSeries["PieStartAngle"] = "270";

            int idx = 0;

            foreach (Payments p in data.incomes)
            {
                idx = incomeSeries.Points.AddXY(0, p.GetSum());
                incomeSeries.Points[idx].Name = p.label;
                incomeSeries.Points[idx].Label = p.label + "\n\\" + p.GetSum();
            }

            foreach (Payments p in data.spendings)
            {
                idx = spendingSeries.Points.AddXY(0, p.GetSum());
                spendingSeries.Points[idx].Name = p.label;
                spendingSeries.Points[idx].Label = p.label + "\n\\" + p.GetSum();
            }

            this.chart_Income.Legends.Clear();
            this.chart_Spending.Legends.Clear();

            this.chart_Income.BackColor = Color.WhiteSmoke;
            this.chart_Spending.BackColor = Color.WhiteSmoke;
            this.chart_Income.ChartAreas[0].BackColor = Color.WhiteSmoke;
            this.chart_Spending.ChartAreas[0].BackColor = Color.WhiteSmoke;

            this.chart_Income.DataManipulator.Filter(CompareMethod.EqualTo, 0, incomeSeries);
            this.chart_Spending.DataManipulator.Filter(CompareMethod.EqualTo, 0, spendingSeries);

            int totalIncome = data.GetTotalIncome();
            int totalSpending = data.GetTotalSpending();

            this.label_TotalIncome.Text = totalIncome.ToString("#,0") + "円";
            this.label_TotalSpending.Text = totalSpending.ToString("#,0") + "円";
            this.label_Total.Text = "＝ " + (totalIncome - totalSpending).ToString("#,0") + "円の" + (totalIncome - totalSpending > 0 ? "黒字(´∀｀)" : "赤字('A`)");
            this.label_Total.ForeColor = (totalIncome - totalSpending > 0 ? Color.Green : Color.Red);

            this.UpdateYearAndMonth(_settings.month);
        }

        private void RemoveValueChangedEventHandler()
        {
            this.numericUpDown_Year.ValueChanged -= new EventHandler(this.numericUpDown_Year_ValueChanged);
            this.comboBox_Month.SelectedValueChanged -= new EventHandler(this.comboBox_Month_SelectedValueChanged);
        }

        private void AddValueChangedEventHandler()
        {
            this.numericUpDown_Year.ValueChanged += new EventHandler(this.numericUpDown_Year_ValueChanged);
            this.comboBox_Month.SelectedValueChanged += new EventHandler(this.comboBox_Month_SelectedValueChanged);
        }

        private void UpdateYearAndMonth(DateTime month)
        {
            this.RemoveValueChangedEventHandler();
            this.numericUpDown_Year.Value = month.Year;
            this.comboBox_Month.SelectedIndex = month.Month - 1;
            this.AddValueChangedEventHandler();
        }

        #endregion

        private void MonthlyDataViewer_Shown(object sender, EventArgs e)
        {
            this.DrawMonthlyData(_settings.month);
            this.AddValueChangedEventHandler();
        }

        private void button_PrevMonth_Click(object sender, EventArgs e)
        {
            _settings.month = _settings.month.AddMonths(-1);
            this.DrawMonthlyData(_settings.month);
        }

        private void button_NextMonth_Click(object sender, EventArgs e)
        {
            _settings.month = _settings.month.AddMonths(1);
            this.DrawMonthlyData(_settings.month);
        }

        private void numericUpDown_Year_ValueChanged(object sender, EventArgs e)
        {
            _settings.month = new DateTime((int)this.numericUpDown_Year.Value, this.comboBox_Month.SelectedIndex + 1, 1);
            this.DrawMonthlyData(_settings.month);
        }

        private void comboBox_Month_SelectedValueChanged(object sender, EventArgs e)
        {
            this.numericUpDown_Year_ValueChanged(sender, e);
        }

        private void MonthlyDataViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 月毎のデータ画面設定をDBに保存する。
            _dataManager.SetMonthlyDataViewerSettings(_settings);
        }
    }
}
