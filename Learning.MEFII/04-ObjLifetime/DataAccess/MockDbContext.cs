using System.Collections.Generic;

namespace _04_ObjLifetime.DataAccess
{
	public class MockDbContext : IMockDbContext
	{
		public List<IPerson> MockPersonDbSet { get; set; }

		public MockDbContext()
		{
			//creates mock connection to mock db
			//and populates mock debset with entries fro fake table.
			MockPersonDbSet = new List<IPerson>
			{
				new Person {Name = "Brett", Age = 21},
				new Person {Name = "Kirk", Age = 68}
			};
		}
	}
}
