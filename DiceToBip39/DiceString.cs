using System.Numerics;
using LanguageExt;
using LanguageExt.Common;


namespace DiceToBip39;

public class DiceString
{
    private readonly string _value;

    private DiceString(string value) => _value = value;

    private const int NoOfDigits = 100;

    public static Fin<DiceString> Create(string s) =>
        ConstrainedStringValidator.Validate(s, NoOfDigits, '1', '6')
            .Map(st => new DiceString(st.Substring(0, NoOfDigits)));

    public BigInteger ToBigInteger() =>
        _value
            .Fold(BigInteger.Zero, (acc, ch) => {
                acc *= 6;
                acc += (uint)(ch - '0') - 1;
                return acc;
            });

}