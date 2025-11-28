using System.ComponentModel.Composition;

namespace _04_InheritedExports
{
	[InheritedExport(typeof(ILogger))]
	interface ILogger
	{
		void Log(string message);
	}
}
