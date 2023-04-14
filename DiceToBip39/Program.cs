using System;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using NBitcoin;
using static DiceToBip39.ProgramExt;


namespace DiceToBip39
{
    internal static class Program
    {
        static int Main(string[] args)
        {
            var mnemonic =
                args
                    .ValidateArgs()
                    .Bind(DiceString.Create)
                    .Map(ToMnemonic);
            mnemonic
                .Map(m => string.Join(" ", m))
                .Match(Console.Write,
                    e => Console.WriteLine(e.ToString()));
            return mnemonic.IsSucc ? 0 : -1;
        }
    }

    public static class ProgramExt
    {
        public static Fin<string> ValidateArgs(this string[] args) =>
            args.Length == 1
                ? FinSucc(args[0])
                : FinFail<string>(
                    $"Usage: DiceToBip39 diceSeed \n\n   diceSeed is a string of at least 100 digits of [1..6]");


        public static Mnemonic ToMnemonic(DiceString diceSeed)
        {
            var bi = diceSeed.ToBigInteger();
            var by = BinaryString.Create(bi, 256).ToByteArray();
            return new Mnemonic(Wordlist.English, by);
        }
    }
}