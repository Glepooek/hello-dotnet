using Shared.Interfaces;
using System;

namespace Shared.Impls
{
	public class TextWriter : IWriter
	{
		public void Write()
		{
			Console.WriteLine("通过实例注册组件");
		}
	}
}
