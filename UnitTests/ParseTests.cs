using FluentAssertions;
using static DiceToBip39.ProgramExt;

namespace UnitTests;

public class ParseTests
{
    [Theory]
    [InlineData("123456", "1865")]
    [InlineData("3", "2")]
    public void DiceToBigInteger_Works(string dice, string expected)
    {
        var sut = DiceToBigInteger(dice);
        sut.ToString().Should().Be(expected);
    }
}