namespace MainApplication
{
    partial class TransitionViewer
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_Transition = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateTimePicker_From = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_To = new System.Windows.Forms.DateTimePicker();
            this.comboBox_KindOfAmount = new System.Windows.Forms.ComboBox();
            this.label_Term = new System.Windows.Forms.Label();
            this.dataGridView_DetailViewer = new System.Windows.Forms.DataGridView();
            this.KindOfAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PercentageOfAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Transition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetailViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_Transition
            // 
            this.chart_Transition.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea_Transition";
            this.chart_Transition.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_Transition.Legends.Add(legend1);
            this.chart_Transition.Location = new System.Drawing.Point(12, 44);
            this.chart_Transition.Name = "chart_Transition";
            series1.ChartArea = "ChartArea_Transition";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_Transition.Series.Add(series1);
            this.chart_Transition.Size = new System.Drawing.Size(797, 415);
            this.chart_Transition.TabIndex = 0;
            this.chart_Transition.Text = "chart1";
            // 
            // dateTimePicker_From
            // 
            this.dateTimePicker_From.CustomFormat = "yyyy年MM月";
            this.dateTimePicker_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_From.Location = new System.Drawing.Point(260, 11);
            this.dateTimePicker_From.Name = "dateTimePicker_From";
            this.dateTimePicker_From.Size = new System.Drawing.Size(150, 25);
            this.dateTimePicker_From.TabIndex = 1;
            this.dateTimePicker_From.ValueChanged += new System.EventHandler(this.dateTimePicker_From_ValueChanged);
            // 
            // dateTimePicker_To
            // 
            this.dateTimePicker_To.CustomFormat = "yyyy年MM月";
            this.dateTimePicker_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_To.Location = new System.Drawing.Point(448, 11);
            this.dateTimePicker_To.Name = "dateTimePicker_To";
            this.dateTimePicker_To.Size = new System.Drawing.Size(150, 25);
            this.dateTimePicker_To.TabIndex = 2;
            this.dateTimePicker_To.ValueChanged += new System.EventHandler(this.dateTimePicker_To_ValueChanged);
            // 
            // comboBox_KindOfAmount
            // 
            this.comboBox_KindOfAmount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_KindOfAmount.FormattingEnabled = true;
            this.comboBox_KindOfAmount.Location = new System.Drawing.Point(654, 12);
            this.comboBox_KindOfAmount.Name = "comboBox_KindOfAmount";
            this.comboBox_KindOfAmount.Size = new System.Drawing.Size(155, 26);
            this.comboBox_KindOfAmount.TabIndex = 3;
            this.comboBox_KindOfAmount.SelectedIndexChanged += new System.EventHandler(this.comboBox_KindOfAmount_SelectedIndexChanged);
            // 
            // label_Term
            // 
            this.label_Term.AutoSize = true;
            this.label_Term.Location = new System.Drawing.Point(416, 16);
            this.label_Term.Name = "label_Term";
            this.label_Term.Size = new System.Drawing.Size(26, 18);
            this.label_Term.TabIndex = 4;
            this.label_Term.Text = "～";
            // 
            // dataGridView_DetailViewer
            // 
            this.dataGridView_DetailViewer.AllowUserToAddRows = false;
            this.dataGridView_DetailViewer.AllowUserToDeleteRows = false;
            this.dataGridView_DetailViewer.AllowUserToOrderColumns = true;
            this.dataGridView_DetailViewer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_DetailViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_DetailViewer.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView_DetailViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_DetailViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_DetailViewer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KindOfAmount,
            this.Amount,
            this.PercentageOfAmount});
            this.dataGridView_DetailViewer.Location = new System.Drawing.Point(12, 470);
            this.dataGridView_DetailViewer.MultiSelect = false;
            this.dataGridView_DetailViewer.Name = "dataGridView_DetailViewer";
            this.dataGridView_DetailViewer.ReadOnly = true;
            this.dataGridView_DetailViewer.RowHeadersVisible = false;
            this.dataGridView_DetailViewer.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_DetailViewer.RowTemplate.Height = 27;
            this.dataGridView_DetailViewer.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_DetailViewer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_DetailViewer.Size = new System.Drawing.Size(797, 213);
            this.dataGridView_DetailViewer.TabIndex = 5;
            this.dataGridView_DetailViewer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_DetailViewer_CellClick);
            // 
            // KindOfAmount
            // 
            this.KindOfAmount.HeaderText = "費用項目";
            this.KindOfAmount.Name = "KindOfAmount";
            this.KindOfAmount.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "金額 [円]";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // PercentageOfAmount
            // 
            this.PercentageOfAmount.HeaderText = "支出の割合 [%]";
            this.PercentageOfAmount.Name = "PercentageOfAmount";
            this.PercentageOfAmount.ReadOnly = true;
            // 
            // TransitionViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(821, 695);
            this.Controls.Add(this.dataGridView_DetailViewer);
            this.Controls.Add(this.label_Term);
            this.Controls.Add(this.comboBox_KindOfAmount);
            this.Controls.Add(this.dateTimePicker_To);
            this.Controls.Add(this.dateTimePicker_From);
            this.Controls.Add(this.chart_Transition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TransitionViewer";
            this.Text = "家計の推移";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TransitionViewer_FormClosing);
            this.Shown += new System.EventHandler(this.TransitionViewer_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Transition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetailViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Transition;
        private System.Windows.Forms.DateTimePicker dateTimePicker_From;
        private System.Windows.Forms.DateTimePicker dateTimePicker_To;
        private System.Windows.Forms.ComboBox comboBox_KindOfAmount;
        private System.Windows.Forms.Label label_Term;
        private System.Windows.Forms.DataGridView dataGridView_DetailViewer;
        private System.Windows.Forms.DataGridViewTextBoxColumn KindOfAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PercentageOfAmount;
    }
}