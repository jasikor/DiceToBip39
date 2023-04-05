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
        public static Validation<Error, string> ValidateArgs(this string[] args) =>
            args.Length == 1
                ? Success<Error, string>(args[0])
                : Fail<Error, string>(
                    $"Usage: DiceToBip39 diceSeed \n\n   diceSeed is a string of at least 100 digits of [1..6]");


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

        private static byte[] DiceToBytes(DiceString diceSeed) => 
            DiceToBinaryString(diceSeed).ToByteArray();

        private static BinaryString256 DiceToBinaryString(DiceString diceSeed) =>
            BinaryString256.ToBinaryString256(diceSeed.DiceToBigInteger());


        
        
    }
}