using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler
{
    public class Customer : User
    {
        public Customer()
        {

        }
        public Customer(string fname, string lname)
        {
            this.FName = fname;
            this.LName = lname;
        }
    }
       
}

