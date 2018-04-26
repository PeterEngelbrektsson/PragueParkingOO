using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// Checks if a string conforms with a white list of char
        /// </summary>
        /// <param name="s">string to check</param>
        /// <param name="whiteList">list of allowed characters</param>
        /// <returns>bool true if string conforms with whitelist</returns>
        public static bool WhiteList(this string s, string whiteList)
        {
            bool foundNonConforming = false;
            foreach (char c in s)
            {
                if (whiteList.IndexOf(c) < 0)
                {
                    // found non conforming char
                    foundNonConforming = true;
                    break;
                }
            }
            return !foundNonConforming;
        }
        /// <summary>
        /// Check if a string contains any blacklisted words.
        /// </summary>
        /// <param name="s">String to check</param>
        /// <param name="blackList">List of black listed forbidden words</param>
        /// <returns>True if contains any black listed word</returns>
        public static bool BlackList(this string s, string[] blackList)
        {
            bool found = false;
            foreach (string blackWord in blackList)
            {
                if (s.IndexOf(blackWord) > -1)
                {
                    // found forbidden word
                    found = true;
                    break;
                }
            }
            return found;
        }
    }
}
