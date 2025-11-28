namespace _01_MEFIIPreview.Interfaces
{
	public interface IRepository<T> where T : class
	{
		T GetPerson(T person);
	}
}
