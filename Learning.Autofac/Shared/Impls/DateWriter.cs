using Shared.Interfaces;
using System;

namespace Shared.Impls
{
	public class DateWriter : IWriter
	{
		private readonly ILogger m_Outputer;
		private readonly IConfigReader m_ConfigReader;

		public DateWriter(ILogger outputer)
		{
			this.m_Outputer = outputer;
		}

		public DateWriter(ILogger outputer, IConfigReader configReader)
		{
			m_Outputer = outputer;
			m_ConfigReader = configReader;
		}


		public void Write()
		{
			this.m_ConfigReader?.Read(string.Empty);
			this.m_Outputer.Log(DateTime.Today.ToShortDateString());
		}
	}
}
