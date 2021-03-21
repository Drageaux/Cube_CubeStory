using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShapedArea : MonoBehaviour
{
    public static GameObject go;
    public static MeshFilter mf;
    public static MeshRenderer mr;
    public static Shader shader;
    // Update is called once per frame

    void Update()
    {
        
         ToDrawSectorSolid(transform, transform.localPosition, 90, 5);//if change value, change Player Detection.cs Wolf as well

    }

    private static GameObject CreateMesh(List<Vector3> vertices)

    {
        int[] triangles;
        Mesh mesh = new Mesh();
        int triangleAmount = vertices.Count - 2;
        triangles = new int[3 * triangleAmount];

        for (int i = 0; i < triangleAmount; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }

        if (go == null)

        {
            go = new GameObject("mesh");
            go.transform.position = new Vector3(0, 0.1f, 0);
            mf = go.AddComponent<MeshFilter>();
            mr = go.AddComponent<MeshRenderer>();
            shader = Shader.Find("Unlit/Color");

        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mf.mesh = mesh;
        mr.material.shader = shader;
        mr.material.color = Color.red;
        return go;
    }  

    //绘制实心扇形        
    public static void ToDrawSectorSolid(Transform t, Vector3 center, float angle, float radius)  

    {  

        int pointAmount = 100;
        float eachAngle = angle / pointAmount;
        Vector3 forward = t.forward;
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add(center);  

        for (int i = 1; i<pointAmount - 1; i++)
        {  
            Vector3 pos = Quaternion.Euler(0f, -angle / 2 + eachAngle * (i - 1), 0f) * forward * radius + center;
            vertices.Add(pos);  
        }
        CreateMesh(vertices);  
    }  
}
