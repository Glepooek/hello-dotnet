using Learning.MEFI.Interfaces;
using System.ComponentModel.Composition;

namespace ExtendedOperations
{
	[Export(typeof(IOperation))]
	[ExportMetadata("Symbol", '%')]
	public class ModOperation : IOperation
	{
		public int Operate(int left, int right)
		{
			return left % right;
		}
	}
}
