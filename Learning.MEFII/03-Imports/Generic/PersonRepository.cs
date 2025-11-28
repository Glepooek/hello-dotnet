namespace _03_Imports.Generic
{
	class PersonRepository<T> : IRepository<T> where T : class
	{
		public T GetPerson(T person)
		{
			return person;
		}
	}
}
