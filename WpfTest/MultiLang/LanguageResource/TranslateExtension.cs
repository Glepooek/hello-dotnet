using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace LanguageResource
{
    /// <summary>
    /// Represents a localization markup extension.
    /// </summary>
    public class TranslateExtension : MarkupExtension
    {
        /// <summary>
        /// Resource key
        /// </summary>
        [ConstructorArgument("key")]
        public string Key { get; set; }

        public TranslateExtension(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Returns an object that should be set on the property where this binding and extension are applied.
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var binder = new Binding("Value")
            {
                Source = new TranslationProvider(Key)
            };
            return binder.ProvideValue(serviceProvider);
        }
    }

    /// <summary>
    /// <para>The source object for the property that uses the TranslateExtension markup extension.</para>
    /// Acts as a listener to a broadcasted weak event for language change.
    /// </summary>
    class TranslationProvider : IWeakEventListener, INotifyPropertyChanged
    {
        string _key;

        /// <summary>
        /// Returns the value of the specified resource.
        /// </summary>
        public object Value => LanguageManager.Resource.GetObject(_key);

        public TranslationProvider(string key)
        {
            _key = key;
            LanguageChangedEventManager.AddListener(LanguageChangedEventPublisher.Instance, this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(LanguageChangedEventManager))
            {
                if (PropertyChanged != null)
                {
                    Console.WriteLine($"---{_key}---");
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                    return true;
                }
            }
            return false;
        }
    }
}
