using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;

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

        new void DisplayBody()
        {
            Console.Write("Type 'add' to add an service or 'remove' to remove a service: ");
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
            else if (UserInput == "add")
            {
                Console.WriteLine("Add Function Needs Written");
            }
            else if (UserInput == "remove")
            {
                Console.WriteLine("Remove Function Needs Written");
            }
        }
    }
}
