using FluentAssertions;
using static DiceToBip39.ProgramExt;

namespace UnitTests;

public class CalculationTests
{
    [Theory]
    [InlineData("1",2 )]
    [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",258 )]
    [InlineData("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789",255 )]
    public void CalculateEntropy_Works(string dice,int expected)
    {
        var sut = CalculateEntropy(dice);
        sut.Should().Be(expected);
    }
}