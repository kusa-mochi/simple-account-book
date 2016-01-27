using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MainApplication
{
    public partial class TransitionViewer : Form
    {
        private TransitionViewerSettings _settings;
        private DataManager _dataManager;

        public TransitionViewer(DataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager");
            }

            InitializeComponent();

            _dataManager = dataManager;

            // DBから初期設定情報を読み込む。
            _settings = _dataManager.GetTransitionViewerSettings();

            this.comboBox_KindOfAmount.Items.AddRange(_dataManager.GetKindOfSpendingList());
            this.comboBox_KindOfAmount.SelectedIndex = _settings.kindOfAmountID;

            this.dateTimePicker_From.ValueChanged -= new EventHandler(this.dateTimePicker_From_ValueChanged);
            this.dateTimePicker_To.ValueChanged -= new EventHandler(this.dateTimePicker_To_ValueChanged);
            this.dateTimePicker_From.Value = _settings.monthFrom;
            this.dateTimePicker_To.Value = _settings.monthTo;
            this.dateTimePicker_From.ValueChanged += new EventHandler(this.dateTimePicker_From_ValueChanged);
            this.dateTimePicker_To.ValueChanged += new EventHandler(this.dateTimePicker_To_ValueChanged);

            this.dataGridView_DetailViewer.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.dataGridView_DetailViewer.Font = new Font("メイリオ", 12, FontStyle.Regular);
            this.dataGridView_DetailViewer.EnableHeadersVisualStyles = false;
            this.dataGridView_DetailViewer.ColumnHeadersDefaultCellStyle.BackColor = CommonConst.TableHeaderColor;
            this.dataGridView_DetailViewer.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_DetailViewer.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_DetailViewer.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_DetailViewer.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView_DetailViewer.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView_DetailViewer.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (ChartArea a in this.chart_Transition.ChartAreas)
            {
                a.AxisY.LabelStyle.Format = "C";
            }
        }

        /// <summary>
        /// 2つの時刻の月差を返す。
        /// </summary>
        /// <param name="dTime1"></param>
        /// <param name="dTime2"></param>
        /// <returns></returns>
        public int MonthDiff(DateTime dTime1, DateTime dTime2)
        {
            return (dTime2.Month + (dTime2.Year - dTime1.Year) * 12) - dTime1.Month;
        }

        private void DrawTransitionData(DateTime monthFrom, DateTime monthTo, int kindOfAmountID)
        {
            // 月差をもとめる。
            int nMonth = this.MonthDiff(monthFrom, monthTo);

            // monthFromがmonthToよりも未来である場合
            if (nMonth < 0)
            {
                MessageBox.Show("期間の指定が不正です。");
                return;
            }

            // 期間が長すぎる場合
            if (nMonth > CommonConst.MaxNumberOfTransitionMonth - 1)
            {
                MessageBox.Show("期間が長すぎます。" + CommonConst.MaxNumberOfTransitionMonth + "ヶ月以内の期間を指定してください。");
                return;
            }

            // 指定された期間にデータが存在するかを調べる
            bool dataExist = false;
            for (DateTime m = monthFrom; this.MonthDiff(m, monthTo) >= 0; m = m.AddMonths(1))
            {
                if (_dataManager.ExistSpendingData(m) == true)
                {
                    _settings.monthFrom = m;
                    dataExist = true;
                    break;
                }
            }

            // 指定された期間にデータが存在しない場合
            if (dataExist == false)
            {
                // DBに登録されている金額の最も新しい年月を表示期間に設定する。
                _settings.monthFrom = _dataManager.GetNewestSpendingDataMonth();
                _settings.monthTo = _settings.monthFrom;
            }
            else
            {
                for (DateTime m = monthTo; this.MonthDiff(monthFrom, m) >= 0; m = m.AddMonths(-1))
                {
                    if (_dataManager.ExistSpendingData(m) == true)
                    {
                        _settings.monthTo = m;
                        break;
                    }
                }
            }

            this.dateTimePicker_From.ValueChanged -= this.dateTimePicker_From_ValueChanged;
            this.dateTimePicker_From.Value = _settings.monthFrom;
            this.dateTimePicker_From.ValueChanged += this.dateTimePicker_From_ValueChanged;

            this.dateTimePicker_To.ValueChanged -= this.dateTimePicker_To_ValueChanged;
            this.dateTimePicker_To.Value = _settings.monthTo;
            this.dateTimePicker_To.ValueChanged += this.dateTimePicker_To_ValueChanged;

            string[] kindOfAmountList = _dataManager.GetKindOfSpendingList();
            int nKindOfAmountList = _dataManager.GetNumberOfKindOfSpendings();

            this.chart_Transition.Series.Clear();

            DataTable dt = new DataTable();
            dt.Columns.Add("col1", 1.GetType());
            dt.Columns.Add("col2", "".GetType());
            dt.Columns.Add("col3", "".GetType());

            //_transitionData.Clear();

            // 指定された期間の家計データをDBから取得する。
            //int iMonth = 0;
            for (DateTime m = _settings.monthFrom; this.MonthDiff(m, _settings.monthTo) >= 0; m = m.AddMonths(1))
            {
                MonthlyData data = _dataManager.GetMonthlyData(m);
                if (data.spendings.Count > 0)
                {
                    //_transitionData.Add(data);

                    if (kindOfAmountID == 0)
                    {
                        //Debug.Assert(iMonth >= 0, "iMonth >= 0");
                        //Debug.Assert(iMonth <= _transitionData.Count - 1, "iMonth(" + iMonth + ") <= _transitionData.Count - 1(" + (_transitionData.Count - 1) + "), data.spendings.Count == " + data.spendings.Count);
                        //foreach (Payments ps in _transitionData[iMonth].spendings)
                        foreach (Payments ps in data.spendings)
                        {
                            dt.Rows.Add(
                                ps.GetSum(),
                                "\'" + m.ToString("yy/MM"),
                                ps.label
                                );
                        }
                    }
                    else
                    {
                        dt.Rows.Add(
                            data.spendings[kindOfAmountID].GetSum(),
                            "\'" + m.ToString("yy/MM"),
                            data.spendings[kindOfAmountID].label
                            );
                    }
                    //iMonth++;
                }
            }

            // DBから取得したデータを元に，棒グラフを描画する。
            DataView dv = new DataView(dt);
            dv.Sort = "col2 asc";
            this.chart_Transition.Palette = ChartColorPalette.None;
            this.chart_Transition.PaletteCustomColors = (_settings.kindOfAmountID == 0) ? CommonConst.ColorPaletteTotalAmount : CommonConst.ColorPalette;
            this.chart_Transition.DataManipulator.FilterSetEmptyPoints = true;
            this.chart_Transition.DataManipulator.FilterMatchedPoints = true;
            this.chart_Transition.DataBindCrossTable(dv, "col3", "col2", "col1", "Label=col1");
            this.chart_Transition.Legends.Clear();

            foreach (Series s in this.chart_Transition.Series)
            {
                this.chart_Transition.DataManipulator.Filter(CompareMethod.EqualTo, 0, s);
                s.ChartType = SeriesChartType.StackedColumn;
                if (_settings.kindOfAmountID == 0)
                {
                    foreach (DataPoint dp in s.Points)
                    {
                        dp.Label = "";
                    }
                }
                else
                {
                    s.Font = new Font("メイリオ", 11, FontStyle.Regular);
                    s.LabelForeColor = Color.FromArgb(30, 30, 30);
                }
            }

            this.DrawTargetAmountLine(kindOfAmountID);
        }

        /// <summary>
        /// 目標金額の横線を積上げ縦棒グラフの上に重ねて表示する。
        /// </summary>
        /// <param name="kindOfAmountID"></param>
        private void DrawTargetAmountLine(int kindOfAmountID)
        {
            if (kindOfAmountID < 0)
            {
                throw new ArgumentOutOfRangeException("kindOfAmountID");
            }

            // DBから目標金額データを取得する。
            int targetAmount = _dataManager.GetTargetAmount(0, kindOfAmountID);

            // 目標金額が設定されていない場合
            if (targetAmount == 0)
            {
                // 何も表示しない。
                return;
            }

            Series ser = new Series();
            ser.LegendText = "目標金額";
            ser.ChartType = SeriesChartType.Line;
            ser.Color = Color.Red;
            ser.BorderDashStyle = ChartDashStyle.Dash;
            ser.BorderWidth = 3;
            ser.XAxisType = AxisType.Secondary;
            ser.YAxisType = AxisType.Primary;
            ser.Points.AddXY(0, targetAmount);
            ser.Points.AddXY(1, targetAmount);
            this.chart_Transition.Series.Add(ser);
            this.chart_Transition.ChartAreas[0].AxisX2.Minimum = 0;
            this.chart_Transition.ChartAreas[0].AxisX2.Maximum = 1;
            this.chart_Transition.ChartAreas[0].AxisX2.LabelStyle.Enabled = false;
        }

        private void DrawDetailData(DateTime month, int kindOfAmountID)
        {
            MonthlyData data = _dataManager.GetMonthlyData(month);
            string[] kindOfAmountList = _dataManager.GetKindOfSpendingList();

            this.dataGridView_DetailViewer.Rows.Clear();

            // データが1件以上存在するとき
            if (data.spendings.Count > 0)
            {
                for (int i = 1; i < kindOfAmountList.Length; i++)
                {
                    this.dataGridView_DetailViewer.Rows.Add(new string[] {
                        kindOfAmountList[i],
                        data.spendings[i].GetSum().ToString("#,0"),
                        (100.0 * data.spendings[i].GetSum() / data.GetTotalSpending()).ToString("F1")
                    });
                }
            }
            else
            {
                // 何もしない。
            }
        }

        private void TransitionViewer_Shown(object sender, EventArgs e)
        {
            this.DrawTransitionData(_settings.monthFrom, _settings.monthTo, _settings.kindOfAmountID);
            this.DrawDetailData(_settings.monthTo, 0);
        }

        private void dateTimePicker_From_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateTimePicker_From.Value > _settings.monthTo)
            {
                this.dateTimePicker_From.Value = _settings.monthTo;
                return;
            }
            if (this.MonthDiff(this.dateTimePicker_From.Value, _settings.monthTo) > CommonConst.MaxNumberOfTransitionMonth - 1)
            {
                this.dateTimePicker_From.Value = _settings.monthTo.AddMonths(-CommonConst.MaxNumberOfTransitionMonth + 1);
            }

            _settings.monthFrom = this.dateTimePicker_From.Value;
            this.DrawTransitionData(_settings.monthFrom, _settings.monthTo, _settings.kindOfAmountID);
        }

        private void dateTimePicker_To_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateTimePicker_To.Value < _settings.monthFrom)
            {
                this.dateTimePicker_To.Value = _settings.monthFrom;
                return;
            }
            if (this.MonthDiff(_settings.monthFrom, this.dateTimePicker_To.Value) > CommonConst.MaxNumberOfTransitionMonth - 1)
            {
                this.dateTimePicker_To.Value = _settings.monthFrom.AddMonths(CommonConst.MaxNumberOfTransitionMonth - 1);
            }

            _settings.monthTo = this.dateTimePicker_To.Value;
            this.DrawTransitionData(_settings.monthFrom, _settings.monthTo, _settings.kindOfAmountID);
            this.DrawDetailData(_settings.monthTo, _settings.kindOfAmountID);
        }

        private void comboBox_KindOfAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            _settings.kindOfAmountID = this.comboBox_KindOfAmount.SelectedIndex;
            this.DrawTransitionData(_settings.monthFrom, _settings.monthTo, _settings.kindOfAmountID);
            this.DrawDetailData(_settings.monthTo, _settings.kindOfAmountID);
        }

        private void TransitionViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 家計の推移画面設定をDBに保存する。
            _dataManager.SetTransitionViewerSettings(_settings);
        }

        private void dataGridView_DetailViewer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 表にデータが1つもないとき
            if(this.dataGridView_DetailViewer.RowCount == 0)
            {
                // 何もしない。
                return;
            }

            if (this.comboBox_KindOfAmount.SelectedIndex == 0)
            {
                // 表の何行目がクリックされたのかを検出する。
                int selectedLine = this.dataGridView_DetailViewer.SelectedCells[0].RowIndex;
                Color[] palette = new Color[CommonConst.ColorPaletteTotalAmount.Length];
                CommonConst.ColorPaletteTotalAmount.CopyTo(palette, 0);
                palette[selectedLine + 1] = Color.Red;
                this.chart_Transition.PaletteCustomColors = palette;
            }
        }
    }
}
