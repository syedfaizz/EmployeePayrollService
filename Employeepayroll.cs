using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace EmployeePayroll
{
    class EmployeePayrolls
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=payroll_service";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
    }
}
