using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;
using EventScheduler.DBClasses;
using System.Text.RegularExpressions;
namespace EventScheduler.UI
{
    public class DailyAvailabilityUI : UserInterface
    {
        public DailyAvailabilityUI()
        {

        }
        public DailyAvailabilityUI(string title)
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
            this.DisplayEmpAvailability();
        }
        public override void DisplayFooter()
        {
            this.Options = new string[] {
                "Change Availability"
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
                this.SetAvailability();
            } else
            {
                Console.WriteLine("Invalid Input. Try Again: ");
            }
        }
        public void DisplayEmpAvailability()
        {

            var DailyDefaults = UseDB.SelectDailyAvailabilities("DailyDefault");
            var Sunday = UseDB.SelectDailyAvailabilities("Sunday");
            var Monday = UseDB.SelectDailyAvailabilities("Monday");
            var Tuesday = UseDB.SelectDailyAvailabilities("Tuesday");
            var Wednesday = UseDB.SelectDailyAvailabilities("Wednesday");
            var Thursday = UseDB.SelectDailyAvailabilities("Thursday");
            var Friday = UseDB.SelectDailyAvailabilities("Friday");
            var Saturday = UseDB.SelectDailyAvailabilities("Saturday");

            Console.WriteLine("{0, -15} {1, -15} {2, -15} {3, -15} {4, -15} {5, -15} {6, -15} {7, -15} {8, -15} {9, -15} {10, -15}", "Employee ID", "First Name", "Last Name", "DailyDefault", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");
            for (int i = 0; i < Sunday.Count; i++)
            {
                int empID = DailyDefaults[i].EmployeeID;
                string empFName = DailyDefaults[i].EmpFName;
                string empLName = DailyDefaults[i].EmpLName;

                string dailyDefault = DailyDefaults[i].StartTime.ToString("hh") + ":" + DailyDefaults[i].StartTime.ToString("mm") + "-" + DailyDefaults[i].EndTime.ToString("hh") + ":" + DailyDefaults[i].EndTime.ToString("mm");
                string sundayStr = Sunday[i].StartTime.ToString("hh") + ":" + Sunday[i].StartTime.ToString("mm") + "-" + Sunday[i].EndTime.ToString("hh") + ":" + Sunday[i].EndTime.ToString("mm");
                string mondayStr = Monday[i].StartTime.ToString("hh") + ":" + Monday[i].StartTime.ToString("mm") + "-" + Monday[i].EndTime.ToString("hh") + ":" + Monday[i].EndTime.ToString("mm");
                string tuesdayStr = Tuesday[i].StartTime.ToString("hh") + ":" + Tuesday[i].StartTime.ToString("mm") + "-" + Tuesday[i].EndTime.ToString("hh") + ":" + Tuesday[i].EndTime.ToString("mm");
                string wednesdayStr = Wednesday[i].StartTime.ToString("hh") + ":" + Wednesday[i].StartTime.ToString("mm") + "-" + Wednesday[i].EndTime.ToString("hh") + ":" + Wednesday[i].EndTime.ToString("mm");
                string thursdayStr = Thursday[i].StartTime.ToString("hh") + ":" + Thursday[i].StartTime.ToString("mm") + "-" + Thursday[i].EndTime.ToString("hh") + ":" + Thursday[i].EndTime.ToString("mm");
                string fridayStr = Friday[i].StartTime.ToString("hh") + ":" + Friday[i].StartTime.ToString("mm") + "-" + Friday[i].EndTime.ToString("hh") + ":" + Friday[i].EndTime.ToString("mm");
                string saturdayStr = Saturday[i].StartTime.ToString("hh") + ":" + Saturday[i].StartTime.ToString("mm") + "-" + Saturday[i].EndTime.ToString("hh") + ":" + Saturday[i].EndTime.ToString("mm");

                Console.WriteLine(
                    "{0, -15} " +
                    "{1, -15} " +
                    "{2, -15} " +
                    "{3, -15} " +
                    "{4, -15} " +
                    "{5, -15} " +
                    "{6, -15} " +
                    "{7, -15} " +
                    "{8, -15} " +
                    "{9, -15} " +
                    "{10, -15}", empID, empFName, empLName, dailyDefault, sundayStr, mondayStr, tuesdayStr, wednesdayStr, thursdayStr, fridayStr, saturdayStr);
            }
        }
        public void SetAvailability()
        {
            var daysOfWeek = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            string empIDStr;
            int employeeID;
            string dayOfWeek;
            string startTime;
            string endTime;
            bool isValidDay = false;

            //Get Employee ID
            while (true)
            {
                Console.WriteLine("Enter the ID of the Employee whose availability you wish to modify: ");
                empIDStr = Console.ReadLine();
                if (Int32.TryParse(empIDStr, out employeeID))
                {
                    break;
                }

            }

            //Get DayOfWeek to Modify
            while(true)
            {
                Console.WriteLine("Type the day of the week to change it's availability (i.e. 'Monday' no abbreviations): ");
                dayOfWeek = Console.ReadLine();
                foreach (String day in daysOfWeek)
                {
                    if (String.Equals(day, dayOfWeek, StringComparison.OrdinalIgnoreCase))
                    {
                        dayOfWeek = day;
                        isValidDay = true;
                        break;
                    }
                }
                if (!String.IsNullOrEmpty(dayOfWeek) && isValidDay )
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Try again. Invalid Input");
                }

            }

            //Get Beginning of Employee's Availability
            while (true)
            {
                string pattern = @"\d{1,2}\:\d{1,2}";
                
                Console.WriteLine("What time does the Employee's availability begin on {0} (hh:mm)':", dayOfWeek);
                string hoursMinutes = Console.ReadLine();
                if (Regex.IsMatch(hoursMinutes, pattern))
                {
                    string[] hhmmString = hoursMinutes.Split(':');
                    int hhInt = Int32.Parse(hhmmString[0]);
                    int mmInt = Int32.Parse(hhmmString[1]);

                    if ((hhInt >= 0 && hhInt <= 23) &&
                        (mmInt >= 0 && mmInt <= 59))
                    {
                        //if (mmInt >= 0 && mmInt < 15)
                        //{
                        //    mmInt = 0;
                        //}
                        //else if (mmInt >= 15 && mmInt < 30)
                        //{
                        //    mmInt = 15;
                        //}
                        //else if (mmInt >= 30 && mmInt < 45)
                        //{
                        //    mmInt = 30;
                        //}
                        //else if (mmInt >= 45 && mmInt < 60)
                        //{
                        //    mmInt = 45;
                        //}
                        startTime = hoursMinutes;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Again.");
                }
            }
            //Get the end of an Employee's Availability
            while (true)
            {
                string pattern = @"\d{1,2}\:\d{1,2}";

                Console.WriteLine("What time does the Employee's availability end on {0} (hh:mm)':", dayOfWeek);
                string hoursMinutes = Console.ReadLine();
                if (Regex.IsMatch(hoursMinutes, pattern))
                {
                    string[] hhmmString = hoursMinutes.Split(':');
                    int hhInt = Int32.Parse(hhmmString[0]);
                    int mmInt = Int32.Parse(hhmmString[1]);

                    if ((hhInt >= 0 && hhInt <= 23) &&
                        (mmInt >= 0 && mmInt <= 59))
                    {
                        //if (mmInt >= 0 && mmInt < 15)
                        //{
                        //    mmInt = 0;
                        //}
                        //else if (mmInt >= 15 && mmInt < 30)
                        //{
                        //    mmInt = 15;
                        //}
                        //else if (mmInt >= 30 && mmInt < 45)
                        //{
                        //    mmInt = 30;
                        //}
                        //else if (mmInt >= 45 && mmInt < 60)
                        //{
                        //    mmInt = 45;
                        //}
                        endTime = hoursMinutes;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Again.");
                }

            }

            UseDB.InsertUpdateDailyAvailability(dayOfWeek, employeeID, TimeSpan.Parse(startTime), TimeSpan.Parse(endTime));
            Console.Clear();
            this.DisplayScreen();

        }
    }
}
