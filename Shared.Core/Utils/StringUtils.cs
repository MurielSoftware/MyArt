using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Utils
{
    /// <summary>
    /// Utils class for working with string.
    /// </summary>
    public class StringUtils
    {
        private static Dictionary<string, string> EMOTICONS = new Dictionary<string, string>()
        {
            {":-)", "/Content/images/emoticons/emotion_smile.png"},
            {":)", "/Content/images/emoticons/emotion_smile.png"},
            {":-(", "/Content/images/emoticons/emotion_unhappy.png"},
            {":(", "/Content/images/emoticons/emotion_unhappy.png"},
            {":-D", "/Content/images/emoticons/emotion_grin.png"},
            {":D", "/Content/images/emoticons/emotion_grin.png"},
            {":-o", "/Content/images/emoticons/emotion_tire.png"},
            {":o", "/Content/images/emoticons/emotion_tire.png"},
            {":-*", "/Content/images/emoticons/emotion_kiss.png"},
            {":*", "/Content/images/emoticons/emotion_kiss.png"},
            {"&lt;3", "/Content/images/emoticons/heart.png"},
            {"<3", "/Content/images/emoticons/heart.png"}
        };

        /// <summary>
        /// Separates the list of the string with the specific separator and removes the last one.
        /// </summary>
        /// <param name="input">The list of the string values</param>
        /// <param name="separator">The separator which will be used for separating</param>
        /// <returns>Returns the separated string</returns>
        public static string SeparateString(IList<string> input, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string value in input)
            {
                sb.Append(value).Append(separator);
            }
            return RemoveLastCharacters(sb.ToString(), separator.Length);
        }

        /// <summary>
        /// Separates the list of the referenced string by the specific separator and allows it to use the tooltip which will be loaded remotely. 
        /// </summary>
        /// <param name="referencies">The dictionary of the referencies</param>
        /// <param name="action">The action to load the tooltip remotely</param>
        /// <param name="separator">The separator to separate the strings</param>
        /// <returns>Returns the string with the remotely loaded tooltips</returns>
        public static string SeparateStringWithTooltips(Dictionary<Guid, string> referencies, string action, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<Guid, string> reference in referencies)
            {
                sb.Append(string.Format("<abbr title='' data-action='{0}' data-id='{1}'>{2}</abbr>", action, reference.Key, reference.Value)).Append(separator);
            }
            return RemoveLastCharacters(sb.ToString(), separator.Length);
        }

        /// <summary>
        /// Removes the last n characters from the end of the string.
        /// </summary>
        /// <param name="input">The input string value</param>
        /// <param name="numberOfCharactersToRemove">The count of the characters to be removed</param>
        /// <returns>String without the last n characters</returns>
        private static string RemoveLastCharacters(string input, int numberOfCharactersToRemove)
        {
            if (input.Length > numberOfCharactersToRemove)
            {
                return input.Substring(0, input.Length - numberOfCharactersToRemove);
            }
            return input;
        }

        /// <summary>
        /// Extends the string by the emoticons.
        /// </summary>
        /// <param name="text">The input text</param>
        /// <returns>Returns the string where the specific strings are replaced by the emoticons</returns>
        public static string ExtendByEmoticons(string text)
        {
            foreach (string key in EMOTICONS.Keys)
            {
                text = text.Replace(key, string.Format("<img src=\"{0}\" alt=\"\" />", EMOTICONS[key]));
            }
            return text;
        }

        /// <summary>
        /// Parses the time from string to DateTime where date is now.
        /// </summary>
        /// <param name="time">Time in string format HH : mm</param>
        /// <returns>The DateTime with parsed time</returns>
        public static DateTime? ParseTime(string time)
        {
            if(string.IsNullOrEmpty(time))
            {
                return null;
            }
            string[] temp = time.Trim(' ').Split(':');
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(temp[0]), int.Parse(temp[1]), 0);
        }

        /// <summary>
        /// Parses the time from DateTime to time string.
        /// </summary>
        /// <param name="time">The appropriate DateTime</param>
        /// <returns>Time in string format HH : mm</returns>
        public static string ParseTime(DateTime time)
        {
            return time.ToShortTimeString();
        }

        public static List<string> GetAlphabet()
        {
            List<string> alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => new string((char)i, 1)).ToList();
            alphabet.Insert(8, "CH");
            return alphabet;
        }
    }
}
