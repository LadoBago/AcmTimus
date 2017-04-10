using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        private static Heap<Struct0> H;
        static void Main(string[] args)
        {
#if !ONLINE_JUDGE
            StreamReader sr = new StreamReader("Input.txt");
            StreamWriter sw = new StreamWriter("Output.txt");
            Console.SetIn(sr);
            Console.SetOut(sw);
#endif
            int n = Convert.ToInt32(Console.ReadLine());
            H = new Heap<Struct0>(2 * n, new Struct0Comparer());
            int[] tokens = null;
            for (int i = 0; i < n; i++)
            {
                tokens = Console.ReadLine().Split(' ', '\t').Select(e => int.Parse(e)).ToArray();
                H.Add(new Struct0(tokens[0], i));
                H.Add(new Struct0(tokens[1], i));
            }

            int m = Convert.ToInt32(Console.ReadLine());
            int c = 0;

            Stack<int> stack = new Stack<int>(2 * n);
            Struct0[] a = new Struct0[2 * n];
            int j = 0;
            while (H.Count > 0)
                a[j++] = H.Fetch();

            for (int i = 0; i < m; i++)
            {
                c = Convert.ToInt32(Console.ReadLine());

            }

#if !ONLINE_JUDGE
            ;
            sr.Close();
            sw.Flush();
            sw.Close();
#endif
        }
    }

    class Struct0Comparer : IComparer<Struct0>
    {
        public int Compare(Struct0 x, Struct0 y)
        {
            if (x.Point == 0)
                return -1;

            if (y.Point == 0)
                return 1;

            return y.Point.CompareTo(x.Point);
        }
    }

    struct Struct0
    {
        public int Point { get; set; }
        public int Comp { get; set; }

        public Struct0(int point, int comp)
        {
            this.Point = point;
            this.Comp = comp;
        }
    }

    class Heap<T>
    {
        private T[] _List;
        private int[] _Indices;
        private IComparer<T> _Comparer;
        private int _NextIndex;
        public int Count { get; set; }
        public int Capacity { get; set; }

        public Heap(int capacity, IComparer<T> comparer)
        {
            this._NextIndex = capacity;
            this._List = new T[2 * capacity];
            this._Indices = new int[2 * capacity];

            for (int i = 0; i < 2 * capacity; i++)
            {
                this._List[i] = default(T);

                if (i < capacity)
                    this._Indices[i] = -1;
                else
                    this._Indices[i] = i;
            }

            this._Comparer = comparer;
            this.Count = 0;
            this.Capacity = capacity;
        }

        public void Add(T item)
        {
            this._List[this._NextIndex] = item;
            this._Update(this._NextIndex);
            this._NextIndex++;
            this.Count++;

        }

        private void _Update(int i)
        {
            if (i == 0)
                return;

            if (_Comparer.Compare(this._List[(i - 1) / 2], this._List[i]) >= 0)
                return;
            this._Indices[(i - 1) / 2] = this._Indices[i];
            this._List[(i - 1) / 2] = this._List[i];
            this._Update((i - 1) / 2);
        }

        public T Fetch()
        {
            T res = this._List[0];
            this._List[this._Indices[0]] = default(T);
            this._Update(this._Indices[0]);
            this.Count--;

            return res;
        }

    }
}
