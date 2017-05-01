using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
#if !ONLINE_JUDGE
            StreamReader sr = new StreamReader("Input.txt");
            StreamWriter sw = new StreamWriter("Output.txt");
            Console.SetIn(sr);
            Console.SetOut(sw);
#endif
            int n = int.Parse(Console.ReadLine());
            BigInteger r = rec2(BigInteger.Zero, 4096, 0);

            for (int i = 1; i < n; i++)
            {
                BigInteger res = rec(i);
                Console.WriteLine("{0}-{1}", i, res.ToString());
                Console.WriteLine(BigInteger.Remainder(res, BigInteger.Pow(new BigInteger(2), i)).ToString());
            }
#if !ONLINE_JUDGE
            ;
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static BigInteger rec(int n)
        {
            switch (n)
            {
                case 0: return 1;
                case 1: return 2;
                case 2: return 12;
                case 3: return 112;
                case 4: return 112;
                case 5: return 2112;
                case 6: return 2112;
                case 7: return 122112;
                case 8: return 122112;
                case 09: return 12122112;
                case 10: return 12122112;
                case 11: return 12122112;
            }

            BigInteger prev = rec(n - 1);
            BigInteger twosPrev = BigInteger.Pow(2, n - 1);
            BigInteger twos = BigInteger.Multiply(twosPrev, new BigInteger(2));
            BigInteger pr = BigInteger.Divide(prev, twosPrev);
            if (pr.IsEven)
                return prev;

            int len = prev.ToString().Length;
            BigInteger tens = BigInteger.Pow(new BigInteger(10), len);

            BigInteger m;
            BigInteger d = BigInteger.DivRem(tens, twosPrev, out m);



            BigInteger res = BigInteger.One;

            return res;
        }

        private static BigInteger rec2(BigInteger a1, int n, int c)
        {
            c++;
            if (c > 12)
                return BigInteger.MinusOne;

            BigInteger b1 = BigInteger.Add(BigInteger.Multiply(new BigInteger(10), a1), BigInteger.One);
            BigInteger b2 = BigInteger.Add(b1, BigInteger.One);

            if (BigInteger.Compare(BigInteger.Remainder(b1, n), BigInteger.Zero) == 0)
                return b1;

            if (BigInteger.Compare(BigInteger.Remainder(b2, n), BigInteger.Zero) == 0)
                return b2;

            BigInteger res = rec2(b1, n, c);
            if (BigInteger.Compare(res, BigInteger.MinusOne) == 0)
                res = rec2(b2, n, c);

            return res;
        }
    }
}