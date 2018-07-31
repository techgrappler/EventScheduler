﻿using System;
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
        public DateTime Time { get; set; }
    }
}
