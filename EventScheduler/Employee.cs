using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.DBClasses;
using EventScheduler.Interfaces;

namespace EventScheduler
{
    public class Employee : User, IDBItem
    {
        public Employee(int id, string fname, string lname)
        {
            this.ID = id;
            this.FName = fname;
            this.LName = lname;
        }

        public Employee(string fname, string lname)
        {
            this.FName = fname;
            this.LName = lname;
        }
        public Employee()
        { }
        public bool IsAny(int empID)
        {
            var employees = UseDB.SelectEmployee(empID);
            if (employees.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
