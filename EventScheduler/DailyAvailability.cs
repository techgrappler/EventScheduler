using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler
{
    public class DailyAvailability
    {
        public string DayOfWeek { get; set; }
        public int EmployeeID { get; set; }
        public string EmpFName { get; set; }
        public string EmpLName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public DailyAvailability()
        {

        }
        public DailyAvailability (string dayOfWeek, int empId, string empFName, string empLName, TimeSpan startTime, TimeSpan endTime)
        {
            this.DayOfWeek = dayOfWeek;
            this.EmployeeID = empId;
            this.EmpFName = empFName;
            this.EmpLName = empLName;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }
    }
}
