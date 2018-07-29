using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler
{
    public class Customer : User
    {
        public Customer(int id, string fname, string lname)
        {
            this.ID = id;
            this.FName = fname;
            this.LName = lname;
        }
    }
       
}

