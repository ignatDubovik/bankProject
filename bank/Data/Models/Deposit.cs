using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Models
{
    public class Deposit
    {
        public int id { set; get; }
        public int initialMoney { set; get; }
        public DateTime dateOfOpening { set; get; }
        public int plannedFinalAmountOfMoney { set; get; }
        public int depositTypeID { set; get; }
        public virtual DepositType DepositType { set; get; }
        public int contractNumber { set; get; }
        public int clientID { set; get; }
        public virtual Client Client { set; get; }
    }
}
