using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace _02_PropertyExports
{
	[Export]
	public class Connection
	{
		[Import("Timeout")]
		public int Timeout { get; set; }

		public Connection()
		{
			//Compose();
		}

		private void Compose()
		{
			CompositionContainer container = new CompositionContainer();
			container.ComposeParts(this, new Configuration());
		}
	}
}
