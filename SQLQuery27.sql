SELECT Appointments.AppointmentID, Appointments.CustomerID,
                    Appointments.ServiceID,
                    Appointments.EmployeeID,
                    Appointments.StartTime,
                    Appointments.EndTime,
                    Customers.FName,
                    Customers.LName,
                    Services.ServiceName,
                    Employees.FName,
                    Employees.LName
					
                    FROM Appointments
                    JOIN Customers ON Appointments.CustomerID = Customers.CustomerID
                    JOIN Employees ON Appointments.EmployeeID = Employees.EmployeeID 
                    JOIN Services ON Appointments.ServiceID = Services.ServiceID
					WHERE Appointments.EmployeeID = 1
                    ORDER BY Appointments.AppointmentID, Appointments.StartTime
