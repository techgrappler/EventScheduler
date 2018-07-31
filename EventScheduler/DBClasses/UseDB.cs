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

        public static List<Employee> ReadEmployee(string connString)
        {
          
            var employees = new List<Employee>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", connection);
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
    }
}
