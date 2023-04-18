using DiceToBip39;

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
    }
}