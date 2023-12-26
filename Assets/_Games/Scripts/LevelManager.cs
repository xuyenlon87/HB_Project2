using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Vector3 originalBrickF1;
    [SerializeField] Vector3 originalBridgeF1;
    [SerializeField] private int colF1 = 10;
    [SerializeField] private int rowF1 = 10;
    [SerializeField] private float sizeB1 = 30f;
    [SerializeField] private GameObject groundCubePrefab;
    [SerializeField] GameObject brickPrefabGreen;
    [SerializeField] GameObject brickPrefabRed;
    [SerializeField] GameObject brickPrefabBlue;
    [SerializeField] GameObject brickPrefabYellow;
    [SerializeField] GameObject brickBridge;
    [SerializeField] GameObject player;
    [SerializeField] private List<int> numbers = new List<int>();

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
    public GameObject playerClone;
    public List<GameObject> listDelete = new List<GameObject>();

    private int index1 = 0;
    private static LevelManager instance;

    void Awake()
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
        DrawmG1();
        DrawmF1();
        SpawmPlayer();
        DrawBridgeF1();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawmPlayer()
    {
        var xPos = Random.Range(1, 8);
        var zPos = Random.Range(1, 8);
        playerClone = Instantiate(player, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);
        listDelete.Add(playerClone);
    }
    private void DrawmF1()
    {
        for (int i = 0; i < 25; i++)
        {
            numbers.Add(0);
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);
        }
        Suffle(numbers);
        if (listDelete != null)
        {
            foreach (var i in listDelete)
            {
                Destroy(i);
            }
            listDelete.Clear();
        }
        for (int i = (int)originalBrickF1.x; i < colF1; i++)
        {
            for (int j = (int)originalBrickF1.z; j < rowF1; j++)
            {
                if(numbers[index1] == 0)
                {
                    GameObject brickClone = Instantiate(brickPrefabBlue, new Vector3(i, 0.5f, j), Quaternion.identity);
                    index1++;
                    listDelete.Add(brickClone);
                }
                else if (numbers[index1] == 1)
                {
                    GameObject brickClone = Instantiate(brickPrefabRed, new Vector3(i, 0.5f, j), Quaternion.identity);
                    index1++;
                    listDelete.Add(brickClone);
                }
                else if (numbers[index1] == 2)
                {
                    GameObject brickClone = Instantiate(brickPrefabGreen, new Vector3(i, 0.5f, j), Quaternion.identity);
                    index1++;
                    listDelete.Add(brickClone);
                }
                else if (numbers[index1] == 3)
                {
                    GameObject brickClone = Instantiate(brickPrefabYellow, new Vector3(i, 0.5f, j), Quaternion.identity);
                    index1++;
                    listDelete.Add(brickClone);
                }
            }
        }
    }

    private void DrawmG1()
    {
        for (int i = (int)originalBrickF1.x; i <= colF1+1; i++)
        {
            for (int j = (int)originalBrickF1.z; j <= rowF1+1; j++)
            {

                GameObject groundClone = Instantiate(groundCubePrefab, new Vector3(i-1, 0, j-1), Quaternion.identity);
                listDelete.Add(groundClone);
            }
        }
    }
    private void DrawBridgeF1()
    {
        Debug.Log("bridge");
        for (int i = (int)originalBridgeF1.z; i < sizeB1; i++)
        {
            GameObject bridgeClone = Instantiate(brickBridge, new Vector3(originalBridgeF1.x, originalBridgeF1.y, i), Quaternion.identity);
            listDelete.Add(bridgeClone);
        }
    }

    private void Suffle(List<int> list)
    {
        for (int i = list.Count-1; i > 0; i--)
        {

            int j = Random.Range(0, 100);
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }


}
