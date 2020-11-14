using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimxNoise;


public class MapGeneratorScript : MonoBehaviour
{
    public SimpleSimplex noiseScript = new SimpleSimplex();
    double noise;
    public Transform Land;
    public Transform Sea;
    public Transform Mountain;
    public Transform Desert;
    public int mapSize;
    public float seaLevel;
    public float mountAmount;
    private float iMod;
    private float pMod;
    GameObject[] mapPieces;
    Vector3[] mapPiecesPos;
    

    public enum MapType { Continents, Archipelago }
    public MapType mapType;
    // Start is called before the first frame update

    public  double GetNoise(float x, float y, double scale, int max)
    {
        noise = ((noiseScript.noise2D(x * scale, y * scale) + 1.0) * (max / 2.0));
        Debug.Log(noise);
        return noise;

    }

    void Start()
    {
        GenerateMap(mapType);
        int x = 1;
        int y = 2;
        double scale = 4;
        int max = 10;
        GetNoise(x,y,scale,max);
    }

    public void GenerateMap(MapType mapType)
    {   //we set presets for the map type so that we can make defined differences in them
        switch (mapType.ToString())
        {
            case "Continents":
                iMod = Random.Range(.01f,0.03f);
                pMod = Random.Range(.01f, 0.03f);
                break;
            case "Archipelago":
                iMod = .1f;
                pMod = .1f;
                break;
            default:
                break;
        }
        Debug.Log(iMod + " " + pMod);
        //this section is the base plate for the map generation
        //goes over the x coords
        for (int i = 0; i < mapSize; i++)
        {   
            // goes over the y coords
            for (int p = 0; p < mapSize; p++)
            {
                float random = Random.Range(0.15f,0.16f);
                //double scale = 10.067;
                //int max = 6;
                //determine land from sea
                //the larger the mupliyer is the long the lines in that direction are to get continent shapes you need small numbers
                //0.1f and 0.1f is good with a sea height of 6 makes an archiepeglo
                //-.2f and -.2f with height of 6 very small islands
                float num = ((Mathf.PerlinNoise((i * iMod)+random, (p * pMod)-random) * seaLevel));
                //double num = GetNoise((i * (iMod+10.03f)) + random, ((p * pMod/2) + random), scale, max);
                //Debug.Log(num);
                if (num <= Random.Range(1.2f,2.2f))//1.4f for perlin noise
                {   //determines land from mountains
                    float mountNum = (Mathf.PerlinNoise(i * 0.1f, p * 0.1f) * mountAmount);
                    //Debug.Log(mountNum);
                    if (mountNum <= 0.5f)//reverse the in equality to create coast funciton
                    {
                        Instantiate(Mountain, new Vector3(i, 1, p), Quaternion.identity);
                    }
                    if (mountNum >= 1.5f)
                    {
                        Instantiate(Desert, new Vector3(i, 1, p), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Land, new Vector3(i, 1, p), Quaternion.identity);
                    }

                }
                else
                {
                    Instantiate(Sea, new Vector3(i, 1, p), Quaternion.identity);
                }
            }
        }

        //this is the second phase of map generation is redactive or additive depending on map type
        mapPieces = GameObject.FindGameObjectsWithTag("MapPiece");
        //mapPieces[randomPos].name = "fred";
        for (int y = 0; y < 10; y++)
        {
            int posRandom = Random.Range(1010, (mapPieces.Length-1010));
            //Debug.Log(mapPieces[posRandom].transform.position);
            //Debug.Log(mapPieces[posRandom].name);
            // we need a switch here becasue different map types have different charactics
            switch (mapType.ToString())
            {
                case "Continents":
                    switch (mapPieces[posRandom].name)
                    {   //this is addititve
                        case "Sea(Clone)":
                            for (int i = 0; i < Random.Range(8,13); i++)
                            {
                                int p=0;
                                for (p = 0; p < Random.Range(8, 13); p++)
                                {   // this makes an small archipelago
                                    float num = (Mathf.PerlinNoise(i * (Mathf.Sqrt(posRandom)/2f), p * (Mathf.Sqrt(posRandom)/3f)) * 3f);
                                    if (num <=1.2f)
                                    {
                                        GameObject.Destroy(mapPieces[posRandom + (100 * p) + i]);
                                        Instantiate(Land, mapPieces[posRandom + (100 * p) + i].transform.position, Quaternion.identity);
                                    }
                                    if (num >= 2.3f)
                                    {
                                        GameObject.Destroy(mapPieces[posRandom + (100 * p) + i]);
                                        Instantiate(Mountain, mapPieces[posRandom + (100 * p) + i].transform.position, Quaternion.identity);
                                    }
                                }
                            }
                            break;
                            //this is redative
                        case "Land(Clone)":
                            for (int i = 0; i < 10; i++)
                            {
                                int p = 0;
                                //float pNum = RandomCheck(i,p,posRandom);
                                for (p = 0; p < 10; p++)
                                {
                                    float num = (Mathf.PerlinNoise(i * (Mathf.Sqrt(posRandom) / 2f), p * (Mathf.Sqrt(posRandom) / 3f)) * 3f);
                                    if (num <= 1.2f)
                                    {
                                        GameObject.Destroy(mapPieces[posRandom + (100 * p) + i]);
                                        Instantiate(Sea, mapPieces[posRandom + (100 * p) + i].transform.position, Quaternion.identity);
                                    }

                                }

                            }
                            //mapPieces[posRandom].name = "fred";
                            //Debug.Log(mapPieces[posRandom].transform.position + " new");
                            //Debug.Log(mapPieces[posRandom].name + " new");
                            break;
                        default:
                            break;
                    }


                    break;
                case "Archipelago":

                    break;
                default:
                    break;
            }
        }
    }

    //public int RandomCheck(int i ,int p, int posRandom)
    //{
    //    int pNum = Random.Range(3, 10);
    //    if (posRandom + (100 * p) + i < 10000)
    //    {
    //        return pNum;
    //    }
    //    else
    //    {
    //        return RandomCheck(i, p, posRandom);
    //    }
    //}
    //public (Array, Array) UpdateArrays()
    //{
    //    mapPieces = GameObject.FindGameObjectsWithTag("MapPiece");
    //    for (int i = 0; i < mapPieces.Length; i++)
    //    {
    //        mapPiecesPos = mapPieces[i].Transform.Position;
    //    }
    //    return mapPieces,  mapPiecesPos;
    //}



    public void DeleteAll()
    {
        
        foreach (GameObject mapPiece in mapPieces)
        {
           
            switch (mapPiece.name)
            {
                case "Sea":
                    break;
                case "Land":
                    break;
                case "Mountain":
                    break;
                case "Coast":
                    break;
                default:
                    GameObject.Destroy(mapPiece);
                    break;
            }
        }
    }
    IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    //public void PlaceTile(int i, int p)
    //{
    //    int num = Random.Range(0, 99);
    //    if (num <= 50)
    //    {
    //        Instantiate(Land, new Vector3(i, 1, p), Quaternion.identity);
    //    }
    //    else
    //    {
    //        Instantiate(Sea, new Vector3(i, 1, p), Quaternion.identity);
    //    }

    //}
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            DeleteAll();
            GenerateMap(mapType);
        }
    }
}
