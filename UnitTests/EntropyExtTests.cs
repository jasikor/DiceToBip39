using DiceToBip39;
using FluentAssertions;
using LanguageExt.UnitTesting;
using static DiceToBip39.EntropyExt;

namespace UnitTests;

public class EntropyExtTests
{
    [Theory]
    [InlineData(100, 6, 258.49)]
    [InlineData(2, 6, 5.17)]
    void BitsOfEntropy_Calculates_Correctly(int noOfDigits, uint alphabetLength, double expected) =>
        BitsOfEntropy(noOfDigits, alphabetLength).Should().BeApproximately(expected, 0.01);

    [Theory]
    [InlineData(128.0, Entropies.Bit128)]
    [InlineData(138.0, Entropies.Bit128)]
    [InlineData(160.0, Entropies.Bit160)]
    [InlineData(170.0, Entropies.Bit160)]
    [InlineData(192.0, Entropies.Bit192)]
    [InlineData(202.0, Entropies.Bit192)]
    [InlineData(224.0, Entropies.Bit224)]
    [InlineData(230.0, Entropies.Bit224)]
    [InlineData(256.0, Entropies.Bit256)]
    [InlineData(300.0, Entropies.Bit256)]
    void Floor_Calculates_Correctly(double entropy, Entropies expected) =>
        Floor(entropy).Should().Be(expected);

    [Theory]
    [InlineData(1)]
    void Floor_ShouldThrow_OnSmallEntropy(double entropy)
    {
        Action sut = () => { Floor(entropy); };
        sut.Should().Throw<Exception>();
    }
}