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
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(dir);
            Console.ReadLine();
            var emp1 = new Employee(1, "John", "Hobo");
            emp1.FName = "John";
            emp1.LName = "Fletcher";

            Console.WriteLine("{0} {1} is the employee you entered.", emp1.FName, emp1.LName);
            Console.ReadLine();
        }
    }
}
