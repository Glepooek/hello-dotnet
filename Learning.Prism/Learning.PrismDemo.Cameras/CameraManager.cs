using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using System.Windows.Media;

namespace Learning.PrismDemo.Cameras
{
	/// <summary>
	/// 摄像头管理类
	/// </summary>
	public class CameraManager : IDisposable
	{
		#region Singleton

		/// <summary>
		/// 线程锁所用对象
		/// </summary>
		private static readonly object m_Sync = new object();

		private static CameraManager m_Instance;
		/// <summary>
		/// 获取当前的模块管理器
		/// </summary>
		public static CameraManager Instance
		{
			get
			{
				if (m_Instance == null)
				{
					lock (m_Sync)
					{
						if (m_Instance == null)
						{
							m_Instance = new CameraManager();
						}
					}
				}
				return m_Instance;
			}
		}

		#endregion

		#region Fields

		private VideoCapture m_VideoCapture;
		private BackgroundWorker m_BackgroundWorker;
		private int m_CameraIndex = 0;

		public event Action<ImageSource> ReadCameraImage;

		#endregion

		#region Constructor

		private CameraManager()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// 初始化摄像头配置
		/// </summary>
		/// <param name="cameraIndex">摄像头索引</param>
		/// <param name="width">摄像头分辨率，宽</param>
		/// <param name="height">摄像头分辨率，高</param>
		/// <param name="uiWidth">输出到UI的宽度</param>
		/// <param name="uiHeight">输出到UI的高度</param>
		public void ConfigureCamera(int cameraIndex, double width, double height, double uiWidth, double uiHeight)
		{
			if (m_VideoCapture != null || m_BackgroundWorker != null)
			{
				return;
			}

			m_CameraIndex = cameraIndex;

			m_VideoCapture = new VideoCapture()
			{
				FrameWidth = (int)width,
				FrameHeight = (int)height
			};

			m_BackgroundWorker = new BackgroundWorker()
			{
				WorkerSupportsCancellation = true,
				WorkerReportsProgress = true
			};

			m_BackgroundWorker.DoWork += DoReadCamera;
			m_BackgroundWorker.ProgressChanged += WorkerProgressChanged;
		}

		/// <summary>
		/// 开启摄像头
		/// </summary>
		public void StartCamera()
		{
			if (IsOpened())
			{
				return;
			}

			if (m_VideoCapture != null)
			{
				m_VideoCapture.Open(m_CameraIndex, VideoCaptureAPIs.DSHOW);
				if (IsOpened())
				{
					m_BackgroundWorker.RunWorkerAsync();
				}
			}
		}

		/// <summary>
		/// 关闭摄像头
		/// </summary>
		public void StopCamera()
		{
			m_BackgroundWorker.CancelAsync();
			m_VideoCapture.Dispose();
		}

		/// <summary>
		/// 摄像头是否开启
		/// </summary>
		/// <returns></returns>
		public bool IsOpened()
		{
			if (m_VideoCapture == null)
			{
				return false;
			}

			return m_VideoCapture.IsOpened();
		}

		/// <summary>
		/// IDisposable.Dispose
		/// </summary>
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="disposing"></param>
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		protected void Dispose(bool disposing)
		{

		}

		#endregion

		#region Worker Event Handler

		/// <summary>
		/// 读取摄像头画面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		private void DoReadCamera(object sender, DoWorkEventArgs e)
		{
			using var image = new Mat();
			while (!m_BackgroundWorker.CancellationPending)
			{
				m_VideoCapture.Read(image);

				if (image.Empty())
					break;

				m_BackgroundWorker.ReportProgress(0, image);

				Thread.Sleep(100);
			}
		}

		/// <summary>
		/// 后台操作处理进度改变时触发
		/// </summary>
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.UserState is Mat m_ServerImage)
			{
				var bitmapSource = BitmapSourceConverter.ToBitmapSource(m_ServerImage);
				ReadCameraImage?.Invoke(bitmapSource);
			}
		}

		#endregion
	}
}
