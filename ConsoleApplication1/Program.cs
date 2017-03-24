﻿using System;
using System.Collections.Specialized;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main2(string[] args)
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
                decimal[,] a = new decimal[k, k];
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

        private static int _Det(decimal[,] a, int k)
        {
            for (int i = 0; i < k - 1; i++)
            {
                for (int j = i + 1; j < k; j++)
                {
                    if (a[j, i] != 0)
                    {
                        decimal t = a[j, i] / a[i, i];

                        for (int f = i; f < k; f++)
                        {
                            a[j, f] -= t * a[i, f];
                        }
                    }
                }
            }

            decimal[] d = new decimal[51];
            for (int i = 1; i < 50; i++)
                d[i] = 0;

            d[0] = a[0, 0];

            for (int i = 1; i < k; i++)
            {
                d[50] *= a[i, i];
                for (int j = 49; j >= 0; j--)
                {
                    d[j] *= a[i, i];
                    d[j + 1] += (int)(d[j] / 10);
                    d[j] %= 10;
                }
            }

            decimal res = 0;

            for (int i = 50; i >= 9; i--)
            {
                int w = i - 9;
                for (int j = 0; j < w; j++)
                {
                    d[i] = d[i] * 10 - decimal.Truncate(d[i] * 10);
                }
                res += d[i];
            }

            res = (res - decimal.Truncate(res)) * 1000000000;
            res += d[8] * 100000000 + d[7] * 10000000 + d[6] * 1000000 + d[5] * 100000 + d[4] * 10000 + d[3] * 1000 + d[2] * 100 + d[1] * 10 + d[0];

            return Convert.ToInt32(decimal.Round(res, 0));
        }
    }
}
