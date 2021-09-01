using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayroll
{
    public class EmployeeOperation
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=payroll_service";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public static Employee employee = new Employee();
        public void GetAllEmployeeDetail()
        {
            this.sqlConnection.Open();
            string query = "SELECT * FROM dbo.employee_payroll";
            try
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.ID = reader.GetInt32(0);
                        employee.Name = reader.GetString(1);
                        employee.Gender = reader.GetString(2);
                        employee.PhoneNumber = reader.GetString(3);
                        employee.Address = reader.GetString(4);
                        employee.Department = reader.GetString(5);
                        employee.BasicPay = reader.GetInt32(6);
                        employee.Deduction = reader.GetDouble(7);
                        employee.TaxablePay = reader.GetDouble(8);
                        employee.Tax = reader.GetDouble(9);
                        employee.NetPay = reader.GetDouble(10);
                        employee.StartDate = reader.GetDateTime(11);
                        employee.City = reader.GetString(12);
                        employee.Country = reader.GetString(13);
                        //// display the payroll data from database
                        Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}", employee.ID, employee.Name,
                            employee.Gender, employee.PhoneNumber, employee.Address, employee.Department, employee.BasicPay, employee.Deduction,
                            employee.TaxablePay, employee.Tax, employee.NetPay, employee.StartDate, employee.City, employee.Country);
                        Console.WriteLine("\n");
                    }
                }
                else
                {
                    Console.WriteLine("No record found");
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}