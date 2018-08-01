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
            var employees = UseDB.ReadEmployee();
            Console.WriteLine("{0, -40} {1, -40} {2, -40}", "Employee ID", "First Name", "Last Name");
            foreach (Employee emp in employees)
            {
                Console.WriteLine("{0, -40} {1, -40} {2, -40}", emp.ID, emp.FName, emp.LName);
            }
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
                Console.WriteLine("Addd Function Needs Written");
            }
            else if (UserInput == "2")
            {
                Console.WriteLine("Remove Function Needs Written");
            }
        }
    }
}
