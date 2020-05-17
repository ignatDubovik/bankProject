using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Models
{
    public class Client
    {
        public int id { set; get; }
        public string fullname { set; get; }
        public string passport { set; get; }
        public string phoneNumber { set; get; }
        public string adress { set; get; }
        public List<Contract> contracts { set; get; }
        public List<Deposit> deposits { set; get; }

    }
}
