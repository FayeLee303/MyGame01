using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerateManager : MonoBehaviour {
    private int[,] grid;//二维数组
    private GameObject[,] gridObj;//物体的二维数组
    public int mapSizeX = 20; //长
    public int mapSizeZ = 20; //宽
    public GameObject cubePrefab; //地块预制体
    public int updateNum = 5;//优化次数
    public int Rate = 45;
    public int cubeSize = 5;


	// Use this for initialization
	void Start ()
	{
        //初始化数组
	    grid = new int[mapSizeX, mapSizeZ];
	    gridObj = new GameObject[mapSizeX, mapSizeZ];
	    MapCreate();
        int i = 0;
	    while (true)
	    {
	        ResetMap();
	        i++;
	        if (i >= updateNum) break;
	    }
	   InversionMap();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //生成初始的二维数组地图
    public void MapCreate()
    {
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeZ; j++)
            {
                if (Random.Range(0, 100) < Rate)
                {
                    grid[i, j] = 1; //标记有东西
                }
                else
                {
                    grid[i, j] = 0;//标记没有东西
                }
                if (grid[i, j] == 1)
                {
                    //在标记有的位置上实例化方块
                    gridObj[i, j] = Instantiate(cubePrefab, transform.position + new Vector3(i, 0, j)*cubeSize,
                        Quaternion.identity);
                    gridObj[i, j].gameObject.transform.SetParent(this.transform);
                }
            }
        }
    }

    //优化地图，如果一个方块周围方块比较少，即它比较孤立，就把它清除掉
    public void ResetMap()
    {
        //克隆就是两份，如果直接赋值就是一份
        int[,] newGrid = (int[,]) grid.Clone();
        for (int i = 0;i<mapSizeX;i++)
        {
            for (int j = 0; j < mapSizeZ; j++)
            {
                if (grid[i, j] == 1)
                {
                    if (GetWallNumber(i, j) >= 4)
                    {
                    }
                    else
                    {
                        newGrid[i, j] = 0;
                    }
                }
                else
                {
                    if (GetWallNumber(i, j) >= 5)
                    {
                        newGrid[i, j] = 1;//设置为墙
                    }
                }
            }
        }
        CheckMapAndUpdate(newGrid);
    }

    //查看地图是否变化并更新
    public void CheckMapAndUpdate(int[,] newGrid)
    {
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeZ; j++)
            {
                if (newGrid[i, j] != grid[i, j])
                {
                    grid[i, j] = newGrid[i, j];
                    if (newGrid[i, j] == 1)
                    {
                        gridObj[i, j] = Instantiate(cubePrefab, transform.position + new Vector3(i, 0, j)*cubeSize,
                            Quaternion.identity);
                        gridObj[i, j].gameObject.transform.SetParent(this.transform);
                    }
                    else
                    {
                        Destroy(gridObj[i, j]);

                    }
                }
            }
        }
    }

    //获取一个方块周围有几个方块
    public int GetWallNumber(int x, int y)
    {
        int num = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                if (x+i < 0 || x+i > mapSizeX - 1 || y+j < 0 || y+j > mapSizeZ - 1)
                {
                    num++;
                    continue;
                }
                if (grid[x + i, y + j] == 1) num++;
            }
        }
        return num;
    }

    //反转地图，把空的填满，把填满的变空
    public void InversionMap()
    {
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeZ; j++)
            {
                if (grid[i, j] == 1)
                {
                    Destroy(gridObj[i, j]);
                }
                else
                {
                    gridObj[i, j] = Instantiate(cubePrefab, transform.position + new Vector3(i, 0, j)*cubeSize,
                        Quaternion.identity);
                    gridObj[i, j].gameObject.transform.SetParent(this.transform);
                }
            }
        }
    }
}
