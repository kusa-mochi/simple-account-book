using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApplication
{
    /// <summary>
    /// 家計の推移閲覧画面の初期化情報を格納する構造体。
    /// </summary>
    public struct TransitionViewerSettings
    {
        /// <summary>
        /// 表示期間の最初の月
        /// </summary>
        public DateTime monthFrom;

        /// <summary>
        /// 表示期間の最後の月
        /// </summary>
        public DateTime monthTo;

        /// <summary>
        /// 費用項目に対応するID
        /// </summary>
        public int kindOfAmountID;

        public TransitionViewerSettings(DateTime from, DateTime to, int id)
        {
            monthFrom = from;
            monthTo = to;
            kindOfAmountID = id;
        }
    }
}
