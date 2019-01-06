using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh1 : MonoBehaviour
{
    public Material mat;
    public List<Vector3> hillseg = new List<Vector3>();

    float[] values = new float[65];
    

    // Use this for initialization
    void Start()
    {
        //Perlin();
        


        midpointbisecton();


        for(int i=0;i<65;i++)
 
           hillseg.Add(new Vector3(-6+ 0.2f*i,  values[i] , 0));

        // mat.color = Color.cyan;
        DrawTriangle();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void DrawTriangle()
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;


        int[] triangles = new int[(hillseg.Count-1) * 6];
        Vector3[] vertices = new Vector3[(hillseg.Count - 1) * 4];


        for (int i = 0; i < (hillseg.Count - 1); i++)
        {

            vertices[i * 4] = new Vector3(hillseg[i].x, -6, 0);
            vertices[i * 4 + 1] = hillseg[i];
            vertices[i * 4 + 2] = hillseg[i+1];
            vertices[i * 4 + 3] = new Vector3(hillseg[i+1].x, -6, 0);

            triangles[i * 6] = 0 + i * 4;
            triangles[i * 6 + 1] = 1 + i * 4;
            triangles[i * 6 + 2] = 2 + i * 4;
            triangles[i * 6 + 3] = 0 + i * 4;
            triangles[i * 6 + 4] = 2 + i * 4;
            triangles[i * 6 + 5] = 3 + i * 4;
        }


        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }




    void midpointbisecton() {
        //for (int i = 0; i < 17; i++)
            //values[i] = 111;
        values[0] = -5.5f;
        values[32] = 0.5f;
        values[64] = -5.5f;

        for (int i = 0; i < 2; i++)
        {
            values[i*32+16] =UnityEngine.Random.Range(Mathf.Min(values[i * 32 ], values[i * 32 + 32]) , Mathf.Max(values[i * 32], values[i * 32 + 32]));

        }

        for (int i = 0; i < 4; i++)
        {
            values[i * 16 + 8] = UnityEngine.Random.Range(Mathf.Min(values[i * 16], values[i * 16 + 16]) , Mathf.Max(values[i * 16], values[i * 16 + 16]));
        }

        for (int i = 0; i < 8; i++)
        {
            values[i * 8 + 4] = UnityEngine.Random.Range(Mathf.Min(values[i * 8], values[i * 8 + 8]) , Mathf.Max(values[i * 8], values[i * 8 + 8]) );
        }

        for (int i = 0; i < 16; i++)
        {
            values[i * 4 + 2] = UnityEngine.Random.Range(Mathf.Min(values[i * 4], values[i * 4 + 4])-0.1f, Mathf.Max(values[i * 4], values[i * 4 + 4]));
        }

        for (int i = 0; i < 32; i++)
        {
            values[i * 2 + 1] = UnityEngine.Random.Range(Mathf.Min(values[i * 2], values[i * 2 + 2] - 0.1f), Mathf.Max(values[i * 2], values[i * 2 + 2]));
        }

    }

    
 

}
