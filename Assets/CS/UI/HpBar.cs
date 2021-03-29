using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// hpバーの描画
// ポリゴン2つ使って描いてる
public class HpBar : MonoBehaviour
{
    public GameObject _hive;
    hive _hiveSqript;
    Material _mat;
    Color color;
    int stastusFlag = 0b_01;
    public Vector3 hpBarPos;
    public float hpBarWidth;
    private Vector3 hpBarWidth_Vec3;
    public Vector3 hpBarHeight;

    Vector3[] pos = new Vector3[4];
    // Start is called before the first frame update
    void Start()
    {
        _hiveSqript = _hive.GetComponent<hive>();
        pos[0] = hpBarPos;
        pos[1] = hpBarPos + hpBarHeight;
        color = new Color(0, 0.7f, 0.1f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        drawHPBar();
        StatusChenged();
    }
    void drawHPBar()
    {
        hpBarWidth_Vec3 = new Vector3(_hiveSqript.hiveHpPer * hpBarWidth, 0, 0);
        pos[2] = hpBarPos + hpBarWidth_Vec3;
        pos[3] = hpBarPos + hpBarWidth_Vec3 + hpBarHeight;

        var mesh = new Mesh();
        var vertices = new List<Vector3>();
        vertices.AddRange(pos);
        var triangles = new List<int>();

        triangles.Add(1);
        triangles.Add(3);
        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(2);
        triangles.Add(0);

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();
        _mat = this.GetComponent<Renderer>().material;
        Graphics.DrawMesh(mesh, transform.position, transform.rotation, _mat, 0);
        Resources.UnloadUnusedAssets();
    }

    public virtual void StatusChenged() // プレイヤーと同じ
    {
        if ((stastusFlag & 0b_001) == 0b_001)
        {
            this._mat.SetColor("_Color", color);
            stastusFlag--;
            stastusFlag <<= 1;
        }
        color = new Color(0, 1, 0, 1);
        if (_hiveSqript.hiveHpPer > 0.5f)
        { stastusFlag |= 0b_010; }
        if (_hiveSqript.hiveHpPer < 0.5f && _hiveSqript.hiveHpPer > 0.20f)
        {
            color = new Color(1, 1, 0, 1);
            if ((stastusFlag & 0b_100) == 0b_000)
            { stastusFlag |= 0b_001; }
        }
        if (_hiveSqript.hiveHpPer < 0.20f)
        {
            color = new Color(1, 0, 0, 1);
            if ((stastusFlag & 0b_1_000) == 0b_0_000)
            { stastusFlag |= 0b_001; }
        }
    }
}
