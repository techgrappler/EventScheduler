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
            } else
            {
                Console.WriteLine("Invalid Input. TryAgain.");
            }
        }
        public override void DisplayFooter(int option)
        {
            while (true)
            {
                if (option == 1)
                {
                   
                    Employee emp = AddEmployee();
                    if (ConfirmAdd(emp))
                    {
                        int empID = UseDB.InsertEmployee(emp.FName, emp.LName);
                        var daysOfWeek = new string[] { "DailyDefault", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                        foreach(string day in daysOfWeek)
                        {
                            UseDB.InsertUpdateDailyAvailability(day, empID, new TimeSpan(8, 0, 0), new TimeSpan(17, 0, 0));
                        }
                       
                    }
                    
                    Console.Clear();
                    this.DisplayScreen();
                }
                else if (option == 2)
                {
                    int empID = RemoveEmployee();
                    if (ConfirmRemove(empID))
                    {
                        UseDB.DeleteEmployee(empID);
                        UseDB.DeleteDailyAvailability(empID);
                    }
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
                var emp = new Employee();
                Console.WriteLine("Enter the ID of the Employee you wish to remove: ");
                id = Console.ReadLine();
                if (Int32.TryParse(id, out idInt)                               )
                {
                    if (emp.IsAny(idInt))
                    {
                        return idInt;
                    }
                    else { Console.WriteLine("That employee ID does not exist in the system."); }
                }
                else { Console.WriteLine("Invalid input. Try Again."); }

            }
        }
        private bool ConfirmAdd(Employee emp)
        {
            while (true)
            {
                Console.WriteLine("Are you sure you want add {0} {1} as an employee ('yes' or 'no')?", emp.FName, emp.LName);
                UserInput = Console.ReadLine();
                if (UserInput == "yes")
                {
                    Console.WriteLine("A default availability will be set for every day from 08:00-17:00. You can change this by managing avaialbility from the main menue (Enter to continue):");
                    Console.ReadLine();
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
                Console.WriteLine("***Warning***\nRemoving this employee will remove all associated appointments!");
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
            Console.WriteLine("{0, -15} {1, -15} {2, -15}", "Employee ID", "First Name", "Last Name");
            foreach (Employee emp in employees)
            {
                Console.WriteLine("{0, -15} {1, -15} {2, -15}", emp.ID, emp.FName, emp.LName);
            }
        }
        public void DisplayEmployees(List<Employee> employees)
        {
            Console.WriteLine("{0, -15} {1, -15} {2, -15}", "Employee ID", "First Name", "Last Name");
            foreach (Employee emp in employees)
            {
                Console.WriteLine("{0, -15} {1, -15} {2, -15}", emp.ID, emp.FName, emp.LName);
            }
        }
    }
}
