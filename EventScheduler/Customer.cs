using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.DBClasses;

namespace EventScheduler
{
    public class Customer : User, IDBItem
    {
        public Customer()
        {

        }
        public Customer(string fname, string lname)
        {
            this.FName = fname;
            this.LName = lname;
        }

        public bool IsAny(int custID)
        {
            var customers = UseDB.SelectCustomer(custID);
            if (customers.Any())
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

