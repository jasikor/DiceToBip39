using System.Numerics;
using DiceToBip39;
using FluentAssertions;
using LanguageExt.UnitTesting;


namespace UnitTests;

public class DiceStringTests
{
    [Theory]
    [InlineData("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111123456",
        1865)]
    [InlineData("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111113",
        2)]
    [InlineData(
        "1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111113666666",
        2)]
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

    [Theory]
    [InlineData(50, 129)]
    [InlineData(55, 129)]
    [InlineData(62, 160)]
    [InlineData(69, 160)]
    [InlineData(75, 193)]
    [InlineData(80, 193)]
    [InlineData(87, 224)]
    [InlineData(100, 258)]
    [InlineData(110, 258)]
    public void Entropy_Calculates_Correctly(int diceLength, int expected) =>
        DiceString.Create(new string('1', diceLength))
            .ShouldBeSuccess(ds => ds.BitsOfEntropy().Should().Be(expected));
    
}