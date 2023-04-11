﻿using System.Diagnostics;
using System.Numerics;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using NBitcoin;
using static DiceToBip39.BinaryString256;
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
                    .Bind(ToMnemonic);
            mnemonic
                .Map(m => string.Join(" ", m))
                .Match(Console.Write,
                    e => Console.WriteLine(e.ToFullString()));
            return mnemonic.IsSuccess ? 0 : -1;
        }
    }

    public static class ProgramExt
    {
        public static Validation<Error, string> ValidateArgs(this string[] args) =>
            args.Length == 1
                ? Success<Error, string>(args[0])
                : Fail<Error, string>(
                    $"Usage: DiceToBip39 diceSeed \n\n   diceSeed is a string of at least 100 digits of [1..6]");


        public static Validation<Error, Mnemonic> ToMnemonic(DiceString diceSeed)
        {
            var bi = diceSeed.ToBigInteger();
            var by = BinaryString256.Create(bi).ToByteArray();
            return new Mnemonic(Wordlist.English, by);
        }
    }
}