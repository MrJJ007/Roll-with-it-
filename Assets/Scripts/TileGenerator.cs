using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public int Size;
    public Transform Block;
    public Transform Sphere;
    public Transform Enemy;
    public Transform Button;

    // Start is called before the first frame update
    void Start()
    {
        generateTile();
    }
    public void generateTile()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int p = 0; p < Size; p++)
            {   //create the base level dotn want this coloured
                Instantiate(Block, new Vector3(p, -1, i), Quaternion.identity);

                //create the main part of the level this is to be coloured
                int height = Random.Range(0, 4);
                Transform blockA = Instantiate(Block, new Vector3(p, height, i), Quaternion.identity);
                
                //colour the main part of the level
                GameObject cube = blockA.gameObject;
                var cubeRenderer = cube.GetComponent<Renderer>();
                switch (height)
                {
                    case 0:
                        cubeRenderer.material.SetColor("_Color", Color.grey);
                        break;
                    case 1:
                        cubeRenderer.material.SetColor("_Color", Color.green);
                        break;
                    case 2:
                        cubeRenderer.material.SetColor("_Color", Color.red);
                        break;
                    case 3:
                        cubeRenderer.material.SetColor("_Color", Color.blue);
                        break;
                }
                
            }
            
        }
        for (int i = 0; i < 3; i++)
        {   //place spheres and then enemies around the level
            Instantiate(Sphere, new Vector3(Random.Range(1, 19), Random.Range(1, 2), Random.Range(1, 19)), Quaternion.identity);
            Instantiate(Enemy, new Vector3(Random.Range(1, 19), Random.Range(3, 4), Random.Range(1, 19)), Quaternion.identity);
        }
        Button.Translate(new Vector3(3,2,2));
        //Button.Translate(new Vector3(Random.Range(1, 19), Random.Range(1, 2), Random.Range(1, 19)));
    }
    public void deleteCubes()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach (GameObject cube in cubes)
        {
            GameObject.Destroy(cube);
        }
    }
    
}
