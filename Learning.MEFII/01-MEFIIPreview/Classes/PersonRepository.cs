using _01_MEFIIPreview.Interfaces;

namespace _01_MEFIIPreview.Classes
{
	public class PersonRepository<T> : IRepository<T> where T : class
	{
		public T GetPerson(T person)
		{
			return person;
		}
	}
}
