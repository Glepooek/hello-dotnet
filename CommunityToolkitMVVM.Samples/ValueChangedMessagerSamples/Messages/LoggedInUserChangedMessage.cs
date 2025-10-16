using MessagerSamples.Models;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace MessagerSamples.Messages
{
    public class LoggedInUserChangedMessage : ValueChangedMessage<User>
    {
        public LoggedInUserChangedMessage(User value) : base(value)
        {
        }
    }
}
