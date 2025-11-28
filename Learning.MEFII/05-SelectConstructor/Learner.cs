using _05_SelectConstructor.Teaching;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _05_SelectConstructor
{
	// Lazy延迟实例化对象，直到该对象被使用
	public class Learner
	{
		private Teacher teacher;

		public string Name { get; set; }
		public string LastName { get; set; }
		public IEnumerable<Lazy<Exam.Exam>> Exams { get; set; }
		public Educator Teacher { get; set; }

		public Learner(IEnumerable<Lazy<Exam.Exam>> exams)
		{
			Exams = exams;
			Name = "Jack";
			LastName = "Sparrow";
		}

		public Learner(IEnumerable<Educator> teacher)
		{
			Teacher = teacher.OfType<Teacher>().First();
		}

		//[ImportingConstructor] MEF1
		public Learner(IEnumerable<Educator> teacher, IEnumerable<Lazy<Exam.Exam>> exams)
			: this(exams)
		{
			Teacher = teacher.OfType<Teacher>().First();
		}

		public Learner(Teacher teacher)
		{
			// TODO: Complete member initialization
			this.teacher = teacher;
		}
	}
}
