using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public string Class { get; set; }
}

class Program
{
    static void Main()
    {
        // Read student data from the text file
        List<Student> students = ReadStudentData("C:\\Users\\tabib\\source\\student_data.txt");

        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Display sorted student data");
            Console.WriteLine("2. Search for a student by name");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        DisplaySortedStudentData(students);
                        break;
                    case 2:
                        SearchStudentByName(students);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid option.");
            }
        }
    }

    static List<Student> ReadStudentData(string filePath)
    {
        List<Student> students = new List<Student>();
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    Student student = new Student
                    {
                        Name = parts[0].Trim(),
                        Class = parts[1].Trim()
                    };
                    students.Add(student);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found. Make sure the file 'student_data.txt' exists in the project directory.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return students;
    }

    static void DisplaySortedStudentData(List<Student> students)
    {
        List<Student> sortedStudents = students.OrderBy(s => s.Name).ToList();
        Console.WriteLine("Student Data (Sorted by Name):");
        foreach (var student in sortedStudents)
        {
            Console.WriteLine($"{student.Name}, {student.Class}");
        }
    }

    static void SearchStudentByName(List<Student> students)
    {
        Console.Write("Enter the student's name to search: ");
        string searchName = Console.ReadLine();
        var searchResults = students.Where(s => s.Name.ToLower().Contains(searchName.ToLower())).ToList();

        if (searchResults.Any())
        {
            Console.WriteLine("Search Results:");
            foreach (var student in searchResults)
            {
                Console.WriteLine($"{student.Name}, {student.Class}");
            }
        }
        else
        {
            Console.WriteLine("No matching student found.");
        }
    }
}
