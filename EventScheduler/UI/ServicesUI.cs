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
        public override void DisplayBody()
        {
            this.DisplayServices();
        }
        public override void DisplayBody(int option)
        {

        }
        public override void DisplayFooter()
        {
            this.Options = new string[] {
                "Add Service",
                "Remove Service"
            };
            this.DisplayOptions();

            this.UserInput = Console.ReadLine();
            while(true)
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
                DisplayBody(1);
                DisplayFooter(1);
            }
            else if (UserInput == "2")
            {
                DisplayBody(2);
                DisplayFooter(2);
            }
            else
            {
                Console.WriteLine("Invalid Input. Try Again.");
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
                var service = new Service();
                Console.WriteLine("Enter the ID of the service you wish to remove: ");
                id = Console.ReadLine();
                if (Int32.TryParse(id, out idInt))
                {
                    if (service.IsAny(idInt))
                    {
                        return idInt;
                    } 
                    else { Console.WriteLine("That service ID is not in the system. Try again."); }
                    
                }
                else { Console.WriteLine("Invalid input or that service ID does not exist in the system. Try Again."); }

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
                Console.WriteLine("***Warning***\nRemoving this service will remove all associated appointments!");
                Console.WriteLine("Are you sure you want to remove the service with ID {0} ('yes' or 'no) ?", serviceID);
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
        public void DisplayServices()
        {
            var services = UseDB.SelectServices();
            Console.WriteLine("{0, -15} {1, -30} {2, -30}", "Service ID", "Service Name", "Description");
            foreach (Service service in services)
            {
                Console.WriteLine("{0, -15} {1, -30} {2, -30}", service.ID, service.Name, service.Description);
            }
        }
    }
}
