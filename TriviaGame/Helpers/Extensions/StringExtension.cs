using System.Net;

namespace TriviaGame.Helpers.Extensions;

public static class StringExtension
{
    public static string DecodeHtml(this string value)
    {
        return WebUtility.HtmlDecode(value);
    }
}
