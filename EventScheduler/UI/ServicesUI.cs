﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;
using EventScheduler.DBClasses;

namespace EventScheduler.UI
{
    public class ServicesUI : UserInterface
    {
        public ServicesUI()
        {

        }
        public ServicesUI(string title)
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
            var services = UseDB.SelectServices();
            Console.WriteLine("{0, -20} {1, -30} {2, -40}", "Service ID", "Service Name", "Description");
            foreach (Service service in services)
            {
                Console.WriteLine("{0, -20} {1, -30} {2, -40}", service.ID, service.Name, service.Description);
            }
        }

        public override void DisplayFooter()
        {
            var options = new string[] {
                "Add Service",
                "Remove Service"
            };

            var count = 1;
            foreach (string option in options)
            {
                Console.WriteLine("{0}. {1}", count, option);
                count++;
            }
            Console.WriteLine("Type 'main' to return to main menu or 'quit' to exit the program.");

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
                DisplayHeader();
                DisplayBody(1);
                DisplayFooter(1);
            }
            else if (UserInput == "2")
            {
                DisplayHeader();
                DisplayBody(2);
                DisplayFooter(2);
            }
        }

        public override void DisplayFooter(int option)
        {
            while (true)
            {
                if (option == 1)
                {
                    Service service = AddService();
                    if (ConfirmAdd(service)) { UseDB.InsertService(service.Name, service.Description); }
                    Console.Clear();
                    this.DisplayScreen();
                }
                else if (option == 2)
                {
                    int serviceID = RemoveService();
                    if (ConfirmRemove(serviceID)) { UseDB.DeleteService(serviceID); }
                    Console.Clear();
                    this.DisplayScreen();
                }
            }
        }

        private Service AddService()
        {
            string serviceName;
            string serviceDescription;
            while (true)
            {
                Console.WriteLine("Enter the name of the Service you want to add: ");
                serviceName = Console.ReadLine();
                if (!string.IsNullOrEmpty(serviceName))
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
                Console.WriteLine("Enter a description for the service: ");
                serviceDescription = Console.ReadLine();
                if (!string.IsNullOrEmpty(serviceDescription))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try again.");
                }
            }
            return new Service(serviceName, serviceDescription);
        }
        private int RemoveService()
        {
            string id;
            int idInt;

            while (true)
            {
                Console.WriteLine("Enter the ID of the service you wish to remove: ");
                id = Console.ReadLine();
                if (Int32.TryParse(id, out idInt))
                {
                    return idInt;
                }
                else { Console.WriteLine("Invalid Input. Try Again."); }

            }
        }
        private bool ConfirmAdd(Service service)
        {
            while (true)
            {
                Console.WriteLine("Are you sure you want add {0} as an service ('yes' or 'no')?", service.Name);
                UserInput = Console.ReadLine();
                if (UserInput == "yes")
                {
                    return true;
                }
                else if (UserInput == "no")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }
        }
        private bool ConfirmRemove(int serviceID)
        {
            while (true)
            {
                Console.WriteLine("Are you sure you want to remove the service with ID {0} ('yes' or 'np) ?", serviceID);
                UserInput = Console.ReadLine();
                if (UserInput == "yes")
                {
                    return true;
                }
                else if (UserInput == "no")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }
        }
    }
}
