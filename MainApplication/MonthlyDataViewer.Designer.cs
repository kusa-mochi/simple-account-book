namespace MainApplication
{
    partial class MonthlyDataViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_Income = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_Spending = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label_Year = new System.Windows.Forms.Label();
            this.button_PrevMonth = new System.Windows.Forms.Button();
            this.button_NextMonth = new System.Windows.Forms.Button();
            this.label_TotalIncome = new System.Windows.Forms.Label();
            this.label_Total = new System.Windows.Forms.Label();
            this.label_Minus = new System.Windows.Forms.Label();
            this.label_TotalSpending = new System.Windows.Forms.Label();
            this.label_Month = new System.Windows.Forms.Label();
            this.numericUpDown_Year = new System.Windows.Forms.NumericUpDown();
            this.comboBox_Month = new System.Windows.Forms.ComboBox();
            this.label_Income = new System.Windows.Forms.Label();
            this.label_Spending = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Income)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Spending)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Year)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_Income
            // 
            chartArea3.Name = "ChartArea1";
            this.chart_Income.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart_Income.Legends.Add(legend3);
            this.chart_Income.Location = new System.Drawing.Point(40, 69);
            this.chart_Income.Name = "chart_Income";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart_Income.Series.Add(series3);
            this.chart_Income.Size = new System.Drawing.Size(340, 320);
            this.chart_Income.TabIndex = 0;
            this.chart_Income.Text = "chart1";
            // 
            // chart_Spending
            // 
            chartArea4.Name = "ChartArea1";
            this.chart_Spending.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart_Spending.Legends.Add(legend4);
            this.chart_Spending.Location = new System.Drawing.Point(392, 69);
            this.chart_Spending.Name = "chart_Spending";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart_Spending.Series.Add(series4);
            this.chart_Spending.Size = new System.Drawing.Size(340, 320);
            this.chart_Spending.TabIndex = 1;
            this.chart_Spending.Text = "chart2";
            // 
            // label_Year
            // 
            this.label_Year.AutoSize = true;
            this.label_Year.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Year.Location = new System.Drawing.Point(319, 18);
            this.label_Year.Name = "label_Year";
            this.label_Year.Size = new System.Drawing.Size(47, 33);
            this.label_Year.TabIndex = 2;
            this.label_Year.Text = "年";
            // 
            // button_PrevMonth
            // 
            this.button_PrevMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button_PrevMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_PrevMonth.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_PrevMonth.Location = new System.Drawing.Point(135, 11);
            this.button_PrevMonth.Name = "button_PrevMonth";
            this.button_PrevMonth.Size = new System.Drawing.Size(33, 33);
            this.button_PrevMonth.TabIndex = 3;
            this.button_PrevMonth.Text = "←";
            this.button_PrevMonth.UseVisualStyleBackColor = false;
            this.button_PrevMonth.Click += new System.EventHandler(this.button_PrevMonth_Click);
            // 
            // button_NextMonth
            // 
            this.button_NextMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button_NextMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_NextMonth.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_NextMonth.Location = new System.Drawing.Point(550, 11);
            this.button_NextMonth.Name = "button_NextMonth";
            this.button_NextMonth.Size = new System.Drawing.Size(33, 33);
            this.button_NextMonth.TabIndex = 4;
            this.button_NextMonth.Text = "→";
            this.button_NextMonth.UseVisualStyleBackColor = false;
            this.button_NextMonth.Click += new System.EventHandler(this.button_NextMonth_Click);
            // 
            // label_TotalIncome
            // 
            this.label_TotalIncome.AutoSize = true;
            this.label_TotalIncome.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_TotalIncome.Location = new System.Drawing.Point(104, 432);
            this.label_TotalIncome.Name = "label_TotalIncome";
            this.label_TotalIncome.Size = new System.Drawing.Size(79, 28);
            this.label_TotalIncome.TabIndex = 5;
            this.label_TotalIncome.Text = "label1";
            // 
            // label_Total
            // 
            this.label_Total.AutoSize = true;
            this.label_Total.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Total.Location = new System.Drawing.Point(102, 506);
            this.label_Total.Name = "label_Total";
            this.label_Total.Size = new System.Drawing.Size(112, 40);
            this.label_Total.TabIndex = 6;
            this.label_Total.Text = "label2";
            // 
            // label_Minus
            // 
            this.label_Minus.AutoSize = true;
            this.label_Minus.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Minus.Location = new System.Drawing.Point(368, 432);
            this.label_Minus.Name = "label_Minus";
            this.label_Minus.Size = new System.Drawing.Size(40, 28);
            this.label_Minus.TabIndex = 7;
            this.label_Minus.Text = "－";
            // 
            // label_TotalSpending
            // 
            this.label_TotalSpending.AutoSize = true;
            this.label_TotalSpending.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_TotalSpending.Location = new System.Drawing.Point(477, 432);
            this.label_TotalSpending.Name = "label_TotalSpending";
            this.label_TotalSpending.Size = new System.Drawing.Size(79, 28);
            this.label_TotalSpending.TabIndex = 8;
            this.label_TotalSpending.Text = "label4";
            // 
            // label_Month
            // 
            this.label_Month.AutoSize = true;
            this.label_Month.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Month.Location = new System.Drawing.Point(499, 18);
            this.label_Month.Name = "label_Month";
            this.label_Month.Size = new System.Drawing.Size(47, 33);
            this.label_Month.TabIndex = 9;
            this.label_Month.Text = "月";
            // 
            // numericUpDown_Year
            // 
            this.numericUpDown_Year.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.numericUpDown_Year.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDown_Year.Location = new System.Drawing.Point(193, 16);
            this.numericUpDown_Year.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDown_Year.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDown_Year.Name = "numericUpDown_Year";
            this.numericUpDown_Year.Size = new System.Drawing.Size(120, 39);
            this.numericUpDown_Year.TabIndex = 10;
            this.numericUpDown_Year.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_Year.Value = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.numericUpDown_Year.ValueChanged += new System.EventHandler(this.numericUpDown_Year_ValueChanged);
            // 
            // comboBox_Month
            // 
            this.comboBox_Month.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.comboBox_Month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Month.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Month.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_Month.FormattingEnabled = true;
            this.comboBox_Month.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.comboBox_Month.Location = new System.Drawing.Point(372, 14);
            this.comboBox_Month.Name = "comboBox_Month";
            this.comboBox_Month.Size = new System.Drawing.Size(121, 41);
            this.comboBox_Month.TabIndex = 11;
            this.comboBox_Month.SelectedValueChanged += new System.EventHandler(this.comboBox_Month_SelectedValueChanged);
            // 
            // label_Income
            // 
            this.label_Income.AutoSize = true;
            this.label_Income.BackColor = System.Drawing.Color.Transparent;
            this.label_Income.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Income.Location = new System.Drawing.Point(162, 213);
            this.label_Income.Name = "label_Income";
            this.label_Income.Size = new System.Drawing.Size(97, 40);
            this.label_Income.TabIndex = 12;
            this.label_Income.Text = "収入";
            // 
            // label_Spending
            // 
            this.label_Spending.AutoSize = true;
            this.label_Spending.BackColor = System.Drawing.Color.Transparent;
            this.label_Spending.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Spending.Location = new System.Drawing.Point(123, 142);
            this.label_Spending.Name = "label_Spending";
            this.label_Spending.Size = new System.Drawing.Size(97, 40);
            this.label_Spending.TabIndex = 13;
            this.label_Spending.Text = "支出";
            // 
            // MonthlyDataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(770, 588);
            this.Controls.Add(this.label_Income);
            this.Controls.Add(this.label_Spending);
            this.Controls.Add(this.comboBox_Month);
            this.Controls.Add(this.numericUpDown_Year);
            this.Controls.Add(this.label_Month);
            this.Controls.Add(this.label_TotalSpending);
            this.Controls.Add(this.label_Minus);
            this.Controls.Add(this.label_Total);
            this.Controls.Add(this.label_TotalIncome);
            this.Controls.Add(this.button_NextMonth);
            this.Controls.Add(this.button_PrevMonth);
            this.Controls.Add(this.label_Year);
            this.Controls.Add(this.chart_Spending);
            this.Controls.Add(this.chart_Income);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MonthlyDataViewer";
            this.Text = "月毎のデータ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonthlyDataViewer_FormClosing);
            this.Shown += new System.EventHandler(this.MonthlyDataViewer_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Income)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Spending)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Year)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Income;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Spending;
        private System.Windows.Forms.Label label_Year;
        private System.Windows.Forms.Button button_PrevMonth;
        private System.Windows.Forms.Button button_NextMonth;
        private System.Windows.Forms.Label label_TotalIncome;
        private System.Windows.Forms.Label label_Total;
        private System.Windows.Forms.Label label_Minus;
        private System.Windows.Forms.Label label_TotalSpending;
        private System.Windows.Forms.Label label_Month;
        private System.Windows.Forms.NumericUpDown numericUpDown_Year;
        private System.Windows.Forms.ComboBox comboBox_Month;
        private System.Windows.Forms.Label label_Income;
        private System.Windows.Forms.Label label_Spending;
    }
}