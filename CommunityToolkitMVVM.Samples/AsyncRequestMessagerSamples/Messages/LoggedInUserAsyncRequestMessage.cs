using CommunityToolkit.Mvvm.Messaging.Messages;
using MessagerSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagerSamples.Messages
{
    public class LoggedInUserAsyncRequestMessage : AsyncRequestMessage<User>
    {
    }
}
