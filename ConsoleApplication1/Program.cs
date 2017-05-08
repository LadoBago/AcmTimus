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
            int d = n - (n / 10) * 10;
            BigInteger res = rec(ref n, ref d, 1, BigInteger.Zero);
            if (BigInteger.Compare(res, BigInteger.MinusOne) == 0)
                Console.WriteLine("Impossible");
            else
                Console.WriteLine(res.ToString());

#if !ONLINE_JUDGE
            ;
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static BigInteger rec(ref int n, ref int d, int i, BigInteger res)
        {
            int dr = (int)BigInteger.Remainder(BigInteger.Divide(res, BigInteger.Pow(new BigInteger(10), i - 1)), new BigInteger(10));
            int r1 = 1 - dr; if (r1 < 0) r1 += 10;
            int r2 = 2 - dr; if (r2 < 0) r2 += 10;

            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();

            for (int j = 0; j < 10; j++)
            {
                if ((d * j) % r1 == 0) l1.Add(j);
                if ((d * j) % r2 == 0) l2.Add(j);
            }

            foreach (int l in l1)
            {

            }

            return BigInteger.MinusOne;
        }
    }
}