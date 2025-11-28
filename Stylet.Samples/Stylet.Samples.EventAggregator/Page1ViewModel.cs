namespace Stylet.Samples.EventAggregator
{
    public class Page1ViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;

        private Employee mEmployee;
        public Employee Employee
        {
            get { return mEmployee; }
            set { SetAndNotify(ref mEmployee, value); }
        }

        public Page1ViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            Employee = new Employee();
        }

        public void Send()
        {
            eventAggregator.Publish(new EmployeeEvent()
            {
                Name = Employee.Name,
                Age = Employee.Age,
                Sex = Employee.Sex
            });

            eventAggregator.Publish(new EmployeeEvent()
            {
                Name = Employee.Name,
                Age = Employee.Age,
                Sex = Employee.Sex
            }, "employee");
        }

    }
}
