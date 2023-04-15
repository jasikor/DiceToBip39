using System.Numerics;
using DiceToBip39;
using FluentAssertions;

namespace UnitTests;

public class BinaryStringsTests
{
    private const string Zeros256Minus16 =
        "0000000000000000000000000000000000000000000000000000000000000000" +
        "0000000000000000000000000000000000000000000000000000000000000000" +
        "0000000000000000000000000000000000000000000000000000000000000000" +
        "000000000000000000000000000000000000000000000000";

    private const string Zeros256Minus8 = Zeros256Minus16 + "00000000";

    [Theory]
    [InlineData(0, Entropies.Bit256,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})]
    [InlineData(1, Entropies.Bit256,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1})]
    [InlineData(2, Entropies.Bit256,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2})]
    [InlineData(512, Entropies.Bit256,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0})]
    [InlineData(0b1111_1111_1111,  Entropies.Bit256,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 255})]
    [InlineData(0, Entropies.Bit224,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})]
    [InlineData(0, Entropies.Bit192,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})]
    [InlineData(0, Entropies.Bit160,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})]
    [InlineData(0, Entropies.Bit128,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})]

    public void BinaryStringToBytes_Works(BigInteger binary, Entropies entropy, byte[] expected) =>
        BinaryString.Create(binary, entropy).ToByteArray()
            .Should().BeEquivalentTo(expected);

    [Theory]
    [InlineData(255, Zeros256Minus8 + "11111111")]
    [InlineData(254, Zeros256Minus8 + "11111110")]
    [InlineData(0, Zeros256Minus8 + "00000000")]
    [InlineData(8, Zeros256Minus8 + "00001000")]
    public void ToBinaryString_Works(BigInteger seed, string expected)
    {
        BinaryString.Create(seed, Entropies.Bit256).ToString().Should().Be(expected);
    }
}