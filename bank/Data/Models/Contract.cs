using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Models
{
    public class Contract
    {
        public int id { set; get; }
        public DateTime dateOfSigning { set; get; }
        public int clientID { set; get; }
        public virtual Client Client { set; get; }
        public int employeeID { set; get; }
        public virtual Employee Employee { set; get; }
    }
}
