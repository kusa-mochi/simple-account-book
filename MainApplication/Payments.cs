using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApplication
{
    /// <summary>
    /// 1つの費用項目に属する金額を格納する構造体。
    /// </summary>
    public struct Payments
    {
        /// <summary>
        /// 費用項目名
        /// </summary>
        public string label;

        /// <summary>
        /// 費用項目に属する金額と各々の説明文
        /// </summary>
        public List<Payment> payments;

        public Payments(string lab, List<Payment> p = null)
        {
            label = lab;
            payments = p;
        }

        public int GetSum()
        {
            if(payments == null)
            {
                return 0;
            }

            int output = 0;

            foreach (Payment p in payments)
            {
                output += p.amountOfMoney;
            }

            return output;
        }
    }
}
