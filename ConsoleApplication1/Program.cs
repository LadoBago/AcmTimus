using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        private static List<Struct1> L = new List<Struct1>();
        static void Main(string[] args)
        {
#if !ONLINE_JUDGE
            StreamReader sr = new StreamReader("Input.txt");
            StreamWriter sw = new StreamWriter("Output.txt");
            Console.SetIn(sr);
            Console.SetOut(sw);
#endif
            int n = Convert.ToInt32(Console.ReadLine());
            Struct1 s = new Struct1(0);
            int[] tokens = null;
            for (int i = 0; i < n; i++)
            {
                tokens = Console.ReadLine().Split(' ', '\t').Select(e => int.Parse(e)).ToArray();
                if (s.Start != tokens[0])
                {
                    if (s.Start != 0)
                        L.Add(s);
                    s = new Struct1(tokens[0]);
                }
                s.Ends.Add(new Struct0(tokens[1], i));
            }
            L.Add(s);

            int m = Convert.ToInt32(Console.ReadLine());
            int c = 0;
            BinSearch<Struct1> bs1 = new BinSearch<Struct1>(L.ToArray(), e => e.Start);
            for (int i = 0; i < m; i++)
            {
                c = Convert.ToInt32(Console.ReadLine());
                BSResult bsr = bs1.Search(c);
                if (bsr.Index < 0)
                {
                    if (bsr.LeftIndex < 0)
                        Console.WriteLine(-1);
                    else
                    {

                    }
                }
                else
                    Console.WriteLine(L[bsr.Index].Ends.Last().Comp);
            }

#if !ONLINE_JUDGE
            ;
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }
    }

    struct Struct0
    {
        public int End { get; set; }
        public int Comp { get; set; }

        public Struct0(int end, int comp)
        {
            this.End = end;
            this.Comp = comp;
        }
    }

    struct Struct1
    {
        public int Start { get; set; }
        public List<Struct0> Ends { get; set; }

        public Struct1(int s)
        {
            this.Start = s;
            this.Ends = new List<Struct0>();
        }
    }
    class BinSearch<T>
    {
        T[] _Array;
        Func<T, int> _Func;
        public BinSearch(T[] array, Func<T, int> func)
        {
            this._Array = array;
            this._Func = func;
        }

        public BSResult Search(int obj)
        {
            return this.Search(obj, 0, this._Array.Length - 1);
        }
        public BSResult Search(int obj, int l, int r)
        {
            if (l > r)
                return new BSResult(-1, r);

            int t = (l + r) / 2;
            int c = obj.CompareTo(_Func(_Array[t]));
            if (c == 0)
                return new BSResult(t, -1);
            else if (c < 0)
                return Search(obj, l, t - 1);
            else
                return Search(obj, t + 1, l);

        }
    }

    struct BSResult
    {
        public int Index { get; set; }
        public int LeftIndex { get; set; }

        public BSResult(int index, int leftIndex)
        {
            this.Index = index;
            this.LeftIndex = leftIndex;
        }
    }
}
