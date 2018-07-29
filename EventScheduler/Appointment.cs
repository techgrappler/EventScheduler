using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler
{
    public class Appointment
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ServiceID { get; set; }
        public List <int> EmployeeID { get; set; }

        public string Customer { get; set;}
        public string Service { get; set; }
        public List <string> Employees { get; set; }

        public DateTime Time { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
