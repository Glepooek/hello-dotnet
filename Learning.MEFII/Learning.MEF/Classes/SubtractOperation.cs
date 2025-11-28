using Learning.MEFI.Interfaces;
using System.ComponentModel.Composition;

namespace Learning.MEFI.Classes
{
	[Export(typeof(IOperation))]
	[ExportMetadata("Symbol", '-')]
	public class SubtractOperation : IOperation
	{
		public int Operate(int left, int right)
		{
			return left - right;
		}
	}
}
