using System;
using System.ComponentModel;
using System.Windows;

namespace Multithreading
{
	public partial class BackgroundWorkerTest : System.Windows.Window
	{
		public BackgroundWorkerTest()
		{
			InitializeComponent();

			backgroundWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));
		}

		private BackgroundWorker backgroundWorker;

		private void cmdFind_Click(object sender, RoutedEventArgs e)
		{
			// Disable the button and clear previous results.
			cmdFind.IsEnabled = false;
			cmdCancel.IsEnabled = true;
			lstPrimes.Items.Clear();

			// Get the search range.
			int from, to;
			if (!Int32.TryParse(txtFrom.Text, out from))
			{
				MessageBox.Show("Invalid From value.");
				return;
			}
			if (!Int32.TryParse(txtTo.Text, out to))
			{
				MessageBox.Show("Invalid To value.");
				return;
			}

			// Start the search for primes on another thread.
			FindPrimesInput input = new FindPrimesInput(from, to);
			backgroundWorker.RunWorkerAsync(input);
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			// Get the input values.
			FindPrimesInput input = (FindPrimesInput)e.Argument;

			// Start the search for primes and wait.
			int[] primes = Worker.FindPrimes(input.From, input.To, backgroundWorker);

			if (backgroundWorker.CancellationPending)
			{
				e.Cancel = true;
				return;
			}

			// Return the result.
			e.Result = primes;
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				MessageBox.Show("Search cancelled.");
			}
			else if (e.Error != null)
			{
				// An error was thrown by the DoWork event handler.
				MessageBox.Show(e.Error.Message, "An Error Occurred");
			}
			else
			{
				int[] primes = (int[])e.Result;
				foreach (int prime in primes)
				{
					lstPrimes.Items.Add(prime);
				}
			}
			cmdFind.IsEnabled = true;
			cmdCancel.IsEnabled = false;
			progressBar.Value = 0;
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
		}

		private void cmdCancel_Click(object sender, RoutedEventArgs e)
		{
			backgroundWorker.CancelAsync();
		}
	}
}