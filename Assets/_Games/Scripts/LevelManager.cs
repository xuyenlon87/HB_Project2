using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int col1 = 10;
    private int row1 = 10;
    private int sum1 =0;
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
        SpawmPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawmPlayer()
    {
        var xPos = Random.Range(1, 8);
        var zPos = Random.Range(1, 8);
        Instantiate(player, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);
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
        for (int i = 0; i < col1; i++)
        {
            for (int j = 0; j < row1; j++)
            {
                
                int color = Random.Range(0, 4);
                if(color == 0 && countBlue <=25)
                {
                    countBlue += 1;
                    Instantiate(brickPrefabBlue, new Vector3(i, 0.5f, j), Quaternion.identity);
                    listBrickDelete.Add(brickPrefabBlue);
                }
                else if (color == 1 && countRed <=25)
                {
                    countRed += 1;
                    Instantiate(brickPrefabRed, new Vector3(i, 0.5f, j), Quaternion.identity);
                    listBrickDelete.Add(brickPrefabRed);
                }
                else if (color == 2 && countGreen <=25)
                {
                    countGreen += 1;
                    Instantiate(brickPrefabGreen, new Vector3(i, 0.5f, j), Quaternion.identity);
                    listBrickDelete.Add(brickPrefabGreen);
                }
                else if (color == 3 && countYellow <=25)
                {
                    countYellow += 1;
                    Instantiate(brickPrefabYellow, new Vector3(i, 0.5f, j), Quaternion.identity);
                    listBrickDelete.Add(brickPrefabYellow);
                }
            }
        }
    }

}
