using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System.Numerics;
using System.Text;
using NBitcoin;
using static DiceToBip39.ProgramExt;


namespace DiceToBip39
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var words = GetMnemonicWords(args);
            words
                .Map(w => string.Join(" ", w))
                .Match(Console.Write,
                    e => Console.WriteLine(e.ToFullString()));
            return words.IsSuccess ? 0 : -1;
        }
    }

    public static class ProgramExt
    {
        public const int BitsOfEntropy = 256;
        public const int DiceDigitsEntropy = 100;

        public static Validation<Error, string> ValidateArgs(this string[] args) =>
            SeedParameterSupplied(args)
                .Bind(SeedIsAtLeastDiceDigitsEntropyLong)
                .Bind(SeedContainsDiceRolls);


        public static Validation<Error, string> SeedParameterSupplied(this string[] args) =>
            args.Length == 1
                ? Success<Error, string>(args[0])
                : Fail<Error, string>(
                    $"Usage: DiceToBip39 diceSeed \n\n   diceSeed is a string of at least {DiceDigitsEntropy} digits of [1..6]");

        public static Validation<Error, string> SeedIsAtLeastDiceDigitsEntropyLong(this string seed) =>
            seed.Length >= DiceDigitsEntropy
                ? Success<Error, string>(seed)
                : Fail<Error, string>($"diceSeed must be at least {DiceDigitsEntropy} characters long");

        public static Validation<Error, string> SeedContainsDiceRolls(this string seed) =>
            seed.All(c => c is >= '1' and <= '6')
                ? Success<Error, string>(seed)
                : Fail<Error, string>("diceSeed has to be composed of [1..6] only");

        public static Validation<Error, Mnemonic> GetMnemonicWords(string[] args) =>
            args
                .ValidateArgs()
                .Map(DiceToMnemonic);

        public static Mnemonic DiceToMnemonic(string diceSeed)
        {
            var diceBytes = DiceToBytes(diceSeed.Substring(0, DiceDigitsEntropy));
            return new Mnemonic(Wordlist.English, diceBytes);
        }

        private static byte[] DiceToBytes(string diceSeed) => BinaryStringToBytes(DiceToBinaryString(diceSeed));

        private static string DiceToBinaryString(string diceSeed) =>
            ToBinaryString(DiceToBigInteger(diceSeed));

        public static BigInteger DiceToBigInteger(string diceSeed) =>
            diceSeed
                .Fold(BigInteger.Zero, (acc, ch) => {
                    acc *= 6;
                    acc += (ch - '0') - 1;
                    return acc;
                });

        public static string ToBinaryString(BigInteger seed)
        {
            var ret = new StringBuilder();
            for (int i = BitsOfEntropy; i > 0; i--) {
                ret.Insert(0, (seed % 2).ToString());
                seed /= 2;
            }

            return ret.ToString();
        }

        public static byte[] BinaryStringToBytes(string binary)
        {
            int numOfBytes = binary.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i) {
                bytes[i] = Convert.ToByte(binary.Substring(8 * i, 8), 2);
            }

            return bytes;
        }
    }
}