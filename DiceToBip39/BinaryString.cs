using System.Numerics;
using System.Text;


namespace DiceToBip39;

public enum Entropies
{
    Bit128 = 128,
    Bit160 = 160,
    Bit224 = 224,
    Bit192 = 192,
    Bit256 = 256
}

public class BinaryString
{
    private readonly string _value;

    private BinaryString(string value) => _value = value;

    public static BinaryString Create(BigInteger seed, Entropies entropyBits)
    {
        var ret = new StringBuilder();
        for (int i = (int)entropyBits; i > 0; i--) {
            ret.Insert(0, (seed % 2).ToString());
            seed /= 2;
        }

        return new BinaryString(ret.ToString());
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