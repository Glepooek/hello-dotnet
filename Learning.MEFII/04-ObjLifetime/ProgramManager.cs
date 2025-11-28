using _04_ObjLifetime.Repositories;

namespace _04_ObjLifetime
{
	public class ProgramManager
	{
		public IRepository<IPerson> PersonRepo { get; set; }

		public ProgramManager(IRepository<IPerson> personRepo)
		{
			PersonRepo = personRepo;
		}

		public void SimulateUserAction()
		{
			var person = PersonRepo.GetByAge(x => x.Age > 21);
			System.Console.WriteLine("The oldest person in the table is {0} at the tender age of {1}", person.Name, person.Age);
			System.Console.ReadKey();
		}
	}
}
