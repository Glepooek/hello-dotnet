using System.Windows;

namespace Stylet.Samples.Hello
{
    public class ShellViewModel : Screen
    {
        #region Fields

        private readonly IWindowManager mWindowManager;

        #endregion

        #region Properties

        private string mName = string.Empty;
        public string Name
        {
            get { return this.mName; }
            set
            {
                SetAndNotify(ref mName, value);
                //NotifyOfPropertyChange(() => CanBindedToCommandWithoutParams);
                //NotifyOfPropertyChange(()=> CanBindedToCommandWithParams);
                NotifyOfPropertyChange(nameof(CanBindedToCommandWithoutParams));
                NotifyOfPropertyChange(nameof(CanBindedToCommandWithParams));
            }
        }

        // 守护属性，可控制按钮是否可用
        public bool CanBindedToCommandWithoutParams => !string.IsNullOrWhiteSpace(Name);
        // 守护属性，可控制按钮是否可用
        public bool CanBindedToCommandWithParams => !string.IsNullOrWhiteSpace(Name);

        public InnerViewModel InnerViewModel { get; private set; }

        #endregion

        #region Constructor

        public ShellViewModel(IWindowManager windowManager, InnerViewModel innerViewModel)
        {
            // 设置窗口title
            this.DisplayName = "Hello";
            this.mWindowManager = windowManager;
            this.InnerViewModel = innerViewModel;

            // 下面设置该类中的属性更改通知在UI线程中执行
            //this.PropertyChangedDispatcher = Execute.OnUIThread;

            //// 强绑定属性的更改通知
            //var binding = this.Bind(x => x.Name, (o, s) => Debug.WriteLine($"the {s.PropertyName} property of {o} is changed, new value is {s.NewValue}"));
            //// 删除绑定
            //binding.Unbind();

            //// 若绑定属性的更改通知
            //this.PropertyChanged += (o, e) =>
            //{
            //	if (!string.IsNullOrWhiteSpace(e.PropertyName) && e.PropertyName.Equals(nameof(Name)))
            //	{
            //		Debug.WriteLine($"the {e.PropertyName} property of {o} is changed");
            //	}
            //};
        }

        #endregion

        #region Methods

        public void BindedToCommandWithoutParams()
        {
            this.mWindowManager.ShowMessageBox($"Hello, without params");
        }

        public void BindedToCommandWithParams(string name)
        {
            this.mWindowManager.ShowMessageBox($"Hello, the params value is {name}");
        }

        public void BindedToEventWithoutParams()
        {
            this.mWindowManager.ShowMessageBox($"Hello, without params");
        }

        public void BindedToEventWithParams(RoutedEventArgs args)
        {
            this.mWindowManager.ShowMessageBox("Hello, the event params is passed");
        }

        #endregion
    }
}
