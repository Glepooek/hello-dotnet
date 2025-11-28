using Learning.MEFI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Learning.MEFI.Classes
{
	[Export(typeof(ICalculator))]
	public class SimpleCalculator : ICalculator
	{
		//[ImportMany]
		IEnumerable<Lazy<IOperation, IOperator>> operations;

		[ImportingConstructor]
		public SimpleCalculator([ImportMany]IEnumerable<Lazy<IOperation, IOperator>> operation)
		{
			this.operations = operation;
		}

		public string Calculate(string input)
		{
			int left;
			int right;
			Char oper;
			int operIndex = FindFirstNonDigit(input);

			if (operIndex == -1)
			{
				return "Could not parse command.";
			}

			try
			{
				// separate the operands
				int.TryParse(input.Substring(0, operIndex), out left);
				int.TryParse(input.Substring(operIndex + 1), out right);
				oper = input[operIndex];

				foreach (Lazy<IOperation, IOperator> item in operations)
				{
					if (item.Metadata.Symbol.Equals(oper))
					{
						return item.Value.Operate(left, right).ToString();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return "Could not find the operations";
		}

		/// <summary>
		/// 返回第一个非十进制数的位置
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public int FindFirstNonDigit(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!Char.IsDigit(s[i]))
				{
					return i;
				}
			}

			return -1;
		}
	}
}
