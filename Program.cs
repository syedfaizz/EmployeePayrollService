using System;
using System.Data;

namespace EmployeePayroll
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll Service");
            EmployeeOperation employeeOperation = new EmployeeOperation();
            employeeOperation.GetAllEmployeeDetail();
        }
    }
}