namespace EmployeeManager
{
    public class Employee
    {
        public Employee(string lastName, string firstName, string title)
        {
            LastName = lastName;
            FirstName = firstName;
            Title = title;
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
    }
}
