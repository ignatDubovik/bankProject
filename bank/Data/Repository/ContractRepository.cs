using bank.Data.Interfaces;
using bank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Repository
{
    public class ContractRepository : IContracts
    {
        private readonly AppDBContent appDBContent;
        public ContractRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Contract> AllContracts => appDBContent.Contract.ToList();
    }
}
