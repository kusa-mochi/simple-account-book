using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApplication
{
    /// <summary>
    /// メイン画面の初期化情報を格納する構造体。
    /// </summary>
    public struct MainFormSettings
    {
        /// <summary>
        /// 家計データを表示する年月
        /// </summary>
        public DateTime month;

        /// <summary>
        /// 金額の区切り文字。
        /// 金額データ追加入力時に用いられる。
        /// </summary>
        public char amountsSplitCharacter;

        /// <summary>
        /// コメントの区切り文字。
        /// 金額データの追加入力時，変更時，表示時に用いられる。
        /// </summary>
        public char commentSplitCharacter;

        public MainFormSettings(DateTime m, char a, char c)
        {
            month = m;
            amountsSplitCharacter = a;
            commentSplitCharacter = c;
        }
    }
}
