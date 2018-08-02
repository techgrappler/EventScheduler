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
    public class EmployeesUI : UserInterface
    {

        public EmployeesUI()
        {

        }
        public EmployeesUI(string title)
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
            var employees = UseDB.SelectEmployees();
            Console.WriteLine("{0, -20} {1, -20} {2, -20}", "Employee ID", "First Name", "Last Name");
            foreach (Employee emp in employees)
            {
                Console.WriteLine("{0, -20} {1, -20} {2, -20}", emp.ID, emp.FName, emp.LName);
            }
        }
        private new void DisplayBody(int option)
        {


        }

        private new void DisplayFooter()
        {
            var options = new string[] {
                "Add Employee",
                "Remove Employee"
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
            EmployeesUI employeesUI = new EmployeesUI();
            if (option == 1)
            {
                string firstName;
                string lastName;
                while (true)
                {
                    Console.WriteLine("Enter the Employee's first name: ");
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
                    Console.WriteLine("Enter the Employee's last name: ");
                    lastName = Console.ReadLine();
                    if(!string.IsNullOrEmpty(lastName))
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
                        UseDB.InsertEmployee(firstName, lastName);
                        Console.Clear();
                        employeesUI.HeaderTitle = "Manage Employees";
                        employeesUI.DisplayScreen();
                    }
                    else if (confirmation == "no")
                    {
                        Console.Clear();
                        employeesUI.HeaderTitle = "Manage Employees";
                        employeesUI.DisplayScreen();
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
                    Console.WriteLine("Enter the ID of the Employee you wish to remove: ");
                    id = Console.ReadLine();
                    if (Int32.TryParse(id, out idInt))
                    {
                        break;
                    } else { Console.WriteLine("Invalid Input. Try Again."); }

                }


                while (true)
                {
                    Console.WriteLine("Are you sure you want to remove employee with ID {0}('yes' or 'no')?", id);
                    string confirmation = Console.ReadLine();
                    if (confirmation == "yes")
                    {
                        UseDB.DeleteEmployee(Int32.Parse(id));
                        Console.Clear();
                        employeesUI.HeaderTitle = "Manage Employees";
                        employeesUI.DisplayScreen();
                    }
                    else if (confirmation == "no")
                    {
                        Console.Clear();
                        employeesUI.HeaderTitle = "Manage Employees";
                        employeesUI.DisplayScreen();
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
