using System.Numerics;
using LanguageExt;
using LanguageExt.Common;


namespace DiceToBip39;

public class DiceString
{
    private readonly string _value;

    private DiceString(string value) => _value = value;

    private const int NoOfDigits = 100;
    public static Validation<Error, DiceString> Create(string s) =>
        ConstrainedStringValidator.Validate(s, NoOfDigits, '1', '6')
            .Map(s => new DiceString(s.Substring(0, NoOfDigits)));

    public BigInteger DiceToBigInteger() =>
        _value
            .Fold(BigInteger.Zero, (acc, ch) => {
                acc *= 6;
                acc += (ch - '0') - 1;
                return acc;
            });
}