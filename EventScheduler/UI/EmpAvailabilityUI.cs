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
    public class EmpAvailabilityUI : UserInterface
    {
        public EmpAvailabilityUI()
        {

        }
        public EmpAvailabilityUI(string title)
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
            var empAvailabilities = UseDB.SelectEmpAvailabilities();
            Console.WriteLine("{0, -40} {1, -40} {2, -40} {3, -40}", "Employee ID", "Time Slot", "Available", "Booked");
            foreach (EmpAvailability empAvail in empAvailabilities)
            {
                Console.WriteLine("{0, -40} {1, -40} {2, -40} {3, -40}", empAvail.EmployeeID, empAvail.Time, empAvail.IsAvailable, empAvail.IsBooked);
            }
        }
        public override void DisplayFooter()
        {
            var options = new string[] {
                "View Availability",
                "Set Availability"
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
                Console.WriteLine("Addd Function Needs Written");
            }
            else if (UserInput == "2")
            {
                Console.WriteLine("Remove Function Needs Written");
            }
        }
    }
}
