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

        public override void DisplayScreen()
        {
            DisplayHeader();
            DisplayBody();
            DisplayFooter();
        }

        public override void DisplayBody()
        {
            this.DisplayEmployees();
        }
        public override void DisplayBody(int option)
        {


        }

        public override void DisplayFooter()
        {
            this.Options = new string[] {
                "Add Employee",
                "Remove Employee"
            };
            this.DisplayOptions();

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
                    Employee emp = AddEmployee();
                    if (ConfirmAdd(emp)) { UseDB.InsertEmployee(emp.FName, emp.LName); }
                    Console.Clear();
                    this.DisplayScreen();
                }
                else if (option == 2)
                {
                    int empID = RemoveEmployee();
                    if (ConfirmRemove(empID)) { UseDB.DeleteEmployee(empID); }
                    Console.Clear();
                    this.DisplayScreen();
                }
            }
        }

        private Employee AddEmployee()
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
                if (!string.IsNullOrEmpty(lastName))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try again.");
                }
            }
            return new Employee(firstName, lastName);
        }
        private int RemoveEmployee()
        {
            string id;
            int idInt;

            while (true)
            {
                Console.WriteLine("Enter the ID of the Employee you wish to remove: ");
                id = Console.ReadLine();
                if (Int32.TryParse(id, out idInt))
                {
                    return idInt;
                }
                else { Console.WriteLine("Invalid Input. Try Again."); }

            }
        }
        private bool ConfirmAdd(Employee emp)
        {
            while (true)
            {
                Console.WriteLine("Are you sure you want add {0} {1} as an employee?", emp.FName, emp.LName);
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
        private bool ConfirmRemove(int empID)
        {
            while (true)
            {
                Console.WriteLine("Are you sure you want to remove the employee with ID {0} ('yes' or 'no) ?", empID);
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
        public void DisplayEmployees()
        {
            var employees = UseDB.SelectEmployees();
            Console.WriteLine("{0, -40} {1, -40} {2, -40}", "Employee ID", "First Name", "Last Name");
            foreach (Employee emp in employees)
            {
                Console.WriteLine("{0, -40} {1, -40} {2, -40}", emp.ID, emp.FName, emp.LName);
            }
        }
    }
}
