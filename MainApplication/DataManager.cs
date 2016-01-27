using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MainApplication
{
    public class DataManager
    {
        #region データメンバ

        private string _dbFilePath = null;
        private SQLiteConnection _conn = null;
        private SQLiteCommand _command = null;
        private ApplicationSettings _applicationSettings;
        private bool _fileExists = true;

        #endregion

        #region コンストラクタ

        public DataManager(string dbFilePath)
        {
            if (string.IsNullOrEmpty(dbFilePath) == true)
            {
                throw new ArgumentNullException("dbFilePath");
            }

            _fileExists = File.Exists(CommonConst.DBFileName);

            _dbFilePath = dbFilePath;

            // DBとの接続を開始する。
            _conn = new SQLiteConnection("Data Source=" + CommonConst.DBFileName);

            // DBに接続する。
            _conn.Open();
            _command = _conn.CreateCommand();

            // DBファイルが新規作成された場合
            if (_fileExists == false)
            {
                // 空のテーブルを作成する。
                this.CreateNewTables();

                _fileExists = true;
            }

            // アプリケーション設定
            _applicationSettings.amountSplitCharacter = this.GetAmountsSplitCharacter();
            _applicationSettings.commentSplitCharacter = this.GetCommentSplitCharacter();
        }

        #endregion

        #region デストラクタ

        ~DataManager()
        {
        }

        #endregion

        #region パブリックメソッド

        /// <summary>
        /// 指定された年月の家計データを返す。
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public MonthlyData GetMonthlyData(DateTime month)
        {
            MonthlyData data = new MonthlyData();
            data.month = month;
            data.spendings = new List<Payments>();
            data.existSpendingData = false;
            data.incomes = new List<Payments>();
            data.existIncomeData = false;
            data.commentSplitCharacter = this.GetCommentSplitCharacter();

            int nKindOfSpending = this.GetNumberOfKindOfSpendings();
            string[] kindOfSpendingList = this.GetKindOfSpendingList();
            for (int iKindOfSpending = 0; iKindOfSpending < nKindOfSpending; iKindOfSpending++)
            {
                List<Payment> paymentList = new List<Payment>();

                _command.CommandText = @"select * from 支出";
                _command.CommandText += @" where 年月='" + month.ToString("yyyy/MM") + @"'";
                _command.CommandText += @" and 費用項目ID=" + (iKindOfSpending + 1);
                using (SQLiteDataReader reader = _command.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        paymentList.Add(new Payment(int.Parse(reader["金額"].ToString()), reader["説明"].ToString()));
                    }
                }

                if (paymentList.Count > 0)
                {
                    data.spendings.Add(new Payments(kindOfSpendingList[iKindOfSpending], paymentList));
                    data.existSpendingData = true;
                }
                else
                {
                    data.spendings.Add(new Payments(kindOfSpendingList[iKindOfSpending], null));
                }
            }

            int nKindOfIncome = this.GetNumberOfKindOfIncomes();
            string[] kindOfIncomeList = this.GetKindOfIncomeList();
            for (int iKindOfIncome = 0; iKindOfIncome < nKindOfIncome; iKindOfIncome++)
            {
                List<Payment> paymentList = new List<Payment>();

                _command.CommandText = @"select * from 収入";
                _command.CommandText += @" where 年月='" + month.ToString("yyyy/MM") + @"'";
                _command.CommandText += @" and 費用項目ID=" + (iKindOfIncome + 1);
                using (SQLiteDataReader reader = _command.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        paymentList.Add(new Payment(int.Parse(reader["金額"].ToString()), reader["説明"].ToString()));
                    }
                }

                if (paymentList.Count > 0)
                {
                    data.incomes.Add(new Payments(kindOfIncomeList[iKindOfIncome], paymentList));
                    data.existIncomeData = true;
                }
                else
                {
                    data.incomes.Add(new Payments(kindOfIncomeList[iKindOfIncome], null));
                }
            }

            return data;
        }

        /// <summary>
        /// 費用項目に対応する目標金額を返す。
        /// 目標金額が設定されていない場合は0を返す。
        /// </summary>
        /// <param name="kindOfAmountID"></param>
        /// <param name="spendingOrIncome"></param>
        /// <returns></returns>
        public int GetTargetAmount(int spendingOrIncome, int kindOfAmountID)
        {
            _command.CommandText = "select 目標金額 ";
            _command.CommandText += "from " + (spendingOrIncome == 0 ? "支出項目 " : "収入項目 ");
            _command.CommandText += "where 費用項目ID=" + (kindOfAmountID + 1);
            string targetAmountString = _command.ExecuteScalar().ToString();
            if (string.IsNullOrEmpty(targetAmountString) == true)
            {
                return 0;
            }
            int targetAmount = int.Parse(targetAmountString);
            return targetAmount;
        }

        public bool SetTargetAmount(int spendingOrIncome, int kindOfAmountID, int targetAmount)
        {
            _command.CommandText = "update " + ((spendingOrIncome == 0) ? "支出項目 " : "収入項目 ");
            _command.CommandText += "set 目標金額=" + targetAmount.ToString() + " ";
            _command.CommandText += "where 費用項目ID=" + (kindOfAmountID + 1);

            try
            {
                _command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// メイン画面の初期表示内容を決定するためのパラメータを返す。
        /// </summary>
        /// <returns></returns>
        public MainFormSettings GetMainFormSettings()
        {
            // 家計簿の表示年月
            _command.CommandText = "select 設定値 from メイン画面設定 where 設定項目='年月'";
            string monthString = _command.ExecuteScalar().ToString();
            if (string.IsNullOrEmpty(monthString) == true)
            {
                throw new InvalidDataException("MainFormSettings");
            }

            string[] splittedMonthStrings = monthString.Split('/');
            DateTime month = new DateTime(int.Parse(splittedMonthStrings[0]), int.Parse(splittedMonthStrings[1]), 1);

            // 金額の区切り文字
            char amountSplitCharacter = _applicationSettings.amountSplitCharacter;

            // コメントの区切り文字
            char commentSplitCharacter = _applicationSettings.commentSplitCharacter;

            return new MainFormSettings(month, amountSplitCharacter, commentSplitCharacter);
        }

        /// <summary>
        /// メイン画面の初期表示内容を決定するためのパラメータをDBに保存する。
        /// </summary>
        /// <returns></returns>
        public void SetMainFormSettings(MainFormSettings settings)
        {
            _command.CommandText = "update メイン画面設定 ";
            _command.CommandText += "set 設定値='" + settings.month.ToString("yyyy/MM") + "' ";
            _command.CommandText += "where 設定項目='年月'";
            _command.ExecuteNonQuery();
        }

        /// <summary>
        /// 月毎のデータ閲覧画面の初期表示内容を決定するためのパラメータを返す。
        /// </summary>
        /// <returns></returns>
        public MonthlyDataViewerSettings GetMonthlyDataViewerSettings()
        {
            _command.CommandText = "select 設定値 from 月毎のデータ画面設定 where 設定項目='年月'";
            string monthString = _command.ExecuteScalar().ToString();
            if (string.IsNullOrEmpty(monthString) == true)
            {
                throw new InvalidDataException("MonthlyDataViewerSettings");
            }

            string[] splittedMonthStrings = monthString.Split('/');
            DateTime month = new DateTime(int.Parse(splittedMonthStrings[0]), int.Parse(splittedMonthStrings[1]), 1);
            return new MonthlyDataViewerSettings(month);
        }

        /// <summary>
        /// 月毎のデータ閲覧画面の初期表示内容を決定するためのパラメータをDBに保存する。
        /// </summary>
        /// <returns></returns>
        public void SetMonthlyDataViewerSettings(MonthlyDataViewerSettings settings)
        {
            _command.CommandText = "update 月毎のデータ画面設定 ";
            _command.CommandText += "set 設定値='" + settings.month.ToString("yyyy/MM") + "' ";
            _command.CommandText += "where 設定項目='年月'";
            _command.ExecuteNonQuery();
        }

        /// <summary>
        /// 家計の推移閲覧画面の初期表示内容を決定するためのパラメータを返す。
        /// </summary>
        /// <returns></returns>
        public TransitionViewerSettings GetTransitionViewerSettings()
        {
            _command.CommandText = "select 設定値 from 家計の推移画面設定 where 設定項目='開始年月'";
            string startMonthString = _command.ExecuteScalar().ToString();
            _command.CommandText = "select 設定値 from 家計の推移画面設定 where 設定項目='終了年月'";
            string endMonthString = _command.ExecuteScalar().ToString();
            _command.CommandText = "select 設定値 from 家計の推移画面設定 where 設定項目='費用項目ID'";
            int kindOfAmountID = int.Parse(_command.ExecuteScalar().ToString());
            if (
                (string.IsNullOrEmpty(startMonthString) == true) ||
                (string.IsNullOrEmpty(endMonthString) == true) ||
                (kindOfAmountID < 0)
                )
            {
                throw new InvalidDataException("MonthlyDataViewerSettings");
            }

            string[] splittedStartMonthStrings = startMonthString.Split('/');
            DateTime startMonth = new DateTime(int.Parse(splittedStartMonthStrings[0]), int.Parse(splittedStartMonthStrings[1]), 1);
            string[] splittedEndMonthStrings = endMonthString.Split('/');
            DateTime endMonth = new DateTime(int.Parse(splittedEndMonthStrings[0]), int.Parse(splittedEndMonthStrings[1]), 1);
            return new TransitionViewerSettings(startMonth, endMonth, kindOfAmountID);
        }

        /// <summary>
        /// 家計の推移閲覧画面の初期表示内容を決定するためのパラメータをDBに保存する。
        /// </summary>
        /// <returns></returns>
        public void SetTransitionViewerSettings(TransitionViewerSettings settings)
        {
            _command.CommandText = "update 家計の推移画面設定 ";
            _command.CommandText += "set 設定値='" + settings.monthFrom.ToString("yyyy/MM") + "' ";
            _command.CommandText += "where 設定項目='開始年月'";
            _command.ExecuteNonQuery();

            _command.CommandText = "update 家計の推移画面設定 ";
            _command.CommandText += "set 設定値='" + settings.monthTo.ToString("yyyy/MM") + "' ";
            _command.CommandText += "where 設定項目='終了年月'";
            _command.ExecuteNonQuery();

            _command.CommandText = "update 家計の推移画面設定 ";
            _command.CommandText += "set 設定値='" + settings.kindOfAmountID + "' ";
            _command.CommandText += "where 設定項目='費用項目ID'";
            _command.ExecuteNonQuery();
        }

        /// <summary>
        /// すべての支出項目名を格納した配列を返す。
        /// </summary>
        /// <returns></returns>
        public string[] GetKindOfSpendingList()
        {
            List<string> output = new List<string>();

            _command.CommandText = "select 費用項目 from 支出項目";
            using (SQLiteDataReader reader = _command.ExecuteReader())
            {
                while (reader.Read() == true)
                {
                    output.Add(reader["費用項目"].ToString());
                }
            }

            return output.ToArray();
        }

        public string[] GetKindOfIncomeList()
        {
            List<string> output = new List<string>();

            _command.CommandText = "select 費用項目 from 収入項目";
            using (SQLiteDataReader reader = _command.ExecuteReader())
            {
                while (reader.Read() == true)
                {
                    output.Add(reader["費用項目"].ToString());
                }
            }

            return output.ToArray();
        }

        public int GetNumberOfKindOfSpendings()
        {
            _command.CommandText = "select count(*) from 支出項目";
            long output = (long)_command.ExecuteScalar();
            return (int)output;
        }

        public int GetNumberOfKindOfIncomes()
        {
            _command.CommandText = "select count(*) from 収入項目";
            long output = (long)_command.ExecuteScalar();
            return (int)output;
        }

        /// <summary>
        /// 指定された年月に支出／収入データを追加する。
        /// </summary>
        /// <param name="month">年月</param>
        /// <param name="spendingOrIncome">支出／収入の指定</param>
        /// <param name="kindOfPayment">費用項目ID</param>
        /// <param name="inputString">金額データ（区切り文字を用いて複数渡される場合もある）</param>
        /// <returns></returns>
        public bool AddAmounts(DateTime month, int spendingOrIncome, int kindOfPaymentID, string inputString)
        {
            if ((spendingOrIncome != 0) && (spendingOrIncome != 1))
            {
                throw new ArgumentOutOfRangeException("spendingOrIncome");
            }
            if (kindOfPaymentID < 0)
            {
                throw new ArgumentOutOfRangeException("kindOfPaymentID");
            }
            if (inputString == null)
            {
                throw new ArgumentNullException("inputString");
            }

            // DBに金額情報を登録するためのSQL文。
            _command.CommandText = "insert into ";
            _command.CommandText += spendingOrIncome == 0 ? "支出 " : "収入 ";
            _command.CommandText += "(年月,費用項目ID,金額,説明) values ";

            string[] splittedStrings = inputString.Split(_applicationSettings.amountSplitCharacter);
            foreach (var splittedString in splittedStrings.Select((v, i) => new { Index = i, Value = v }))
            {
                if (string.IsNullOrWhiteSpace(splittedString.Value) == true)
                {
                    continue;
                }

                string[] amountAndComment = splittedString.Value.Split(_applicationSettings.commentSplitCharacter);

                try
                {
                    int.Parse(amountAndComment[0]);
                }
                catch
                {
                    // 入力フォーマットが不正であるため，falseを返す。
                    return false;
                }

                string amount = "";
                string comment = "";
                switch (amountAndComment.Length)
                {
                    case 1:
                        amount = amountAndComment[0];
                        Trace.WriteLine("金額：" + amount);
                        break;
                    case 2:
                        amount = amountAndComment[0];
                        if (string.IsNullOrWhiteSpace(amount) == true)
                        {
                            continue;
                        }

                        comment = amountAndComment[1];
                        Trace.WriteLine("金額：" + amount + "，説明：" + comment);
                        break;
                    default:
                        // 入力フォーマットが不正であるため，falseを返す。
                        return false;
                }

                // SQL文を追記する。
                _command.CommandText += "('" + month.ToString("yyyy/MM") + "'," + kindOfPaymentID + "," + amount + ",'" + comment + "')";
                if (splittedString.Index < splittedStrings.Length - 1)
                {
                    _command.CommandText += ",";
                }
            }

            // SQL文を実行し，金額情報をDBに登録する。
            _command.ExecuteNonQuery();

            return true;
        }

        public bool EditAmount(string inputString, DateTime month, int spendingOrIncome, int kindOfAmountID, string selectedString)
        {
            // テキストボックスに入力された文字列を，金額データと説明文とに分ける。
            string[] splittedInputStrings = inputString.Split(_applicationSettings.commentSplitCharacter);

            try
            {
                int.Parse(splittedInputStrings[0]);
            }
            catch
            {
                // 入力フォーマットが不正であるため，falseを返す。
                return false;
            }

            string inputAmount = null;
            string inputComment = null;
            switch (splittedInputStrings.Length)
            {
                case 1:
                    inputAmount = splittedInputStrings[0];
                    break;
                case 2:
                    inputAmount = splittedInputStrings[0];
                    inputComment = splittedInputStrings[1];
                    break;
                default:
                    throw new InvalidDataException("inputString");
            }

            if (inputAmount == null)
            {
                throw new InvalidDataException("inputString");
            }

            // リストボックスで選択された文字列を，金額データと説明文とに分ける。
            string[] splittedSelectedStrings = selectedString.Split(_applicationSettings.commentSplitCharacter);
            string selectedAmount = null;
            string selectedComment = null;
            switch (splittedSelectedStrings.Length)
            {
                case 1:
                    selectedAmount = splittedSelectedStrings[0];
                    break;
                case 2:
                    selectedAmount = splittedSelectedStrings[0];
                    selectedComment = splittedSelectedStrings[1];
                    break;
                default:
                    throw new InvalidDataException("selectedString");
            }

            if (selectedAmount == null)
            {
                throw new InvalidDataException("selectedString");
            }

            _command.CommandText = "update " + ((spendingOrIncome == 0) ? "支出 " : "収入 ");
            _command.CommandText += "set 金額=" + inputAmount + ",説明='" + ((inputComment != null) ? inputComment : "") + "' ";
            _command.CommandText += "where No=(";
            _command.CommandText += "select max(No) ";
            _command.CommandText += "from " + ((spendingOrIncome == 0) ? "支出 " : "収入 ");
            _command.CommandText += "where ";
            _command.CommandText += "年月='" + month.ToString("yyyy/MM") + "' and ";
            _command.CommandText += "費用項目ID=" + (kindOfAmountID + 1) + " and ";
            _command.CommandText += "金額=" + selectedAmount;
            if (selectedComment != null)
            {
                _command.CommandText += " and 説明='" + selectedComment + "'";
            }
            _command.CommandText += ")";
            _command.ExecuteNonQuery();

            return true;
        }

        public bool AddKindListOfAmount(int spendingOrIncome, string kind)
        {
            if (this.GetNumberOfKindOfSpendings() == 20)
            {
                throw new InvalidOperationException();
            }

            if ((spendingOrIncome != 0) && (spendingOrIncome != 1))
            {
                throw new ArgumentOutOfRangeException("spendingOrIncome");
            }

            _command.CommandText = "select count(*) ";
            _command.CommandText += "from " + ((spendingOrIncome == 0) ? "支出項目 " : "収入項目 ");
            _command.CommandText += "where 費用項目='" + kind + "'";
            long numberOfExistKind = (long)_command.ExecuteScalar();

            // 既に同名の費用項目がDBに登録されている場合
            if (numberOfExistKind > 0)
            {
                return false;
            }

            _command.CommandText = "insert into ";
            _command.CommandText += spendingOrIncome == 0 ? "支出項目 " : "収入項目 ";
            _command.CommandText += "(費用項目) values ('" + kind + "')";
            _command.ExecuteNonQuery();

            return true;
        }

        /// <summary>
        /// 費用項目名を変更する。
        /// </summary>
        /// <param name="inputKindName"></param>
        /// <param name="spendingOrIncome"></param>
        /// <param name="selectedKindName"></param>
        public void SetKindListOfAmount(string inputKindName, int spendingOrIncome, string selectedKindName)
        {
            if (inputKindName == null)
            {
                throw new ArgumentNullException("inputKindName");
            }
            if ((spendingOrIncome != 0) && (spendingOrIncome != 1))
            {
                throw new ArgumentOutOfRangeException("spendingOrIncome");
            }
            if (selectedKindName == null)
            {
                throw new ArgumentNullException("selectedKindName");
            }

            _command.CommandText = "update " + ((spendingOrIncome == 0) ? "支出項目 " : "収入項目 ");
            _command.CommandText += "set 費用項目='" + inputKindName + "' ";
            _command.CommandText += "where 費用項目='" + selectedKindName + "'";
            _command.ExecuteNonQuery();
        }

        public bool RemoveAmountData(DateTime month, int spendingOrIncome, int kindOfAmountID, string selectedString)
        {
            // 引数に渡された文字列を，金額データと説明文とに分ける。
            string[] splittedStrings = selectedString.Split(_applicationSettings.commentSplitCharacter);
            int amount = 0;
            string comment = null;
            switch (splittedStrings.Length)
            {
                case 1:
                    amount = this.String2Int(splittedStrings[0]);
                    break;
                case 2:
                    amount = this.String2Int(splittedStrings[0]);
                    comment = splittedStrings[1];
                    break;
                default:
                    throw new InvalidDataException("selectedString");
            }

            //if (amount == null)
            //{
            //    throw new InvalidDataException("selectedString");
            //}

            _command.CommandText = "delete ";
            _command.CommandText += "from " + ((spendingOrIncome == 0) ? "支出 " : "収入 ");
            _command.CommandText += "where No=(";
            _command.CommandText += "select max(No) ";
            _command.CommandText += "from " + ((spendingOrIncome == 0) ? "支出 " : "収入 ");
            _command.CommandText += "where ";
            _command.CommandText += "年月='" + month.ToString("yyyy/MM") + "' and ";
            _command.CommandText += "費用項目ID=" + kindOfAmountID + " and ";
            _command.CommandText += "金額=" + amount.ToString();
            if (comment != null)
            {
                _command.CommandText += " and 説明='" + comment + "'";
            }
            _command.CommandText += ")";
            _command.ExecuteNonQuery();

            return true;
        }

        public char GetAmountsSplitCharacter()
        {
            _command.CommandText = "select 設定値 from アプリ設定 where 設定項目='金額の区切り文字'";
            char amountSplitCharacter = _command.ExecuteScalar().ToString()[0];
            return amountSplitCharacter;
        }

        public void SetAmountsSplitCharacter(char c)
        {
            _command.CommandText = "update アプリ設定 set 設定値='" + c.ToString() + "' where 設定項目='金額の区切り文字'";
            _command.ExecuteNonQuery();

            _applicationSettings.amountSplitCharacter = c;
        }

        public char GetCommentSplitCharacter()
        {
            _command.CommandText = "select 設定値 from アプリ設定 where 設定項目='コメントの区切り文字'";
            char commentSplitCharacter = _command.ExecuteScalar().ToString()[0];
            return commentSplitCharacter;
        }

        public void SetCommentSplitCharacter(char c)
        {
            _command.CommandText = "update アプリ設定 set 設定値='" + c.ToString() + "' where 設定項目='コメントの区切り文字'";
            _command.ExecuteNonQuery();

            _applicationSettings.commentSplitCharacter = c;
        }

        public bool ExistSpendingData(DateTime month)
        {
            _command.CommandText = "select count(*) from 支出 where 年月='" + month.ToString("yyyy/MM") + "'";
            long numberOfExistedData = (long)_command.ExecuteScalar();
            return (numberOfExistedData > 0);
        }

        public DateTime GetNewestSpendingDataMonth()
        {
            _command.CommandText = "select 年月 from 支出 order by 年月 desc";
            using (SQLiteDataReader reader = _command.ExecuteReader())
            {
                if (reader.Read() == true)
                {
                    string[] splittedString = reader["年月"].ToString().Split('/');
                    return new DateTime(int.Parse(splittedString[0]), int.Parse(splittedString[1]), 1);
                }
                else
                {
                    return DateTime.Now;
                }
            }
        }

        #endregion

        #region プライベートメソッド

        private void CreateNewTables()
        {
            _command.CommandText = "create table 支出 (No integer not null primary key autoincrement, 年月 text not null, 費用項目ID integer not null, 金額 integer not null, 説明 text)";
            _command.ExecuteNonQuery();
            _command.CommandText = "create table 収入 (No integer not null primary key autoincrement, 年月 text not null, 費用項目ID integer not null, 金額 integer not null, 説明 text)";
            _command.ExecuteNonQuery();
            _command.CommandText = "create table 支出項目 (費用項目ID integer not null primary key autoincrement, 費用項目 text not null, 目標金額 integer)";
            _command.ExecuteNonQuery();
            _command.CommandText = "create table 収入項目 (費用項目ID integer not null primary key autoincrement, 費用項目 text not null, 目標金額 integer)";
            _command.ExecuteNonQuery();
            _command.CommandText = "create table メイン画面設定 (設定項目 text not null primary key, 設定値 text)";
            _command.ExecuteNonQuery();
            _command.CommandText = "create table 月毎のデータ画面設定 (設定項目 text not null primary key, 設定値 text)";
            _command.ExecuteNonQuery();
            _command.CommandText = "create table 家計の推移画面設定 (設定項目 text not null primary key, 設定値 text)";
            _command.ExecuteNonQuery();
            _command.CommandText = "create table アプリ設定 (設定項目 text not null primary key, 設定値 text)";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into 支出項目 (費用項目) values ";
            _command.CommandText += "('総支出')";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into 収入項目 (費用項目) values ";
            _command.CommandText += "('総収入')";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into メイン画面設定 (設定項目,設定値) values ";
            _command.CommandText += "('年月','" + DateTime.Now.ToString("yyyy/MM") + "')";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into 月毎のデータ画面設定 (設定項目,設定値) values ";
            _command.CommandText += "('年月','" + DateTime.Now.ToString("yyyy/MM") + "')";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into 家計の推移画面設定 (設定項目,設定値) values ";
            _command.CommandText += "('開始年月','" + DateTime.Now.AddMonths(-CommonConst.MaxNumberOfTransitionMonth + 1).ToString("yyyy/MM") + "'),";
            _command.CommandText += "('終了年月','" + DateTime.Now.ToString("yyyy/MM") + "'),";
            _command.CommandText += "('費用項目ID','0')";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into アプリ設定 (設定項目,設定値) values ";
            _command.CommandText += "('金額の区切り文字','+'),('コメントの区切り文字',':')";
            _command.ExecuteNonQuery();
        }

        private int String2Int(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
            {
                throw new ArgumentNullException("s");
            }

            int output = -1;

            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
            if (Int32.TryParse(
                    s,
                    NumberStyles.Integer | NumberStyles.AllowThousands,
                    provider,
                    out output
                    ) == false
                )
            {
                throw new ArgumentException("s");
            }

            return output;
        }

        #endregion
    }
}
