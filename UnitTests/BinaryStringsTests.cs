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
    [InlineData(Zeros256Minus8 + "00000000",
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})]
    [InlineData(Zeros256Minus8 + "00000001",
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1})]
    [InlineData(Zeros256Minus8 + "00000010",
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2})]
    [InlineData(Zeros256Minus16 + "0000001000000000",
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0})]
    [InlineData(Zeros256Minus16 + "0000001011111111",
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 255})]
    [InlineData(Zeros256Minus16 + "00000010111111110",
        new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 255})]
    public void BinaryStringToBytes_Works(string binary, byte[] expected) =>
        BinaryString256.Create(binary)
            .Map(bs => bs.ToByteArray())
            .Map(bytes => bytes.Should().BeEquivalentTo(expected))
            .IfFail(e => Assert.Fail("should never get here. If id does, InlineData is incorrect"));


    [Theory]
    [InlineData("255", Zeros256Minus8 + "11111111")]
    [InlineData("254", Zeros256Minus8 + "11111110")]
    [InlineData("0", Zeros256Minus8 + "00000000")]
    [InlineData("8", Zeros256Minus8 + "00001000")]
    public void ToBinaryString_Works(string seed, string expected) =>
        BinaryString256.ToBinaryString256(BigInteger.Parse(seed)).ToString().Should().Be(expected);


}