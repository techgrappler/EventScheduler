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
                        var cust = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
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

        public static void InsertEmployee(string firstName, string lastName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Employees (FName, LName) VALUES (@firstName, @lastName) ", connection);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.ExecuteNonQuery();
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

        public static void InsertCustomer(string firstName, string lastName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers (FName, LName) VALUES (@firstName, @lastName) ", connection);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
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
    }
}
