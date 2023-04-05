using System.Numerics;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;


namespace DiceToBip39;

public class DiceString
{
    private readonly string _value;

    private DiceString(string value)
    {
        _value = value;
    }

    public static Validation<Error, DiceString> Create(string s)
    {
        return s
            .AtLeast100Long()
            .Bind(s => s.Contains1To6Only())
            .Map(s => new DiceString(s.Substring(0,100)));
    }
    
    public BigInteger DiceToBigInteger() =>
        _value
            .Fold(BigInteger.Zero, (acc, ch) => {
                acc *= 6;
                acc += (ch - '0') - 1;
                return acc;
            });

}

static class Validations
{
    public static Validation<Error, string> AtLeast100Long(this string s) =>
        s.Length >= 100
            ? Success<Error, string>(s)
            : Fail<Error, string>($"DiceString must be at least 100 characters long");

    public static Validation<Error, string> Contains1To6Only(this string s) =>
        s.All(c => c is >= '1' and <= '6')
            ? Success<Error, string>(s)
            : Fail<Error, string>("DiceString has to be composed of [1..6] only");
}