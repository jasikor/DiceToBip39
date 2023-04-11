using System.Numerics;
using DiceToBip39;
using FluentAssertions;
using LanguageExt.UnitTesting;

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
    [InlineData(0,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})]
    [InlineData(1,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1})]
    [InlineData(2,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2})]
    [InlineData(512,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0})]
    [InlineData(0b1111_1111_1111,
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 255})]
    public void BinaryStringToBytes_Works(BigInteger binary, byte[] expected) =>
        BinaryString256.Create(binary).ToByteArray()
            .Should().BeEquivalentTo(expected);

    [Theory]
    [InlineData("255", Zeros256Minus8 + "11111111")]
    [InlineData("254", Zeros256Minus8 + "11111110")]
    [InlineData("0", Zeros256Minus8 + "00000000")]
    [InlineData("8", Zeros256Minus8 + "00001000")]
    public void ToBinaryString_Works(string seed, string expected)
    {
        var ui = BigInteger.Parse(seed);
        BinaryString256.Create(ui).ToString().Should().Be(expected);
    }
}