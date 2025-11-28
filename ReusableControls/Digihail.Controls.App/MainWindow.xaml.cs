using Abt.Controls.SciChart;
using Abt.Controls.SciChart.Model.DataSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ReusableControls.Demos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			#region FastLineChart And  ChartOverviewWithScale

			//dataMonetorChart.Loaded += (sender, e) =>
			//{
			//	dataMonetorChart.ChartSurface.XAxis.MajorTickLineStyle = Application.Current.FindResource("TickStyle") as Style;
			//	dataMonetorChart.ChartSurface.YAxis.MajorTickLineStyle = Application.Current.FindResource("TickStyle") as Style;
			//	dataMonetorChart.ChartSurface.XAxis.MajorGridLineStyle = Application.Current.FindResource("GridLineStyle") as Style;
			//};

			//Loaded += (sender, e) =>
			//{
			//	#region test data

			//	var rnd = new Random();
			//	var FLDatas = new XyDataSeries<DateTime, double>();
			//	var xFLDatas = new List<DateTime>();
			//	var yFLDatas = new List<double>();

			//	for (int i = 0; i < 555; i++)
			//	{
			//		xFLDatas.Add(DateTime.Now.AddHours(i));
			//		yFLDatas.Add(rnd.Next(0, 50));
			//	}

			//	((XyDataSeries<DateTime, double>)FLDatas).Append(xFLDatas, yFLDatas);
			//	dataMonetorChart.Datas = FLDatas;
			//	dataMonetorChart.XAxisVisibleRange = new DateRange(xFLDatas.Last().AddHours(-72), xFLDatas.Last());

			//	#endregion
			//};

			#endregion

			#region BindableSliderScale

			//timeScaleControl.MainTickValue = 2;
			//timeScaleControl.BindingDatas = Enumerable.Range(0, 55).Select(days => DateTime.Now.AddDays(days)).ToList<DateTime>();
			//timeScaleControl.Minimum = 1;
			//timeScaleControl.Maximum = timeScaleControl.BindingDatas.Count;

			#endregion

			#region FastLineChart

			//Loaded += (sender, e) =>
			//{
			//	#region test data

			//	var rnd = new Random();
			//	var FLDatas = new XyDataSeries<DateTime, double>();
			//	var xFLDatas = new List<DateTime>();
			//	var yFLDatas = new List<double>();

			//	for (int i = 0; i < 555; i++)
			//	{
			//		xFLDatas.Add(DateTime.Now.AddMinutes(i));
			//		yFLDatas.Add(rnd.Next(0, 50));
			//	}

			//	((XyDataSeries<DateTime, double>)FLDatas).Append(xFLDatas, yFLDatas);
			//	dataMonetorChart.Datas = FLDatas;

			//	#endregion
			//};

			#endregion
		}
	}
}
