using System.Text.RegularExpressions;

namespace DefaultNamespace.StaticClass
{
    public class RemoveWhitespaces
    {
        public static string RemoveWhitespacesUsingRegex(string source)
        {
            return Regex.Replace(source, @"\s", string.Empty);
        }
    }
}