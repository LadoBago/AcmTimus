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
        private static Dictionary<string, int>[] _Dict;
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
            _Dict = new Dictionary<string, int>[k];
            for (int i = 0; i < k; i++)
                _Dict[i] = new Dictionary<string, int>();

            for (int i = 0; i < k; i++)
                for (int j = 0; j < k; j++)
                    a[i, j] = _a[i, j];

            Console.WriteLine(_Rec(a));

#if !ONLINE_JUDGE
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }

        private static int _Rec(int[,] a0)
        {
            int k = a0.GetLength(0);
            if (k == 2)
                return a0[0, 1];

            string key = _GetKey(a0);
            if (_Dict[k - 1].ContainsKey(key))
                return _Dict[k - 1][key];

            int j0;
            for (j0 = k - 2; j0 >= 0; j0--)
                if (a0[k - 1, j0] > 0)
                    break;

            if (j0 < 0)
                return 0;

            int res = a0[k - 1, j0] * (_Rec(_Deletion(a0, k - 1, j0)) + _Rec(_Contraction(a0, j0)));
            res = res % 1000000000;
            _Dict[k - 1].Add(key, res);

            return res;
        }

        private static string _GetKey(int[,] a0)
        {
            StringBuilder res = new StringBuilder(50);
            int k = a0.GetLength(0);

            for (int i = 0; i < k; i++)
                for (int j = 0; j < k; j++)
                    res.AppendFormat(".{0}", a0[i, j]);

            return res.ToString();
        }

        private static int[,] _Deletion(int[,] a0, int i0, int j0)
        {
            int k = a0.GetLength(0);
            int[,] res = new int[k, k];

            for (int i = 0; i < k; i++)
                for (int j = 0; j < k; j++)
                    res[i, j] = a0[i, j];

            res[i0, j0] = res[j0, i0] = 0;

            return res;
        }

        private static int[,] _Contraction(int[,] a0, int j0)
        {
            int k = a0.GetLength(0);
            int[,] tmpa = _Deletion(a0, k - 1, j0);

            for (int i = 0; i < k; i++)
            {
                tmpa[i, j0] += tmpa[i, k - 1];
                tmpa[j0, i] += tmpa[k - 1, i];
            }

            int[,] res = new int[k - 1, k - 1];

            for (int i = 0; i < k - 1; i++)
                for (int j = 0; j < k - 1; j++)
                    res[i, j] = tmpa[i, j];

            return res;
            
        }
    }
}
