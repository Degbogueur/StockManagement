using System.Globalization;
using System.Text;

namespace StockManagement.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Normalizes a string for consistent comparisons:
    /// - Trims leading/trailing spaces
    /// - Converts to lowercase (culture-invariant)
    /// - Removes diacritics (accents)
    /// - Collapses multiple spaces into one
    /// </summary>
    public static string ToNormalize(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Step 1: Trim and uppercase (invariant)
        string normalized = input.Trim().ToUpperInvariant();

        // Step 2: Remove diacritics (accents)
        normalized = RemoveDiacritics(normalized);

        // Step 3: Collapse multiple spaces into a single space
        normalized = string.Join(" ", normalized.Split(new[] { ' ' },
                                    StringSplitOptions.RemoveEmptyEntries));

        return normalized;
    }

    private static string RemoveDiacritics(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        var normalizedForm = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var ch in normalizedForm)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(ch);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(ch);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}
