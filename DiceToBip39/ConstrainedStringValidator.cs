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
    private static Validation<Error, string> Validate(this string s, Func<string, bool> validator, Error e) =>
        validator(s)
            ? Success<Error, string>(s)
            : Fail<Error, string>(e);

    public static Validation<Error, string> AtLeastLong(this string s, int length) =>
        s.Validate(s => s.Length >= length,
            $"String must be at least {length} characters long");

    public static Validation<Error, string> ContainsOnly(this string s, char first, char last) =>
        s.Validate(s =>
                s.All(c => c >= first && c <= last),
            $"String has to be composed of [{first}..{last}] only");
}