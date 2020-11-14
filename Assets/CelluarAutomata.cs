using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelluarAutomata : MonoBehaviour
{
    public Transform Sea;
    public Transform Land;
    public Transform Mountain;
    public Transform Desert;
    public Transform Hill;
    public int width;
    public int height;
    public int deathNumber;
    public int birthNumber;
    public int NumOfSteps;
    public float chanceToStartAlive;
    //for archelpeligo map death 2 birth 3 step 3 chance 0.6
    //for continents map ditto step 12 chance 0.5 sometimes creates pangea
    
    //1 sea, 0 land, 2 mountain, 3 desert
    
    // Start is called before the first frame update
    void Start()
    {
        generateMap();
    }

    public void generateMap()
    {
        int[,] map = new int[width, height];
        map = initialiseMap(map);
        for (int i = 0; i < NumOfSteps; i++)
        {
            map = doSimulationStep(map);
        }
        
        map = drawLivery(map);
        map = drawEdges(map);
        drawMap(map);
    }
    //this draws a messy amount of mountains to the map to later be cleaned up
    public int[,] drawLivery(int[,] map)
    {
        Debug.Log("smelly");
        float i1 = Random.value;
        float p1 = Random.value;
        for (int i = 0; i < width; i++)
        {
            for (int p = 0; p < height; p++)
            {

                //float num = Mathf.PerlinNoise(Random.Range(0.01f,0.99f), Random.Range(0.01f, 0.99f));
                float num = Mathf.PerlinNoise(i1+i/(float)width*100f,p1+p/(float)height*100f);
                Debug.Log(num);
                //Debug.Log(num);
                //mountain
                if (num  >=0.65f)
                {
                    if (map[i,p]==0)
                    {
                        map[i, p] = 2;
                    }
                 //desert  
                }else if(num <= 0.35f)
                {
                    //Debug.Log(num);
                    if (map[i, p] == 0)
                    {
                        map[i, p] = 3;
                    }
                }
            }

        }
        cleanUpLivery(map, 2, 1);
        cleanUpLivery(map, 2, 1);
        cleanUpLivery(map, 3, 3);
        cleanUpLivery(map, 3, 1);
        return map;
    }
    //this is the remove the mountains that spawn on their own ie have no neighbours WORK IN PROGRESS
    public int[,] cleanUpLivery(int[,]map,int landType,int numberReq)
    {
        for (int x = 1; x < width-1; x++)
        {
            for (int y = 1; y < height-1; y++)
            {
                if (map[x, y] == landType)
                {
                    int count = 0;
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            
                            int neighbour_x = x + i;
                            int neighbour_y = y + j;
                            
                            //if looking at center
                            if (i == 0 && j == 0)
                            {

                            }
                            else if (map[neighbour_x, neighbour_y] == landType)
                            {
                                count = count + 1;
                            }

                        }
                    }
                    if (count < numberReq)
                    {
                        map[x, y] = 0;
                    }
                }
            }
        }
        return map;
    }
    // this draws the edges of the map in water
    public int[,] drawEdges(int[,] map)
    {
        for (int i = 0; i < width; i++)
        {
            map[i, 0] = 1;
            map[i, height-1] = 1;
        }
        for (int i = 0; i < height; i++)
        {
            map[0, i] = 1;
            map[width-1, i] = 1;
        }
        return map;
    }

    public int[,] initialiseMap(int[,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Random.Range(0,1.0f) < chanceToStartAlive)
                {
                    map[x,y] = 1;
                }
            }
        }
        return map;
    }

    public int countAliveNeighbours(int[,] map , int x, int y)
    {
        int count = 0;
        for (int i =- 1; i < 2; i++)
        {
            for (int j =- 1; j < 2; j++)
            {
                int neighbour_x = x + i;
                int neighbour_y = y + j;
                //if looking at center
                if(i==0 && j== 0)
                {
                    
                }else if(neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= width || neighbour_y >= height)
                {
                    count = count + 1;
                }else if (map[neighbour_x,neighbour_y]==1)
                {
                    count = count + 1;
                }
            }
        }
        return count;
    }



    public int[,] doSimulationStep(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int nbs = countAliveNeighbours(oldMap, x, y);
                if (oldMap[x,y]==1)
                {
                    if (nbs<deathNumber)
                    {
                        newMap[x, y] = 0;
                    }
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }
                else
                {
                    if (nbs > birthNumber)
                    {
                        newMap[x, y] = 1;
                    }
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }
        return newMap;
    }


    public void drawMap(int[,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                switch (map[x,y])
                {
                    case 0:
                        Instantiate(Land, new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(Sea, new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Mountain, new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Desert, new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(Hill, new Vector3(x, 1, y), Quaternion.identity);
                        break;

                    default:
                        break;
                }
                
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
