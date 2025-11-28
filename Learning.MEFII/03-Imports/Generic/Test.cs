namespace _03_Imports.Generic
{
	public class Test : ITest
	{
		private IRepository<Person> repository;

		public Test()
		{
		}

		public Test(IRepository<Person> repository)
		{
			this.repository = repository;
		}

		public void Run()
		{
			Person p = repository.GetPerson(new Person() { Description = "init from test's person" });
			System.Console.WriteLine(string.Format("{0},the test is successful", p.Description));
		}
	}
}
