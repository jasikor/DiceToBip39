using static DiceToBip39.ConstrainedStringValidator;

namespace UnitTests;

public class ConstrainedStringValidationTests
{
    [Theory]
    [InlineData("1", 1, '0', '1')]
    [InlineData("", 0, '0', '1')]
    [InlineData("abc", 3, 'a', 'z')]
    [InlineData("abc", 2, 'a', 'z')]
    [InlineData("abc", 0, 'a', 'z')]
    public void Validate_Accepts_GoodData(string input, int length, char first, char last) =>
        Validate(input, length, first, last).ShouldBeSuccess();
    
    [Theory]
    [InlineData("2", 1, '0', '1')]
    [InlineData("ab", 3, 'a', 'z')]
    [InlineData(null, 3, 'a', 'z')]
    public void Validate_Rejects_BadData(string input, int length, char first, char last) =>
        Validate(input, length, first, last).ShouldBeFail();
}