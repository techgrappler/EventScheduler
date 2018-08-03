using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.DBClasses;
using EventScheduler.UI;
using System.Text;
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

                int serviceID;
                int employeeID;
                int customerID;
                string dateString;
                string hoursMinutes;
                DateTime date = new DateTime();
                DateTime startTime = new DateTime();
                DateTime endTime = new DateTime();


                var customers = UseDB.SelectCustomers();
                Console.WriteLine("{0, -40} {1, -40} {2, -40}", "Customer ID", "First Name", "Last Name");
                foreach (Customer cust in customers)
                {
                    Console.WriteLine("{0, -40} {1, -40} {2, -40}", cust.ID, cust.FName, cust.LName);
                }
                while (true)
                {
                    Console.WriteLine("Enter the ID of the customer for which you are scheduling an event or type 'new' to add a new customer: ");
                    UserInput = Console.ReadLine();
                    if (Int32.TryParse(UserInput, out customerID))
                    {
                        break;
                    }
                    else if (UserInput == "new")
                    {
                        CustomersUI customersUI = new CustomersUI();
                        {
                            string firstName;
                            string lastName;
                            while (true)
                            {
                                Console.WriteLine("Enter the Customer's first name: ");
                                firstName = Console.ReadLine();
                                if (!string.IsNullOrEmpty(firstName))
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
                                Console.WriteLine("Enter the Customer's last name: ");
                                lastName = Console.ReadLine();
                                if (!string.IsNullOrEmpty(lastName))
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
                                Console.WriteLine("Are you sure you want to add {0} {1} ('yes' or 'no')?", firstName, lastName);
                                string confirmation = Console.ReadLine();
                                if (confirmation == "yes")
                                {
                                    UseDB.InsertCustomer(firstName, lastName);
                                    Console.Clear();
                                    DisplayHeader();
                                    DisplayBody(2);
                                  
                                    DisplayFooter(2);
                                    Console.WriteLine("User Added");
                                }
                                else if (confirmation == "no")
                                {
                                    Console.Clear();
                                    DisplayHeader();
                                    DisplayBody(2);
                                    DisplayFooter(2);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Try again.");
                                }
                            }
                        }
                    }
                    else
                    { Console.WriteLine("Invalid Input. Try again."); }
                }

                var services = UseDB.SelectServices();
                Console.WriteLine("{0, -40} {1, -40} {2, -40}", "Service ID", "Service Name", "Description");
                foreach (Service service in services)
                {
                    Console.WriteLine("{0, -40} {1, -40} {2, -40}", service.ID, service.Name, service.Description);
                }

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


                var employees = UseDB.SelectEmployees();
                Console.WriteLine("{0, -40} {1, -40} {2, -40}", "Employee ID", "First Name", "Last Name");
                foreach (Employee emp in employees)
                {
                    Console.WriteLine("{0, -40} {1, -40} {2, -40}", emp.ID, emp.FName, emp.LName);
                }

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

                while (true)
                {
                    string pattern = @"\d{1,2}\/\d{1,2}\/\d{4}";
                    Console.WriteLine("Enter a date for the appointment using the format 'mm/dd/yyyy': ");
                    dateString = Console.ReadLine();
                    if (Regex.IsMatch(dateString, pattern) && DateTime.TryParse(dateString, out date))
                    {
                        break;
                    } else
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

                UseDB.InsertAppointment(customerID, serviceID, employeeID, startTime, endTime);
                DateTime time = new DateTime();
                TimeSpan timeSpan = new TimeSpan(0, 15, 0);

                for(time = startTime; time <= endTime; time = time + timeSpan)
                {
                    UseDB.InsertUpdateEmpAvailability(employeeID, time, 0, 1);
                }
                
                Console.Clear();
                appointmentsUI.HeaderTitle = "Manage Appointments";
                appointmentsUI.DisplayScreen();

            }



        }
    }
}
