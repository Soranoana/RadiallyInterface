//Analysis.cs
using UnityEngine;

public class Analysis : MonoBehaviour {

    Mesh mesh;

    int[] verticesTrans = new int[24] { 0, 1, 2, 3, 4, 5, 6, 7, 2, 3, 4, 5, 6, 0, 1, 7, 1, 3, 5, 7, 6, 4, 2, 0 };

    void Start() {
        Debug.Log("このゲームオブジェクトの名前：" + gameObject.name);
        mesh = GetComponent<MeshFilter>().mesh;
        Debug.Log("メッシュの頂点数：" + mesh.vertexCount);
        for (int i = 0; i < mesh.vertexCount; i++) {
            Debug.Log("頂点番号：" + i + "　頂点座標：" + mesh.vertices[i]);
        }
        Debug.Log("ポリゴン数" + mesh.triangles.Length);
        for (int i = 0; i < mesh.triangles.Length; i += 3) {
            Debug.Log("ポリゴン番号：" + i / 3 + "　頂点番号：" + mesh.triangles[i] + " " + mesh.triangles[i + 1] + " " + mesh.triangles[i + 2] + " ");
        }
        Debug.Log("頂点番号を少なくする");
        int[] triangles = new int[mesh.triangles.Length];
        for (int i = 0; i < mesh.triangles.Length; i++) {
            triangles[i] = verticesTrans[mesh.triangles[i]];
        }
        GetComponent<MeshFilter>().mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh.RecalculateNormals();
    }

    void Update() {

    }
}
