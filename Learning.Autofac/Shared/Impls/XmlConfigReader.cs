using Shared.Interfaces;
using System;

namespace Shared.Impls
{
	public class XmlConfigReader : IConfigReader
	{
		private string m_SectionName = string.Empty;
		public XmlConfigReader(string sectionName)
		{
			m_SectionName = sectionName;
		}

		public void Read(string sectionName = "")
		{
			if (!string.IsNullOrEmpty(m_SectionName))
			{
				Console.WriteLine($"read configration from xml file, sectionName: {m_SectionName}");
			}
			else
			{
				Console.WriteLine("read configration from xml file");
			}
		}
	}
}
