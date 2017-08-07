using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Shared.I18n.Utils
{
    /// <summary>
    /// Gets the localized string value from the resources.
    /// </summary>
    public class ResourceUtils
    {
        /// <summary>
        /// Gets the localized string value
        /// </summary>
        /// <param name="key">The key of the localized string</param>
        /// <returns>The localized string value</returns>
        public static string GetString(string key, params object[] args)
        {
            ResourceManager resourceManager = new ResourceManager("Shared.I18n.Resources.Resource", Assembly.GetExecutingAssembly());
            string internationalizedString = resourceManager.GetString(key);
            if (internationalizedString == null || string.Empty.Equals(internationalizedString))
            {
                return key;
            }
            return string.Format(internationalizedString, args);
        }
    }
}
