using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ObservableRecipientSamples.Messages;
using ObservableRecipientSamples.Models;

namespace ObservableRecipientSamples
{
    internal class MainViewModel : ObservableRecipient
    {
        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage<AppEnum>>(this, (r, m) =>
            {
                switch (m.Value)
                {
                    case AppEnum.AppStart:
                        break;
                    case AppEnum.AppLoading:
                        break;
                    case AppEnum.AppStop:
                        break;
                    default:
                        break;
                }
            });
            WeakReferenceMessenger.Default.Register<MainViewModel, LoggedInUserChangedMessage<AppEnum>>(this, (r, m) => r.Receive(m));
        }

        protected override void OnDeactivated()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }

        private void Receive(LoggedInUserChangedMessage<AppEnum> message)
        {
            // Handle the message here
            switch (message.Value)
            {
                case AppEnum.AppStart:
                    break;
                case AppEnum.AppLoading:
                    break;
                case AppEnum.AppStop:
                    break;
                default:
                    break;
            }
        }
    }
}
