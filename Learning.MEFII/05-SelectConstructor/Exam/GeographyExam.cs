using System;

namespace _05_SelectConstructor.Exam
{
	/// <summary>
	/// 地理考试
	/// </summary>
	public class GeographyExam : Exam
	{
		public string SourceMaterial { get; set; }
		public GeographyExam()
		{
			NoOfPages = 15;
			NoOfSections = 5;
			Subject = "Geography Maps";
			TimeLimit = new TimeSpan(0, 2, 0, 0, 0);
			TotalMarks = 150;
		}
	}
}
