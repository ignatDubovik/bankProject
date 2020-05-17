using bank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Interfaces
{
    public interface IClients
    {
        IEnumerable<Client> AllClients { get; }
    }
}
