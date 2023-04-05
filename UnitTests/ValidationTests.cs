using DiceToBip39;
using LanguageExt.UnitTesting;

namespace UnitTests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData(new object[] {new[] {"123456"}})]
        public void SeedParameterSupplied_Validates_SuppliedParameter(string[] input) =>
            input.ValidateArgs().ShouldBeSuccess();

        [Theory]
        [InlineData(new object[] {new string[] { }})]
        public void ValidateArgs_Rejects_NoParameters(string[] input) =>
            input.ValidateArgs().ShouldBeFail();

        [Theory]
        [InlineData(new object[] {
            new[]
                {"1234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234"}
        })]
        [InlineData(new object[] {
            new[] {
                "12345612345612345612345612345612345612345612345612345612345612345612345612345612345612345612345612341111111"
            }
        })]
        [InlineData(new object[] {new[] {""}})]
        public void ValidateArgs_Succeeds_OnCorrectParameters(string[] input) => 
            input.ValidateArgs().ShouldBeSuccess();

        [Theory]
        [InlineData(new object[] {new string[] { }})]
        public void ValidateArgs_Fails_OnIncorrectParameters(string[] input) => 
            input.ValidateArgs().ShouldBeFail();
    }
}