using System;
using System.Windows.Controls;

namespace LayoutPanels
{
	/// <summary>
	/// Interaction logic for SimpleInkCanvas.xaml
	/// </summary>

	public partial class SimpleInkCanvas : System.Windows.Window
	{

		public SimpleInkCanvas()
		{
			InitializeComponent();


			foreach (InkCanvasEditingMode mode in Enum.GetValues(typeof(InkCanvasEditingMode)))
			{
				lstEditingMode.Items.Add(mode);
				lstEditingMode.SelectedItem = inkCanvas.EditingMode;
			}
		}

	}
}