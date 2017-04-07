using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        private static float[,] a;
        static void Main(string[] args)
        {
#if !ONLINE_JUDGE
            StreamReader sr = new StreamReader("Input.txt");
            StreamWriter sw = new StreamWriter("Output.txt");
            Console.SetIn(sr);
            Console.SetOut(sw);
#endif
            int n = Convert.ToInt32(Console.ReadLine());
            a = new float[4, n];
            int[] tokens = Console.ReadLine().Split(' ', '\t').Select(e => int.Parse(e)).ToArray();

            for (int i = 0; i < n; i++)
            {
                a[0, i] = tokens[i];
                a[1, i] = 0.75F * tokens[i];
                a[2, i] = 1.1F * tokens[i];
                a[3, i] = 0.825F * tokens[i];
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
