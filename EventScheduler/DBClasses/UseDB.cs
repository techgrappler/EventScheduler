using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EventScheduler.DBClasses
{
    public abstract class UseDB
    {
        public static string ConnectionString { get; set; }

        public static List<Employee> SelectEmployees()
        {
          
            var employees = new List<Employee>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var emp = new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        employees.Add(emp);
                    }
                }

            }
            return employees;
        }

        public static List<Service> SelectServices()
        {

            var services = new List<Service>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Services", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var service = new Service();
                        service.ID = reader.GetInt32(0);
                        service.Name = reader.GetString(1);
                        service.Description = reader.GetString(2);
                        services.Add(service);
                    }
                }

            }
            return services;
        }

        public static List<Appointment> SelectAppointments()
        {

            var appointments = new List<Appointment>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Appointments.AppointmentID," +
                    " Appointments.CustomerID" +
                    ", Appointments.ServiceID," +
                    " Appointments.EmployeeID," +
                    " Appointments.StartTime," +
                    " Appointments.EndTime," +
                    " Customers.FName," +
                    " Customers.LName," +
                    " Services.ServiceName," +
                    " Employees.FName," +
                    " Employees.LName " +
                    "FROM Appointments " +
                    "JOIN Customers ON Appointments.CustomerID = Customers.CustomerID " +
                    "JOIN Employees ON Appointments.EmployeeID = Employees.EmployeeID " +
                    "JOIN Services ON Appointments.ServiceID = Services.ServiceID " +
                    "ORDER BY Appointments.AppointmentID, Appointments.StartTime", connection);
                

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var apt = new Appointment();
                        apt.ID = reader.GetInt32(0);
                        apt.CustomerID = reader.GetInt32(1);
                        apt.ServiceID = reader.GetInt32(2);
                        apt.EmployeeID = reader.GetInt32(3);
                        apt.StartTime = reader.GetDateTime(4);
                        apt.EndTime = reader.GetDateTime(5);
                        apt.CustomerName = reader.GetString(6).TrimEnd() + " " + reader.GetString(7).TrimEnd();
                        apt.ServiceName = reader.GetString(8).TrimEnd();
                        apt.EmployeeName = reader.GetString(9).TrimEnd() + " " + reader.GetString(10).TrimEnd();
                        appointments.Add(apt);
                    }
                }

            }
            return appointments;
        }
        
        public static List<Appointment> SelectAppointments(int empID)
        {

            var appointments = new List<Appointment>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Appointments.AppointmentID," +
                    " Appointments.CustomerID" +
                    ", Appointments.ServiceID," +
                    " Appointments.EmployeeID," +
                    " Appointments.StartTime," +
                    " Appointments.EndTime," +
                    " Customers.FName," +
                    " Customers.LName," +
                    " Services.ServiceName," +
                    " Employees.FName," +
                    " Employees.LName " +
                    "FROM Appointments " +
                    "JOIN Customers ON Appointments.CustomerID = Customers.CustomerID " +
                    "JOIN Employees ON Appointments.EmployeeID = Employees.EmployeeID " +
                    "JOIN Services ON Appointments.ServiceID = Services.ServiceID " +
                    "WHERE Appointments.EmployeeID=@empID " +
                    "ORDER BY Appointments.AppointmentID, Appointments.StartTime", connection);

                cmd.Parameters.AddWithValue("@empID", empID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var apt = new Appointment();
                        apt.ID = reader.GetInt32(0);
                        apt.CustomerID = reader.GetInt32(1);
                        apt.ServiceID = reader.GetInt32(2);
                        apt.EmployeeID = reader.GetInt32(3);
                        apt.StartTime = reader.GetDateTime(4);
                        apt.EndTime = reader.GetDateTime(5);
                        apt.CustomerName = reader.GetString(6).TrimEnd() + " " + reader.GetString(7).TrimEnd();
                        apt.ServiceName = reader.GetString(8).TrimEnd();
                        apt.EmployeeName = reader.GetString(9).TrimEnd() + " " + reader.GetString(10).TrimEnd();
                        appointments.Add(apt);
                    }
                }

            }
            return appointments;
        }
        public static List<Customer> SelectCustomers()
        {

            var customers = new List<Customer>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cust = new Customer();
                        cust.ID = reader.GetInt32(0);
                        cust.FName = reader.GetString(1);
                        cust.LName = reader.GetString(2);
                        customers.Add(cust);
                    }
                }

            }
            return customers;
        }

        public static List<EmpAvailability> SelectEmpAvailabilities()
        {
            var empAvailabilities = new List<EmpAvailability>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EmpAvailability", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var empAvailability = new EmpAvailability();
                        empAvailability.EmployeeID = reader.GetInt32(0);
                        empAvailability.Time = reader.GetDateTime(1);
                        empAvailability.IsAvailable = (reader.GetInt16(2) == 1);
                        empAvailability.IsBooked = (reader.GetInt16(3) == 1);
                        empAvailabilities.Add(empAvailability);
                    }
                }

            }
            return empAvailabilities;
        }

        public static List<DailyAvailability> SelectDailyAvailabilities(string dayOfWeek)


        {
            var dailyAvailabilities = new List<DailyAvailability>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                //SqlCommand cmd = new SqlCommand("SELECT DailyAvailability.EmployeeID, Employees.FName, Employees.LName, DailyAvailability.DayOfWeek, DailyAvailability.StartTime, DailyAvailability.EndTime " +
                //    "FROM DailyAvailability " +
                //    "JOIN Employees ON Employees.EmployeeID = DailyAvailability.EmployeeID " +
                //    "ORDER BY EmployeeID, (CASE  " +
                //    "WHEN DayOfWeek = 'DailyDefault' THEN 0 " +
                //    "WHEN DayOfWeek = 'Sunday' THEN 1 " +
                //    "WHEN DayOfWeek = 'Monday' THEN 2 " +
                //    "WHEN DayOfWeek = 'Tuesday' THEN 3 " +
                //    "WHEN DayOfWeek = 'Wednesday' THEN 4 " +
                //    "WHEN DayOfWeek = 'Thursday' THEN 5 " +
                //    "WHEN DayOfWeek = 'Friday' THEN 6 " +
                //    "WHEN DayOfWeek = 'Saturday' THEN 7 END)", connection);
                SqlCommand cmd = new SqlCommand("SELECT DailyAvailability.EmployeeID, Employees.FName, Employees.LName, DailyAvailability.DayOfWeek, DailyAvailability.StartTime, DailyAvailability.EndTime FROM DailyAvailability JOIN Employees ON Employees.EmployeeID = DailyAvailability.EmployeeID WHERE DayOfWeek = @dayOfWeek", connection);
                cmd.Parameters.AddWithValue("@dayOfWeek", dayOfWeek);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var dailyAvailability = new DailyAvailability();
                        dailyAvailability.EmployeeID = reader.GetInt32(0);
                        dailyAvailability.EmpFName = reader.GetString(1);
                        dailyAvailability.EmpLName = reader.GetString(2);
                        dailyAvailability.DayOfWeek = reader.GetString(3);
                        dailyAvailability.StartTime = reader.GetTimeSpan(4);
                        dailyAvailability.EndTime = reader.GetTimeSpan(5);

                        dailyAvailabilities.Add(dailyAvailability);
                    }
                }

            }
            return dailyAvailabilities;
        }

        public static DailyAvailability SelectDailyAvailabily(int empID, string dayOfWeek)
        {
            var dailyAvailability = new DailyAvailability();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT DailyAvailability.EmployeeID, Employees.FName, Employees.LName, DailyAvailability.DayOfWeek, DailyAvailability.StartTime, DailyAvailability.EndTime FROM DailyAvailability JOIN Employees ON Employees.EmployeeID = DailyAvailability.EmployeeID WHERE DailyAvailability.EmployeeID=@empID AND DayOfWeek = @dayOfWeek ", connection);
                cmd.Parameters.AddWithValue("@dayOfWeek", dayOfWeek);
                cmd.Parameters.AddWithValue("@empID", empID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       
                        dailyAvailability.EmployeeID = reader.GetInt32(0);
                        dailyAvailability.EmpFName = reader.GetString(1);
                        dailyAvailability.EmpLName = reader.GetString(2);
                        dailyAvailability.DayOfWeek = reader.GetString(3);
                        dailyAvailability.StartTime = reader.GetTimeSpan(4);
                        dailyAvailability.EndTime = reader.GetTimeSpan(5);
                    }
                }

            }
            return dailyAvailability;
        }


        public static int InsertEmployee(string firstName, string lastName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Employees (FName, LName) VALUES (@firstName, @lastName) ", connection);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                int lastInsertedID = Convert.ToInt32(cmd.ExecuteScalar());
                return lastInsertedID;
                //cmd.ExecuteNonQuery();
            }
        }

        public static void InsertService(string serviceName, string serviceDescription)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Services (ServiceName, ServiceDescription) VALUES (@serviceName, @serviceDescription) ", connection);
                cmd.Parameters.AddWithValue("@serviceName", serviceName);
                cmd.Parameters.AddWithValue("@serviceDescription", serviceDescription);
                cmd.ExecuteNonQuery();
            }
        }

        public static void InsertAppointment(int customerID, int serviceID, int employeeID, DateTime startTime, DateTime endTime)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Appointments (CustomerID, ServiceID, EmployeeID, StartTime, EndTime) VALUES (@customerID, @serviceID, @employeeID, @startTime, @endTime) ", connection);
                cmd.Parameters.AddWithValue("@customerID", customerID);
                cmd.Parameters.AddWithValue("@serviceID", serviceID);
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                cmd.Parameters.AddWithValue("@startTime", startTime);
                cmd.Parameters.AddWithValue("@endTime", endTime);
                cmd.ExecuteNonQuery();
            }

        }

        public static void InsertUpdateEmpAvailability(int employeeID, DateTime time, int isAvailable = 0, int isBooked = 1)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "IF EXISTS (SELECT * From EmpAvailability WHERE EmployeeID=@employeeID AND Time=@time) " +
                    "UPDATE EmpAvailability SET IsBooked = 1 WHERE Time=@time " +
                    "ELSE INSERT INTO EmpAvailability(EmployeeID, Time, IsAvailable, IsBooked) VALUES(@employeeID, @time, @isAvailable, @isBooked)  ", connection);
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@isAvailable", isAvailable);
                cmd.Parameters.AddWithValue("@isBooked", isBooked);

                cmd.ExecuteNonQuery();
            }
        }

        public static int InsertCustomer(string firstName, string lastName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers (FName, LName) VALUES (@firstName, @lastName)", connection);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                int lastInsertedID = Convert.ToInt32(cmd.ExecuteScalar());
                //cmd.ExecuteNonQuery();
                return lastInsertedID;
            }
        }

        public static void InsertUpdateDailyAvailability(string dayOfWeek, int employeeID, TimeSpan startTime, TimeSpan endTime)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "IF EXISTS (SELECT * From DailyAvailability WHERE DayOfWeek=@dayOfWeek AND EmployeeID=@employeeID) " +
                    "UPDATE DailyAvailability SET StartTime=@startTime, EndTime=@endTime WHERE DayOfWeek=@dayOfWeek AND EmployeeID=@employeeID " +
                    "ELSE INSERT INTO DailyAvailability(DayOfWeek, EmployeeID, StartTime, EndTime) VALUES(@dayOfWeek, @employeeID, @startTime, @endTime)  ", connection);
                cmd.Parameters.AddWithValue("@dayOfWeek", dayOfWeek);
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                cmd.Parameters.AddWithValue("@startTime", startTime);
                cmd.Parameters.AddWithValue("@endTime", endTime);

                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteEmployee(int employeeID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeID=@employeeID", connection);
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteService(int serviceID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Services WHERE ServiceID=@serviceID", connection);
                cmd.Parameters.AddWithValue("@serviceID", serviceID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteAppointment(int appointmentID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Appointments WHERE AppointmentID=@appointmentID", connection);
                cmd.Parameters.AddWithValue("@appointmentID", appointmentID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCustomer(int customerID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerID=@customerID", connection);
                cmd.Parameters.AddWithValue("@customerID", customerID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteDailyAvailability(int employeeID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM DailyAvailability WHERE EmployeeID=@employeeID", connection);
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
