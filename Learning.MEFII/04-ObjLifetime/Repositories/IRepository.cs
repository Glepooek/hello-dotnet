using System;

namespace _04_ObjLifetime.Repositories
{
	public interface IRepository<TEntity>
		where TEntity : class
	{
		TEntity GetByAge(Func<IPerson, bool> filter);
	}
}
