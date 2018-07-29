using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler
{
    public class Availability
    {
        public int EmployeeID { get; set; }
        public DateTime Time { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsBooked { get; set; }

    }
}
