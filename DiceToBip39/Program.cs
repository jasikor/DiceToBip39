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
        private const int BitsOfEntropy = 256;
        private const int DiceDigitsEntropy = 100;

        public static Validation<Error, string> ValidateArgs(this string[] args) =>
            args.Length == 1
                ? Success<Error, string>(args[0])
                : Fail<Error, string>(
                    $"Usage: DiceToBip39 diceSeed \n\n   diceSeed is a string of at least {DiceDigitsEntropy} digits of [1..6]");


        public static Validation<Error, Mnemonic> GetMnemonicWords(string[] args) =>
            args
                .ValidateArgs()
                .Bind(s => DiceString.Create(s))
                .Map(DiceToMnemonic);

        public static Mnemonic DiceToMnemonic(DiceString diceSeed)
        {
            var diceBytes = DiceToBytes(diceSeed);
            return new Mnemonic(Wordlist.English, diceBytes);
        }

        private static byte[] DiceToBytes(DiceString diceSeed) => BinaryStringToBytes(DiceToBinaryString(diceSeed));

        private static string DiceToBinaryString(DiceString diceSeed) =>
            ToBinaryString(diceSeed.DiceToBigInteger());


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