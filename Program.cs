using System;
using System.Collections.Generic;
using System.Linq;

public class Group
{
    private List<Student> students;

    public string Name { get; set; }
    public string Specialization { get; set; }
    public int Course { get; set; }

    public Group()
    {
        students = new List<Student>();
    }

    public Group(Student[] studentsArray)
    {
        students = studentsArray.ToList();
    }

    public Group(List<Student> studentsList)
    {
        students = new List<Student>(studentsList);
    }

    public Group(Group otherGroup)
    {
        Name = otherGroup.Name;
        Specialization = otherGroup.Specialization;
        Course = otherGroup.Course;
        students = new List<Student>(otherGroup.students);
    }

    public void ShowAllStudents()
    {
        Console.WriteLine($"{Name} ({Specialization}), {Course} курс:");
        var orderedStudents = students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();
        for (int i = 0; i < orderedStudents.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {orderedStudents[i]}");
        }
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public void EditStudent(Student oldStudent, Student newStudent)
    {
        var index = students.IndexOf(oldStudent);
        if (index >= 0)
        {
            students[index] = newStudent;
        }
    }

    public void TransferStudent(Student student, Group newGroup)
    {
        students.Remove(student);
        newGroup.AddStudent(student);
    }

    public void ExpelAllFailedStudents()
    {
        students.RemoveAll(s => !s.PassedSession);
    }

    public void ExpelOneFailedStudent()
    {
        var failedStudents = students.Where(s => !s.PassedSession).ToList();
        if (failedStudents.Count > 0)
        {
            var leastGrades = failedStudents.Min(s => s.AverageGrade);
            var studentToExpel = failedStudents.FirstOrDefault(s => s.AverageGrade == leastGrades);
            students.Remove(studentToExpel);
        }
    }
}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public bool PassedSession { get; set; }
    public Dictionary<string, int> Grades { get; set; }

    public double AverageGrade
    {
        get
        {
            if (Grades.Count == 0)
            {
                return 0.0;
            }
            var sum = Grades.Sum(g => g.Value);
            return (double)sum / Grades.Count;
        }
    }

    public Student()
    {
        Grades = new Dictionary<string, int>();
    }

    public Student(string firstName, string lastName, int age) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public void AddGrade(string subject, int grade)
    {
        Grades[subject] = grade;
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName}, возраст: {Age}, средний балл: {AverageGrade:0.00}";
    }
}