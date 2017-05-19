using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ConsoleApplication1
{
    class Program
    {
        private static int lastDigit, n;
        private static BigInteger minRes;
        static void Main(string[] args)
        {
#if !ONLINE_JUDGE
            StreamReader sr = new StreamReader("Input.txt");
            StreamWriter sw = new StreamWriter("Output.txt");
            Console.SetIn(sr);
            Console.SetOut(sw);
#endif
            n = int.Parse(Console.ReadLine());
            lastDigit = n % 10;
            minRes = BigInteger.Zero;
            BigInteger res = rec(BigInteger.Zero, 0);
            if (res.IsZero)
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

        private static BigInteger rec(BigInteger res, int i)
        {
            string str = res.ToString();
            string strTrimed = str.TrimEnd(new char[] { '1', '2' });

            if (!minRes.IsZero && BigInteger.Compare(res, minRes) >= 0)
                return BigInteger.Zero;
            if (string.IsNullOrEmpty(strTrimed))
                return res;
            if (str.Length + i >= 30)
                return BigInteger.Zero;

            i += (str.Length - strTrimed.Length);
            int dr = int.Parse(strTrimed[strTrimed.Length - 1].ToString());
            int r1 = 1 - dr; if (r1 < 0) r1 += 10;
            int r2 = 2 - dr; if (r2 < 0) r2 += 10;

            BigInteger tmpRes = BigInteger.Zero;
            for (int j = 1; j < 10; j++)
            {
                int rem = (lastDigit * j) % 10;
                if (rem == r1 || rem == r2)
                {
                    BigInteger p10 = BigInteger.Pow(new BigInteger(10), i);
                    tmpRes = rec(BigInteger.Divide(BigInteger.Add(res, BigInteger.Multiply(new BigInteger(n), BigInteger.Multiply(new BigInteger(j), p10))), p10), i);
                    if (!tmpRes.IsZero)
                    {
                        if (minRes.IsZero || BigInteger.Compare(tmpRes, minRes) < 0)
                            minRes = tmpRes;
                    }
                }
            }

            return tmpRes;
        }
    }
}