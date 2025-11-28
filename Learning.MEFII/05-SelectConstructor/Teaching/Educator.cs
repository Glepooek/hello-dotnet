using _05_SelectConstructor.Subjects;

namespace _05_SelectConstructor.Teaching
{
	/// <summary>
	/// 教育工作者
	/// </summary>
	public abstract class Educator
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public void Teach(Subject subject)
		{

		}
	}
}
