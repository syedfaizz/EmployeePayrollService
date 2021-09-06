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
        readonly List<Employee> empList = new List<Employee>();
        public List<Employee> GetAllEmployeeDetails()
        {
            this.sqlConnection.Open();
            try
            {
                SqlCommand command = new SqlCommand("spGetAllEmployee", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                foreach (DataRow data in dataTable.Rows)
                {
                    empList.Add
                        (
                            new Employee
                            {
                                ID = Convert.ToInt32(data["Id"]),
                                Name = Convert.ToString(data["Name"]),
                                Gender = Convert.ToString(data["Gender"]),
                                PhoneNumber = Convert.ToString(data["PhoneNumber"]),
                                Address = Convert.ToString(data["Address"]),
                                Department = Convert.ToString(data["Department"]),
                                BasicPay = Convert.ToInt32(data["BasicPay"]),
                                Deduction = Convert.ToDouble(data["Deduction"]),
                                TaxablePay = Convert.ToDouble(data["TaxablePay"]),
                                Tax = Convert.ToDouble(data["Tax"]),
                                NetPay = Convert.ToDouble(data["NetPay"]),
                                StartDate = Convert.ToDateTime(data["StartDate"]),
                                City = Convert.ToString(data["City"]),
                                Country = Convert.ToString(data["Country"])
                            }
                        );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
            return empList;
        }
        public int UpdateEmployeeSalary()
        {
            Employee emp = new Employee();
            emp.Name = "Terissa";
            emp.BasicPay = 3000000;
            try
            {
                this.sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spUpdateEmployee", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Name", emp.Name);
                sqlCommand.Parameters.AddWithValue("@BasicPay", emp.BasicPay);
                int result = sqlCommand.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("Salary is updated...");
                else
                    Console.WriteLine("Salary not updated!");
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public void GetEmployeeDetailsByDate()
        {
            Employee employee = new Employee();
            DateTime startDate = new DateTime(1997, 01, 10);
            DateTime endDate = new DateTime(2010, 04, 15);
            try
            {
                this.sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spGetDataByDate", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = sqlCommand.ExecuteReader();
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