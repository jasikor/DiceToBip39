using DiceToBip39;
using FluentAssertions;
using static DiceToBip39.ProgramExt;

namespace UnitTests;

public class ParseTests
{
    [Theory]
    [InlineData("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111123456", "1865")]
    [InlineData("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111113", "2")]
    public void DiceToBigInteger_Works(string dice, string expected)
    {
        DiceString.Create(dice)
            .Map(d => d.DiceToBigInteger())
            .Map(bi => bi.ToString())
            .Map(s => s.Should().Be(expected))
            .IfFail(e => Assert.Fail("should never get here. If id does, InlineData is incorrect"));
    }
}