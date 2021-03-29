
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//昇順のソート(int型)で実験

public class sort : MonoBehaviour
{
    int num;
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

    string str;
    // Start is called before the first frame update

    //最初からついてるソートは強かった。(クイックソート？)(nlogn?)
    //一回ソートでは標準が早いが一部取り出してまた入れてソートし直すと優先度付きが早い
    //何回も取り出すものは優先度付きキュー

    //基数ソートの配列を減らせばもう少し早くなるかも
    //lon2(n) > logn(Max/min)　なら基数
    //桁数がわかっていてデータが多いと基数

    //バケットソートが爆速
    //整数ならバケットソート
    //データ数が少なくて範囲が大きいとインスタンス生成に時間がかかるため向かな

    //データの範囲がわからない && １回しか出さないのであれば標準のソートを使用する
    void Start()
    {
        num = 1000000;
        sw.Start();
        PriorityQueue _queue = new PriorityQueue(num * 2);
        for (int i = 0; i < num; i++)
        {
            _queue.Push(Random.Range(0, 10));
        }
        int n = 0;
        for (int i = 0; i < num; i++)
        {
            n = _queue.Pull();
        }
        sw.Stop();
        Debug.Log("優先度付きキュー:" + sw.ElapsedMilliseconds + "ms");

        sw.Reset();
        sw.Start();
        int[] randomArray = new int[num];
        for (int i = 0; i < num; i++)
        {
            randomArray[i] = Random.Range(0, 10);
        }
        System.Array.Sort(randomArray);
        sw.Stop();
        Debug.Log("標準:" + sw.ElapsedMilliseconds + "ms");

        sw.Reset();
        sw.Start();
        BucketSort _bucket = new BucketSort(num, 1);
        for (int i = 0; i < num; i++)
        {
            _bucket.Add(Random.Range(0, 10));
        }
        _bucket.Sort();
        sw.Stop();
        Debug.Log("基数:" + sw.ElapsedMilliseconds + "ms");


        sw.Reset();
        sw.Start();
        Bucket _b = new Bucket(num, 0, 10);
        for (int i = 0; i < num; i++)
        {
            _b.Add(Random.Range(0, 10));
        }
        int[] s = _b.sortedArray();
        sw.Stop();
        Debug.Log("バケット:" + sw.ElapsedMilliseconds + "ms");
        string str = ""; for (int i = 0; i < 100; i++)
            str += s[i] + " ";
        Debug.Log(str);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

//優先度付きキュー
public class PriorityQueue
{
    private int[] array;
    private int count = 0;
    public int Length => count;
    private int max;
    public PriorityQueue(int max)
    {
        array = new int[max];
        this.max = max;
    }

    public void Push(int value)
    {
        if (count != max - 1)
        {
            array[count] = value;
            if (count != 0)
            {
                int parent = (count - 1) / 2;
                int num = count;
                while (num > 0)
                {
                    if (array[num] < array[parent])
                        Swap(num, parent);
                    else
                        break;

                    num = parent;
                    parent = (num - 1) / 2;
                }
            }
            count++;
        }
        else
            Debug.Log("配列オーバー");
    }

    public int Pull()
    {
        int result = array[0];
        count--;
        if (count >= 0)
        {
            Swap(0, count);
            int num = 0;
            int child = ChildComp(num);
            while (num < count)
            {
                if (child >= count)
                    break;
                else if (array[num] < array[child])
                    break;
                Swap(num, child);
                num = child;
                child = ChildComp(num);
            }
        }
        return result;
    }
    private int ChildComp(int num)
    {
        int result = 0;
        int childlen = 2 * (num + 1);
        if (childlen >= count - 2)
            result = childlen - 1;
        else if (array[childlen] < array[childlen - 1])
            result = childlen;
        else
            result = childlen - 1;
        return result;
    }
    private void Swap(int n, int m)
    {
        int value = array[n];
        array[n] = array[m];
        array[m] = value;
    }
}
// 負の数未対応
// 基数ソートもどき
// add する時に１桁目割り振った方がいいかも
public class BucketSort
{
    public int[] array;
    private int[,,] bukcket;
    private int[,] bukcketNum;
    private int count = 0;
    private int Length => count;
    public int Digit;

    public BucketSort(int length, int digit)
    {
        array = new int[length];
        bukcket = new int[digit, 10, length];
        bukcketNum = new int[digit, 10];
        this.Digit = digit;
    }
    public BucketSort(int length, int digit, int[] a)
    {
        array = new int[length];
        bukcket = new int[digit, 10, length];
        bukcketNum = new int[digit, 10];
        for (int i = 0; i < a.Length; i++)
        { array[i] = a[i]; }
        count = a.Length;
        this.Digit = digit;
        Debug.Log(bukcket.GetLength(2));
    }
    public void Add(int a)
    {
        array[count] = a;
        count++;
    }

    public void AddArray(int[] a)
    {
        int num = a.Length;
        for (int i = count; i < count + num; i++)
        { array[i] += a[i]; }
        count += num;
    }

    public void Sort()
    {
        for (int i = 0; i < Digit; i++)
        { Divide(i); }
        int c = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < bukcketNum[Digit - 1, i]; j++)
            {
                array[c] = bukcket[Digit - 1, i, j];
                c++;
            }
        }
    }
    private void Divide(int digit)
    {
        int pow10 = (int)Mathf.Pow(10, digit);
        if (digit != 0)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < bukcketNum[digit - 1, i]; j++)
                {
                    int k = (bukcket[digit - 1, i, j] / pow10) % 10;
                    bukcket[digit, k, bukcketNum[digit, k]] = bukcket[digit - 1, i, j];
                    bukcketNum[digit, k] += 1;
                }
            }
        }
        else if (digit == 0)
        {
            for (int i = 0; i < count; i++)
            {
                int j = array[i] % 10;
                bukcket[0, j, bukcketNum[0, j]] = array[i];
                bukcketNum[0, j]++;
            }
        }
    }


}

// こっちが本当のバケットソート
public class Bucket
{
    private int[] array;
    private int[] bucket;
    private int min;
    private int max;
    private int range => max - min + 1;
    private int count;
    public int Length => count;
    public Bucket(int num, int min, int max)
    {
        this.min = min;
        this.max = max;
        array = new int[num];
        bucket = new int[range];
    }

    public void Add(int v)
    {
        bucket[value(v)]++;
        count++;
    }
    public void AddRange(int[] v)
    {
        foreach (var item in v)
        {
            Add(item);
        }
    }

    public int[] sortedArray()
    {
        int[] result = new int[count];
        int c = 0;
        for (int i = 0; i < range; i++)
        {

            if (bucket[i] != 0)
            {
                int l = bucket[i];
                while (l > 0)
                {
                    l--;
                    result[c] = i;
                    c++;
                }
            }
            if (c == count)
                break;
        }
        return result;
    }

    private int value(int v)
    {
        return v - min;
    }

}
