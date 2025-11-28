using _02_MultipleInterfaces.Interfaces;

namespace _02_MultipleInterfaces.Classes
{
	public class Person : IHuman, IMammal, IHomosapien
	{
		public HairType HairType
		{
			get;
			set;
		}

		public string IdentityNo
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int LegNum
		{
			get;
			set;
		}

		public string BloodType
		{
			get;
			set;
		}

		public Person()
		{
			Name = "anyu";
			IdentityNo = "19881022";
			HairType = HairType.Hair;
			LegNum = 2;
		}

		public override string ToString()
		{
			return $"Name:{Name}, IdentityNo:{IdentityNo}, HairType:{HairType}, LegNum:{LegNum}";
		}
	}
}
