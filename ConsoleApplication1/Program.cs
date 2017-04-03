using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        private static List<int>[] s, w;
        private static bool[,] aa;
        static void Main(string[] args)
        {
#if !ONLINE_JUDGE
            StreamReader sr = new StreamReader("Input.txt");
            StreamWriter sw = new StreamWriter("Output.txt");
            Console.SetIn(sr);
            Console.SetOut(sw);
#endif
            Console.WriteLine(DateTime.Now.Ticks);

            int n = Convert.ToInt32(Console.ReadLine());

            bool[,] a = new bool[n, n];
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
                    a[i, j] = false;
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
                if (w[i].Count == 0)
                    roots.Add(i);

            Console.WriteLine(DateTime.Now.Ticks);

            foreach (int root in roots)
            {
                DFSUtil(root, root);
                List<int> v2 = new List<int>();

                for (int i = 0; i < n; i++)
                    if (aa[root, i])
                        v2.Add(i);

                for (int i = 0; i < v2.Count; i++)
                    for (int j = i + 1; j < v2.Count; j++)
                        a[v2[i], v2[j]] = a[v2[j], v2[i]] = true;
            }

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

            Console.WriteLine(DateTime.Now.Ticks);

#if !ONLINE_JUDGE
            ;
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static void DFSUtil(int u, int v)
        {
            aa[u, v] = u != v;
            foreach (int i in s[v])
                if (!aa[u, i])
                    DFSUtil(u, i);
        }
    }
}
