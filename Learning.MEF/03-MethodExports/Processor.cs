using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace _03_MethodExports
{
	[Export]
	class Processor
	{
		[Import(typeof(Action<string>))]
		//[Import("MessageSender")]
		public Action<string> MessageSender { get; set; }

		public Processor()
		{
			//Compose();
		}

		private void Compose()
		{
			CompositionContainer container = new CompositionContainer();
			container.ComposeParts(this, new MessageSender());
		}
	}
}
