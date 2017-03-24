using System;
using System.Collections.Specialized;
using System.IO;
using System.Numerics;

namespace ConsoleApplication1
{
    class Program2
    {
        static void Main(string[] args)
        {
#if !ONLINE_JUDGE
            StreamReader sr = new StreamReader("Input.txt");
            StreamWriter sw = new StreamWriter("Output.txt");
            Console.SetIn(sr);
            Console.SetOut(sw);
#endif
            string[] tokens = Console.ReadLine().Split(' ', '\t');
            int n = Convert.ToInt32(tokens[0]);
            int m = Convert.ToInt32(tokens[1]);


            int[,] _a = new int[n * m, n * m];
            int[,] b = new int[n, m];
            int[] c = new int[] { -1, -1, 0x1FFFF };

            for (int i = 0; i < n * m; i++)
                for (int j = 0; j < n * m; j++)
                    _a[i, j] = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    b[i, j] = 0;

            int k = 0;

            string line = null, preline = null;
            for (int i = 0; i < n; i++)
            {
                line = Console.ReadLine();

                for (int j = 0; j < m; j++)
                {
                    if (line[j] == '.')
                    {
                        b[i, j] = k;
                        if (j > 0)
                            if (line[j - 1] == '.')
                                _a[k, b[i, j - 1]] = _a[b[i, j - 1], k] = 1;

                        if (i > 0)
                            if (preline[j] == '.')
                                _a[k, b[i - 1, j]] = _a[b[i - 1, j], k] = 1;

                        k++;
                    }
                }
                preline = line;
            }
            if (k > 1)
            {
                int[,] a = new int[k, k];
                bool degree0 = false;
                for (int i = 0; i < k; i++)
                {
                    int t = 0;
                    for (int j = 0; j < k; j++)
                    {
                        if (_a[i, j] == 1)
                        {
                            t++;
                            a[i, j] = -1;
                        }
                    }
                    a[i, i] = t;
                    if (a[i, i] == 0)
                    {
                        degree0 = true;
                        break;
                    }
                }
                k--;
                if (!degree0)
                    Console.WriteLine(_Det(a, k));
                else
                    Console.WriteLine(0);
            }
            else
                Console.WriteLine(1);

#if !ONLINE_JUDGE
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static int _Det(int[,] a0, int k)
        {
            Num[,] a = new Num[k, k];

            for (int i = 0; i < k; i++)
                for (int j = 0; j < k; j++)
                    a[i, j] = new Num(new BigInteger(a0[i, j]), BigInteger.One);

            for (int i = 0; i < k - 1; i++)
            {
                for (int j = i + 1; j < k; j++)
                {
                    if (BigInteger.Compare(a[j, i].Up, BigInteger.Zero) != 0)
                    {
                        Num t = a[j, i] / a[i, i];

                        for (int f = i; f < k; f++)
                        {
                            a[j, f] -= t * a[i, f];
                        }
                    }
                }
            }

            return Convert.ToInt32(decimal.Round(0, 0));
        }
    }

    struct Num
    {
        public BigInteger Up;
        public BigInteger Down;

        public Num(BigInteger up, BigInteger down)
        {
            Up = up;
            Down = down;
        }

        public static void Normalize(ref Num num)
        {
            BigInteger gcd = BigInteger.GreatestCommonDivisor(num.Up, num.Down);
            if (BigInteger.Compare(gcd, BigInteger.One) > 0)
            {
                num.Down = BigInteger.Divide(num.Down, gcd);
                num.Up = BigInteger.Divide(num.Up, gcd);
            }
        }
        public void Normalize()
        {
            Normalize(ref this); 
        }

        public static Num operator *(Num l, Num r)
        {
            if (BigInteger.Compare(l.Up, BigInteger.Zero) == 0)
                return new Num(BigInteger.Zero, BigInteger.One);
            if (BigInteger.Compare(r.Up, BigInteger.Zero) == 0)
                return new Num(BigInteger.Zero, BigInteger.One);

            Num res = new Num(BigInteger.Multiply(l.Up, r.Up), BigInteger.Multiply(l.Down, r.Down));
            res.Normalize();
            return res;
        }
        public static Num operator /(Num l, Num r)
        {
            if (BigInteger.Compare(l.Up, BigInteger.Zero) == 0)
                return new Num(BigInteger.Zero, BigInteger.One);

            Num res = new Num(BigInteger.Multiply(l.Up, r.Down), BigInteger.Multiply(l.Down, r.Up));
            res.Normalize();
            return res;
        }
        public static Num operator -(Num l, Num r)
        {
            Num res = new Num(BigInteger.Subtract(BigInteger.Multiply(l.Up, r.Down), BigInteger.Multiply(r.Up, l.Down)), BigInteger.Multiply(l.Down, r.Down));
            res.Normalize();
            return res;
        }
        public static Num operator +(Num l, Num r)
        {
            Num res = new Num(BigInteger.Add(BigInteger.Multiply(l.Up, r.Down), BigInteger.Multiply(r.Up, l.Down)), BigInteger.Multiply(l.Down, r.Down));
            res.Normalize();
            return res;
        }
    }
}
