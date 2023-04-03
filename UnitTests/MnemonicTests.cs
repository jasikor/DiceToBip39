using FluentAssertions;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnitTesting;
using static DiceToBip39.ProgramExt;

namespace UnitTests;

public class MnemonicTests
{
    
    [Theory]
    [InlineData("123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456", 
        "defy trip fatal jaguar mean rack rifle survey satisfy drift twist champion steel wife state furnace night consider glove olympic oblige donor novel left")]

    [InlineData("1234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234", 
        "defy trip fatal jaguar mean rack rifle survey satisfy drift twist champion steel wife state furnace night consider glove olympic oblige donor novel left")]
    public void DiceToMnemonic_Works(string dice, string expected)
    {
        var sut = DiceToMnemonic(dice);
        string.Join(" ", sut.Words).Should().Be(expected);
    }
}