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
            DisplayAppointments();
        }
        public override void DisplayFooter()
        {
            this.Options = new string[] {
                "Book New Appointment"
            };
            this.DisplayOptions();

            while (true)
            {
                this.UserInput = Console.ReadLine();
                while (true)
                {
                    if (string.IsNullOrEmpty(UserInput))
                    {
                        Console.Write("Invalid Input. Try Again: ");
                        this.UserInput = Console.ReadLine();
                    }
                    else { break; }
                }

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
                    Console.Clear();
                    DisplayHeader();
                    DisplayBody(1);
                    DisplayFooter(1);
                }
                else
                {
                    Console.WriteLine("Invalid Input. Tray Again: ");
                }
            }
            
        }
        public override void DisplayBody(int option)
        {

        }
        public override void DisplayFooter(int option)
        {
            AppointmentsUI appointmentsUI = new AppointmentsUI();

            if (option == 1)
            {
                AddAppointment();
                Console.Clear();
                this.DisplayScreen();
            }
        }
        public void DisplayAppointments()
        {
            var appointments = UseDB.SelectAppointments();

            appointments.OrderBy(appointment => appointment.StartTime);
            appointments.OrderBy(appointment => appointment.EmployeeID);
            Console.WriteLine("There are {0} appointments", appointments.Count);
            Console.WriteLine("{0, -30} {1, -25} {2, -30} {3, -30} {4, -30}", "Employee", "Service", "Customer", "Start Time", "End Time");
            foreach (Appointment apt in appointments)
            {
                Console.WriteLine("{0, -30} {1, -25} {2, -30} {3, -30} {4, -30}", apt.EmployeeName, apt.ServiceName, apt.CustomerName, apt.StartTime, apt.EndTime);
            }
        }
        private void AddAppointment()
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

            //Get time for appointment

            while (true)
            {
                while (true)
                {
                    string pattern = @"\d{1,2}\/\d{1,2}\/\d{4}";
                    Console.WriteLine("What day would you like to book the appointment for? ('mm/dd/yyyy'): ");
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

                    Console.WriteLine("What time would you like the appointment to start? ('hh:mm'):");
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
                if(startTime > DateTime.Now)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("This program is great but it isn't a TIME MACHINE.\nCannot book appointments in the past....How about trying again?");
                }
            }        
            while (true)
            {
                string pattern = @"\d{1,2}\:\d{1,2}";
                string error = "Invalid Input. Try Again.";
                Console.WriteLine("How long would you like the appointment to last? ('hh:mm'):");
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
                        endTime = startTime + hhmm;
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("{0}", error);
                }
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
            //Get list of all employees
            var employees = UseDB.SelectEmployees();
            var emp = new EmpAvailability();
            //Remove any employee from list that is unavailable
            foreach (Employee employee in employees.ToList())
            {
                var apts = UseDB.SelectAppointments(employee.ID);
                foreach(Appointment apt in apts)
                {
                    if (!emp.IsEmployeeAvailable(employee.ID, apt.StartTime, apt.EndTime))
                    {
                        employees.Remove(employee);
                    }
                }
                if(!emp.IsEmployeeAvailable(employee.ID, startTime, endTime))
                {
                    employees.Remove(employee);
                }
            }
            //If there are not available employees, start booking process over.
            if (!employees.Any())
            {
                Console.WriteLine("All employees are either booked or unavailable during that time frame. How about trying again?");
                Console.Read();
                Console.Clear();
                this.DisplayScreen();
            }
            else
            {
                //Otherwise, list all available employees that can performt the service
                Console.WriteLine("The following employees are available during that timeframe: ");
                employeesUI.DisplayEmployees(employees);
                while (true)
                {
                    Console.WriteLine("Which employee would you like to perform at the appointment? Enter ID number: ");
                    UserInput = Console.ReadLine();
                    if (Int32.TryParse(UserInput, out employeeID))
                    {
                        break;
                    }
                    else { Console.WriteLine("Invalid Input. Try again."); }
                }

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

               // return new Appointment(customerID, serviceID, employeeID, startTime, endTime);
            }
        }
    }
}
