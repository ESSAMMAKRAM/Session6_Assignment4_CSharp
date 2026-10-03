using System;
using System.Globalization;

namespace Assignment02_OOP
{
    #region Enums & Supporting Structures

    [Flags]
    public enum SecurityPrivileges
    {
        Guest = 1,
        Developer = 2,
        Secretary = 4,
        DBA = 8,
        SecurityOfficer = Guest | Developer | Secretary | DBA
    }

    public class HiringDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public HiringDate(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Day:D2}/{Month:D2}/{Year}";
        }
    }

    #endregion

    #region Part 01: Employee Classes

    public class Employee
    {
        private int id;
        private string name;
        private char gender;
        private decimal salary;
        private SecurityPrivileges securityLevel;
        private HiringDate hireDate;

        public int ID
        {
            get => id;
            set => id = value > 0 ? value : throw new ArgumentException("ID must be positive.");
        }

        public string Name
        {
            get => name;
            set => name = !string.IsNullOrWhiteSpace(value) ? value : "Unknown";
        }

        public char Gender
        {
            get => gender;
            set
            {
                char upperChar = char.ToUpper(value);
                if (upperChar == 'M' || upperChar == 'F')
                {
                    gender = upperChar;
                }
                else
                {
                    throw new ArgumentException("Gender must be 'M' (Male) or 'F' (Female).");
                }
            }
        }

        public decimal Salary
        {
            get => salary;
            set => salary = value >= 0 ? value : 0;
        }

        public SecurityPrivileges SecurityLevel
        {
            get => securityLevel;
            set => securityLevel = value;
        }

        public HiringDate HireDate
        {
            get => hireDate;
            set => hireDate = value ?? new HiringDate(1, 1, 2026);
        }

        public Employee()
        {
            ID = 1;
            Name = "Default Employee";
            Gender = 'M';
            Salary = 5000m;
            SecurityLevel = SecurityPrivileges.Guest;
            HireDate = new HiringDate(1, 1, 2026);
        }

        public Employee(int id, string name, char gender, decimal salary, SecurityPrivileges securityLevel, HiringDate hireDate)
        {
            ID = id;
            Name = name;
            Gender = gender;
            Salary = salary;
            SecurityLevel = securityLevel;
            HireDate = hireDate;
        }

        public override string ToString()
        {
            string formattedSalary = string.Format(CultureInfo.CurrentCulture, "{0:C}", Salary);
            return $"ID: {ID}, Name: {Name}, Gender: {Gender}, Salary: {formattedSalary}, Security Level: {SecurityLevel}, Hire Date: {HireDate}";
        }
    }

    #endregion

    #region Part 02: Static Binding (Shape & Cube)

    public class Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Shape(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public virtual double Area()
        {
            return Width * Height;
        }

        public override string ToString()
        {
            return $"(Width={Width},Height={Height})";
        }
    }

    public class Cube : Shape
    {
        public double Depth { get; set; }

        public Cube(double width, double height, double depth) : base(width, height)
        {
            Depth = depth;
        }

        public new double Area()
        {
            return base.Area() * Depth;
        }

        public void Print()
        {
            Console.WriteLine($"Width: {Width}, Height: {Height}, Depth: {Depth}");
        }
    }

    #endregion

    #region Part 02 & 03: Dynamic Binding (Person, Doctor, Engineer)

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public void Greet()
        {
            Console.WriteLine("I am a person's basic data.");
        }

        public virtual void Display()
        {
            Console.WriteLine($"Person [ID: {ID}, Name: {Name}, Age: {Age}]");
        }
    }

    public class Doctor : Person
    {
        public string Specialty { get; set; }

        public new void Greet()
        {
            Console.WriteLine("I am a Doctor.");
        }

        public override void Display()
        {
            Console.WriteLine($"Doctor [ID: {ID}, Name: {Name}, Age: {Age}, Specialty: {Specialty}]");
        }
    }

    public class Engineer : Person
    {
        public string Field { get; set; }

        public new void Greet()
        {
            Console.WriteLine("I am an Engineer.");
        }

        public override void Display()
        {
            Console.WriteLine($"Engineer [ID: {ID}, Name: {Name}, Age: {Age}, Field: {Field}]");
        }
    }

    #endregion

    class Program
    {
        static void ProcessPerson(Person person)
        {
            person.Greet();
            person.Display();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== Part 01: Employees ===");
            Employee[] EmpArr = new Employee[3];

            try
            {
                EmpArr[0] = new Employee(101, "Ahmed Ali", 'M', 12000m, SecurityPrivileges.DBA, new HiringDate(15, 5, 2022));
                EmpArr[1] = new Employee(102, "Mona Mohamed", 'F', 6000m, SecurityPrivileges.Guest, new HiringDate(10, 8, 2024));
                EmpArr[2] = new Employee(103, "Khaled Omar", 'M', 20000m, SecurityPrivileges.SecurityOfficer, new HiringDate(1, 1, 2020));

                foreach (var emp in EmpArr)
                {
                    Console.WriteLine(emp.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\n=== Part 02: Static Binding ===");
            Shape shape = new Shape(2, 3);
            Console.WriteLine($"shape.Area() = {shape.Area()}");

            Cube cube = new Cube(2, 3, 4);
            Console.WriteLine($"cube.Area() = {cube.Area()}");

            Shape shapeRef = new Cube(2, 3, 4);
            Console.WriteLine($"shapeRef.Area() = {shapeRef.Area()}");

            object obj = new Cube(1, 2, 3);
            Console.WriteLine($"obj.ToString() -> {obj.ToString()}");

            Console.WriteLine("\n=== Part 03: Dynamic Binding ===");
            Person doc = new Doctor { ID = 1, Name = "Dr. Hazem", Age = 40, Specialty = "Cardiology" };
            Person eng = new Engineer { ID = 2, Name = "Eng. Sara", Age = 28, Field = "Software Engineering" };

            Console.WriteLine("Processing Doctor:");
            ProcessPerson(doc);

            Console.WriteLine("\nProcessing Engineer:");
            ProcessPerson(eng);
        }
    }
}
