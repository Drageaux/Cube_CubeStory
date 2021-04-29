using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShapedArea : MonoBehaviour
{
    private GameObject go;
    private MeshFilter mf;
    private MeshRenderer mr;
    public Shader shader;
    public int num_wolf = 1;
    // Update is called once per frame

    void Update()
    {
        
         ToDrawSectorSolid(transform, transform.localPosition, 100, 10);//if change value, change Player Detection.cs Wolf as well

    }

    private GameObject CreateMesh(List<Vector3> vertices,float yPos)

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
            go = new GameObject("mesh"+num_wolf);
            go.transform.position = new Vector3(0, 1.0f+yPos, 0);
            mf = go.AddComponent<MeshFilter>();
            mr = go.AddComponent<MeshRenderer>();
            //shader = Shader.Find("UniGLTF/UniUnlit");
            

        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mf.mesh = mesh;
        mr.material.shader = shader;
        //mr.material.color = Color.red;
        mr.material.color = new Color32(255,0,0,80);
        return go;
    }  

    //draw sector solid        
    public void ToDrawSectorSolid(Transform t, Vector3 center, float angle, float radius)  

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
        //Debug.Log(center.y);
        CreateMesh(vertices,0);  
    }

    private void OnDestroy()
    {
        Destroy(go);
    }
}
