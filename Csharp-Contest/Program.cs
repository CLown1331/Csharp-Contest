﻿namespace Csharp_Contest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    /*
     *
     */

    static class Program
    {
        private const int StackSize = 64 * (1 << 20);
        private const int Sz = (int)2e5 + 10;
        private const int Mod = (int)1e9 + 7;

        private static void Solve()
        {
            int[,] dp = new int[Sz, 10];
            for (int j = 0; j <= 9; j++)
            {
                dp[0, j] = 1;
            }

            for (int i = 1; i < Sz; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    if (j == 9)
                    {
                        dp[i, j] = (dp[i, j] + dp[i - 1, 0]) % Mod;
                        dp[i, j] = (dp[i, j] + dp[i - 1, 1]) % Mod;
                        continue;
                    }

                    dp[i, j] = (dp[i, j] + dp[i - 1, j + 1]) % Mod;
                }
            }

            int t = NextInt();
            int ans = 0;
            for (int cases = 1; cases <= t; cases++)
            {
                ans = 0;
                string n = NextString();
                int m = NextInt();
                foreach (char c in n)
                {
                    ans = (ans + dp[m, c - '0']) % Mod;
                }

                OutputPrinter.WriteLine(ans);
            }
        }

        public static void Main(string[] args)
        {
#if CLown1331
            for (int testCase = 0; testCase < 1; testCase++)
            {
                Solve();
            }

            Utils.CreateFileForSubmission();
            if (Debugger.IsAttached) Thread.Sleep(Timeout.Infinite);
#else
            Thread t = new Thread(Solve, StackSize);
            t.Start();
            t.Join();
#endif
            OutputPrinter.Flush();
            ErrorPrinter.Flush();
        }

        private static int NextInt() => int.Parse(Reader.NextString());

        private static long NextLong() => long.Parse(Reader.NextString());

        private static double NextDouble() => double.Parse(Reader.NextString());

        private static string NextString() => Reader.NextString();

        private static string NextLine() => Reader.ReadLine();

        private static IEnumerable<T> OrderByRand<T>(this IEnumerable<T> x) => x.OrderBy(_ => XorShift);

        private static long Count<T>(this IEnumerable<T> x, Func<T, bool> pred) => Enumerable.Count(x, pred);

        private static IEnumerable<T> Repeat<T>(T v, long n) => Enumerable.Repeat<T>(v, (int)n);

        private static IEnumerable<int> Range(long s, long c) => Enumerable.Range((int)s, (int)c);

        private static uint XorShift
        {
            get
            {
                Xsc().MoveNext();
                return Xsc().Current;
            }
        }

        private static IEnumerator<uint> Xsc()
        {
            uint x = 123456789, y = 362436069, z = 521288629, w = (uint)(DateTime.Now.Ticks & 0xffffffff);
            while (true)
            {
                uint t = x ^ (x << 11);
                x = y;
                y = z;
                z = w;
                w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));
                yield return w;
            }
        }

        private static class Reader
        {
            private static readonly Queue<string> Param = new Queue<string>();
#if CLown1331
            private static readonly StreamReader InputReader = new StreamReader("input.txt");
#else
            private static readonly StreamReader InputReader = new StreamReader(Console.OpenStandardInput());
#endif

            public static string NextString()
            {
                if (Param.Count == 0)
                {
                    foreach (string item in ReadLine().Split(' '))
                    {
                        if (string.IsNullOrWhiteSpace(item))
                        {
                            continue;
                        }

                        Param.Enqueue(item);
                    }
                }

                return Param.Dequeue();
            }

            public static string ReadLine()
            {
                return InputReader.ReadLine();
            }
        }

        private static Printer OutputPrinter = new Printer(Console.OpenStandardOutput());

        private static Printer ErrorPrinter = new Printer(Console.OpenStandardError());

        private sealed class Printer : StreamWriter
        {
            public Printer(Stream stream)
                : base(stream, new UTF8Encoding(false, true)) =>
                this.AutoFlush = false;

            public Printer(Stream stream, Encoding encoding)
                : base(stream, encoding) =>
                this.AutoFlush = false;

            public override IFormatProvider FormatProvider => CultureInfo.InvariantCulture;
        }
    }
}
