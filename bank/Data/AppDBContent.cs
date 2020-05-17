using bank.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace bank.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options): base(options)
        {

        }
        public DbSet<Client> Client { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<DepositType> DepositType { get; set; }
        public DbSet<Deposit> Deposit { get; set; }
    }
}
