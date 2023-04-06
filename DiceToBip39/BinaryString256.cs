using System.Numerics;
using System.Text;
using LanguageExt;
using LanguageExt.Common;


namespace DiceToBip39;

public class BinaryString256
{
    private readonly string _value;

    private BinaryString256(string value) => _value = value;

    public const int NoOfBits = 256;

    public static Validation<Error, BinaryString256> Create(string s) =>
        ConstrainedStringValidator.Validate(s, NoOfBits, '0', '1')
            .Map(s => new BinaryString256(s.Substring(0, NoOfBits)));


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
