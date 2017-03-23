using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static decimal[,] a;
        static int k;
        static StringDictionary dict = new StringDictionary();
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

            k = 0;

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
                k--;
                a = new decimal[k, k];
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
                    a[i, i] = t + _a[i, k];
                    if (a[i, i] == 0)
                    {
                        degree0 = true;
                        break;
                    }

                }
                if (!degree0)
                    Console.WriteLine(_Det());
                else
                    Console.WriteLine(0);
            }
            else
                Console.WriteLine(0);

#if !ONLINE_JUDGE
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static int _Det()
        {
            System.Numerics.BigInteger bi = new BigInteger();
            
            for (int i = 0; i < k - 1; i++)
            {
                for (int j = i + 1; j < k; j++)
                {
                    decimal t = a[j, i] / a[i, i];

                    for (int f = 0; f < k; f++)
                    {
                        a[j, f] -= t * a[i, f];
                    }
                }
            }

            decimal res = a[0, 0];

            for (int i = 1; i < k; i++)
            {
                res *= a[i, i];
                res %= 1000000000;
            }

            return Convert.ToInt32(decimal.Round(res, 0));
        }
    }
}

//19872369301840986112
             //671702784.98267021986205253039M
