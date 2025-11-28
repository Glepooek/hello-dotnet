using Learning.PrismDemo.Models;
using Prism.Events;

namespace Learning.PrismDemo.Messages
{
	public class ViewStateChangedMessage : PubSubEvent<ViewState>
	{
	}
}
