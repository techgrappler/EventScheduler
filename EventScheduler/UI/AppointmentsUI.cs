using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;

namespace EventScheduler.UI
{
    public class AppointmentsUI : UserInterface
    {
        public AppointmentsUI()
        {

        }
        public AppointmentsUI(string title)
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
            Console.Write("Type 'view' to view appointments or 'book' to book a new appointment: ");
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
            else if (UserInput == "view")
            {
                Console.WriteLine("View Function Needs Written");
            }
            else if (UserInput == "book")
            {
                Console.WriteLine("Book Function Needs Written");
            }
        }
    }
}
