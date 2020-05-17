using bank.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank.Data
{
    public class DBObjects
    {
        public static void initial(AppDBContent content)
        {
            
            

            if(!content.Client.Any())//если ни черта нет
            {
                content.AddRange(
                    new Client
                    {
                        fullname = "Краснов Игорь Петрович",
                        passport = "MR2314253",
                        phoneNumber = "(33)2134321",
                        adress = "Минск, Горького, 21-2"
                    },
                    new Client
                    {
                        fullname = "Петрова Анна Дмитриевна",
                        passport = "MC8721312",
                        phoneNumber = "(33)4441115",
                        adress = "Минск, Голубева, 16-81"
                    }
                    );
            }

            if (!content.Employee.Any())//если ни черта нет
            {
                content.AddRange(
                    new Employee
                    {
                        EmployeeLogin = "admin",
                        EmployeePassword = "qwerty",
                        EmployeeFullName = "Дубовик Игнат Сергеевич",
                        EmployeePhoto = "~/img/admin.jpg",
                        isAdmin = true
                    },
                    new Employee
                    {
                        EmployeeLogin = "ioprig",
                        EmployeePassword = "MR2314253",
                        EmployeeFullName = "~/img/prig.jpg",
                        EmployeePhoto = "Минск, Горького, 27-4",
                        isAdmin = false
                    }
                    );
            }

            if (!content.Contract.Any())//если ни черта нет
            {
                content.AddRange(
                    new Contract
                    {
                        dateOfSigning = new DateTime(2020, 4, 20),
                        clientID = 1,
                        employeeID = 2
                    },
                    new Contract
                    {
                        dateOfSigning = new DateTime(2020, 2, 14),
                        clientID = 2,
                        employeeID = 2
                    }
                    );
            }

            if (!content.DepositType.Any())//если ни черта нет
            {
                content.AddRange(
                    new DepositType
                    {
                        typeName = "Стандарт",
                        minMoney = 100,
                        maxMoney = 10000,
                        period = 120,
                        capitalization = 1,
                        percent = 5,
                    },
                    new DepositType
                    {
                        typeName = "Улучшенный",
                        minMoney = 10000,
                        maxMoney = 100000,
                        period = 64,
                        capitalization = 4,
                        percent = 6,
                    },
                    new DepositType
                    {
                        typeName = "Премьер",
                        minMoney = 100000,
                        maxMoney = 1000000,
                        period = 64,
                        capitalization = 6,
                        percent = 12,
                    },
                    new DepositType
                    {
                        typeName = "Легендарный",
                        minMoney = 1000000,
                        maxMoney = 10000000,
                        period = 48,
                        capitalization = 8,
                        percent = 15,
                    }
                    );
            }

            if (!content.Deposit.Any())//если ни черта нет
            {
                content.AddRange(
                    new Deposit
                    {
                        initialMoney = 100,
                        dateOfOpening = new DateTime(2020,4,4),
                        plannedFinalAmountOfMoney = 34891,
                        depositTypeID = 1,
                        contractNumber = 1,
                        clientID = 1
                    },
                    new Deposit
                    {
                        initialMoney = 100000,
                        dateOfOpening = new DateTime(2019,4,4),
                        plannedFinalAmountOfMoney = 200662,
                        depositTypeID = 3,
                        contractNumber = 1,
                        clientID = 1
                    }
                    );
            }

            content.SaveChanges();
        }
    }
}
