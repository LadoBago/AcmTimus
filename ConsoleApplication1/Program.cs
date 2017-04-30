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
            for (int i = 1; i < n; i++)
            {
                BigInteger res = rec(i);
                Console.WriteLine(res.ToString());
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
            }

            BigInteger prev = rec(n - 1);
            BigInteger twos = BigInteger.Pow(2, n - 1);
            BigInteger pr = BigInteger.Divide(prev, twos);
            if (pr.IsEven)
                return prev;

            int len = prev.ToString().Length;
            BigInteger tens = BigInteger.Pow(new BigInteger(10), len);
            BigInteger m = BigInteger.Remainder(tens, twos);
            BigInteger d = BigInteger.Divide(twos, m);
            BigInteger r = rec((int)BigInteger.Log(d, 2D));

            BigInteger res = BigInteger.Add(BigInteger.Multiply(r, tens), prev);

            return res;
        }
    }
}