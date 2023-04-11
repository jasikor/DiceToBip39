using System.Numerics;
using System.Text;
using LanguageExt;
using LanguageExt.Common;


namespace DiceToBip39;

public class BinaryString256
{
    private readonly string _value;

    private BinaryString256(string value) => _value = value;

    private const int NoOfBits = 256;

    public static BinaryString256 Create(BigInteger seed)
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