using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Learning.PrismDemo.Utilities
{
	/// <summary>
	/// 执行指定的<see cref="ICommand"/>
	/// </summary>
	/// <remarks>
	/// CommandParameter与TriggerParameterPath均未设置时传入命令的是事件参数；
	/// 设置CommandParameter，不设置TriggerParameterPath时，传入命令的是CommandParameter；
	/// 不设置CommandParameter，设置TriggerParameterPath时，根据TriggerParameterPath从事件参数中查找对应属性的值，并将该值传入命令。
	/// </remarks>
	public class InvokeCommandAction : TriggerAction<DependencyObject>
	{
		/// <summary>
		/// 获取或设置此操作应调用的命令
		/// </summary>
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		/// <summary>
		/// 命令依赖项属性
		/// </summary>
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(InvokeCommandAction), new PropertyMetadata(null));

		/// <summary>
		/// 获得或设置命令参数
		/// </summary>
		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		/// <summary>
		/// 命令参数依赖项属性
		/// </summary>
		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(InvokeCommandAction), new PropertyMetadata(null));

		/// <summary>
		/// 事件参数子属性名
		/// </summary>
		public string TriggerParameterPath
		{
			get { return (string)GetValue(TriggerParameterPathProperty); }
			set { SetValue(TriggerParameterPathProperty, value); }
		}

		/// <summary>
		/// 事件参数子属性名依赖项属性
		/// </summary>
		public static readonly DependencyProperty TriggerParameterPathProperty =
			DependencyProperty.Register(nameof(TriggerParameterPath), typeof(string), typeof(InvokeCommandAction), new PropertyMetadata(string.Empty));


		protected override void Invoke(object parameter)
		{
			if (Command == null)
			{
				throw new Exception($"调用{nameof(Command)}属性为NULL");
			}

			if (CommandParameter != null)
			{
				parameter = CommandParameter;
			}

			// 根据TriggerParameterPath从事件参数中查找对应属性的值，
			if (!string.IsNullOrWhiteSpace(TriggerParameterPath))
			{
				object propertyValue = parameter;
				var propertyInfo = propertyValue.GetType().GetTypeInfo().GetProperty(TriggerParameterPath);
				propertyValue = propertyInfo.GetValue(propertyValue);

				parameter = propertyValue;
			}

			if (Command.CanExecute(parameter))
			{
				Command.Execute(parameter);
			}
		}
	}
}
