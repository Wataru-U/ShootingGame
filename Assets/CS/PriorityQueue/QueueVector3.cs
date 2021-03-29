using UnityEngine;

// イメージ
// 0  1  5  7
//       4
//    2  3
//       6

// 優先度付きキュー
// 距離に応じて優先度が変わる

// pullするとき最後尾と交代させてから並べ替えをすると途中で止まる可能性が出て処理が少し減るっぽい
// すでに出ているものをまた出す間違いもなさそう

public class QueueVector3
{
    private Vector3 pos;
    private Vector3[] posList;
    private Vector2[] disList; //x 距離の２乗　y番号
    private int count;
    public int Length => count;
    private Vector3 average;
    public Vector3 Average => average / count;
    public float AverageDis => Distance(Average);

    //配列の長さは長めに作る
    public QueueVector3(int num, Vector3 p)  // 配列の長さと現在地の設定
    {
        pos = p;
        posList = new Vector3[num];
        disList = new Vector2[num];
        count = 0;
        average = Vector3.zero;
    }

    public void Add(Vector3 p)
    {
        posList[count] = p;
        Vector2 dis = new Vector2(0, count);
        dis.x = Distance(p);
        disList[count] = dis;
        average += p;

        //swap
        if (!(count == 0))
        {
            int parent = (count - 1) / 2;
            int num = count;
            while (parent >= 0)
            {
                if (disList[num].x < disList[parent].x) // 親と距離を比べて近かったら交換
                {
                    Swap(num, parent);
                    num = parent;
                    parent = (num - 1) / 2;
                }
                else
                    break;
            }
        }
        count++;
    }

    public Vector3 Pull()
    {
        int resultNum = 0;
        int num = 0;
        int child = 0;
        while (child < Length)
        {
            if (2 * (num + 1) >= Length) //配列の端の時
                child = 2 * num + 1;
            else if (disList[2 * num + 1].x < disList[2 * (num + 1)].x)
                child = 2 * num + 1;
            else
                child = 2 * (num + 1);
            Swap(num, child);
            num = child;

            if (num * 2 + 1 > Length)
                break;
        }
        resultNum = (int)disList[num].y;
        disList[num].x = 10000000; // 小さいものをとるので大きくしておく


        return posList[resultNum];
    }

    //いつ入れたものかだけ取りだす　配列は壊れる
    public int PullNum()
    {
        int num = 0;
        int child = 0;
        while (child < Length)
        {
            if (2 * (num + 1) >= Length)
                child = 2 * num + 1;
            else if (disList[2 * num + 1].x < disList[2 * (num + 1)].x)
                child = 2 * num + 1;
            else
                child = 2 * (num + 1);
            Swap(num, child);
            num = child;

            if (num * 2 + 1 > Length)
                break;
        }
        disList[num].x = 10000000;

        return (int)disList[num].y;
    }

    public Vector2 PullDis()
    {
        int num = 0;
        int child = 0;
        while (child < Length)
        {
            if (2 * (num + 1) >= Length)
                child = 2 * num + 1;
            else if (disList[2 * num + 1].x < disList[2 * (num + 1)].x)
                child = 2 * num + 1;
            else
                child = 2 * (num + 1);
            Swap(num, child);
            num = child;

            if (num * 2 + 1 > Length)
                break;
        }
        Vector2 dis = disList[num];
        disList[num].x = 10000000;

        return dis;
    }

    private float Distance(Vector3 p)
    {
        float x = (p.x - pos.x) * (p.x - pos.x);
        float y = (p.y - pos.y) * (p.y - pos.y);
        float z = (p.z - pos.z) * (p.z - pos.z);
        return x + y + z;
    }

    private void Swap(int a, int b)
    {
        Vector2 p = disList[a];
        disList[a] = disList[b];
        disList[b] = p;
    }
}
