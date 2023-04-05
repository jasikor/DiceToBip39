using DiceToBip39;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnitTesting;


namespace UnitTests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData(new object[] {new string[] {"123456"}})]
        public void SeedParameterSupplied_Validates_SuppliedParameter(string[] input) =>
            input.SeedParameterSupplied().ShouldBeSuccess();

        [Theory]
        [InlineData(new object[] {new string[] { }})]
        public void SeedParameterSupplied_Rejects_NoParameters(string[] input) =>
            input.SeedParameterSupplied().ShouldBeFail();

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
        [InlineData(new object[] {new string[] {""}})]
        public void ValidateArgs_Succeeds_OnCorrectParameters(string[] input) => 
            input.ValidateArgs().ShouldBeSuccess();

        [Theory]
        [InlineData(new object[] {new string[] { }})]
        public void ValidateArgs_Fails_OnIncorrectParameters(string[] input) => 
            input.ValidateArgs().ShouldBeFail();
    }
}