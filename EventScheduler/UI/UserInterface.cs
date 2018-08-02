using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;

namespace EventScheduler.UI
{
    public abstract class UserInterface : IUserInterface
    {
        public string UserInput { get; set;}
        public string HeaderTitle { get; set; }

        public void DisplayScreen()
        {
            DisplayHeader();
            DisplayBody();
            DisplayFooter();
        }
        public void DisplayScreen(int option)
        {
            DisplayHeader(option);
            DisplayBody(option);
            DisplayFooter(option);
        }
        public void GetInput()
        {
            this.UserInput = Console.ReadLine();
        }

        protected void DisplayHeader()
        {
            Console.Write("**********");
            for (int i = 0; i < HeaderTitle.Length - 1; i++)
            {
                Console.Write("*");
            }
            Console.Write("**********");
            Console.WriteLine();
            Console.WriteLine("*        {0}         *", HeaderTitle);
            Console.Write("**********");
            for (int i = 0; i < HeaderTitle.Length - 1; i++)
            {
                Console.Write("*");
            }
            Console.Write("**********");
            Console.WriteLine();
        }
        protected void DisplayBody()
        {
            Console.WriteLine("Body Content Goes Here");
        }
        protected void DisplayFooter()
        {
            Console.Write("Type 'main' to return to the main menu or 'quit' to quit at any time. ");
            this.UserInput = Console.ReadLine();
            if (UserInput == "main")
            {
                Console.Clear();
                MainUI UI = new MainUI("Event Scheduler", "v1.0");
                UI.DisplayScreen();
            } else if (UserInput == "quit")
            {
                Environment.Exit(0);
            }
        }

        protected void DisplayHeader(int option)
        {
            Console.WriteLine("Customized Header Content Goes Here");
        }
        protected void DisplayBody(int option)
        {
            Console.WriteLine("Customized Body Content Goes Here");
        }
        protected void DisplayFooter(int option)
        {
            Console.WriteLine("Customized Footer Content Goes Here");
        }

    }
}
