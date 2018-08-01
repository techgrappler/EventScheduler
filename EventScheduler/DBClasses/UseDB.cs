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

        public static List<Appointment> ReadAppointments()
        {

            var appointments = new List<Appointment>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Appointments.AppointmentID, Appointments.CustomerID, Appointments.ServiceID, Appointments.EmployeeID, Appointments.Time, Customers.FName, Customers.LName, Services.ServiceName, Employees.FName, Employees.LName FROM Appointments JOIN Customers ON Appointments.CustomerID = Customers.CustomerID JOIN Employees ON Appointments.EmployeeID = Employees.EmployeeID JOIN Services ON Appointments.ServiceID = Services.ServiceID JOIN TimeSlots ON Appointments.Time = TimeSlots.Time ORDER BY Appointments.AppointmentID, Appointments.Time", connection);
                

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
                        apt.Time = reader.GetDateTime(4);
                        apt.CustomerName = reader.GetString(5).TrimEnd() + " " + reader.GetString(6).TrimEnd();
                        apt.ServiceName = reader.GetString(7).TrimEnd();
                        apt.EmployeeName = reader.GetString(8).TrimEnd() + " " + reader.GetString(9).TrimEnd();
                        appointments.Add(apt);
                    }
                }

            }
            return appointments;
        }

        public static List<Employee> ReadEmployee()
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
        
        public static List<Service> ReadServices()
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

        public static List<EmpAvailability> ReadEmpAvailability()
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

    }
}
