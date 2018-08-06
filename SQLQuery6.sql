SELECT Appointments.AppointmentID, 
Appointments.CustomerID, 
Customers.FName, Customers.LName, 
Employees.FName, Employees.LName,
Services.ServiceName, Appointments.Time
FROM Appointments
JOIN Customers ON Appointments.CustomerID=Customers.CustomerID
JOIN Employees ON Appointments.EmployeeID=Employees.EmployeeID
JOIN Services ON Appointments.ServiceID=Services.ServiceID
JOIN TimeSlots ON Appointments.Time=TimeSlots.Time
ORDER BY AppointmentID
