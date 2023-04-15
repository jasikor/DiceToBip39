using LanguageExt;

namespace DiceToBip39;

public enum Entropies
{
    Bit128 = 128,
    Bit160 = 160,
    Bit192 = 192,
    Bit224 = 224,
    Bit256 = 256
}

public static class EntropyExt
{
    public static double BitsOfEntropy(int noOfDigits, uint alphabetLength) =>
        Math.Log(Math.Pow(alphabetLength, noOfDigits), 2.0);

    public static Entropies Floor(double entropy) =>
        ((Entropies[]) Enum.GetValues(typeof(Entropies)))
        .Last(x => (int) x <= (int) entropy);
}