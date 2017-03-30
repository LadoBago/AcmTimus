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
            List<int> roots = new List<int>();

            s = new List<int>[n];
            w = new List<int>[n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = false;

            for (int i = 0; i < n; i++)
            {
                s[i] = new List<int>();
                w[i] = new List<int>();
            }

            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();

                for (int j = 0; j < n; j++)
                {
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

            foreach (int root in roots)
            {
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(root);
                bool[] inqueue = new bool[n];
                List<int> v2 = new List<int>();

                for (int i = 0; i < n; i++)
                    inqueue[i] = false;

                while (queue.Count > 0)
                {
                    int deq = queue.Dequeue();
                    foreach (int sng in s[deq])
                    {
                        if (!inqueue[sng])
                        {
                            queue.Enqueue(sng);
                            inqueue[sng] = true;
                        }
                    }

                    v2.Add(deq);
                }

                for (int i = 1; i < v2.Count; i++)
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

#if !ONLINE_JUDGE
            ;
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }
    }
}
