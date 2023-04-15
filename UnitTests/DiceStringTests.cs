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
    [InlineData(50, Entropies.Bit128)]
    [InlineData(55, Entropies.Bit128)]
    [InlineData(62, Entropies.Bit160)]
    [InlineData(69, Entropies.Bit160)]
    [InlineData(75, Entropies.Bit192)]
    [InlineData(80, Entropies.Bit192)]
    [InlineData(87, Entropies.Bit224)]
    [InlineData(89, Entropies.Bit224)]
    [InlineData(100, Entropies.Bit256)]
    [InlineData(110, Entropies.Bit256)]
    public void CreatingDiceString_TrimsLength_Correctly(int diceLength, Entropies expected) =>
        DiceString.Create(new string('1', diceLength))
            .ShouldBeSuccess(ds => ds.Entropy().Should().Be(expected));
    
}