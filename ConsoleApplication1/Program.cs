using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int m = Convert.ToInt32(tokens[1]);

            int[,] _a = new int[n * m, n * m];
            int[,] b = new int[n, m];

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

            int[,] a = new int[k, k];

            for (int i = 0; i < k; i++)
                for (int j = 0; j < k; j++)
                    a[i, j] = _a[i, j];



#if !ONLINE_JUDGE
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static int _Rec(int[,] a)
        {


            return 0;
        }

        private static int[,] _Deletion(int[,] a, int i, int j)
        {
            int k = a.GetLength(0);
            int[,] res = new int[k, k];

            for (int i1 = 0; i1 < k; i1++)
                for (int j1 = 0; j1 < k; j1++)
                    res[i1, j1] = a[i1, j1];

            res[i, j] = res[j, i] = 0;

            return res;
        }

        private static int[,] _Contraction(int[,] a, int i, int j)
        {
            int k = a.GetLength(0) - 1;
            int[,] res = new int[k, k];

            
        }
    }
}
