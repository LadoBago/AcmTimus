using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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
            string[] tokens = Console.ReadLine().Split(' ', '\t');
            int n = Convert.ToInt32(tokens[0]);

            int[,] a = new int[n, n + 1];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n + 1; j++)
                    a[i, j] = j == n ? 1 : 0;

            for (int i = 0; i < n; i++)
            {
                tokens = Console.ReadLine().Split(' ', '\t');
                int[] t = tokens.Select(e => int.Parse(e)).ToArray();
                for (int j = 0; j < (t.Length - 1); j++)
                {
                    a[t[j] - 1, i] = 1;
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (a[i, i] == 0)
                {
                    for (int r = i + 1; r < n; r++)
                    {
                        if (a[i, r] == 1)
                        {
                            for (int u = i; u < n + 1; u++)
                                a[i, u] ^= a[r, u];
                            break;
                        }
                    }
                }

                for (int j = 0; j < n; j++)
                {
                    if (j == i)
                        continue;

                    if (a[j, i] == 0)
                        continue;

                    for (int f = i; f < n + 1; f++)
                    {
                        a[j, f] ^= a[i, f];
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (a[i, n] == 1)
                {
                    Console.Write("{0} ", i + 1);
                }
            }


#if !ONLINE_JUDGE
                sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }
    }
}
