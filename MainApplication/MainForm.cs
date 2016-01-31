using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MainApplication
{
    public partial class MainForm : Form
    {
        #region データメンバ

        private DataManager _dataManager;
        private MainFormSettings _settings;
        private MonthlyData _monthlyData;
        private int _spendingOrIncome;  // 0:支出, 1:収入
        private bool _isMenuOpened;
        private Button[] _itemButtons;
        private Button[] _amountButtons;
        private string[] _kindOfAmountList;
        private int _selectedIndexOfAmountList;
        private int _selectedKindButtonID;

        #endregion

        #region コンストラクタ

        public MainForm()
        {
            InitializeComponent();

            this.ColorSettings();
            this.Icon = Properties.Resources.applicationIcon;
            this.Text = CommonConst.ApplicationName;
            this.ToolStripMenuItem_ItemSetting_Remove.Enabled = false;  // 未実装のため，無効にする。
            _dataManager = new DataManager(CommonConst.DBFileName);
            _spendingOrIncome = 0;
            _isMenuOpened = false;

            this.InitButtonArray();

            // DBから初期設定情報を読み込む。
            _settings = _dataManager.GetMainFormSettings();

            this.SetYearControlValueWithoutEventHandler();

            // 表示月を変えるとコンボボックスの値を変えたことによるイベントが発生し，イベントハンドラが実行される。
            // イベントハンドラの処理により，表示月の家計簿データがDBから読み込まれる。
            this.comboBox_Month.SelectedIndex = _settings.month.Month - 1;

            //this.SetTooltipProperties();
            //this.SetButtonProperties();
            this.AdjustButtonShape();
            this.DrawPlusMark(_dataManager.GetNumberOfKindOfSpendings());
        }

        #endregion

        #region プライベートメソッド

        private void ColorSettings()
        {
            this.button_PrevMonth.BackColor = CommonConst.PrevNextMonthButtonColor;
            this.button_NextMonth.BackColor = CommonConst.PrevNextMonthButtonColor;
            this.numericUpDown_Year.BackColor = CommonConst.MonthControlButtonColor;
            this.comboBox_Month.BackColor = CommonConst.MonthControlButtonColor;
        }

        private void InitButtonArray()
        {
            _itemButtons = new Button[CommonConst.MaxNumberOfKindOfAmount];
            _itemButtons[0] = this.button1;
            _itemButtons[1] = this.button2;
            _itemButtons[2] = this.button3;
            _itemButtons[3] = this.button4;
            _itemButtons[4] = this.button5;
            _itemButtons[5] = this.button6;
            _itemButtons[6] = this.button7;
            _itemButtons[7] = this.button8;
            _itemButtons[8] = this.button9;
            _itemButtons[9] = this.button10;
            _itemButtons[10] = this.button201;
            _itemButtons[11] = this.button202;
            _itemButtons[12] = this.button203;
            _itemButtons[13] = this.button204;
            _itemButtons[14] = this.button205;
            _itemButtons[15] = this.button206;
            _itemButtons[16] = this.button207;
            _itemButtons[17] = this.button208;
            _itemButtons[18] = this.button209;
            _itemButtons[19] = this.button210;

            _amountButtons = new Button[CommonConst.MaxNumberOfKindOfAmount];
            _amountButtons[0] = this.button101;
            _amountButtons[1] = this.button102;
            _amountButtons[2] = this.button103;
            _amountButtons[3] = this.button104;
            _amountButtons[4] = this.button105;
            _amountButtons[5] = this.button106;
            _amountButtons[6] = this.button107;
            _amountButtons[7] = this.button108;
            _amountButtons[8] = this.button109;
            _amountButtons[9] = this.button110;
            _amountButtons[10] = this.button301;
            _amountButtons[11] = this.button302;
            _amountButtons[12] = this.button303;
            _amountButtons[13] = this.button304;
            _amountButtons[14] = this.button305;
            _amountButtons[15] = this.button306;
            _amountButtons[16] = this.button307;
            _amountButtons[17] = this.button308;
            _amountButtons[18] = this.button309;
            _amountButtons[19] = this.button310;

            this.SetButtonEventHandlers();
        }

        private void SetYearControlValueWithoutEventHandler()
        {
            this.numericUpDown_Year.ValueChanged -= new EventHandler(this.numericUpDown_Year_ValueChanged);
            this.numericUpDown_Year.Value = _settings.month.Year;
            this.numericUpDown_Year.ValueChanged += new EventHandler(this.numericUpDown_Year_ValueChanged);
        }

        private void SetTooltipProperties()
        {
            this.toolTip_ViewAmounts.InitialDelay = 0;
            this.toolTip_ViewAmounts.ReshowDelay = 0;
            this.toolTip_ViewAmounts.AutoPopDelay = 60000;
            this.toolTip_ViewAmounts.ShowAlways = true;
        }

        private void SetButtonEventHandlers()
        {
            _itemButtons[0].MouseDown += new MouseEventHandler(this.KindButton_RightClick);

            // 「総収入」「総支出」はクリックしても何も起こらない仕様のため，i = 0のボタンにはイベントハンドラを登録しない。
            for (int i = 1; i < CommonConst.MaxNumberOfKindOfAmount; i++)
            {
                _itemButtons[i].Click += new EventHandler(this.KindButton_Click);
                _itemButtons[i].MouseDown += new MouseEventHandler(this.KindButton_RightClick);
                _itemButtons[i].ContextMenuStrip = this.contextMenuStrip_ItemSetting;
                _itemButtons[i].BackColor = CommonConst.ItemButtonColor;
                _amountButtons[i].Click += new EventHandler(this.AmountButton_Click);
            }
        }

        private void SetButtonProperties()
        {
            this.SetTotalItemButtonAmountButtonProperties();
            this.SetItemButtonAmountButtonProperties(_kindOfAmountList);
        }

        private void SetTotalItemButtonAmountButtonProperties()
        {
            _itemButtons[0].ContextMenuStrip = this.contextMenuStrip_TotalAmountSetting;
            _itemButtons[0].BackColor = CommonConst.ItemButtonColor;
            //_itemButtons[0].MouseDown += new MouseEventHandler(this.KindButton_RightClick);
            _amountButtons[0].Enabled = false;
        }

        private void SetItemButtonAmountButtonProperties(string[] kindOfAmountList)
        {
            //// 「総収入」「総支出」はクリックしても何も起こらない仕様のため，i = 0のボタンにはイベントハンドラを登録しない。
            //for (int i = 1; i < CommonConst.MaxNumberOfKindOfAmount; i++)
            //{
            //    _itemButtons[i].Click += new EventHandler(this.KindButton_Click);
            //    _itemButtons[i].MouseDown += new MouseEventHandler(this.KindButton_RightClick);
            //    _itemButtons[i].ContextMenuStrip = this.contextMenuStrip_ItemSetting;
            //    _itemButtons[i].BackColor = CommonConst.ItemButtonColor;
            //    _amountButtons[i].Click += new EventHandler(this.AmountButton_Click);
            //}

            int nKind = kindOfAmountList.Length;
            for (int i = 0; i < nKind; i++)
            {
                _itemButtons[i].Visible = true;
                _itemButtons[i].Text = _kindOfAmountList[i];
                _amountButtons[i].Visible = true;
                _amountButtons[i].Text = _monthlyData.GetTotalPerKindOfPayment(_spendingOrIncome, _kindOfAmountList[i]).ToString("#,0");
                this.toolTip_ViewAmounts.SetToolTip(
                    _amountButtons[i],
                    _monthlyData.GetPopupStringOfPayment(
                        _spendingOrIncome,
                        _kindOfAmountList[i]
                        )
                    );
            }
            for (int i = nKind; i < CommonConst.MaxNumberOfKindOfAmount; i++)
            {
                _itemButtons[i].Visible = false;
                _amountButtons[i].Visible = false;
            }
        }

        private void AddAmountToListbox(List<Payments> data)
        {
            // 文字数の最大値
            int maxLength = 0;

            // 登録された金額をリストボックスに追加する。
            foreach (Payment p in data[_selectedKindButtonID].payments)
            {
                string itemString = p.amountOfMoney.ToString("#,0") + _dataManager.GetCommentSplitCharacter().ToString() + p.comment;
                if (itemString.Length > maxLength)
                {
                    maxLength = itemString.Length;
                }

                this.listBox_AmountsDetail.Items.Add(itemString);
                if (this.listBox_AmountsDetail.Height <= 300)
                {
                    this.listBox_AmountsDetail.Height += this.listBox_AmountsDetail.Font.Height;
                }
            }

            this.listBox_AmountsDetail.Width = (int)this.listBox_AmountsDetail.Font.SizeInPoints * maxLength;
        }

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

        private void OpenMenu()
        {
            this.panel_Main.Left = 200;

            this.panel_Menu.Top = 0;
            this.panel_Menu.Left = 0;
            this.panel_Menu.Width = 200;
            this.panel_Menu.Height = this.Height;
            this.panel_Menu.Visible = true;

            _isMenuOpened = true;
        }

        private void CloseMenu()
        {
            this.panel_Main.Left = 0;
            this.panel_Menu.Visible = false;

            _isMenuOpened = false;
        }

        private int GetSenderButtonID(Button sender, Button[] buttonList)
        {
            for (int i = 0; i < CommonConst.MaxNumberOfKindOfAmount; i++)
            {
                if ((Button)sender == buttonList[i])
                {
                    return i;
                }
            }

            return -1;
        }

        private void DrawPlusMark(int nKind)
        {
            // 費用項目数が最大でないとき
            if (nKind < CommonConst.MaxNumberOfKindOfAmount)
            {
                // 費用項目を追加するための「＋」を表示する。
                this.button_Plus.Top = this._itemButtons[nKind].Top;
                this.button_Plus.Left = this._itemButtons[nKind].Left + ((this._itemButtons[nKind].Width - this.button_Plus.Width) / 2);
                this.button_Plus.Visible = true;
            }
            else
            {
                this.button_Plus.Visible = false;
            }
        }

        /// <summary>
        /// 指定された月の家計簿データを表示する。
        /// </summary>
        /// <param name="month"></param>
        private void DrawMonthlyData(DateTime month)
        {
            // 初期設定情報に記載された月の家計簿データをDBから読み込む。
            _monthlyData = _dataManager.GetMonthlyData(month);

            _isMenuOpened = false;

            int nKind = 0;
            switch (_spendingOrIncome)
            {
                case 0: // 支出
                    _kindOfAmountList = _dataManager.GetKindOfSpendingList();
                    nKind = _dataManager.GetNumberOfKindOfSpendings();
                    break;
                case 1: // 収入
                    _kindOfAmountList = _dataManager.GetKindOfIncomeList();
                    nKind = _dataManager.GetNumberOfKindOfIncomes();
                    break;
                default:
                    break;
            }

            _settings.month = month;

            this.listBox_AmountsDetail.Visible = false;
            this.SetTooltipProperties();
            this.SetButtonProperties();
        }

        #endregion

        private void button_Menu_Click(object sender, EventArgs e)
        {
            if (_isMenuOpened == false)
            {
                this.OpenMenu();
            }
            else
            {
                this.CloseMenu();
            }
        }

        private void label_Setting_MouseEnter(object sender, EventArgs e)
        {
            this.label_Setting.BackColor = CommonConst.MainMenuHoverColor;
        }

        private void label_Setting_MouseLeave(object sender, EventArgs e)
        {
            this.label_Setting.BackColor = CommonConst.MainMenuColor;
        }

        private void label_MonthlyData_MouseEnter(object sender, EventArgs e)
        {
            this.label_MonthlyData.BackColor = CommonConst.MainMenuHoverColor;
        }

        private void label_MonthlyData_MouseLeave(object sender, EventArgs e)
        {
            this.label_MonthlyData.BackColor = CommonConst.MainMenuColor;
        }

        private void label_Transition_MouseEnter(object sender, EventArgs e)
        {
            this.label_Transition.BackColor = CommonConst.MainMenuHoverColor;
        }

        private void label_Transition_MouseLeave(object sender, EventArgs e)
        {
            this.label_Transition.BackColor = CommonConst.MainMenuColor;
        }

        private void label_Close_MouseEnter(object sender, EventArgs e)
        {
            this.label_Close.BackColor = CommonConst.MainMenuHoverColor;
        }

        private void label_Close_MouseLeave(object sender, EventArgs e)
        {
            this.label_Close.BackColor = CommonConst.MainMenuColor;
        }

        private void label_Setting_Click(object sender, EventArgs e)
        {
            this.CloseMenu();

            ApplicationSettingForm applicationSettingForm = new ApplicationSettingForm(_dataManager);
            applicationSettingForm.ShowDialog();
            this.DrawMonthlyData(_settings.month);
        }

        private void label_MonthlyData_Click(object sender, EventArgs e)
        {
            this.CloseMenu();

            MonthlyDataViewer monthlyDataViewer = new MonthlyDataViewer(_dataManager);
            monthlyDataViewer.ShowDialog();
        }

        private void label_Transition_Click(object sender, EventArgs e)
        {
            this.CloseMenu();

            TransitionViewer transitionViewer = new TransitionViewer(_dataManager);
            transitionViewer.ShowDialog();
        }

        private void label_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label_Plus_Click(object sender, EventArgs e)
        {
            this.button_Plus.Visible = false;
            this.textBox_NewKindOfAmountLabel.Width = _itemButtons[0].Width;
            this.textBox_NewKindOfAmountLabel.Top = this.button_Plus.Top;
            this.textBox_NewKindOfAmountLabel.Left = this.button_Plus.Left - 35;
            this.textBox_NewKindOfAmountLabel.Visible = true;
            this.textBox_NewKindOfAmountLabel.Focus();
        }

        private void KindButton_Click(object sender, EventArgs e)
        {
            this.textBox_InputAmounts.Visible = false;
            this.listBox_AmountsDetail.Visible = false;

            // 押されたボタンを特定する。
            _selectedKindButtonID = this.GetSenderButtonID((Button)sender, _itemButtons);

            // 金額入力欄の表示位置を設定する。
            this.textBox_InputAmounts.Top = this._itemButtons[_selectedKindButtonID].Top + 20;
            if (this.textBox_InputAmounts.Top > this.Height - 70)
            {
                this.textBox_InputAmounts.Top = this.Height - 70;
            }
            this.textBox_InputAmounts.Left = this._itemButtons[_selectedKindButtonID].Left + 2;

            // 金額入力欄を表示する。
            this.textBox_InputAmounts.Visible = true;

            // 金額入力欄にフォーカスをあてる。
            this.textBox_InputAmounts.Focus();
        }

        private void KindButton_RightClick(object sender, MouseEventArgs e)
        {
            this.textBox_InputAmounts.Visible = false;
            this.listBox_AmountsDetail.Visible = false;

            // 右クリックされていない場合
            if (e.Button != MouseButtons.Right)
            {
                // 何もせずに処理を終了する。
                return;
            }

            // 押されたボタンを特定する。
            int senderID = this.GetSenderButtonID((Button)sender, _itemButtons);

            _selectedKindButtonID = senderID;
        }

        private void AmountButton_Click(object sender, EventArgs e)
        {
            List<Payments> data = _spendingOrIncome == 0 ? _monthlyData.spendings : _monthlyData.incomes;
            if (data.Count == 0)
            {
                return;
            }

            this.textBox_InputAmounts.Visible = false;

            // 押されたボタンを特定する。
            _selectedKindButtonID = this.GetSenderButtonID((Button)sender, _amountButtons);

            if (data[_selectedKindButtonID].payments == null)
            {
                this.listBox_AmountsDetail.Visible = false;
                return;
            }

            // リストボックスが表示されている場合
            if (this.listBox_AmountsDetail.Visible == true)
            {
                // リストボックスを非表示にする。
                this.listBox_AmountsDetail.Visible = false;
                return;
            }

            // 登録された金額一覧の表示位置を設定する。
            this.listBox_AmountsDetail.Top = this._amountButtons[_selectedKindButtonID].Top + 20;
            this.listBox_AmountsDetail.Left = this._amountButtons[_selectedKindButtonID].Left + 2;

            // リストボックスの表示内容を空にする。
            this.listBox_AmountsDetail.Items.Clear();
            this.listBox_AmountsDetail.Height = 4;

            this.AddAmountToListbox(data);

            // リストボックスの位置を調整する。
            if (this.listBox_AmountsDetail.Top + this.listBox_AmountsDetail.Height > this.Height)
            {
                this.listBox_AmountsDetail.Top = this.Height - this.listBox_AmountsDetail.Height - 50;
            }

            // 登録された金額の一覧を表示する。
            this.listBox_AmountsDetail.Visible = true;
        }

        private void panel_Main_MouseDown(object sender, MouseEventArgs e)
        {
            this.textBox_InputAmounts.Text = "";
            this.textBox_NewKindOfAmountLabel.Text = "";
            this.textBox_EditAmount.Text = "";
            this.textBox_InputTargetAmount.Text = "";
            this.textBox_SetKindOfAmountName.Text = "";

            this.textBox_InputAmounts.Visible = false;
            this.textBox_NewKindOfAmountLabel.Visible = false;
            this.textBox_EditAmount.Visible = false;
            this.textBox_InputTargetAmount.Visible = false;
            this.textBox_SetKindOfAmountName.Visible = false;
            this.listBox_AmountsDetail.Visible = false;

            if (_kindOfAmountList.Length <= 19)
            {
                this.button_Plus.Visible = true;
            }
        }

        private void tabControl_SpendingAndIncome_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel_Main_MouseDown(sender, e);
        }

        private void tabPage_Spending_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel_Main_MouseDown(sender, e);
        }

        private void tabPage_Income_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel_Main_MouseDown(sender, e);
        }

        private void tabControl_SpendingAndIncome_Selected(object sender, TabControlEventArgs e)
        {
            _spendingOrIncome = e.TabPageIndex;
            _itemButtons[0].ContextMenuStrip = _spendingOrIncome == 0 ? this.contextMenuStrip_TotalAmountSetting : null;
            this.ToolStripMenuItem_ItemSetting_SetTargetAmount.Enabled = _spendingOrIncome == 0;
            this.DrawMonthlyData(_monthlyData.month);
            this.DrawPlusMark(_spendingOrIncome == 0 ? _dataManager.GetNumberOfKindOfSpendings() : _dataManager.GetNumberOfKindOfIncomes());
        }

        private void listBox_AmountsDetail_MouseUp(object sender, MouseEventArgs e)
        {
            // 右クリックされた場合
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // マウス座標から選択すべきアイテムのインデックスを取得
                int index = this.listBox_AmountsDetail.IndexFromPoint(e.Location);

                // インデックスが取得できたら
                if (index >= 0)
                {
                    // インデックスで指定されるリストボックスの項目を選択された状態にする。
                    this.listBox_AmountsDetail.SelectedIndex = index;

                    // インデックスをフォームのデータメンバに保持する。
                    // このインデックスは，ユーザがコンテキストメニューから「編集」または「削除」を
                    // 選択した後の処理に用いられる。
                    _selectedIndexOfAmountList = index;
                }
            }
        }

        /// <summary>
        /// リストボックスに表示された金額データの右クリックメニューから「編集」をクリックしたときに実行されるメソッド。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_AmountsDetail_Edit_Click(object sender, EventArgs e)
        {
            // 金額データの入力欄を表示する。
            this.textBox_EditAmount.Top = this.listBox_AmountsDetail.Top + (12 * _selectedIndexOfAmountList);
            this.textBox_EditAmount.Left = this.listBox_AmountsDetail.Left + 2;
            this.textBox_EditAmount.BringToFront();

            // 現在登録されている金額をテキストボックスに表示する。
            int amount = -1;
            string comment = "";
            string[] splittedString = ((string)this.listBox_AmountsDetail.SelectedItem).Split(_settings.commentSplitCharacter);
            switch (splittedString.Length)
            {
                case 1:
                    amount = _dataManager.AmountString2Int(splittedString[0]);
                    break;
                case 2:
                    amount = _dataManager.AmountString2Int(splittedString[0]);
                    comment = splittedString[1];
                    break;
                default:
                    throw new Exception("登録されている金額情報の形式が不正です。");
            }

            this.textBox_EditAmount.Text = amount.ToString() + _settings.commentSplitCharacter.ToString() + comment;

            this.textBox_EditAmount.Visible = true;

            this.textBox_EditAmount.Focus();
        }

        /// <summary>
        /// リストボックスに表示された金額データの右クリックメニューから「削除」をクリックしたときに実行されるメソッド。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_AmountsDetail_Remove_Click(object sender, EventArgs e)
        {
            // リストボックスの選択された項目の文字列を取得する。
            string selectedString = (string)this.listBox_AmountsDetail.SelectedItem;

            if (string.IsNullOrEmpty(selectedString) == true)
            {
                throw new InvalidOperationException();
            }

            bool result = false;

            // 選択された金額データをDBから削除する。
            result = _dataManager.RemoveAmountData(_settings.month, _spendingOrIncome, _selectedKindButtonID + 1, selectedString);

            if (result == false)
            {
                MessageBox.Show("金額データを削除できませんでした。");
            }

            // 画面の表示を更新する。
            this.DrawMonthlyData(_settings.month);
        }

        private void ToolStripMenuItem_ItemSetting_SetTargetAmount_Click(object sender, EventArgs e)
        {
            this.textBox_InputTargetAmount.Top = _itemButtons[_selectedKindButtonID].Top + 20;
            this.textBox_InputTargetAmount.Left = _itemButtons[_selectedKindButtonID].Left + 2;
            this.textBox_InputTargetAmount.Text = "";
            this.textBox_InputTargetAmount.Visible = true;
            this.textBox_InputTargetAmount.Focus();
        }

        private void ToolStripMenuItem_ItemSetting_SetName_Click(object sender, EventArgs e)
        {
            this.textBox_SetKindOfAmountName.Top = _itemButtons[_selectedKindButtonID].Top + 20;
            this.textBox_SetKindOfAmountName.Left = _itemButtons[_selectedKindButtonID].Left + 1;
            this.textBox_SetKindOfAmountName.Text = _itemButtons[_selectedKindButtonID].Text;
            this.textBox_SetKindOfAmountName.Visible = true;
            this.textBox_SetKindOfAmountName.Focus();
        }

        private void ToolStripMenuItem_ItemSetting_Remove_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ToolStripMenuItem_TotalAmountSetting_SetTargetAmount_Click(object sender, EventArgs e)
        {
            this.textBox_InputTargetAmount.Top = _itemButtons[_selectedKindButtonID].Top + 20;
            this.textBox_InputTargetAmount.Left = _itemButtons[_selectedKindButtonID].Left + 2;
            this.textBox_InputTargetAmount.Text = "";
            this.textBox_InputTargetAmount.Visible = true;
            this.textBox_InputTargetAmount.Focus();
        }

        /// <summary>
        /// 金額データ入力欄にフォーカスがある状態でEnterキーが押された場合に実行されるメソッド。
        /// 金額データをDBに追加登録する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_InputAmounts_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enterキーが押されていない場合
            if (e.KeyChar != (char)Keys.Enter)
            {
                return;
            }

            // ビープ音を鳴らさないための措置。
            e.Handled = true;

            if (this.textBox_InputAmounts.Text == "")
            {
                this.textBox_InputAmounts.Visible = false;
                return;
            }

            bool result;
            result = _dataManager.AddAmounts(_settings.month, _spendingOrIncome, _selectedKindButtonID + 1, this.textBox_InputAmounts.Text);
            if (result == false)
            {
                string amountSplit = _dataManager.GetAmountsSplitCharacter().ToString();
                string commentSplit = _dataManager.GetCommentSplitCharacter().ToString();
                string messageString = "入力形式が不正です。以下の形式で入力してください。\n【入力例】\n";
                messageString += "108";
                messageString += amountSplit;
                messageString += "160";
                messageString += commentSplit;
                messageString += "メンチカツ";
                messageString += amountSplit;
                messageString += "240";
                messageString += amountSplit;
                messageString += "550";
                messageString += amountSplit;
                messageString += "1000";
                MessageBox.Show(messageString);
                return;
            }

            this.textBox_InputAmounts.Text = "";
            this.textBox_InputAmounts.Visible = false;

            // 画面を更新する。
            this.DrawMonthlyData(_settings.month);
        }

        private void textBox_InputTargetAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enterキーが押されていない場合
            if (e.KeyChar != (char)Keys.Enter)
            {
                return;
            }

            // ビープ音を鳴らさないための措置。
            e.Handled = true;

            bool result;
            int amount = int.Parse(this.textBox_InputTargetAmount.Text);
            result = _dataManager.SetTargetAmount(_spendingOrIncome, _selectedKindButtonID, amount);
            if (result == false)
            {
                MessageBox.Show("入力された金額が不正です。");
                return;
            }

            MessageBox.Show("『" + _dataManager.GetKindOfSpendingList()[_selectedKindButtonID] + "』の目標金額を " + amount.ToString("#,#") + "円 に設定しました。");

            this.textBox_InputTargetAmount.Text = "";
            this.textBox_InputTargetAmount.Visible = false;
        }

        private void textBox_EditAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enterキーが押されていない場合
            if (e.KeyChar != (char)Keys.Enter)
            {
                return;
            }

            // ビープ音を鳴らさないための措置。
            e.Handled = true;

            if (this.textBox_EditAmount.Text == "")
            {
                this.textBox_EditAmount.Visible = false;
                return;
            }

            bool result = _dataManager.EditAmount(
                this.textBox_EditAmount.Text,
                _settings.month,
                _spendingOrIncome,
                _selectedKindButtonID,
                (string)this.listBox_AmountsDetail.SelectedItem
                );

            if (result == false)
            {
                string amountSplit = _dataManager.GetAmountsSplitCharacter().ToString();
                string commentSplit = _dataManager.GetCommentSplitCharacter().ToString();
                string messageString = "入力形式が不正です。以下の形式で入力してください。\n【入力例】\n";
                messageString += "160";
                messageString += commentSplit;
                messageString += "メンチカツ";
                MessageBox.Show(messageString);
                return;
            }

            this.textBox_EditAmount.Text = "";
            this.textBox_EditAmount.Visible = false;

            // 画面の表示を更新する。
            this.DrawMonthlyData(_settings.month);

            List<Payments> data = _spendingOrIncome == 0 ? _monthlyData.spendings : _monthlyData.incomes;
            if (data.Count == 0)
            {
                return;
            }

            // リストボックスの表示内容を空にする。
            this.listBox_AmountsDetail.Items.Clear();
            this.listBox_AmountsDetail.Height = 0;

            // 登録された金額をリストボックスに追加する。
            foreach (Payment p in data[_selectedKindButtonID].payments)
            {
                this.listBox_AmountsDetail.Items.Add(p.amountOfMoney + ":" + p.comment);
                if (this.listBox_AmountsDetail.Height <= 300)
                {
                    this.listBox_AmountsDetail.Height += 20;
                }
            }

            // 登録された金額の一覧を表示する。
            this.listBox_AmountsDetail.Visible = true;
        }

        private void textBox_NewKindOfAmountLabel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enterキーが押されていない場合
            if (e.KeyChar != (char)Keys.Enter)
            {
                return;
            }

            // ビープ音を鳴らさないための措置。
            e.Handled = true;

            // 費用項目入力欄に入力された文字列を取得する。
            string kindOfAmountString = this.textBox_NewKindOfAmountLabel.Text;

            if (string.IsNullOrEmpty(kindOfAmountString) == false)
            {
                // DBに費用項目を登録する。
                bool result = _dataManager.AddKindListOfAmount(_spendingOrIncome, kindOfAmountString);

                // 既に費用項目が登録されている場合
                if (result == false)
                {
                    MessageBox.Show("\"" + kindOfAmountString + "\"は既に項目として追加されています。別の名前を指定してください。");
                    return;
                }
            }

            // 費用項目入力欄を非表示にする。
            this.textBox_NewKindOfAmountLabel.Text = "";
            this.textBox_NewKindOfAmountLabel.Visible = false;

            if (string.IsNullOrEmpty(kindOfAmountString) == false)
            {
                // 画面の表示を更新する。
                this.DrawMonthlyData(_settings.month);
            }

            // 費用項目が19個以下の場合
            if (_kindOfAmountList.Length <= 19)
            {
                // ＋記号を次の位置に表示する。
                this.DrawPlusMark(_kindOfAmountList.Length);
            }

            // 追加された費用項目のボタンにフォーカスを当てる。
            _itemButtons[_kindOfAmountList.Length - 1].Focus();
        }

        private void textBox_SetKindOfAmountName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enterキーが押されていない場合
            if (e.KeyChar != (char)Keys.Enter)
            {
                return;
            }

            // ビープ音を鳴らさないための措置。
            e.Handled = true;

            // 費用項目名入力欄に入力された文字列を取得する。
            string kindOfAmountName = this.textBox_SetKindOfAmountName.Text;

            // 費用項目名に何も設定されていない場合
            if (string.IsNullOrEmpty(kindOfAmountName) == true)
            {
                // 何もしない。
                return;
            }

            _dataManager.SetKindListOfAmount(kindOfAmountName, _spendingOrIncome, _itemButtons[_selectedKindButtonID].Text);

            // 画面の表示を更新する。
            this.DrawMonthlyData(_settings.month);

            this.textBox_SetKindOfAmountName.Visible = false;
        }

        private void button_PrevMonth_Click(object sender, EventArgs e)
        {
            DateTime month = _monthlyData.month.AddMonths(-1);

            this.numericUpDown_Year.ValueChanged -= new EventHandler(this.numericUpDown_Year_ValueChanged);
            this.comboBox_Month.SelectedIndexChanged -= new EventHandler(this.comboBox_Month_SelectedValueChanged);
            this.numericUpDown_Year.Value = month.Year;
            this.comboBox_Month.SelectedIndex = month.Month - 1;
            this.numericUpDown_Year.ValueChanged += new EventHandler(this.numericUpDown_Year_ValueChanged);
            this.comboBox_Month.SelectedIndexChanged += new EventHandler(this.comboBox_Month_SelectedValueChanged);

            this.DrawMonthlyData(month);
        }

        private void button_NextMonth_Click(object sender, EventArgs e)
        {
            DateTime month = _monthlyData.month.AddMonths(1);

            this.numericUpDown_Year.ValueChanged -= new EventHandler(this.numericUpDown_Year_ValueChanged);
            this.comboBox_Month.SelectedIndexChanged -= new EventHandler(this.comboBox_Month_SelectedValueChanged);
            this.numericUpDown_Year.Value = month.Year;
            this.comboBox_Month.SelectedIndex = month.Month - 1;
            this.numericUpDown_Year.ValueChanged += new EventHandler(this.numericUpDown_Year_ValueChanged);
            this.comboBox_Month.SelectedIndexChanged += new EventHandler(this.comboBox_Month_SelectedValueChanged);

            this.DrawMonthlyData(month);
        }

        private void numericUpDown_Year_ValueChanged(object sender, EventArgs e)
        {
            DateTime month = new DateTime((int)this.numericUpDown_Year.Value, this.comboBox_Month.SelectedIndex + 1, 1);
            this.DrawMonthlyData(month);
        }

        private void comboBox_Month_SelectedValueChanged(object sender, EventArgs e)
        {
            this.numericUpDown_Year_ValueChanged(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // メイン画面設定をDBに保存する。
            _dataManager.SetMainFormSettings(_settings);
        }

        private void listBox_AmountsDetail_Click(object sender, EventArgs e)
        {
            this.textBox_EditAmount.Visible = false;
        }
    }
}
