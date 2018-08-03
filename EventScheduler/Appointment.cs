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
        public int EmployeeID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public string EmployeeName { get; set; }

        public Appointment()
        {

        }
        public Appointment(int customerID, int serviceId, int employeeID, DateTime startTime, DateTime endTime)
        {
            this.CustomerID = customerID;
            this.ServiceID = serviceId;
            this.EmployeeID = employeeID;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

    }
}
