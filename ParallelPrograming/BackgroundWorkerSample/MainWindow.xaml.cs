using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace BackgroundWorkerSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The backgroundworker object on which the time consuming operation shall be executed
        /// </summary>
        BackgroundWorker m_Worker;

        public MainWindow()
        {
            InitializeComponent();

            m_Worker = new BackgroundWorker();
            m_Worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            m_Worker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            m_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
            m_Worker.WorkerReportsProgress = true;
            m_Worker.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// On completed do the appropriate task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //If it was cancelled midway
            if (e.Cancelled)
            {
                txtStatus.Text = "Task Cancelled.";
            }
            else if (e.Error != null)
            {
                txtStatus.Text = "Error while performing background operation.";
            }
            else
            {
                txtStatus.Text = "Task Completed...";
            }
            btnStartAsyncOperation.IsEnabled = true;
            btnCancel.IsEnabled = false;
        }

        /// <summary>
        /// Notification is performed here to the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Here you play with the main UI thread
            progressBar.Value = e.ProgressPercentage;
            txtStatus.Text = "Processing......" + progressBar.Value.ToString() + "%";
        }

        /// <summary>
        /// Time consuming operations go here </br>
        /// i.e. Database operations,Reporting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //NOTE : Never play with the UI thread here...

            //time consuming operation
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                m_Worker.ReportProgress(i);

                //If cancel button was pressed while the execution is in progress
                //Change the state from cancellation ---> cancel'ed
                if (m_Worker.CancellationPending)
                {
                    e.Cancel = true;
                    m_Worker.ReportProgress(0);
                    return;
                }
            }

            //Report 100% completion on operation completed
            m_Worker.ReportProgress(100);
        }

        private void btnStartAsyncOperation_Click(object sender, EventArgs e)
        {
            btnStartAsyncOperation.IsEnabled = false;
            btnCancel.IsEnabled = true;
            //Start the async operation here
            m_Worker.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (m_Worker.IsBusy)
            {
                //Stop/Cancel the async operation here
                m_Worker.CancelAsync();
            }
        }
    }
}
