using System;
using System.Text;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// DataManagerクラスの動作確認を行うクラス。
    /// SQLiteのDBを作成し，そのDBを用いてデータの追加・編集・削除のテストを行う。
    /// </summary>
    [TestClass]
    public class DataManagerTest
    {
        SQLiteConnection _conn = null;
        SQLiteCommand _command = null;

        public DataManagerTest()
        {
            // Test.dbファイルが存在する場合
            if(File.Exists("Test.db") == true)
            {
                // Test.dbファイルを削除する。
                File.Delete("Test.db");
            }

            // DBファイルを作成する。
            _conn = new SQLiteConnection("Data Source=Test.db");

            // DBに接続する。
            _conn.Open();
        }

        ~DataManagerTest()
        {
        }

        #region 追加のテスト属性

        // 各テストを実行する前に実行されるメソッド。
        [TestInitialize()]
        public void EachTestInitialize()
        {
            // SQLコマンドを格納するインスタンス
            _command = _conn.CreateCommand();

            // 空のテーブルを作成する。
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

            // 各テーブルにテストデータを挿入する。
            _command.CommandText = "insert into 支出 (年月,費用項目ID,金額,説明) values ";
            _command.CommandText += "('2015/08',2,216,''),";
            _command.CommandText += "('2015/08',2,233,''),";
            _command.CommandText += "('2015/08',2,218,'あああ'),";
            _command.CommandText += "('2015/08',3,219,'いいい'),";
            _command.CommandText += "('2015/08',3,256,''),";
            _command.CommandText += "('2015/08',4,256,'ううう'),";
            _command.CommandText += "('2015/08',4,256,''),";
            _command.CommandText += "('2015/08',4,256,''),";
            _command.CommandText += "('2015/08',5,256,'いいい'),";
            _command.CommandText += "('2015/08',5,256,'あああ'),";
            _command.CommandText += "('2015/08',5,226,''),";
            _command.CommandText += "('2015/08',5,227,'えええ'),";
            _command.CommandText += "('2015/09',2,233,''),";
            _command.CommandText += "('2015/09',2,233,'おおお'),";
            _command.CommandText += "('2015/09',2,1080,''),";
            _command.CommandText += "('2015/09',2,233,''),";
            _command.CommandText += "('2015/09',3,233,''),";
            _command.CommandText += "('2015/09',3,233,''),";
            _command.CommandText += "('2015/09',4,233,'いいい'),";
            _command.CommandText += "('2015/09',4,12345,'あああ'),";
            _command.CommandText += "('2015/09',5,236,''),";
            _command.CommandText += "('2015/09',5,256,'おおお'),";
            _command.CommandText += "('2015/09',5,238,''),";
            _command.CommandText += "('2015/10',2,239,''),";
            _command.CommandText += "('2015/10',3,240,''),";
            _command.CommandText += "('2015/10',3,219,'えええ'),";
            _command.CommandText += "('2015/10',4,252,'あああ'),";
            _command.CommandText += "('2015/10',4,4567,''),";
            _command.CommandText += "('2015/10',5,252,'あああ'),";
            _command.CommandText += "('2015/10',5,245,''),";
            _command.CommandText += "('2015/11',2,246,'あああ'),";
            _command.CommandText += "('2015/11',2,233,'ううう'),";
            _command.CommandText += "('2015/11',2,233,'おおお'),";
            _command.CommandText += "('2015/11',2,233,''),";
            _command.CommandText += "('2015/11',2,233,''),";
            _command.CommandText += "('2015/11',3,233,''),";
            _command.CommandText += "('2015/11',3,233,'あああ'),";
            _command.CommandText += "('2015/11',3,256,'けろけろ'),";
            _command.CommandText += "('2015/11',4,256,'あああ'),";
            _command.CommandText += "('2015/11',5,256,''),";
            _command.CommandText += "('2015/11',5,256,'あああ'),";
            _command.CommandText += "('2015/11',5,257,'')";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into 収入 (年月,費用項目ID,金額,説明) values ";
            _command.CommandText += "('2015/08',2,240000,''),";
            _command.CommandText += "('2015/08',3,150000,''),";
            _command.CommandText += "('2015/09',2,238000,''),";
            _command.CommandText += "('2015/09',3,145000,'おおお'),";
            _command.CommandText += "('2015/09',4,3000,''),";
            _command.CommandText += "('2015/10',2,280000,''),";
            _command.CommandText += "('2015/10',3,190000,''),";
            _command.CommandText += "('2015/11',2,267000,'あああ'),";
            _command.CommandText += "('2015/11',3,178000,'ううう'),";
            _command.CommandText += "('2015/12',4,500000,''),";
            _command.CommandText += "('2015/12',2,280000,'あああ'),";
            _command.CommandText += "('2015/12',3,200000,'')";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into 支出項目 (費用項目,目標金額) values ";
            _command.CommandText += "('総支出',220000),";
            _command.CommandText += "('食料品',25000),";
            _command.CommandText += "('買い食い',3000),";
            _command.CommandText += "('外食',12000),";
            _command.CommandText += "('日用品',10000)";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into 収入項目 (費用項目,目標金額) values ";
            _command.CommandText += "('総収入',480000),";
            _command.CommandText += "('頼太収入',280000),";
            _command.CommandText += "('玲子収入',200000),";
            _command.CommandText += "('その他',0)";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into メイン画面設定 (設定項目,設定値) values ";
            _command.CommandText += "('年月','2015/12')";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into 月毎のデータ画面設定 (設定項目,設定値) values ";
            _command.CommandText += "('年月','2015/12')";
            _command.ExecuteNonQuery();

            _command.CommandText = "insert into 家計の推移画面設定 (設定項目,設定値) values ";
            _command.CommandText += "('開始年月','2015/09'),";
            _command.CommandText += "('終了年月','2015/12'),";
            _command.CommandText += "('費用項目ID','3')";
            _command.ExecuteNonQuery();
        }

        // 各テストを実行した後に実行されるメソッド。
        [TestCleanup()]
        public void EachTestCleanup()
        {
            // DB内のすべてのテーブルを削除する。
            _command.CommandText = "drop table 支出;drop table 収入;drop table 支出項目;drop table 収入項目;drop table メイン画面設定;drop table 月毎のデータ画面設定;drop table 家計の推移画面設定;";
            _command.ExecuteNonQuery();
        }

        #endregion

        #region AddAmountsメソッドのテスト

        [TestMethod]
        public void TestAddAmounts_OneAmount()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        [TestMethod]
        public void TestAddAmounts_OneAmount_InvalidCharacter()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        [TestMethod]
        public void TestAddAmounts_OneAmountAndComment()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        [TestMethod]
        public void TestAddAmounts_OneAmountAndComment_InvalidCharacterInComment()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        [TestMethod]
        public void TestAddAmounts_TenAmounts()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        [TestMethod]
        public void TestAddAmounts_TenAmounts_InvalidCharacter()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        [TestMethod]
        public void TestAddAmounts_TenAmountsAndComments()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        [TestMethod]
        public void TestAddAmounts_TenAmountsAndComments_InvalidCharacterInComment()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        [TestMethod]
        public void TestAddAmounts_Empty()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        [TestMethod]
        public void TestAddAmounts_Null()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //
        }

        #endregion
    }
}
