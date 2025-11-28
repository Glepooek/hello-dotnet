using System.Collections.Generic;

namespace _04_ObjLifetime.DataAccess
{
	public interface IMockDbContext
	{
		List<IPerson> MockPersonDbSet { get; set; }
	}
}
