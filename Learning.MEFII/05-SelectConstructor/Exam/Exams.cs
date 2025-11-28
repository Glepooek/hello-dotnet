using System;

namespace _05_SelectConstructor.Exam
{
	/// <summary>
	/// 考试
	/// </summary>
	public abstract class Exam
	{
		public string Name { get; set; }
		public bool IsPractical { get; set; }
		public int TotalMarks { get; set; }
		public int NoOfSections { get; set; }
		public int NoOfPages { get; set; }
		public string Subject { get; set; }
		public TimeSpan TimeLimit { get; set; }
	}
}
