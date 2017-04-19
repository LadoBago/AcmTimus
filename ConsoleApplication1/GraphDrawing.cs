using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GraphDrawing
    {
        private List<Distance>[] _Distances;
        private int[,] _Adj;

        public GraphDrawing()
        {
        }

        public int[] plot(int NV, int[] edges)
        {
            int[] res = new int[2 * NV];
            int edgesCount = edges.Length / 3;
            _Distances = new List<Distance>[NV];
            _Adj = new int[NV, NV];
            bool[] flags = new bool[NV];
            int[,] components = new int[2, NV];
            int componentsCount = 0;

            for (int i = 0; i < NV; i++)
            {
                _Distances[i] = new List<Distance>();
                components[0, i] = -1;
                components[1, i] = -1;
                flags[i] = false;
                for (int j = 0; j < NV; j++)
                    _Adj[i, j] = 0;
            }

            DSU dsu = new DSU(NV);

            for (int i = 0; i < edgesCount; i++)
            {
                _Distances[edges[3 * i]].Add(new Distance(edges[3 * i + 1], edges[3 * i + 2]));
                _Distances[edges[3 * i + 1]].Add(new Distance(edges[3 * i], edges[3 * i + 2]));

                _Adj[edges[3 * i], edges[3 * i + 1]] = edges[3 * i + 2];
                _Adj[edges[3 * i + 1], edges[3 * i]] = edges[3 * i + 2];

                dsu.Union(edges[3 * i], edges[3 * i + 1]);
            }

            List<List<int>> paths = new List<List<int>>();

            for (int i = 0; i < NV; i++)
            {
                int parent = dsu.Find(i);

                if (components[0, parent] == -1)
                    components[0, parent] = componentsCount++;

                components[1, i] = components[0, parent];
            }

            for (int i = 0; i < NV; i++)
            {
                if (_Distances[i].Count == 1 && !flags[i])
                {
                    int t = _Distances[i][0].ToVertex;
                    List<int> tmpStack = new List<int>();
                    do
                    {
                        components[1, t] = -1;
                        flags[t] = true;
                        tmpStack.Add(t);
                        t = _Distances[t][0].ToVertex != t ? _Distances[t][0].ToVertex : _Distances[t][1].ToVertex;
                    }
                    while (_Distances[t].Count <= 2);

                    paths.Add(tmpStack);
                }
            }

            return res;
        }
    }

    class Distance
    {
        public int ToVertex { get; set; }
        public int DesiredLength { get; set; }

        public Distance(int toVertex, int desiredLength)
        {
            this.ToVertex = toVertex;
            this.DesiredLength = desiredLength;
        }
    }

    class DSU
    {
        public int N { get; set; }
        private int[] _P, _R;

        public DSU(int n)
        {
            this.N = n;
            this._P = new int[n];
            this._R = new int[n];

            for (int i = 0; i < n; i++)
            {
                _R[i] = 0;
                _P[i] = i;
            }
        }

        public int Find(int i)
        {
            if (_P[i] == i)
                return i;
            return _P[i] = Find(_P[i]);
        }

        public void Union(int a, int b)
        {
            a = Find(a);
            b = Find(b);
            if (a != b)
            {
                N--;
                if (_R[a] < _R[b])
                    _P[a] = _P[b];
                else if (_R[a] > _R[b])
                    _P[b] = _P[a];
                else
                {
                    _P[b] = _P[a];
                    _R[a]++;
                }
            }
        }
    }

}
