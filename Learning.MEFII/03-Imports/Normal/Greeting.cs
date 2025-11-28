namespace _03_Imports.Normal
{
	public class Greeting : IGreeting
	{
		public void Greet(string name)
		{
			System.Console.WriteLine(string.Format("Hello,{0}", name));
		}
	}
}
