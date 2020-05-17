using bank.Data.Interfaces;
using bank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Repository
{
    public class DepositRepository : IDeposits
    {
        private readonly AppDBContent appDBContent;
        public DepositRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Deposit> AllDeposits => appDBContent.Deposit.ToList();
    }
}
