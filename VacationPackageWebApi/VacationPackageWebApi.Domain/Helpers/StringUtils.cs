namespace VacationPackageWebApi.Domain.Helpers;

public static class StringUtils
{
    public static string MaskStringShowingCharacters(this string input, int charactersToShow)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        if (charactersToShow <= input.Length)
        {
            return string.Concat(GenerateNewMaskedString(input.Length - charactersToShow),
                input.AsSpan(input.Length - charactersToShow, charactersToShow));
        }

        return input;

    }

    public static string MaskEntireString(this string input)
    {
        return !string.IsNullOrEmpty(input) ? GenerateNewMaskedString(input.Length) : string.Empty;
    }

    private static string GenerateNewMaskedString(int length)
    {
        return new string('*', length);
    }
}
