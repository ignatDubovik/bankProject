using bank.Data.Interfaces;
using bank.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Repository
{
    public class ClientRepository : IClients
    {
        private readonly AppDBContent appDBContent;
        public ClientRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Client> AllClients => appDBContent.Client.ToList();
    }
}
