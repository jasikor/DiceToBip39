using System.Numerics;
using DiceToBip39;
using FluentAssertions;
using LanguageExt.UnitTesting;

namespace UnitTests;

public class UBigIntegerTests
{
    [Theory]
    [InlineData(-1)]
    public void UBigInteger_Create_FailsOnNegativeNumbers(BigInteger i) =>
        UBigInteger.Create(i).ShouldBeFail();

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    public void UBigInteger_Create_Succeeds(BigInteger i) =>
        UBigInteger.Create(i).ShouldBeSuccess(ui => ui.Equals(i).Should().BeTrue());

    [Theory]
    [InlineData(0, '0')]
    [InlineData(1, '1')]
    [InlineData(2, '0')]
    [InlineData(3, '1')]
    public void LastBit_Returns_LastBit(BigInteger i, char expected) =>
        UBigInteger.Create(i).ShouldBeSuccess(ui => ui.LastBit().Should().Be(expected));
    
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 2)]
    [InlineData(5, 2)]
    [InlineData(6, 3)]
    public void ShiftRight_WorksCorrectly(BigInteger i, BigInteger expected) =>
        UBigInteger.Create(i).ShouldBeSuccess(ui => ui.ShiftRight().Equals(expected).Should().BeTrue());
}