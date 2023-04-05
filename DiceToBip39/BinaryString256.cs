using System.Numerics;
using System.Text;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;


namespace DiceToBip39;

public class BinaryString256
{
    private readonly string _value;

    private BinaryString256(string value)
    {
        _value = value;
    }

    public static Validation<Error, BinaryString256> Create(string s) =>
        s.Contains1Or0Only()
            .Bind(s => s.AtLeast256Long())
            .Map(s => new BinaryString256(s));

    public const int NoOfBits = 256;

    public static BinaryString256 ToBinaryString256(BigInteger seed)
    {
        var ret = new StringBuilder();
        for (int i = NoOfBits; i > 0; i--) {
            ret.Insert(0, (seed % 2).ToString());
            seed /= 2;
        }

        return new BinaryString256(ret.ToString());
    }

    public byte[] ToByteArray()
    {
        byte[] bytes = new byte[_value.Length / 8];
        for (int i = 0; i < _value.Length / 8; ++i) {
            bytes[i] = Convert.ToByte(_value.Substring(8 * i, 8), 2);
        }

        return bytes;
    }

    public override string ToString() => _value;
}

static internal class BinaryString256Validations
{
    public static Validation<Error, string> Contains1Or0Only(this string s) =>
        s.All(c => c == '1' || c == '0')
            ? Success<Error, string>(s)
            : Fail<Error, string>("DiceString has to be composed of 1s and 0s only");

    public static Validation<Error, string> AtLeast256Long(this string s) =>
        s.Length >= BinaryString256.NoOfBits
            ? Success<Error, string>(s)
            : Fail<Error, string>($"DiceString must be at least 100 characters long");
}

static class BinaryStrin256Ext { }