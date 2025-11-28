namespace Learning.PrismDemo.Services
{
	public class Service : IService
	{
		public bool LoginIn(string userName, string password, string serviceAddress)
		{
			if (string.IsNullOrWhiteSpace(userName)
				|| string.IsNullOrWhiteSpace(password)
				|| string.IsNullOrWhiteSpace(serviceAddress))
			{
				return false;
			}

			if (userName.Equals("anyu") && password.Equals("123456"))
			{
				return true;
			}

			return false;
		}
	}
}
