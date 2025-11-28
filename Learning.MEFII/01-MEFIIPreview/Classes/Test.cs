using _01_MEFIIPreview.Interfaces;
using System;

namespace _01_MEFIIPreview.Classes
{
	public class Test : ITest
	{
		private IRepository<Person> m_PersonRepository;

		public Test(IRepository<Person> _person)
		{
			this.m_PersonRepository = _person;
		}

		public void Run()
		{
			Person p = m_PersonRepository.GetPerson(new Person() { Name = "lff" });
			Console.WriteLine(string.Format("init {0}, the test is successful", p.Name));
		}
	}
}
