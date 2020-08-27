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
 *  #import_SegmentTree.cs
 */
    static class Program
    {
        private static int mod = (int)(1e9) + 7;
        private static int[] ar;
        private static int n;
        private static int m;

        static void Solve()
        {
            n = NextInt();
            m = NextInt();
            ar = Repeat(0, n).Select((_, index) => NextInt()).ToArray();
            SegmentTree<int> tree = new SegmentTree<int>(ar, Merge, 0);
            while (m-- > 0)
            {
                int type = NextInt();
                if (type == 1)
                {
                    int index = NextInt();
                    int value = NextInt();
                    tree.Update(1, 0, n - 1, index, value);
                }
                else
                {
                    int k = NextInt();
                    int l = NextInt();
                    Console.WriteLine(tree.Kth(1, 0, n - 1, k, l));
                }

            }
        }

        private static int Kth(this SegmentTree<int> tree, int node, int b, int e, int k, int l)
        {
            if (b == e)
            {
                return (tree[node] >= k) ? b : -1;
            }

            int mid = (b + e) >> 1;
            int left = node << 1;
            int right = left | 1;
            if (tree[left] < k || mid <= l)
            {
                return tree.Kth(right, mid + 1, e, k, l);
            }

            return tree.Kth(left, b, mid, k, l);
        }

        private static int Merge(int arg1, int arg2)
        {
            return Math.Max(arg1, arg2);
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
            if (args.Length == 0)
            {
                Console.SetOut(new Printer(Console.OpenStandardOutput()));
            }

            Thread t = new Thread(Solve, 134217728);
            t.Start();
            t.Join();
            Console.Out.Flush();
#endif
        }

        static int NextInt() => int.Parse(Console_.NextString());
        static long NextLong() => long.Parse(Console_.NextString());
        static double NextDouble() => double.Parse(Console_.NextString());
        static string NextString() => Console_.NextString();
        static string NextLine() => Console.ReadLine();
        static IEnumerable<T> OrderByRand<T>(this IEnumerable<T> x) => x.OrderBy(_ => xorshift);
        static long Count<T>(this IEnumerable<T> x, Func<T, bool> pred) => Enumerable.Count(x, pred);
        static IEnumerable<T> Repeat<T>(T v, long n) => Enumerable.Repeat<T>(v, (int)n);
        static IEnumerable<int> Range(long s, long c) => Enumerable.Range((int)s, (int)c);
        static uint xorshift { get { _xsi.MoveNext(); return _xsi.Current; } }
        static IEnumerator<uint> _xsi = _xsc();
        static IEnumerator<uint> _xsc() { uint x = 123456789, y = 362436069, z = 521288629, w = (uint)(DateTime.Now.Ticks & 0xffffffff); while (true) { var t = x ^ (x << 11); x = y; y = z; z = w; w = (w ^ (w >> 19)) ^ (t ^ (t >> 8)); yield return w; } }

        static class Console_
        {
            static Queue<string> param = new Queue<string>();
            public static string NextString()
            {
                if (param.Count == 0)
                {
                    foreach (string item in NextLine().Split(' '))
                    {
                        if (string.IsNullOrWhiteSpace(item))
                        {
                            continue;
                        }

                        param.Enqueue(item);
                    }
                }
                return param.Dequeue();
            }
        }
        class Printer : StreamWriter
        {
            public override IFormatProvider FormatProvider => CultureInfo.InvariantCulture;
            public Printer(Stream stream) : base(stream, new UTF8Encoding(false, true)) { base.AutoFlush = false; }
            public Printer(Stream stream, Encoding encoding) : base(stream, encoding) { base.AutoFlush = false; }
        }
    }
}