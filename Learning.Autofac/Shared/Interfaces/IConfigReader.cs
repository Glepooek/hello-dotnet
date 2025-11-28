using System.Diagnostics.CodeAnalysis;

namespace Shared.Interfaces
{
	public interface IConfigReader
	{
		void Read(string sectionName);
	}
}
