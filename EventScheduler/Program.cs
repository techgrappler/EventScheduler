using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;
using EventScheduler.DBClasses;
using EventScheduler.Validators;

namespace EventScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dir = AppDomain.CurrentDomain.BaseDirectory;
            //Console.WriteLine(dir);
            //Console.ReadLine();
            //var emp1 = new Employee(1, "John", "Hobo");
            //emp1.FName = "John";
            //emp1.LName = "Fletcher";

            //Console.WriteLine("{0} {1} is the employee you entered.", emp1.FName, emp1.LName);
            //Console.ReadLine();

            //string connString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + "\"C:\\Users\\Samuel Fletcher\\source\\repos\\ConsoleApp2\\ConsoleApp2\\EmpSchedules.mdf\"" + "; Integrated Security = True; Connect Timeout = 30";
            //var employees = UseDB.ReadEmployee(connString);

            //foreach (Employee emp in employees)
            //{
            //    Console.WriteLine("ID: {0} FirstName: {1} LastName: {2}", emp.ID, emp.FName, emp.LName);
            //}
            //Console.ReadLine();

            //int num = 8;
            //string str = "Hello";
            //string[] str2 = new string[] { "Hello", "My name is Sam" };

            //ValidateInput validate = new ValidateInput();
            //string value = validate.ValidateString(num);

            //Console.WriteLine("The input type is a: {0}", value);
            //Console.ReadLine();

            ////INSERT Default Availability for All Employees
            //DateTime start = new DateTime(2018, 1, 1, 8, 00, 0);
            //DateTime end = new DateTime(2018, 1, 1, 17, 00, 0);
            //DateTime sunStart = new DateTime(2018, 1, 1, 0, 0, 0);
            //DateTime sunEnd = new DateTime(2018, 1, 1, 0, 0, 0);
            //UseDB.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + "\"C:\\Users\\Samuel Fletcher\\source\\repos\\EventScheduler\\EventScheduler\\TestDB.mdf\"" + "; Integrated Security = True; Connect Timeout = 30";
            //var employees = UseDB.SelectEmployees();
            //foreach (Employee emp in employees)
            //{
            //    UseDB.InsertUpdateDailyAvailability("Monday", emp.ID, start, end);
            //    UseDB.InsertUpdateDailyAvailability("Tuesday", emp.ID, start, end);
            //    UseDB.InsertUpdateDailyAvailability("Wednesday", emp.ID, start, end);
            //    UseDB.InsertUpdateDailyAvailability("Thursday", emp.ID, start, end);
            //    UseDB.InsertUpdateDailyAvailability("Friday", emp.ID, start, end);
            //    UseDB.InsertUpdateDailyAvailability("Saturday", emp.ID, start, end);
            //    UseDB.InsertUpdateDailyAvailability("Sunday", emp.ID, sunStart, sunEnd);
            //    UseDB.InsertUpdateDailyAvailability("DailyDefault", emp.ID, start, end);

            //}


            //Run the Event Scheduler app
            Console.SetWindowSize(200, 50);
            UseDB.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + "\"C:\\Users\\Samuel Fletcher\\source\\repos\\EventScheduler\\EventScheduler\\TestDB.mdf\"" + "; Integrated Security = True; Connect Timeout = 30";
            MainUI UI = new MainUI("Event Scheduler", "v1.0");
            UI.DisplayScreen();
            Console.ReadLine();



        }
    }
}
