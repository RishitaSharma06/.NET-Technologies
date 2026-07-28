using System;
using System.Collections.Generic;

namespace SimplePayrollSystem
{
    // 1. INTERFACE: Defines the contract for anything that can generate a paycheck
    public interface IPayable
    {
        decimal CalculatePay();
    }

    // 2. INHERITANCE: Base class holding common properties and enforcing the interface
    public abstract class Employee : IPayable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Abstract method: Each child class MUST implement its own pay calculation
        public abstract decimal CalculatePay();
    }

    // 3. POLYMORPHISM: Full-Time Employee (Monthly Base Salary)
    public class FullTimeEmployee : Employee
    {
        public decimal MonthlySalary { get; set; }

        public FullTimeEmployee(int id, string name, decimal monthlySalary) 
            : base(id, name)
        {
            MonthlySalary = monthlySalary;
        }

        // Overrides base calculation
        public override decimal CalculatePay()
        {
            return MonthlySalary;
        }
    }

    // 3. POLYMORPHISM: Part-Time Employee (Hourly Rate * Hours Worked)
    public class PartTimeEmployee : Employee
    {
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }

        public PartTimeEmployee(int id, string name, decimal hourlyRate, int hoursWorked) 
            : base(id, name)
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        // Overrides base calculation
        public override decimal CalculatePay()
        {
            return HourlyRate * HoursWorked;
        }
    }

    // 4. MAIN PROGRAM: Demonstrating Polymorphism
    class Program
    {
        static void Main(string[] args)
        {
            // PolyMorphic list holding different types of employees
            List<Employee> employees = new List<Employee>();

            employees.Add(new FullTimeEmployee(101, "Alice", 5000.00m));
            employees.Add(new PartTimeEmployee(102, "Bob", 20.00m, 80));

            Console.WriteLine("--- PAYROLL REPORT ---");

            // Loop through all employees without worrying about their specific type
            foreach (Employee emp in employees)
            {
                // Polymorphic Call: Calls the correct CalculatePay() automatically
                decimal pay = emp.CalculatePay();
                
                Console.WriteLine($"ID: {emp.Id} | Name: {emp.Name} | Total Pay: ${pay}");
            }
        }
    }
}
