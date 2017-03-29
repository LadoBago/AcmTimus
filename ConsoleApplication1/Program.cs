using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        private static List<int>[] s, w;
        static void Main(string[] args)
        {
#if !ONLINE_JUDGE
            StreamReader sr = new StreamReader("Input.txt");
            StreamWriter sw = new StreamWriter("Output.txt");
            Console.SetIn(sr);
            Console.SetOut(sw);
#endif
            int n = Convert.ToInt32(Console.ReadLine());
            bool[,] a = new bool[n, n];

            s = new List<int>[n];
            w = new List<int>[n];

            for (int i = 0; i < n; i++)
            {
                s[i] = new List<int>();
                w[i] = new List<int>();
            }

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = false;

            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();

                for (int j = 0; j < n; j++)
                {
                    if (line[j] == '1')
                    {
                        a[i, j] = true;
                        s[i].Add(j);
                        w[j].Add(i);
                    }
                }
            }

            int q = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < q; i++)
            {
                int[] tokens = Console.ReadLine().Split(' ', '\t').Select(e => int.Parse(e)).ToArray();
                int t1 = tokens[0] - 1;
                int t2 = tokens[1] - 1;

                if (IsRout(t1, t2))
                {
                    if (w[t1].Count == 0)
                        Console.WriteLine("YES");
                    else
                        Console.WriteLine("NO");
                }
                else if (IsRout(t2, t1))
                {
                    if (w[t2].Count == 0)
                        Console.WriteLine("YES");
                    else
                        Console.WriteLine("NO");
                }
                else
                    Console.WriteLine("YES");
            }
            /*
             რუთები მოვძებნო, ანუ რომლისთვისაც არავის მოუგია და იმათი DFS ები დავიმახსოვრო.
             */


#if !ONLINE_JUDGE
            ;
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static bool IsRout(int i, int j)
        {
            bool res = false;
            foreach (int v in s[i])
            {
                if (v == j)
                {
                    res = true;
                    break;
                }
                res = IsRout(v, j);
                if (res)
                    break;
            }
            return res;
        }
    }
}
