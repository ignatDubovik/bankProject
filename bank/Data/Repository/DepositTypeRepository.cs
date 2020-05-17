using bank.Data.Interfaces;
using bank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Repository
{
    public class DepositTypeRepository : IDepositTypes
    {
        private readonly AppDBContent appDBContent;
        public DepositTypeRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<DepositType> AllDepositTypes => appDBContent.DepositType.ToList();
    }
}
