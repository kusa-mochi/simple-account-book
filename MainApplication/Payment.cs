using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApplication
{
    /// <summary>
    /// 1回の支払い／収入の金額と説明文を格納する構造体。
    /// </summary>
    public struct Payment
    {
        public int amountOfMoney;
        public string comment;

        public Payment(int m, string c = null)
        {
            if(m < 0)
            {
                throw new ArgumentOutOfRangeException("m");
            }

            amountOfMoney = m;
            comment = c;
        }
    }
}
