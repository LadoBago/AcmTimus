using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static int[,] a;
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
                a = new int[k, k];

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
                }
                Console.WriteLine(_Det(0, c));
            }
            else
                Console.WriteLine(0);

#if !ONLINE_JUDGE
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static int _Det(int l, int[] c)
        {
            int res = 0;
            int i1 = 0;

            string key = string.Format("{0}.{1}.{2}.{3}", c[0], c[1], c[2], l);
            if (dict.ContainsKey(key))
                return int.Parse(dict[key]);

            for (int i = 0; i < k; i++)
            {
                if ((c[i / 32] & (1 << (i % 32))) == 0)
                    continue;

                if (l == k - 1)
                {
                    res = a[l, i];
                    break;
                }

                int[] c1 = new int[3];
                c.CopyTo(c1, 0);
                c1[i / 32] &= ~(1 << (i % 32));
                if (i1 % 2 == 0)
                    res += a[l, i] * _Det(l + 1, c1);
                else
                    res -= a[l, i] * _Det(l + 1, c1);

                res %= 1000000000;
                i1++;
            }

            dict.Add(key, res.ToString());
            return res;
        }
    }
}
