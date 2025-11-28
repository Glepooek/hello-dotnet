using System.ComponentModel.Composition;
using System.Configuration;

namespace _02_PropertyExports
{
	class Configuration
	{
		[Export("Timeout")]
		public int Timeout
		{
			get
			{
				return int.Parse(ConfigurationManager.AppSettings["timeout"]);
			}
		}
	}
}
