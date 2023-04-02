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
        "forget pretty arrange tail manage join unfold call blast harsh witness toy weapon gap uncover dry almost plug ahead recipe grant cook dilemma cream")]
    public void DiceToMnemonic_Works(string dice, string expected)
    {
        var sut = DiceToMnemonic(dice);
        string.Join(" ", sut.Words).Should().Be(expected);
    }
}