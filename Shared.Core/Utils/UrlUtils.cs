using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Core.Utils
{
    public static class UrlUtils
    {
        /// <summary>
        /// Transform the url to the SEO.
        /// </summary>
        /// <param name="url">The url to the transform</param>
        /// <returns>The transformed SEO url</returns>
        public static string ToSeoUrl(this string url)
        {
            string encodedUrl = url.ToLower();
            encodedUrl = RemoveDiacritic(encodedUrl);
            encodedUrl = Regex.Replace(encodedUrl, @"[;,!?:.'+&]", "");
            encodedUrl = Regex.Replace(encodedUrl, @"\s+", "-");
            encodedUrl = encodedUrl.Trim();
            return encodedUrl;
        }

        private static string RemoveDiacritic(string url)
        {
            string normalizedString = url.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
