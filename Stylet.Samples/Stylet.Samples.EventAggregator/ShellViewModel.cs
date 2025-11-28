namespace Stylet.Samples.EventAggregator
{
    // 如果要订阅多个事件，需要实现多个IHandle<T>
    public class ShellViewModel : Conductor<IScreen>, IHandle<EmployeeEvent>
    {
        public IObservableCollection<Employee> Employees { get; private set; }

        public ShellViewModel(IEventAggregator eventAggregator, Page1ViewModel page1ViewModel)
        {
            this.DisplayName = "Stylet.Samples.EventAggregator";
            this.ActiveItem = page1ViewModel;
            this.Employees = new BindableCollection<Employee>();

            // 订阅
            eventAggregator?.Subscribe(this, "employee");
            // eventAggregator?.Unsubscribe(this); 取消订阅所有通道
            // eventAggregator?.Unsubscribe(this, "employee"); 取消订阅employee通道
        }

        public void Handle(EmployeeEvent message)
        {
            Employees.Add(new Employee()
            {
                Name = message.Name,
                Age = message.Age,
                Sex = message.Sex
            });
        }
    }
}
