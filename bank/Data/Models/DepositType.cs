using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Models
{
    public class DepositType
    {
        public int id { get; set; }
        public string typeName { get; set; }
        public int minMoney { get; set; }
        public int maxMoney { get; set; }
        public int period { get; set; }
        public int capitalization { get; set; }
        public int percent { get; set; }
        public List<Deposit> deposits { set; get; }
    }
}
