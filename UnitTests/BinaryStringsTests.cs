using System.Numerics;
using FluentAssertions;
using static DiceToBip39.ProgramExt;

namespace UnitTests;

public class BinaryStringsTests
{
    [Theory]
    [InlineData("00000000", new byte[] {0})]
    [InlineData("00000001", new byte[] {1})]
    [InlineData("00000010", new byte[] {2})]
    [InlineData("0000001000000000", new byte[] {2,0})]
    [InlineData("0000001011111111", new byte[] {2,255})]
    [InlineData("00000010111111110", new byte[] {2,255})]
    public void BinaryStringToBytes_Works(string binary, byte[] expected)
    {
        var sut = BinaryStringToBytes(binary);
        sut.Should().BeEquivalentTo(expected);
    }
    
    
    
    
    
    [Theory]
    [InlineData("255",8, "11111111")]
    [InlineData("254",8, "11111110")]
    [InlineData("0",8,   "00000000")]
    [InlineData("8",8,   "00001000")]
    [InlineData("8",4,   "1000")]
    public void ToBinaryString_Works(string seed, int noOfBits, string expected)
    {
        var sut = ToBinaryString(BigInteger.Parse(seed), noOfBits);
        sut.Should().BeEquivalentTo(expected);
    }
    
    
    
}