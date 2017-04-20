using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GraphDrawing
    {
        private List<int>[] _AdjList;
        private int[,] _AdjMatrix;

        public GraphDrawing()
        {
        }

        public int[] plot(int NV, int[] edges)
        {
            int[] res = new int[2 * NV];
            int edgesCount = edges.Length / 3;
            _AdjList = new List<int>[NV];
            _AdjMatrix = new int[NV, NV];
            bool[] flags = new bool[NV];
            int[][] components = new int[][] { new int[NV], new int[NV], new int[NV] };
            int componentsCount = 0;

            for (int i = 0; i < NV; i++)
            {
                _AdjList[i] = new List<int>();
                components[0][i] = components[1][i] = -1;
                components[2][i] = 0;

                flags[i] = false;
                for (int j = 0; j < NV; j++)
                    _AdjMatrix[i, j] = 0;
            }

            DSU dsu = new DSU(NV);

            for (int i = 0; i < edgesCount; i++)
            {
                _AdjList[edges[3 * i]].Add(edges[3 * i + 1]);
                _AdjList[edges[3 * i + 1]].Add(edges[3 * i]);

                _AdjMatrix[edges[3 * i], edges[3 * i + 1]] = edges[3 * i + 2];
                _AdjMatrix[edges[3 * i + 1], edges[3 * i]] = edges[3 * i + 2];

                components[2][edges[3 * i]]++;
                components[2][edges[3 * i + 1]]++;

                dsu.Union(edges[3 * i], edges[3 * i + 1]);
            }

            List<List<int>> paths = new List<List<int>>();

            for (int i = 0; i < NV; i++)
            {
                int parent = dsu.Find(i);

                if (components[0][parent] == -1)
                    components[0][parent] = componentsCount++;

                components[1][i] = components[0][parent];
            }

            for (int i = 0; i < NV; i++)
            {
                if (components[2][i] == 1 && !flags[i])
                {
                    int t = _AdjList[i][0];
                    List<int> tmpStack = new List<int>();
                    do
                    {
                        components[1][t] = -1;
                        flags[t] = true;
                        tmpStack.Add(t);
                        t = _AdjList[t][0] != t ? _AdjList[t][0] : _AdjList[t][1];
                    }
                    while (components[2][t] <= 2);

                    components[2][t]--;
                    paths.Add(tmpStack);
                }
            }

            return res;
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
