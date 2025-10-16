using ObservableRecipientSamples.Models;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservableRecipientSamples.Messages
{
    public class LoggedInUserChangedMessage<T> : ValueChangedMessage<T>
    {
        public LoggedInUserChangedMessage(T value) : base(value)
        {
        }
    }
}
