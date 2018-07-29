using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;

namespace EventScheduler
{
    public abstract class User : IUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public int ID { get; set; }
    }
}
