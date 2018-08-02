using System;
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

        private new void DisplayBody()
        {
            var services = UseDB.SelectServices();
            Console.WriteLine("{0, -20} {1, -30} {2, -40}", "Service ID", "Service Name", "Description");
            foreach (Service service in services)
            {
                Console.WriteLine("{0, -20} {1, -30} {2, -40}", service.ID, service.Name, service.Description);
            }
        }
        
        private new void DisplayFooter()
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

        private new void DisplayFooter(int option)
        {
            ServicesUI servicesUI = new ServicesUI();
            if (option == 1)
            {
                string serviceName;
                string serviceDescription;

                while (true)
                {
                    Console.WriteLine("Enter a name for the service you would like to add: ");
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
                    Console.WriteLine("Enter a description: ");
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
                while (true)
                {
                    Console.WriteLine("Are you sure you want to add the {0} service ('yes' or 'no')?", serviceName);
                    string confirmation = Console.ReadLine();
                    if (confirmation == "yes")
                    {
                        UseDB.InsertService(serviceName, serviceDescription);
                        Console.Clear();
                        servicesUI.HeaderTitle = "Manage Services";
                        servicesUI.DisplayScreen();
                    }
                    else if (confirmation == "no")
                    {
                        Console.Clear();
                        servicesUI.HeaderTitle = "Manage Services";
                        servicesUI.DisplayScreen();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                }
            }

            if (option == 2)
            {

                string id;
                int idInt;

                while (true)
                {
                    Console.WriteLine("Enter the ID of the Service you wish to remove: ");
                    id = Console.ReadLine();
                    if (Int32.TryParse(id, out idInt))
                    {
                        break;
                    }
                    else { Console.WriteLine("Invalid Input. Try Again."); }
                }

                while (true)
                {
                    Console.WriteLine("Are you sure you want to remove the Service with ID {0}('yes' or 'no')?", id);
                    string confirmation = Console.ReadLine();
                    if (confirmation == "yes")
                    {
                        UseDB.DeleteService(Int32.Parse(id));
                        Console.Clear();
                        servicesUI.HeaderTitle = "Manage Services";
                        servicesUI.DisplayScreen();
                    }
                    else if (confirmation == "no")
                    {
                        Console.Clear();
                        servicesUI.HeaderTitle = "Manage Services";
                        servicesUI.DisplayScreen();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                }
            }
        }
    }
}
