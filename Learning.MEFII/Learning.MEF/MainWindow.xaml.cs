using Learning.MEFI.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace Learning.MEFI
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		[Import(typeof(ICalculator))]
		ICalculator Calculator { get; set; }

		private CompositionContainer container;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Compose();

			if (!string.IsNullOrEmpty(input.Text))
			{
				MessageBox.Show(Calculator.Calculate(input.Text));
			}
		}

		private void Compose()
		{
			AggregateCatalog catalogs = new AggregateCatalog();
			catalogs.Catalogs.Add(new AssemblyCatalog(typeof(MainWindow).Assembly));
			catalogs.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));

			container = new CompositionContainer(catalogs);
			container.ComposeParts(this);
		}
	}
}
