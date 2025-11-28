namespace _03_Imports.Normal
{
	public class Test2
	{
		public IGreeting Greeting { get; set; }
		public IPerson Person { get; set; }

		public void Run()
		{
			Greeting.Greet(Person.Name = "anyu");
		}
	}
}
