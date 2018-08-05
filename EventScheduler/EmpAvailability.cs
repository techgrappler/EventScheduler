using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;
using EventScheduler.DBClasses;

namespace EventScheduler
{
    public class EmpAvailability
    {
        public int EmployeeID { get; set; }
        public DateTime Time { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsBooked { get; set; }

        public bool IsEmployeeAvailable(int empID, DateTime startTime, DateTime endTime)
        {
            string dayOfWeek = startTime.DayOfWeek.ToString();
            var dayAvailability = UseDB.SelectDailyAvailabily(empID, dayOfWeek);
            if (startTime.TimeOfDay >= dayAvailability.StartTime && endTime.TimeOfDay <= dayAvailability.EndTime)
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
