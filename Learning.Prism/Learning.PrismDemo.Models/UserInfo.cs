using Prism.Mvvm;

namespace Learning.PrismDemo.Models
{
	public class UserInfo : BindableBase
	{
		private string m_UserName = "anyu";
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName
		{
			get
			{
				return m_UserName;
			}
			set
			{
				if (m_UserName != value)
				{
					m_UserName = value;
					RaisePropertyChanged();
				}
			}
		}

		private string m_Password = "123456";
		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			get
			{
				return m_Password;
			}
			set
			{
				if (m_Password != value)
				{
					m_Password = value;
					RaisePropertyChanged();
				}
			}
		}

		/// <summary>
		/// 所在省
		/// </summary>
		public string Province { get; set; }
		/// <summary>
		/// 所在市
		/// </summary>
		public string City { get; set; }
		/// <summary>
		/// 所在县（区）
		/// </summary>
		public string County { get; set; }
		/// <summary>
		/// 格言
		/// </summary>
		public string Motto { get; set; }
	}
}
