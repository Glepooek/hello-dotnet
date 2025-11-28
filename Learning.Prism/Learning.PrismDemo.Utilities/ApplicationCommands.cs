using Prism.Commands;

namespace Learning.PrismDemo.Utilities
{
	public interface IApplicationCommands
	{
		CompositeCommand ExitCommand { get; }
	}

	public class ApplicationCommands : IApplicationCommands
	{
		public CompositeCommand ExitCommand { get; } = new CompositeCommand();
	}
}
