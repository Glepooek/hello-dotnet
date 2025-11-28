using System.Linq;

namespace Stylet.Samples.MasterDetail
{
    public class ShellViewModel : Screen
    {
        public IObservableCollection<Employee> Employees { get; private set; }

        private Employee mSelectedEmployee;
        public Employee SelectedEmployee
        {
            get { return mSelectedEmployee; }
            set { SetAndNotify(ref mSelectedEmployee, value); }
        }


        public ShellViewModel()
        {
            this.DisplayName = "Master-Detail";

            Employees = new BindableCollection<Employee>()
            {
                new Employee() { Name = "Fred" },
                new Employee() { Name = "Bob" }
            };

            SelectedEmployee = Employees.FirstOrDefault();
        }

        public void AddEmployee()
        {
            Employees.Add(new Employee() { Name = "Unnamed" });
        }

        public void RemoveEmployee(Employee employee)
        {
            if (Employees.Contains(employee))
            {
                Employees.Remove(employee);
            }
        }
    }
}
