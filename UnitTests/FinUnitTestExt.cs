using LanguageExt;
using LanguageExt.Common;

namespace UnitTests;

public static class FinUnitTestExt
{
    public static void ShouldBeSuccess<TSuccess>(this Fin<TSuccess> @this,
        Action<TSuccess> successValidation = null)
        => @this.Match(successValidation ?? Noop, ThrowIfFail);

    public static void ShouldBeFail<TSuccess>(this Fin<TSuccess> @this,
        Action<Error> failValidation = null)
        => @this.Match(ThrowIfSuccess, failValidation ?? Noop);


    private static void Noop<T>(T _) { }

    private static void ThrowIfFail<T>(T _)
        => throw new Exception("Expected Success, got Fail instead.");

    private static void ThrowIfSuccess<T>(T _)
        => throw new Exception("Expected Fail, got Success instead.");
}