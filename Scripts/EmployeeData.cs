using System;
using System.Collections.Generic;

// Serializable, Unity'nin bu sınıfı Inspector'da göstermesine veya JSON'a dönüştürmesine olanak tanır.
[Serializable]
public class Employee
{
    public string employeeName;
    public string position;

    public Employee(string name, string pos)
    {
        employeeName = name;
        position = pos;
    }
}

[Serializable]
public class EmployeeList
{
    public List<Employee> employees;

    public EmployeeList()
    {
        employees = new List<Employee>();
    }
}