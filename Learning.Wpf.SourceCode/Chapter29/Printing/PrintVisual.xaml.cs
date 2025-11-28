using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Printing
{
	/// <summary>
	/// Interaction logic for PrintVisual.xaml
	/// </summary>

	public partial class PrintVisual : System.Windows.Window
	{

		public PrintVisual()
		{
			InitializeComponent();
		}

		private void cmdPrint_Click(object sender, RoutedEventArgs e)
		{
			//PrintDialog printDialog = new PrintDialog();
			//if (printDialog.ShowDialog() == true)
			//{
			//    printDialog.PrintVisual(canvas, "A Simple Drawing");
			//}

			PrintDialog printDialog = new PrintDialog();

			if (printDialog.ShowDialog() == true)
			{
				// Scale the TextBlock in both dimensions.
				double zoom;
				if (Double.TryParse(txtScale.Text, out zoom))
				{
					grid.Visibility = Visibility.Hidden;

					// Add a background to make it easier to see the canvas bounds.
					canvas.Background = Brushes.LightSteelBlue;

					// Resize it.
					canvas.LayoutTransform = new ScaleTransform(zoom / 100, zoom / 100);

					// Get the size of the page.
					Size pageSize = new Size(printDialog.PrintableAreaWidth - 20, printDialog.PrintableAreaHeight - 20);

					// Trigger the sizing of the element.                                    
					canvas.Measure(pageSize);
					canvas.Arrange(new Rect(10, 10, pageSize.Width, pageSize.Height));

					// Print the element.
					printDialog.PrintVisual(canvas, "A Scaled Drawing");

					// Reset the canvas.
					canvas.Background = null;
					canvas.LayoutTransform = null;
					grid.Visibility = Visibility.Visible;

				}
				else
				{
					MessageBox.Show("Invalid scale value.");
				}

			}

		}
	}
}