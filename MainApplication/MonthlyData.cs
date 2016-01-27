using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApplication
{
    /// <summary>
    /// 1ヶ月分の家計データを格納する構造体。
    /// </summary>
    public struct MonthlyData
    {
        /// <summary>
        /// 年月
        /// </summary>
        public DateTime month;

        /// <summary>
        /// 収入
        /// </summary>
        public List<Payments> incomes;

        public bool existIncomeData;

        /// <summary>
        /// 支出
        /// </summary>
        public List<Payments> spendings;

        public bool existSpendingData;

        public char commentSplitCharacter;

        public MonthlyData(DateTime m, char cSplitChar, List<Payments> inc = null, bool existIn = false, List<Payments> ps = null, bool existSp = false)
        {
            month = m;
            incomes = inc;
            existIncomeData = existIn;
            spendings = ps;
            existSpendingData = existSp;
            commentSplitCharacter = cSplitChar;
        }

        public int GetTotalIncome()
        {
            return this.TotalPayments(incomes);
        }

        public int GetTotalSpending()
        {
            return this.TotalPayments(spendings);
        }

        public int GetTotalPerKindOfPayment(int spendingOrIncome, string kindOfPayment)
        {
            if (kindOfPayment == "総支出")
            {
                return this.GetTotalSpending();
            }

            if (kindOfPayment == "総収入")
            {
                return this.GetTotalIncome();
            }

            foreach (Payments ps in ((spendingOrIncome == 0) ? spendings : incomes))
            {
                if (ps.label == kindOfPayment)
                {
                    return ps.GetSum();
                }
            }

            return 0;
        }

        public string GetPopupStringOfPayment(int spendingOrIncome, string kindOfPayment)
        {
            string output = "";

            foreach (Payments ps in ((spendingOrIncome == 0) ? spendings : incomes))
            {
                if (ps.payments == null)
                {
                    continue;
                }

                if (ps.label == kindOfPayment)
                {
                    foreach (Payment p in ps.payments)
                    {
                        output += p.amountOfMoney.ToString("#,0");
                        if ((p.comment != null) && (p.comment.Length > 0))
                        {
                            output += commentSplitCharacter.ToString() + p.comment;
                        }
                        output += "\n";
                    }

                    return output;
                }
            }

            return null;
        }

        private int TotalPayments(List<Payments> psList)
        {
            int output = 0;
            foreach (Payments ps in psList)
            {
                output += ps.GetSum();
            }

            return output;
        }
    }
}
