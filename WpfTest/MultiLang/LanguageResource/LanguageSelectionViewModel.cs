using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace LanguageResource
{
    /// <summary>
    /// Represents the view model for languages resource.
    /// </summary>
    public class LanguageSelectionViewModel
    {
        /// <summary>
        /// Provides the language collection.
        /// </summary>
        public ICollection<CultureInfo> Languages { get; private set; }

        public LanguageSelectionViewModel()
        {
            Languages = LanguageManager.Languages;
        }

        /// <summary>
        /// Sets a new UI culture based on the provided argument and raises the language change event.
        /// </summary>
        public bool ChangeLanguage(string languageName)
        {
            if (!string.IsNullOrEmpty(languageName))
            {
                CultureInfo culture = Languages.FirstOrDefault(x => x.NativeName == languageName);

                if (culture != null)
                {
                    // Sets new culture
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;

                    // Invokes the language change signal in the LanguageChangedEventPublisher
                    LanguageChangedEventPublisher.Instance.OnLanguageChanged();

                    return true;
                }
            }
            return false;
        }
    }
}
