using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int col = 10;
    private int row = 10;
    public int currentMap = 0;

    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }
    [SerializeField] List<TextAsset> testMap = new List<TextAsset>();
    [SerializeField] GameObject brickPrefabGreen;
    [SerializeField] GameObject brickPrefabBlack;
    [SerializeField] GameObject brickPrefabRed;
    [SerializeField] GameObject brickPrefabBlue;
    [SerializeField] GameObject brickPrefabYellow;
    [SerializeField] GameObject brickGround;
    [SerializeField] GameObject player;

    private int countBlue = 0;
    private int countGreen = 0;
    private int countYellow = 0;
    private int countRed = 0;

    public List<GameObject> listBrickDelete = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DrawMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DrawMap()
    {
        if (listBrickDelete != null)
        {
            foreach (var i in listBrickDelete)
            {
                Destroy(i);
            }
            listBrickDelete.Clear();
        }
        //if (currentMap >= 0 && currentMap < testMap.Count)
        //{
        //    string data = testMap[currentMap].text;
        //    Debug.Log(currentMap);
        //    string[] dataArray = data.Split("\r\n");
        //    int row = dataArray.Length;
        //    int col = dataArray[0].Split(",").Length;
        //    int[,] map = new int[row, col];
        //    for (int i = 0; i < row; i++)
        //    {
        //        if (dataArray.Length > 0)
        //        {
        //            string[] dataArray2 = dataArray[i].Split(",");
        //            for (int j = 0; j < dataArray2.Length; j++)
        //            {
        //                map[i, j] = int.Parse(dataArray2[j]);
        //                if (map[i, j] == 2)
        //                {
        //                    GameObject brick = Instantiate(brickPrefabGreen, new Vector3(j, 0.5f, -i), Quaternion.identity);
        //                    listBrickDelete.Add(brick);
        //                }
        //                else if (map[i, j] == 1)
        //                {
        //                    GameObject brickStart = Instantiate(brickPrefabBlack, new Vector3(j, 0.5f, -i), Quaternion.identity);
        //                    listBrickDelete.Add(brickStart);
        //                    //player.transform.position = brickStart.transform.position + Vector3.up;
        //                }
        //                else if (map[i, j] == 3)
        //                {
        //                    GameObject brick = Instantiate(brickPrefabRed, new Vector3(j, 0.5f, -i), Quaternion.identity);
        //                    listBrickDelete.Add(brick);
        //                }
        //                else if (map[i, j] == 0)
        //                {
        //                    GameObject brick = Instantiate(brickGround, new Vector3(j, 0.5f, -i), Quaternion.identity);
        //                    listBrickDelete.Add(brick);
        //                }
        //            }
        //        }
        //    }
        //}
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                int color = Random.Range(0, 4);
                if(color == 0 && countBlue <=25)
                {
                    countBlue += 1;
                    Instantiate(brickPrefabBlue, new Vector3(i, 0.5f, j), Quaternion.identity);
                }
                if (color == 1 && countRed <=25)
                {
                    countRed += 1;
                    Instantiate(brickPrefabRed, new Vector3(i, 0.5f, j), Quaternion.identity);
                }
                if (color == 2 && countGreen <=25)
                {
                    countGreen += 1;
                    Instantiate(brickPrefabGreen, new Vector3(i, 0.5f, j), Quaternion.identity);
                }
                if (color == 3 && countYellow <=25)
                {
                    countYellow += 1;
                    Instantiate(brickPrefabYellow, new Vector3(i, 0.5f, j), Quaternion.identity);
                }
                if(countBlue == 25)
                {
                    color = Random.Range(1, 4);
                }
                if (countRed == 25)
                {
                    color = Random.Range(2, 4);
                }
                if (countGreen == 25)
                {
                    color = Random.Range(3, 4);
                }

            }
        }
    }

}
