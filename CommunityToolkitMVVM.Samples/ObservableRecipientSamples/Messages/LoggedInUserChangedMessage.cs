using ObservableRecipientSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ObservableRecipientSamples.Messages
{
    public class LoggedInUserChangedMessage<T> : ValueChangedMessage<T>
    {
        public LoggedInUserChangedMessage(T value) : base(value)
        {
        }
    }
}
