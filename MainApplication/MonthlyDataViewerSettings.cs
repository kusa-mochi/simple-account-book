using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApplication
{
    /// <summary>
    /// 月毎のデータ閲覧画面の初期化情報を格納する構造体。
    /// </summary>
    public struct MonthlyDataViewerSettings
    {
        /// <summary>
        /// 家計データを表示する年月
        /// </summary>
        public DateTime month;

        public MonthlyDataViewerSettings(DateTime m)
        {
            month = m;
        }
    }
}
