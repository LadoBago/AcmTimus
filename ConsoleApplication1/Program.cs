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
            int N = int.Parse(Console.ReadLine());
            int E = int.Parse(Console.ReadLine());
            int[] edges = new int[E * 3];
            for (int i = 0; i < E; ++i)
            {
                int[] tokens = Console.ReadLine().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(e => int.Parse(e)).ToArray();
                Array.Copy(tokens, 0, edges, 3 * i, 3);
            }

            GraphDrawing gd = new GraphDrawing();
            int[] ret = gd.plot(N, edges);

            Console.WriteLine(gd.Cnt);
            Console.WriteLine(gd.maxK);
            Console.WriteLine(ret.Length);
            for (int i = 0; i < ret.Length; ++i)
            {
                Console.WriteLine(ret[i]);
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