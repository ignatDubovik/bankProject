using bank.Data.Interfaces;
using bank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data.Repository
{
    public class EmployeeRepository : IEmployees
    {
        private readonly AppDBContent appDBContent;
        public EmployeeRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Employee> AllEmployees => appDBContent.Employee.ToList();
        
        public Employee logedEmployee(Employee emp)
        {
            Employee em = new Employee();
            try
            {
                em = appDBContent.Employee.Where(p => (p.EmployeeLogin == emp.EmployeeLogin && p.EmployeePassword== emp.EmployeePassword)).Single();
            }
            catch (Exception ex)
            {
                em.EmployeeLogin = "incorrect";
            }
            return em;
        }
        
    }
}
