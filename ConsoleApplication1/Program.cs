using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        private static List<int>[] s, w;
        private static bool[,] aa, a;
        static void Main(string[] args)
        {
#if !ONLINE_JUDGE
            StreamReader sr = new StreamReader("Input.txt");
            StreamWriter sw = new StreamWriter("Output.txt");
            Console.SetIn(sr);
            Console.SetOut(sw);
#endif
            //Console.WriteLine(DateTime.Now.Ticks);

            int n = Convert.ToInt32(Console.ReadLine());

            a = new bool[n, n];
            aa = new bool[n, n];
            List<int> roots = new List<int>();

            s = new List<int>[n];
            w = new List<int>[n];

            for (int i = 0; i < n; i++)
            {
                s[i] = new List<int>();
                string line = Console.ReadLine();

                for (int j = 0; j < n; j++)
                {
                    a[i, j] = i == j;
                    aa[i, j] = false;
                    if (i == 0)
                        w[j] = new List<int>();

                    if (line[j] == '1')
                    {
                        s[i].Add(j);
                        w[j].Add(i);
                    }
                }
            }

            for (int i = 0; i < n; i++)
                DFS_U(i, i);

            int q = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < q; i++)
            {
                int[] tokens = Console.ReadLine().Split(' ', '\t').Select(e => int.Parse(e)).ToArray();
                int t1 = tokens[0] - 1;
                int t2 = tokens[1] - 1;

                if (!a[t1, t2])
                    Console.WriteLine("YES");
                else
                    Console.WriteLine("No");
            }

            //Console.WriteLine(DateTime.Now.Ticks);

#if !ONLINE_JUDGE
            ;
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static void DFS_U(int u, int v)
        {
            aa[u, v] = true;
            foreach (int i in w[v])
                if (!aa[u, i])
                    DFS_U(u, i);

            if (!a[u, v])
                DFS_D(u, v);
        }

        private static void DFS_D(int u, int v)
        {
            foreach (int i in s[v])
                if (!a[u, i])
                {
                    a[u, i] = true;
                    DFS_D(u, i);
                }
        }
    }
}
