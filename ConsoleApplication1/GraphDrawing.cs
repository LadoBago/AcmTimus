using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GraphDrawing
{
    private const int PXLS = 701;
    private const int MAX_VALUE = PXLS * PXLS;

    private double[,] _AdjMatrix;
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
        _AdjMatrix = new double[NV, NV];
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
        int x = 0, y = 0, bestX = 0, bestY = 0;

        double ratio = 0, tmpRatio = 0, badRatio = 0, bestRatio = 0;

        for (int i = 0; i < edgesCount; i++)
        {
            _AdjList[edges[3 * i]].Add(edges[3 * i + 1]);
            _AdjList[edges[3 * i + 1]].Add(edges[3 * i]);

            _AdjMatrix[edges[3 * i], edges[3 * i + 1]] = edges[3 * i + 2];
            _AdjMatrix[edges[3 * i + 1], edges[3 * i]] = edges[3 * i + 2];
        }

        for (int i = 0; i < NV; i++)
            _AdjList[i].Sort();

        int[] tmpRes = new int[2 * NV];
        int cx = 0; int cy = 0, tmpInt = 0, leftBound = 0, rightBound = 0, xCounter = 0;
        double distance = 0, tmpDouble = 0;
        do
        {
            Cnt++;
            tmpRes = new int[2 * NV];
            ratio = 0.9D +_R.NextDouble() * 0.2D;
            tmpRatio = 0;

            for (int i = 0; i < NV; i++)
            {
                BinSearch bs = new BinSearch(_AdjList[i]);
                BSResult bsRes = bs.Search(i);
                bestRatio = double.MaxValue;
                if (bsRes.LeftIndex >= 0)
                {
                    cy = tmpRes[2 * _AdjList[i][0]];
                    cx = tmpRes[2 * _AdjList[i][0] + 1];
                    tmpDouble = Math.Sqrt(Math.Abs(1D - ratio));
                    distance = _AdjMatrix[i, _AdjList[i][0]] * (1D - tmpDouble + _R.NextDouble() * 2 * tmpDouble);
                    leftBound = (int)Math.Max(0, cx - distance);
                    rightBound = (int)Math.Min(700, cx + distance);

                    for (int l = 0; l < 1500; l++)
                    {
                        xCounter = 0;
                        do
                        {
                            xCounter++;
                            x = (int)Math.Round(leftBound + _R.NextDouble() * (rightBound - leftBound));
                            tmpInt = GetY2(cx, x, distance);
                            y = cy + tmpInt * xCounter % 2 == 0 ? 1 : -1;
                            if (y < 0 || y > 700 || _Pixels[y, x])
                                y = cy + tmpInt * xCounter % 2 == 0 ? -1 : 1;
                        } while ((y < 0 || y > 700 || _Pixels[y, x]) && xCounter <= 701);

                        if (xCounter > 701)
                            break;

                        badRatio = ratio;
                        for (int j = bsRes.LeftIndex; j >= 0; j--)
                        {
                            tmpRatio = GetDistance(y, x, tmpRes[2 * _AdjList[i][j]], tmpRes[2 * _AdjList[i][j] + 1]) / _AdjMatrix[i, _AdjList[i][j]];
                            if (Math.Abs(tmpRatio - ratio) > Math.Abs(badRatio - ratio))
                                badRatio = tmpRatio;
                        }

                        if (Math.Abs(badRatio - ratio) < Math.Abs(bestRatio - ratio))
                        {
                            bestRatio = badRatio;
                            bestY = y;
                            bestX = x;
                        }
                    }

                    if (xCounter > 701)
                        break;

                    if (bestRatio < minRatio)
                    {
                        minRatio = bestRatio;
                        if (minRatio / maxRatio < maxK)
                            break;
                    }
                    if (bestRatio > maxRatio)
                    {
                        maxRatio = bestRatio;
                        if (minRatio / maxRatio < maxK)
                            break;
                    }
                }
                else
                {
                    do
                    {
                        r = GetNumber();
                        bestY = r / PXLS;
                        bestX = r % PXLS;
                    } while (_Pixels[bestY, bestX]);
                }

                tmpRes[2 * i] = bestY;
                tmpRes[2 * i + 1] = bestX;

                _Pixels[bestY, bestX] = true;
            }

            if (xCounter <= 701)
            {
                double tmpMaxK = minRatio / maxRatio;

                if (tmpMaxK > maxK)
                {
                    res = tmpRes;
                    maxK = tmpMaxK;
                }
            }
            for (int i = 0; i < NV; i++)
                _Pixels[tmpRes[2 * i], tmpRes[2 * i + 1]] = false;

        } while (TimeSpan.FromTicks(DateTime.Now.Ticks - startTicks).TotalMilliseconds < 9900);

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

    private int GetY2(int x1, int x2, double distance)
    {
        return (int)Math.Round(Math.Sqrt(distance * distance - (x1 - x2) * (x1 - x2)));
    }
}

class BinSearch
{
    List<int> _Array;
    public BinSearch(List<int> array)
    {
        _Array = array;
    }

    public BSResult Search(int obj)
    {
        return Search(obj, 0, _Array.Count - 1);
    }
    public BSResult Search(int obj, int l, int r)
    {
        if (l > r)
            if (l < _Array.Count && obj.CompareTo(_Array[l]) > 0)
                    return new BSResult(-1, l);
            else
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
    public int Index;
    public int LeftIndex;

    public BSResult(int index, int leftIndex)
    {
        this.Index = index;
        this.LeftIndex = leftIndex;
    }
}
