using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnitTesting;
using static DiceToBip39.ProgramExt;


namespace UnitTests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData("123456")]
        [InlineData("3")]
        public void SeedContainsDiceRolls_CorrectlyValidates(string input)
        {
            var sut = input.SeedContainsDiceRolls();
            sut.ShouldBeSuccess();
        }

        [Theory]
        [InlineData("0")]
        [InlineData("a")]
        [InlineData("11111a11111")]
        [InlineData("1234560123456")]
        public void SeedContainsDiceRolls_CorrectlyRejects(string input)
        {
            Validation<Error, string> sut = input.SeedContainsDiceRolls();
            sut.ShouldBeFail();
        }

        [Theory]
        [InlineData(
            "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        [InlineData(
            "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")]
        public void SeedIsAtLeast100Long_CorrectlyValidates_LongStrings(string input)
        {
            var sut = input.SeedIsAtLeast100Long();
            sut.ShouldBeSuccess();
        }

        [Theory]
        [InlineData(new object[] {new string[] {"123456"}})]
        public void SeedParameterSupplied_Validates_SuppliedParameter(string[] input)
        {
            Validation<Error, string> sut = input.SeedParameterSupplied();
            sut.ShouldBeSuccess();
        }

        [Theory]
        [InlineData(new object[] {new string[] { }})]
        public void SeedParameterSupplied_Rejects_NoParameters(string[] input)
        {
            Validation<Error, string> sut = input.SeedParameterSupplied();
            sut.ShouldBeFail();
        }

        [Theory]
        [InlineData(new object[] {
            new string[]
                {"1234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234"}
        })]
        [InlineData(new object[] {
            new string[] {
                "12345612345612345612345612345612345612345612345612345612345612345612345612345612345612345612345612341111111"
            }
        })]
        public void ValidateArgs_Succeeds_OnCorrectParameters(string[] input)
        {
            Validation<Error, string> sut = input.ValidateArgs();
            sut.ShouldBeSuccess();
        }

        [Theory]
        [InlineData(new object[] {new string[] {"12345612345612345612345612345612345612345612"}})]
        [InlineData(new object[] {
            new string[] {
                "aaa1234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234"
            }
        })]
        [InlineData(new object[] {new string[] { }})]
        [InlineData(new object[] {new string[] {""}})]
        public void ValidateArgs_Fails_OnIncorrectParameters(string[] input)
        {
            Validation<Error, string> sut = input.ValidateArgs();
            sut.ShouldBeFail();
        }
    }
}