using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    public float numberOfMount = 0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];


        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

                //float y = yGenerator(i,x);
                float y = Mathf.PerlinNoise(x * 1f, z * .3f) * 4f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }
        mountainGenerator();
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        for (int p = 0; p < zSize; p++)
        {
            for (int i = 0; i < xSize; i++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }


    }
    float mountainGenerator()
    {

        int mountainCore = Random.Range(200, 40000);
        int mountainSize = Random.Range(5,10);
        float mountainHeight = Random.Range(10, 15);
        Debug.Log(mountainCore);
        vertices[mountainCore].y = mountainHeight;
        vertices[mountainCore+1].y = mountainHeight-1;
        vertices[mountainCore-200].y = mountainHeight - 1;
        vertices[mountainCore-1].y = mountainHeight - 1;
        vertices[mountainCore-1].y = mountainHeight - 1;
        vertices[mountainCore+200].y = mountainHeight - 1;
        vertices[mountainCore+200].y = mountainHeight - 1;
        vertices[mountainCore+1].y = mountainHeight - 1;
        vertices[mountainCore+1].y = mountainHeight - 1;

        vertices[mountainCore+1].y = mountainHeight - 2;
        for (int i = 0; i < 2; i++)
        {
            vertices[mountainCore -200].y = mountainHeight - 2;
        }
        for (int i = 0; i < 3; i++)
        {
            vertices[mountainCore - 1].y = mountainHeight - 2;
        }
        for (int i = 0; i < 3; i++)
        {
            vertices[mountainCore + 200].y = mountainHeight - 2;
        }
        for (int i = 0; i < 3; i++)
        {
            vertices[mountainCore + 1].y = mountainHeight - 2;
        }
        //vertices[mountainCore + 200 + 1].y = mountainHeight;
        //vertices[mountainCore + 1].y = mountainHeight;
        //vertices[mountainCore + 1].y = mountainHeight;
        //vertices[mountainCore + 200 + 1].y = mountainHeight;
        //vertices[mountainCore + 200 + 2].y = mountainHeight;

        //for (int i = 0; i < mountainSize; i++)
        //{

        //    for (int p = 0; p < mountainSize; p++)
        //    {

        //        vertices[mountainCore - i].y = mountainHeight;
        //        vertices[mountainCore + i].y = mountainHeight;
        //        vertices[mountainCore +  200*i].y = mountainHeight;
        //        vertices[mountainCore -  200*i].y = mountainHeight;

        //    }
        //}

        return mountainCore;
    }
    //float yGenerator(int i, int x)
    //{
    //    float y = 0;
    //    float heightOfLeft = vertices[i].y;
    //    float heightOfBehind = vertices[i].y;
    //    int heightMod = Random.Range(0, 4);
    //    float chanceToHit = 1;

    //    //deteremines the height value of the last made points
    //    if (i == 0)
    //    {
    //        heightOfLeft = vertices[i].y;
    //        heightOfBehind = vertices[i].y;
    //    }
    //    else if (x >= 201)
    //    {
    //        heightOfLeft = vertices[i - 1].y;
    //        heightOfBehind = vertices[i - 199].y;
    //    }
    //    else
    //    {
    //        heightOfLeft = vertices[i - 1].y;
    //    }

    //    //give you a chance
    //    if (heightOfLeft <= 0.5 | heightOfBehind <= 0.5)
    //    {
    //        chanceToHit = 0 + numberOfMount;
    //    }
    //    else if (heightOfLeft > 0.5 && heightOfLeft <= 1.0 | heightOfBehind > 0.5 && heightOfBehind <= 1.0)
    //    {
    //        chanceToHit = 0 + numberOfMount;
    //    }
    //    else if (heightOfLeft > 1.0 && heightOfLeft <= 1.5 | heightOfBehind > 1.0 && heightOfBehind <= 1.5)
    //    {
    //        chanceToHit = 0 + numberOfMount;
    //    }
    //    else if (heightOfLeft > 1.5 && heightOfLeft <= 2.0 | heightOfBehind > 1.5 && heightOfBehind <= 2.0)
    //    {
    //        chanceToHit = 1 + numberOfMount;
    //    }
    //    else if (heightOfLeft > 2.0 && heightOfLeft <= 2.5 | heightOfBehind > 2.0 && heightOfBehind <= 2.5)
    //    {
    //        chanceToHit = 5 + numberOfMount;
    //    }
    //    else if (heightOfLeft > 2.5 && heightOfLeft <= 3.0 | heightOfBehind > 2.5 && heightOfBehind <= 3.0)
    //    {
    //        chanceToHit = 15 + numberOfMount;
    //    }
    //    else if (heightOfLeft > 3.0 && heightOfLeft <= 3.5 | heightOfBehind > 3.0 && heightOfBehind <= 3.5)
    //    {
    //        chanceToHit = 50 + numberOfMount;
    //    }
    //    else if (heightOfLeft > 3.5 && heightOfLeft <= 4.0 | heightOfBehind > 3.5 && heightOfBehind <= 4.0)
    //    {
    //        chanceToHit = 100 + numberOfMount;
    //    }

    //    float chance = Random.Range(0, chanceToHit);


    //    if (i == 0 && x == 0)
    //    {
    //        y = (Mathf.PerlinNoise(x * 1f, i * .3f) * 3f);
    //        return y;
    //    }
    //    else
    //    {
    //        if (chance >= 1)
    //        {
    //            y = (Mathf.PerlinNoise(x * 1f, i * .3f) * 2f) * 0.3f + 4f + (heightOfBehind / 2) + (heightOfLeft / 2);
    //            Debug.Log("chance hit ");
    //            return y;

    //        }
    //        y = Mathf.PerlinNoise(x * .3f, i * .3f) * 1f;
    //        return y;
    //    }
    //    //y = Mathf.PerlinNoise(x * 1f, i * .3f) * 1f;

    //}

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }
    //private void OnDrawGizmos()
    //{
    //    if (vertices == null)
    //        return;
    //    Gizmos.DrawSphere(vertices[405], 0.5f);
    //    Gizmos.DrawSphere(vertices[404], 0.5f);
    //    Gizmos.DrawSphere(vertices[204], 0.5f);
    //}
}
