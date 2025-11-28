using Learning.MEFI.Interfaces;
using System.ComponentModel.Composition;

namespace Learning.MEFI.Classes
{
	[Export(typeof(IOperation))]
	[ExportMetadata("Symbol", '+')]
	public class AddOperation : IOperation
	{
		public int Operate(int left, int right)
		{
			return left + right;
		}
	}
}
