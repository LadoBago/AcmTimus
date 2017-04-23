using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GraphDrawing
{
    private const int PXLS = 701;
    private const int MAX_VALUE = PXLS * PXLS;

    private int[,] _AdjMatrix;
    private List<int>[] _AdjList;
    private Random _R;
    private bool[,] _Pixels = new bool[PXLS, PXLS];

    public double maxK = 0;
    public int Cnt = 0;

    public GraphDrawing()
    {
        _R = new Random(DateTime.Now.Millisecond);
    }

    public int[] plot(int NV, int[] edges)
    {
        long startTicks = DateTime.Now.Ticks;
        int[] res = new int[2 * NV];
        _AdjMatrix = new int[NV, NV];
        _AdjList = new List<int>[NV];

        int edgesCount = edges.Length / 3;

        for (int i = 0; i < PXLS; i++)
            for (int j = 0; j < PXLS; j++)
                _Pixels[i, j] = false;

        for (int i = 0; i < NV; i++)
        {
            _AdjList[i] = new List<int>();
            for (int j = 0; j < NV; j++)
            {
                _AdjMatrix[i, j] = 0;
            }
        }

        double minRatio = double.MaxValue;
        double maxRatio = 0;
        int r = 0;
        int x = 0, y = 0;

        double desiredLength = 0, actualLength = 0, ratio = 0;

        for (int i = 0; i < edgesCount; i++)
        {
            _AdjList[edges[3 * i]].Add(edges[3 * i + 1]);
            _AdjList[edges[3 * i + 1]].Add(edges[3 * i]);

            _AdjMatrix[edges[3 * i], edges[3 * i + 1]] = edges[3 * i + 2];
            _AdjMatrix[edges[3 * i + 1], edges[3 * i]] = edges[3 * i + 2];
        }

        for (int i = 0; i < NV; i++)
            _AdjList[i].Sort();

        do
        {
            Cnt++;
            int[] tmpRes = new int[2 * NV];
            for (int i = 0; i < NV; i++)
            {
                BinSearch bs = new BinSearch(_AdjList[i]);
                BSResult bsRes = bs.Search(i);
                for (int j = 0; j <= bsRes.LeftIndex; j++)
                {
                    do
                    {
                        r = GetNumber();
                        y = r / PXLS;
                        x = r % PXLS;
                    } while (_Pixels[y, x]);
                    tmpRes[2 * i] = y;
                    tmpRes[2 * i + 1] = x;
                }
                _Pixels[y, x] = true;
            }

            for (int i = 0; i < edgesCount; i++)
            {
                desiredLength = edges[3 * i + 2];
                actualLength = GetDistance(tmpRes[2 * edges[3 * i]], tmpRes[2 * edges[3 * i] + 1], tmpRes[2 * edges[3 * i + 1]], tmpRes[2 * edges[3 * i + 1] + 1]);
                ratio = actualLength / desiredLength;

                if (ratio < minRatio)
                    minRatio = ratio;
                if (ratio > maxRatio)
                    maxRatio = ratio;
            }

            double tmpMaxK = minRatio / maxRatio;

            if (tmpMaxK > maxK)
            {
                res = tmpRes;
                maxK = tmpMaxK;
            }

            for (int i = 0; i < NV; i++)
                _Pixels[tmpRes[2 * i], tmpRes[2 * i + 1]] = false;

        } while (TimeSpan.FromTicks(DateTime.Now.Ticks - startTicks).TotalMilliseconds < 19950);

        return res;
    }

    private int GetNumber()
    {
        return _R.Next(MAX_VALUE);
    }

    private double GetDistance(int y1, int x1, int y2, int x2)
    {
        return Math.Sqrt((y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2));
    }
}

class BinSearch
{
    List<int> _Array;
    public BinSearch(List<int> array)
    {
        this._Array = array;
    }

    public BSResult Search(int obj)
    {
        return this.Search(obj, 0, this._Array.Count - 1);
    }
    public BSResult Search(int obj, int l, int r)
    {
        if (l > r)
            return new BSResult(-1, r);

        int t = (l + r) / 2;
        int c = obj.CompareTo(_Array[t]);
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
