using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace DiceToBip39;

public static class ConstrainedStringValidator
{
    public static Validation<Error, string> Validate(string s, int length, char first, char last) =>
        s
            .AtLeastLong(length)
            .Bind(s => s.ContainsOnly(first, last));
}

public static class StringFactoryExt
{
    public static Validation<Error, string> AtLeastLong(this string s, int length) =>
        s.Length >= length
            ? Success<Error, string>(s)
            : Fail<Error, string>($"String must be at least {length} characters long");

    public static Validation<Error, string> ContainsOnly(this string s, char first, char last) =>
        s.All(c => c >= first && c <= last)
            ? Success<Error, string>(s)
            : Fail<Error, string>($"String has to be composed of [{first}..{last}] only");
}