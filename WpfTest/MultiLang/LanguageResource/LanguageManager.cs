using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace LanguageResource
{
    /// <summary>
    /// Provides resource manager and culture info.
    /// </summary>
    class LanguageManager
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager("LanguageResource.Properties.Resources", Assembly.GetExecutingAssembly());
        private static readonly CultureInfo[] _cultures = new CultureInfo[]
            {
                new CultureInfo("zh"),
                new CultureInfo("en"),
                new CultureInfo("ms")
            };

        /// <summary>
        /// Provides the resource manager to use to access the resources.
        /// </summary>
        public static ResourceManager Resource => _resourceManager;

        /// <summary>
        /// Provides the available culture info.
        /// </summary>
        public static ICollection<CultureInfo> Languages => _cultures;
    }
}
