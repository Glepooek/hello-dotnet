using System.Windows;

namespace Test.ChangeLang
{
    public class LangResourceManager
    {
        private static ResourceDictionary currentLanguage;
        public static ResourceDictionary CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                if (currentLanguage != value)
                {
                    currentLanguage = value;
                    UpdateLanguage();
                }
            }
        }

        public static string? GetResourceString(string key)
        {
            var res = Application.Current.TryFindResource(key);
            return res != null ? res.ToString() : string.Empty;
        }

        private static void UpdateLanguage()
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(currentLanguage);
        }
    }
}
