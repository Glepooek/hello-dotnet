namespace Learning.MEFI.Interfaces
{
	/// <summary>
	/// 根据输入的操作数和操作符计算
	/// </summary>
	// [InheritedExport(typeof(ICalculator))]
	public interface ICalculator
	{
		string Calculate(string input);
	}
}
