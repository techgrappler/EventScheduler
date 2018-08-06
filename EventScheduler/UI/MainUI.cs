using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;

namespace EventScheduler.Interfaces
{
    public class MainUI : UserInterface
    {
        new string UserInput { get ; set ; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }

        //Load UIs for each section of app
        AppointmentsUI appointmentsUI = new AppointmentsUI();
        DailyAvailabilityUI availabilityUI = new DailyAvailabilityUI();
        EmployeesUI employeesUI = new EmployeesUI();
        ServicesUI servicesUI = new ServicesUI();
        CustomersUI customersUI = new CustomersUI();

        public MainUI(string appName, string appVersion)
        {
            this.HeaderTitle = appName + appVersion;
            this.AppName = appName;
            this.AppVersion = appVersion;
        }
        new public void DisplayScreen()
        {
            DisplayHeader();
            DisplayBody();
            DisplayFooter();
        }
        new void GetInput()
        {
            this.UserInput = Console.ReadLine();
        }
        new void DisplayBody()
        {

            //Create List of Options for User to Select
            var options = new string[] {
                "Manage Employees",
                "Manage Customers",
                "Manage Services Offered",
                "Manage Appointments",
                "Manage Availability"
            };

            var count = 1;
            foreach (string option in options)
            {
                Console.WriteLine("{0}. {1}", count, option);
                count++;
            }
            Console.WriteLine("Type 'quit' to exit the program.");
        }
        new void DisplayFooter()
        {
            Console.Write("Select from the options above (1-5): ");
            while(true)
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


                if (UserInput == "1")
                {
                    Console.Clear();
                    employeesUI.HeaderTitle = "Employees";
                    employeesUI.DisplayScreen();
                }
                if (UserInput == "2")
                {
                    Console.Clear();
                    customersUI.HeaderTitle = "Customers";
                    customersUI.DisplayScreen();
                }
                if (UserInput == "3")
                {
                    Console.Clear();
                    servicesUI.HeaderTitle = "Services and Products";
                    servicesUI.DisplayScreen();
                }
                else if (UserInput == "4")
                {
                    Console.Clear();
                    appointmentsUI.HeaderTitle = "Appointments";
                    appointmentsUI.DisplayScreen();
                }
                else if (UserInput == "5")
                {
                    Console.Clear();
                    availabilityUI.HeaderTitle = "Employee Availability";
                    availabilityUI.DisplayScreen();
                }
                else if (UserInput == "quit")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid Input.Try Again: ");
                }
            }
        }
    }
}
