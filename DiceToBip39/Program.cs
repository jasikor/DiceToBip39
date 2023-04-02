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
                .Match(m => Console.WriteLine(string.Join(" ", m.Words)),
                    e => Console.WriteLine(e.ToFullString()));
            return words.IsSuccess ? 0 : -1;
        }
    }

    public static class ProgramExt
    {
        public static Validation<Error, string> ValidateArgs(this string[] args) =>
            SeedParameterSupplied(args)
                .Bind(SeedIsAtLeast100Long)
                .Bind(SeedContainsDiceRolls);


        public static Validation<Error, string> SeedParameterSupplied(this string[] args) =>
            args.Length == 1
                ? Success<Error, string>(args[0])
                : Fail<Error, string>(
                    "Usage: DiceToBip39 diceSeed \n\n   diceSeed is a string of at least 100 digits of [1..6]");

        public static Validation<Error, string> SeedIsAtLeast100Long(this string seed) =>
            seed.Length >= 100
                ? Success<Error, string>(seed)
                : Fail<Error, string>("diceSeed must be at least 100 characters long");

        public static Validation<Error, string> SeedContainsDiceRolls(this string seed) =>
            seed.All(c => c >= '1' && c <= '6')
                ? Success<Error, string>(seed)
                : Fail<Error, string>("diceSeed has to be composed of [1..6] only");

        public static Validation<Error, Mnemonic> GetMnemonicWords(string[] args) =>
            args
                .ValidateArgs()
                .Map(DiceToMnemonic);

        private static Mnemonic DiceToMnemonic(string diceSeed)
        {
            var entropyBytes = DiceToBytes(diceSeed);
            return new Mnemonic(Wordlist.English, entropyBytes);
        }

        private static byte[] DiceToBytes(string diceSeed)
        {
            var binary = TrimTo256Bits(DiceToBinaryString(diceSeed));
            var entropyBytes = BinaryStringToBytes(binary);
            return entropyBytes;
        }

        private static string DiceToBinaryString(string diceSeed) =>
            ToBinaryString(DiceToBigInteger(diceSeed), CalculateEntropy(diceSeed));

        private static string TrimTo256Bits(string binary) =>
            binary.Length > 256
                ? binary[^256..]
                : binary;

        public static int CalculateEntropy(string diceSeed) =>   
            (int) BigInteger.Log(BigInteger.Pow(6, diceSeed.Length), 2.0);

        public static BigInteger DiceToBigInteger(string diceSeed)
        {
            return diceSeed
                .Fold(BigInteger.Zero, (acc, ch) => {
                    acc *= 6;
                    acc += (ch - '0') - 1;
                    return acc;
                });
        }

        public static string ToBinaryString(BigInteger seed, int noOfBits)
        {
            var ret = new StringBuilder();
            for (int i = noOfBits; i > 0; i--) {
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