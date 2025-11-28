using System;

namespace _05_SelectConstructor.Exam
{
	/// <summary>
	/// 历史考试
	/// </summary>
	public class HistoryExam : Exam
	{
		public string SourceMaterial { get; set; }
		public HistoryExam()
		{
			NoOfPages = 5;
			NoOfSections = 3;
			Subject = "African History";
			TimeLimit = new TimeSpan(0, 2, 30, 0, 0);
			TotalMarks = 200;
		}
	}
}
