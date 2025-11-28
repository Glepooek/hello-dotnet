namespace _03_Imports.Generic
{
	public interface IRepository<T> where T : class
	{
		T GetPerson(T person);
	}
}
