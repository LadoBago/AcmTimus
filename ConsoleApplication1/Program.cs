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
            int n = Convert.ToInt32(Console.ReadLine());
            bool[,] a = new bool[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = false;

            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();

                for (int j = 0; j < n; j++)
                {
                    if (line[j] == '1')
                        a[j, i] = true;
                }
            }


            int q = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < q; i++)
            {
                int[] tokens = Console.ReadLine().Split(' ', '\t').Select(e => int.Parse(e)).ToArray();
                Console.Write(!a[tokens[0], tokens[1]] ? "YES" : "NO");
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
