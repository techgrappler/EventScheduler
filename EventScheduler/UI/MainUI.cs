﻿using System;
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
        EmpAvailabilityUI availabilityUI = new EmpAvailabilityUI();
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
            this.UserInput = Console.ReadLine();
            string selection = this.UserInput;

            if (selection == "1")
            {
                Console.Clear();
                employeesUI.HeaderTitle = "Manage Employees";
                employeesUI.DisplayScreen();
            }
            if (selection == "2")
            {
                Console.Clear();
                customersUI.HeaderTitle = "Manage Customers";
                customersUI.DisplayScreen();
            }
            if (selection == "3")
            {
                Console.Clear();
                servicesUI.HeaderTitle = "Manage Services Offered";
                servicesUI.DisplayScreen();
            }
            else if (selection == "4")
            {
                Console.Clear();
                appointmentsUI.HeaderTitle = "Manage Appointments";
                appointmentsUI.DisplayScreen();
            }
            else if (selection == "5")
            {
                Console.Clear();
                availabilityUI.HeaderTitle = "Manage Availability";
                availabilityUI.DisplayScreen();
            }
            else if (UserInput == "quit")
            {
                Environment.Exit(0);
            }
        }
    }
}
