using System.Numerics;
using DiceToBip39;
using FluentAssertions;
using LanguageExt.UnitTesting;


namespace UnitTests;

public class DiceStringTests
{
   
    
    [Theory]
    [InlineData("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111123456", 1865)]
    [InlineData("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111113", 2)]
    [InlineData("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111113666666", 2)]
    public void DiceToBigInteger_Works_OnCorrectData(string dice, BigInteger expected) =>
        DiceString.Create(dice)
            .Map(d => d.ToBigInteger())
            .ShouldBeSuccess(bi => bi.Should().Be(expected));
    
    
    [Theory]
    [InlineData("1111111")]
    [InlineData("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000")]
    [InlineData("")]
    public void DiceToBigInteger_RejectsWrongData(string dice) =>
        DiceString.Create(dice)
            .Map(d => d.ToBigInteger())
            .ShouldBeFail();
}