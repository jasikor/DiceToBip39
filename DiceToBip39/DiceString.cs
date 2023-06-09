﻿using System.Numerics;
using LanguageExt;
using static DiceToBip39.EntropyExt;



namespace DiceToBip39;

public class DiceString
{
    private readonly string _value;

    private DiceString(string value) => _value = value;


    public static Fin<DiceString> Create(string s) =>
        ConstrainedStringValidator.Validate(s, _goodDiceLengths.Min(), '1', '6')
            .Map(st => new DiceString(TruncateToNearestGoodLength(s)));

    static private readonly int[] _goodDiceLengths = new[] {50, 62, 75, 87, 100};

    private static string TruncateToNearestGoodLength(string s) =>
        s.Substring(0, NearestGoodLenth(s.Length));

    private static int NearestGoodLenth(int length) =>
        _goodDiceLengths
            .Last(l => l <= length);

    public BigInteger ToBigInteger() =>
        _value
            .Fold(BigInteger.Zero, (acc, ch) => {
                acc *= 6;
                acc += (uint) (ch - '0') - 1;
                return acc;
            });


    public int BitsOfEntropy() =>
        (int) EntropyExt.BitsOfEntropy(_value.Length, 6);

    public Entropies Entropy() => Floor(BitsOfEntropy());
}