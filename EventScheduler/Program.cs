using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;

namespace EventScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            Console.ReadLine();
            var ui = new MainUI();
            ui.DisplayScreen();
            Console.ReadLine();
        }
    }
}
