namespace SocketClient
{
	public class User
	{
		public string Name { get; set; }
		public int Age { get; set; }

		public override string ToString()
		{
			return string.Format("Name is {0}, Age is {1}", Name, Age);
		}

		/// <summary>
		/// User对象
		/// </summary>
		public static User Instance
		{
			get
			{
				return SingletonProvider<User>.Instance;
			}
		}

		private User() { }
	}
}
