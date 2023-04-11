// See https://aka.ms/new-console-template for more information
using PayrollEmployee;
using System;


//naming the project 
namespace PayrollEmployee
    //creating a data structions for the employees 
{
    public struct Employee
    {
        public int EmployeeId;
        public string EmployeeFirstName;
        public string EmployeeLastName;
        public double AnnualIncome;
        public double KiwiSaver;
        public double fortnightPayroll;
        public double hourlywage;
        
    }
    class Program
    {
        //creating Menu serach to set payroll system
        public static void Main(string[] args)
        {
            //set employee text file with employee details in it. 
           
            Employee[] employees = ReadEmployeeDetails("employee_payroll.txt");
            //function read employee using loop. dispays options for user to choose from menu  
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine();
                Console.WriteLine("1. Fortnight payroll calculation");
                Console.WriteLine("2. Sort and display all people's records");
                Console.WriteLine("3. Search for an employee");
                Console.WriteLine("4. Save into text file");
                Console.WriteLine("0. Exit");

                Console.WriteLine("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                //switch statements control the structure of the code dividing it in cases. 
                //cases are related to the read employee function menu above
                switch (choice)
                {
                    case 1:
                        CalculateFortnightPayroll(employees);
                        DisplayEmployee(employees);
                        break;

                    case 2:
                        SortEmployeeRecord(employees);
                        DisplayEmployee(employees);
                        break;

                    case 3:
                        Console.WriteLine("Enter the Employee ID:");
                        int EmployeeId = int.Parse(Console.ReadLine());
                        SearchEmployee(employees, EmployeeId);
                        break
    ;
                    case 4:
                        SaveEmployeeDetails(employees);
                        Console.WriteLine("Succesfully Saved");
                        break;

                    case 0: 
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }

        }
        //calculating fornight payroll,kiwisaver, tax and hourly wage using loop 
        static void CalculateFortnightPayroll(Employee[] employees)
        {
            double tax;
            double kiwisaver;
            double hourlywage;
            for (int i= 0; i < employees.Length; i++) 
            {
                if (employees[i].AnnualIncome <= 14000)
                {
                    tax = employees[i].AnnualIncome * 0.105;
                } 
                else if (employees[i].AnnualIncome > 14000 && employees[i].AnnualIncome<= 48000)
                {
                    tax = 14000 * 0.105 + (employees[i].AnnualIncome - 14000) * 0.175;
                }
                else if (employees[i].AnnualIncome > 48000 && employees[i].AnnualIncome <= 70000)
                {
                    tax = 14000 * 0.105 + (48000-14000) * 0.175 + (employees[i].AnnualIncome - 48000) * 0.3;
                }
                else if (employees[i].AnnualIncome > 70000 && employees[i].AnnualIncome <= 180000)
                {
                    tax = 14000 * 0.105 + (48000-14000) *0.175 + (70000-180000) * 0.3 + 
                    (employees[i].AnnualIncome - 70000) * 0.33;
                }
                else
                {
                    tax = 14000 * 0.105 + (48000 - 14000) * 0175 + (70000 - 48000) * 0.3 + (70000 - 180000) * 0.33 +
                    (employees[i].AnnualIncome - 180000) * 0.39;
                }
                employees[i].fortnightPayroll = Math.Round(
                (employees[i].AnnualIncome - employees[i].AnnualIncome * employees[i].KiwiSaver - tax) / 52 * 2, 2);
                employees[i].hourlywage = employees[i].AnnualIncome / 52 / 40;
            }
        }
         
        //function for reading the employee details
        
        static Employee[] ReadEmployeeDetails(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            Employee[] employees = new Employee[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                Employee employee = new Employee();
                employee.EmployeeId = int.Parse(parts[0]);
                employee.EmployeeFirstName = parts[1];
                employee.EmployeeLastName = parts[2];
                employee.AnnualIncome = double.Parse(parts[3]);
                employee.KiwiSaver = double.Parse(parts[4]);
                employees[i] = employee;
            }

            return employees;
        }

        //sort Employees details with bubble sourt function,
        //it shows first the Id out of order and then will compare and sort employee
        //details in the following order:ID, full Name, Annual Income, Kiwi Saver, forthnigh payroll hourly wage
        static void SortEmployeeRecord(Employee[] employees)
        {
            for (int i = 0; i < employees.Length - 1; i++)
            {
                for (int j = 0; j < employees.Length - i - 1; j++)
                {
                    if (employees[j].EmployeeId > employees[j + 1].EmployeeId)
                    {
                        int tempId = employees[j].EmployeeId;
                        string tempEmployeeFirstName = employees[j].EmployeeFirstName;
                        string tempEmployeesLastName = employees[j].EmployeeLastName;
                        double tempAnnualIncome = employees[j].AnnualIncome;
                        double tempKiwiSaver = employees[j].KiwiSaver;
                        double tempfortnightPayroll = employees[j].fortnightPayroll;
                        double temphourlywage = employees[j].hourlywage;
                        employees[j] = employees[j + 1];
                        employees[j + 1].EmployeeId = tempId;
                        employees[j + 1].EmployeeFirstName = tempEmployeeFirstName;
                        employees[j + 1].EmployeeLastName = tempEmployeesLastName;
                        employees[j + 1].AnnualIncome = tempAnnualIncome;
                        employees[j + 1].KiwiSaver = tempKiwiSaver;
                        employees[j + 1].fortnightPayroll = tempfortnightPayroll;
                        employees[j + 1].hourlywage = temphourlywage;
                    }
                }
            }
        }

        //Search Menu function to search Employee using the return value of data. 
        //searching for an employee,by inserting and ID if the employee is not in the list a message to the user get display.
        static void SearchEmployee(Employee[] employees, int id)
        {
            foreach (Employee employee in employees)
            {
                if (employee.EmployeeId == id)
                {
                    Console.WriteLine("Employee ID\tFirst Name\tLast Name\tAnnual Income\tKiwi Saver\tfortnightPayroll\thourlywage");
                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t{3:C}\t{4:P}\t\t{5:C}\t\t{6:C}",
                        employee.EmployeeId,
                        employee.EmployeeFirstName,
                        employee.EmployeeLastName,
                        employee.AnnualIncome,
                        employee.KiwiSaver,
                        employee.fortnightPayroll,
                        employee.hourlywage);
                    return;
                }
            }
            Console.WriteLine("Employee with ID {0} not found.", id);

        }

        //create a display static function linked to switch case 2 and switch case one.
        //this allow the user to see the employee details and income.

        static void DisplayEmployee(Employee[] employees)
        {
            Console.WriteLine("Employee ID\tFirst Name\tLast Name\tAnnual Income\tKiwi Saver\tfortnightPayroll\thourlywage");
            foreach (Employee employee in employees)

            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}\t{3:C}\t{4:P}\t\t{5:C}\t\t{6:C}",
                employee.EmployeeId, employee.EmployeeFirstName, employee.EmployeeLastName, employee.AnnualIncome,
                employee.KiwiSaver, employee.fortnightPayroll, employee.hourlywage);
            }
        }

        // save employee details function, allow the file to get saved and display a confirmation text to the user.
        static void SaveEmployeeDetails(Employee[] employees)
        {
            using (StreamWriter writer = new StreamWriter("weekly_payroll.txt"))
            {
                writer.WriteLine("EmployeeId,EmployeeFirstName,EmployeeLastName,AnnualIncome,KiwiSaver,fortnightPayroll,hourlywage");
                foreach (Employee employee in employees)
                {
                    writer.WriteLine("{0},{1},{2},{3:C},{4:P},{5:C},{6:C}",
                    employee.EmployeeId, employee.EmployeeFirstName, employee.EmployeeLastName, employee.AnnualIncome,
                    employee.KiwiSaver, employee.fortnightPayroll, employee.hourlywage);
                }
            }
        }
    }

}
