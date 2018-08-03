using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.DBClasses;
using EventScheduler.UI;
using System.Text.RegularExpressions;

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

        public override void DisplayBody()
        {
            var appointments = UseDB.SelectAppointments();

            appointments.OrderBy(appointment => appointment.StartTime);
            appointments.OrderBy(appointment => appointment.EmployeeID);
            Console.WriteLine("There are {0} appointments", appointments.Count);
            Console.WriteLine("{0, -30} {1, -30} {2, -30} {3, -30} {4, -30}", "Employee", "Service", "Customer", "Start Time", "End Time");
            foreach (Appointment apt in appointments)
            {
                Console.WriteLine("{0, -30} {1, -30} {2, -30} {3, -30} {4, -30}", apt.EmployeeName, apt.ServiceName, apt.CustomerName, apt.StartTime, apt.EndTime);
            }
        }
        public override void DisplayFooter()
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
                Console.Clear();
                DisplayHeader();
                DisplayBody(2);
                DisplayFooter(2);
            }
        }

        public override void DisplayBody(int option)
        {

        }
        public override void DisplayFooter(int option)
        {
            AppointmentsUI appointmentsUI = new AppointmentsUI();

            if (option == 2)
            {
                AddAppointment();
                Console.Clear();
                this.DisplayScreen();
            }
        }

        private Appointment AddAppointment()
        {

            int serviceID;
            int employeeID;
            int customerID;
            string dateString;
            string hoursMinutes;
            DateTime date = new DateTime();
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();
            ServicesUI servicesUI = new ServicesUI();
            EmployeesUI employeesUI = new EmployeesUI();
            CustomersUI customersUI = new CustomersUI();

            //Get customer for appointment
            customersUI.DisplayCustomers();
            while (true)
            {
                Console.WriteLine("Which customer is the appointment for (enter ID or type 'new' for new customer)?: ");
                UserInput = Console.ReadLine();
                if (Int32.TryParse(UserInput, out customerID))
                {
                    break;
                }
                else if (UserInput == "new")
                { 
                    Customer cust = customersUI.AddCustomer();
                    customerID = UseDB.InsertCustomer(cust.FName, cust.LName);
                    break;
                }
                else
                { Console.WriteLine("Invalid Input. Try again."); }
            }

            //Get service to be scheduled
            servicesUI.DisplayServices();
            while (true)
            {
                Console.WriteLine("Enter the ID of the service you would like to schedule: ");
                UserInput = Console.ReadLine();
                if (Int32.TryParse(UserInput, out serviceID))
                {
                    break;
                }
                else { Console.WriteLine("Invalid Input. Try again."); }
            }

            //Get employee for appointment
            employeesUI.DisplayEmployees();
            while (true)
            {
                Console.WriteLine("Enter the ID of the employee that will perform the service: ");
                UserInput = Console.ReadLine();
                if (Int32.TryParse(UserInput, out employeeID))
                {
                    break;
                }
                else { Console.WriteLine("Invalid Input. Try again."); }
            }

            //Get time for appointment
            while (true)
            {
                string pattern = @"\d{1,2}\/\d{1,2}\/\d{4}";
                Console.WriteLine("Enter a date for the appointment using the format 'mm/dd/yyyy': ");
                dateString = Console.ReadLine();
                if (Regex.IsMatch(dateString, pattern) && DateTime.TryParse(dateString, out date))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try again.");
                }
            }
            while (true)
            {
                string pattern = @"\d{1,2}\:\d{1,2}";

                Console.WriteLine("Enter a time for the appointment using the format 'hh:mm':");
                hoursMinutes = Console.ReadLine();
                if (Regex.IsMatch(hoursMinutes, pattern))
                {
                    string[] hhmmString = hoursMinutes.Split(':');
                    int hhInt = Int32.Parse(hhmmString[0]);
                    int mmInt = Int32.Parse(hhmmString[1]);

                    if ((hhInt >= 0 && hhInt <= 23) &&
                        (mmInt >= 0 && mmInt <= 59))
                    {
                        if (mmInt >= 0 && mmInt < 15)
                        {
                            mmInt = 0;
                        }
                        else if (mmInt >= 15 && mmInt < 30)
                        {
                            mmInt = 15;
                        }
                        else if (mmInt >= 30 && mmInt < 45)
                        {
                            mmInt = 30;
                        }
                        else if (mmInt >= 45 && mmInt < 60)
                        {
                            mmInt = 45;
                        }

                        TimeSpan hhmm = new TimeSpan(hhInt, mmInt, 0);
                        startTime = date + hhmm;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Again.");
                }
            }
            while (true)
            {
                string pattern = @"\d{1,2}\:\d{1,2}";

                Console.WriteLine("Enter a duration for the appointment using the format 'hh:mm':");
                hoursMinutes = Console.ReadLine();
                if (Regex.IsMatch(hoursMinutes, pattern))
                {
                    string[] hhmmString = hoursMinutes.Split(':');
                    int hhInt = Int32.Parse(hhmmString[0]);
                    int mmInt = Int32.Parse(hhmmString[1]);

                    if ((hhInt >= 0 && hhInt <= 23) &&
                       (mmInt >= 0 && mmInt <= 59))
                    {
                        if (mmInt >= 0 && mmInt < 15)
                        {
                            mmInt = 0;
                        }
                        else if (mmInt >= 15 && mmInt < 30)
                        {
                            mmInt = 15;
                        }
                        else if (mmInt >= 30 && mmInt < 45)
                        {
                            mmInt = 30;
                        }
                        else if (mmInt >= 45 && mmInt < 60)
                        {
                            mmInt = 45;
                        }
                    }
                    TimeSpan hhmm = new TimeSpan(hhInt, mmInt, 0);
                    endTime = startTime + hhmm;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Again.");
                }
            }

            //Add the new appointment to the database
            UseDB.InsertAppointment(customerID, serviceID, employeeID, startTime, endTime);

            //Set the employee's availability to 'booked' for the time period covered by the appointment 
            DateTime time = new DateTime();
            TimeSpan timeSpan = new TimeSpan(0, 15, 0);
            for (time = startTime; time <= endTime; time = time + timeSpan)
            {
                UseDB.InsertUpdateEmpAvailability(employeeID, time, 0, 1);
            }

            //Reset the user interface to view all appointments
            Console.Clear();
            this.DisplayScreen();

            return new Appointment(customerID, serviceID, employeeID, startTime, endTime);
        }
    }
}
