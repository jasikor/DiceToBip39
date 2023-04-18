using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace DiceToBip39;

public static class ConstrainedStringValidator
{
    public static Fin<string> Validate(string s, int minLength, char first, char last) =>
        s
            .IsNotNull()
            .Bind(s => s.AtLeastLong(minLength))
            .Bind(s => s.ContainsOnly(first, last));
}

public static class ConstrainedStringValidatorExt
{
    private static Fin<string> Validate(this string s, Func<string, bool> validator, Error e) =>
        validator(s)
            ? FinSucc(s)
            : FinFail<string>(e);

    public static Fin<string> AtLeastLong(this string s, int length) =>
        s.Validate(s => s.Length >= length,
            $"Parameter is just {s.Length} characters long, but should be at least {length}");

    public static Fin<string> ContainsOnly(this string s, char first, char last) =>
        s.Validate(s =>
                s.All(c => c >= first && c <= last),
            $"Parameter has to be composed of [{first}..{last}] only");

    public static Fin<string> IsNotNull(this string s) =>
        s.Validate(s => !s.IsNull(), new ArgumentNullException());
}