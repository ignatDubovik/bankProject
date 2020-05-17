using bank.Data.Interfaces;
using bank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Mocks
{
    public class MockClients : IClients
    {
        public IEnumerable<Client> AllClients
        {
            get
            {
                return new List<Client>
                {
                    new Client{ fullname="Игнатик",passport="8456428"},
                    new Client{ fullname="qwerty",passport="2213"}
                };
            }
        }
    }
}
