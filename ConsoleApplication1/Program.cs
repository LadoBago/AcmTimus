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
            int pow = 0;

            while (n % 2 == 0)
            {
                n = n / 2;
                pow++;
            }

            while (true)
            {
                BigInteger res = rec(pow++);

                if (res.ToString().Length > 30)
                {
                    Console.WriteLine("Impossible");
                    break;
                }

                if (BigInteger.Compare(BigInteger.Remainder(res, new BigInteger(n)), BigInteger.Zero) == 0)
                {
                    Console.WriteLine(res.ToString());
                    break;
                }

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
            }

            BigInteger m;

            BigInteger prev = rec(n - 1);
            int len = prev.ToString().Length;
            BigInteger twosPrev = BigInteger.Pow(2, n - 1);
            BigInteger d = BigInteger.DivRem(prev, twosPrev, out m);
            if (d.IsEven)
                return prev;

            BigInteger tens = BigInteger.Pow(new BigInteger(10), len - 1);
            d = BigInteger.DivRem(tens, twosPrev, out m);

            if (BigInteger.Compare(BigInteger.Multiply(tens, new BigInteger(2)), prev) > 0
                && BigInteger.Compare(m, BigInteger.Zero) == 0
                && !d.IsEven)
            {
                return BigInteger.Add(prev, tens);
            }

            tens = BigInteger.Multiply(tens, new BigInteger(10));
            d = BigInteger.DivRem(tens, twosPrev, out m);

            if (BigInteger.Compare(m, BigInteger.Zero) == 0 && !d.IsEven)
                return BigInteger.Add(tens, prev);

            BigInteger d1 = BigInteger.Divide(BigInteger.Divide(BigInteger.Multiply(twosPrev, m), BigInteger.GreatestCommonDivisor(twosPrev, m)), m);

            int log = (int)BigInteger.Log(d1, 2D);
            BigInteger r1 = rec(log);

            if (!BigInteger.Divide(r1, d1).IsEven)
                return BigInteger.Add(BigInteger.Multiply(tens, r1), prev);

            int rl = r1.ToString().Length;
            BigInteger res;
            if (rl == log)
                res = BigInteger.Add(BigInteger.Multiply(BigInteger.Add(BigInteger.Pow(10, log), r1), tens), prev);
            else
                res = BigInteger.Add(BigInteger.Multiply(BigInteger.Add(BigInteger.Multiply(BigInteger.Pow(10, log - 1), new BigInteger(2)), r1), tens), prev);

            return res;
        }
    }
}