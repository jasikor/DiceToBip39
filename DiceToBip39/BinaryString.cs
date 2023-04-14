using System.Numerics;
using System.Text;


namespace DiceToBip39;

public class BinaryString
{
    private readonly string _value;

    private BinaryString(string value) => _value = value;

    private const int NoOfBits = 256;

    public static BinaryString Create(BigInteger seed)
    {
        var ret = new StringBuilder();
        for (int i = NoOfBits; i > 0; i--) {
            ret.Insert(0, (seed % 2).ToString());
            seed /= 2;
        }

        return new BinaryString(ret.ToString());
    }

    static readonly int[] _goodEntropies = new[] {128, 160, 192, 224, 256};

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