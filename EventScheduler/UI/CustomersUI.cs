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
    public class CustomersUI : UserInterface
    {
        public CustomersUI()
        {

        }
        public CustomersUI(string title)
        {
            this.HeaderTitle = title;
        }
        public override void DisplayScreen()
        {
            DisplayHeader();
            DisplayBody();
            DisplayFooter();
        }
        public override void DisplayBody()
        {
            var customers = UseDB.SelectCustomers();
            Console.WriteLine("{0, -20} {1, -20} {2, -20}", "Customer ID", "First Name", "Last Name");
            foreach (Customer cust in customers)
            {
                Console.WriteLine("{0, -20} {1, -20} {2, -20}", cust.ID, cust.FName, cust.LName);
            }
        }
        public override void DisplayBody(int option)
        {


        }
        public override void DisplayFooter()
        {
            this.Options = new string[] {
                "Add Customer",
                "Remove Customer"
            };
            this.DisplayOptions();

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
                {
                    Console.WriteLine("Invalid Input. Try Again: ");
                }
            }
        }
        public override void DisplayFooter(int option)
        {
            while (true)
            {
                if (option == 1)
                {
                    Customer cust = AddCustomer();
                    if (ConfirmAdd(cust)) { UseDB.InsertCustomer(cust.FName, cust.LName); }
                    Console.Clear();
                    this.DisplayScreen();
                }
                else if (option == 2)
                {
                    int custID = RemoveCustomer();
                    if (ConfirmRemove(custID)) { UseDB.DeleteCustomer(custID); }
                    Console.Clear();
                    this.DisplayScreen();
                }
            }
        }
        public Customer AddCustomer()
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
            return new Customer(firstName, lastName);
        }
        public int RemoveCustomer()
        {
            string id;
            int idInt;

            while (true)
            {
                var cust = new Customer();
                Console.WriteLine("Enter the ID of the Customer you wish to remove: ");
                id = Console.ReadLine();
                if (Int32.TryParse(id, out idInt))
                {
                    if(cust.IsAny(idInt))
                    {
                        return idInt;
                    }
                    else { Console.WriteLine("That customer ID is not in the system. Try again."); }
                    
                }
                else { Console.WriteLine("Invalid input or that cust ID does not exist in the system. Try Again."); }

            }
        }
        public bool ConfirmAdd(Customer cust)
        {
            while (true)
            {
                Console.WriteLine("Are you sure you want add {0} {1} as an Customer ('yes' or 'no')?", cust.FName, cust.LName);
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
        public bool ConfirmRemove(int custID)
        {
            while (true)
            {
                Console.WriteLine("***Warning***\nRemoving this customer will remove all associated appointments!");
                Console.WriteLine("Are you sure you want to remove the Customer with ID {0} ('yes' or 'no) ?", custID);
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
        public void DisplayCustomers()
        {
            var customers = UseDB.SelectCustomers();
            Console.WriteLine("{0, -15} {1, -15} {2, -15}", "Customer ID", "First Name", "Last Name");
            foreach (Customer cust in customers)
            {
                Console.WriteLine("{0, -15} {1, -15} {2, -15}", cust.ID, cust.FName, cust.LName);
            }
        }
    }
}
