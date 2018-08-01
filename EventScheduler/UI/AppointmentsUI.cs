using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.DBClasses;

namespace EventScheduler.UI
{
    public class AppointmentsUI : UserInterface
    {
        public AppointmentsUI()
        {

        }
        public AppointmentsUI(string title)
        {
            this.HeaderTitle = title;
        }

        public new void DisplayScreen()
        {
            DisplayHeader();
            DisplayBody();
            DisplayFooter();
        }

        private new void DisplayBody()
        {
            var appointments = UseDB.ReadAppointments();

            appointments.OrderBy(appointment => appointment.Time);
            appointments.OrderBy(appointment => appointment.EmployeeID);
            Console.WriteLine("There are {0} appointments", appointments.Count);
            Console.WriteLine("{0, -40} {1, -40} {2, -40} {3, -40}", "Employee", "Service", "Customer", "Date\\Time");
            foreach (Appointment apt in appointments)
            {
                Console.WriteLine("{0, -40} {1, -40} {2, -40} {3, -40}", apt.EmployeeName, apt.ServiceName, apt.CustomerName, apt.Time);
            }
        }

        private new void DisplayFooter()
        {
            var options = new string[] {
                "View Appointments",
                "Book New Appointment"
            };

            var count = 1;
            foreach (string option in options)
            {
                Console.WriteLine("{0}. {1}", count, option);
                count++;
            }



            Console.Write("Select from the options above (1-2): ");
            this.UserInput = Console.ReadLine();
            if (UserInput == "main")
            {
                Console.Clear();
                MainUI UI = new MainUI("Event Scheduler", "v1.0");
                UI.DisplayScreen();
            }
            else if (UserInput == "quit")
            {
                Environment.Exit(0);
            }
            else if (UserInput == "1")
            {
                Console.WriteLine("View Function Needs Written");
            }
            else if (UserInput == "2")
            {
                Console.WriteLine("Book Function Needs Written");
            }
        }
    }
}
