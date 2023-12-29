using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Vector3 originalBrickF1;//vị trí gốc của floor1
    [SerializeField] Vector3 originalBridgeF1;//vị trí gốc của bridge1
    [SerializeField] private int colF1 = 10;//mảng 2 chiều tạo floor1
    [SerializeField] private int rowF1 = 10;//mảng 2 chiều tạo floor1
    [SerializeField] private float sizeB1 = 30f;
    [SerializeField] private GameObject groundCubePrefab;
    [SerializeField] GameObject brickPrefabGreen;
    [SerializeField] GameObject brickPrefabRed;
    [SerializeField] GameObject brickPrefabBlue;
    [SerializeField] GameObject brickPrefabYellow;
    [SerializeField] GameObject brickBridgePrefab;
    [SerializeField] GameObject playerPrefab;
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
    public List<Vector3> listSpawmBrick;
    public FixedJoystick fixedJoystick;
    private int index = 0;
    private static LevelManager instance;
    private int maxColorBrickF1 =25;

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

    }

    // Update is called once per frame
    void Update()
    {
        if(listSpawmBrick.Count > 0)
        {
            Invoke("SpawmBrick", 0.5f);
        }
    }

    public void SpawmPlayer()
    {
        var xPos = Random.Range(1, 8);
        var zPos = Random.Range(1, 8);
        playerClone = Instantiate(playerPrefab, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);
        playerClone.GetComponent<JoystickPlayerExample>().fixedJoystick = this.fixedJoystick;
        listDelete.Add(playerClone);
    }
    public void DrawmF1()
    {
        //Tạo list obj map cũ => clear khi tạo map mới
        if (listDelete != null)
        {
            foreach (var i in listDelete)
            {
                Destroy(i);
            }
            listDelete.Clear(); 
        }

        //Tạo list chứa tất cả brick có màu
        for (int i = 0; i < maxColorBrickF1; i++)
        {
            numbers.Add(0);
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);
        }
        Suffle(numbers);
        //Tạo brick có màu trong ma trận 2 chiều
        for (int i = (int)originalBrickF1.x; i < colF1; i++)
        {
            for (int j = (int)originalBrickF1.z; j < rowF1; j++)
            {
                if(index < numbers.Count)
                {
                    if (numbers[index] == 0)
                    {
                        GameObject brickClone = Instantiate(brickPrefabBlue, new Vector3(i, 0.5f, j), Quaternion.identity);
                        index++;
                        listDelete.Add(brickClone);
                    }
                    else if (numbers[index] == 1)
                    {
                        GameObject brickClone = Instantiate(brickPrefabRed, new Vector3(i, 0.5f, j), Quaternion.identity);
                        index++;
                        listDelete.Add(brickClone);
                    }
                    else if (numbers[index] == 2)
                    {
                        GameObject brickClone = Instantiate(brickPrefabGreen, new Vector3(i, 0.5f, j), Quaternion.identity);
                        index++;
                        listDelete.Add(brickClone);
                    }
                    else if (numbers[index] == 3)
                    {
                        GameObject brickClone = Instantiate(brickPrefabYellow, new Vector3(i, 0.5f, j), Quaternion.identity);
                        index++;
                        listDelete.Add(brickClone);
                    }
                }  
            }
        }
    }

    //Tạo ground theo Vector brick gốc
    public void DrawG1()
    {
        for (int i = (int)originalBrickF1.x; i <= colF1 + 1; i++)
        {
            for (int j = (int)originalBrickF1.z; j <= rowF1 + 1; j++)
            {
                GameObject groundClone = Instantiate(groundCubePrefab, new Vector3(i - 1, 0, j - 1), Quaternion.identity);
                listDelete.Add(groundClone);
                Debug.Log("G1");
            }
        }
    }
    //Tạo bridgeF1 theo vector brick gốc
    public void DrawBridgeF1()
    {
        for (int i = (int)originalBridgeF1.z; i < sizeB1; i++)
        {
            GameObject bridgeClone = Instantiate(brickBridgePrefab, new Vector3(originalBridgeF1.x, originalBridgeF1.y, i), Quaternion.identity);
            listDelete.Add(bridgeClone);
            originalBridgeF1.y += 0.1f;
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

    private void SpawmBrick()
    {
        int randomColor = Random.Range(0, 4);
        switch (randomColor)
        {
            case 0:
                GameObject brickClone0 = Instantiate(brickPrefabBlue, listSpawmBrick[0], Quaternion.identity);
                listSpawmBrick.Remove(listSpawmBrick[0]);
                break;
            case 1:
                GameObject brickClone1 = Instantiate(brickPrefabRed, listSpawmBrick[0], Quaternion.identity);
                listSpawmBrick.Remove(listSpawmBrick[0]);
                break;
            case 2:
                GameObject brickClone2 = Instantiate(brickPrefabGreen, listSpawmBrick[0], Quaternion.identity);
                listSpawmBrick.Remove(listSpawmBrick[0]);
                break;
            case 3:
                GameObject brickClone3 = Instantiate(brickPrefabYellow, listSpawmBrick[0], Quaternion.identity);
                listSpawmBrick.Remove(listSpawmBrick[0]);
                break;
        }
    }

}
