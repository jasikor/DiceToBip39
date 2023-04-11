using System.Dynamic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace DiceToBip39;

public readonly struct UBigInteger : IEquatable<BigInteger>
{
    private readonly BigInteger _value;

    private UBigInteger(BigInteger value) => _value = value;

    public static Validation<Error, UBigInteger> Create(BigInteger i)
        => i >= 0
            ? Success<Error, UBigInteger>(new UBigInteger(i))
            : Fail<Error, UBigInteger>("UBigInteger must be >= 0");

    public bool Equals(BigInteger other) => _value.Equals(other);

    public char LastBit() => _value % 2 == 0 ? '0' : '1';

    public UBigInteger ShiftRight() => new UBigInteger(_value / 2);
}