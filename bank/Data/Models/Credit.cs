using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Models
{
    public class Credit
    {
        public int creditSum { set; get; }
        public int creditPeriod { set; get; }
        public int percent { set; get; }
        public int payout { set; get; } //сумма всех выплат
        public bool isDiff { set; get; }


        public int payOutDiff()
        {
            double payout=0;
            payout = this.creditSum*(1+(((double)this.percent)/100)*(((double)this.creditPeriod+1)/2));

            return (int)payout;
        }

        public int payOutAnnuit()
        {
            double payout = 0;
            //double annualPayout = 0; //ежегодный/ежемесячный платеж
            payout = Math.Pow(1+(((double)this.percent)/100),(double)(this.creditPeriod));
            payout -= 1;
            payout = 1 / payout;
            payout += 1;
            payout *= (((double)this.creditSum)*(((double)this.percent)/100));
            payout *= (double)this.creditPeriod;

            return (int)payout;
        }


    }
}
