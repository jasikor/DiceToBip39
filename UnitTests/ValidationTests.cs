using DiceToBip39;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnitTesting;
using static DiceToBip39.ProgramExt;


namespace UnitTests
{
    public class ValidationTests
    {
        
        
        [Theory]
        //           0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
        [InlineData("1234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234561234")]
        [InlineData("123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123412")]
        public void Create_CorrectlyCreates(string input)
        {
            var sut = DiceString.Create(input);
            sut.ShouldBeSuccess();
        }

        [Theory]
        //           0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
        [InlineData("123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456")]
        [InlineData("0123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123456123")]
        public void Create_Fails(string input)
        {
            var sut = DiceString.Create(input);
            sut.ShouldBeFail();
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
        [InlineData(new object[] {new string[] {""}})]
        public void ValidateArgs_Succeeds_OnCorrectParameters(string[] input)
        {
            Validation<Error, string> sut = input.ValidateArgs();
            sut.ShouldBeSuccess();
        }

        [Theory]
        [InlineData(new object[] {new string[] { }})]
        public void ValidateArgs_Fails_OnIncorrectParameters(string[] input)
        {
            Validation<Error, string> sut = input.ValidateArgs();
            sut.ShouldBeFail();
        }
    }
}