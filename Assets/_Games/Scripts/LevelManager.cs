using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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
    private static LevelManager instance;
    private float timer;

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
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                SpawmBrick();
            }
        }
    }

    public void SpawmPlayer(float min, float max)
    {
        var xPos = Random.Range(min, max);
        var zPos = Random.Range(min, max);
        playerClone = Instantiate(playerPrefab, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);
        playerClone.GetComponent<JoystickPlayerExample>().fixedJoystick = this.fixedJoystick;
        listDelete.Add(playerClone);
    }
    public void DrawmF1(int maxOneColor, Vector3 originalBrick, int maxCol, int maxRow )
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
        for (int i = 0; i < maxOneColor; i++)
        {
            numbers.Add(0);
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);
        }
        Suffle(numbers);
        //Tạo brick có màu trong ma trận 2 chiều
        int index = 0;
        GameObject brickClone = null;
        for (int i = (int)originalBrick.x; i < maxCol; i++)
        {
            for (int j = (int)originalBrick.z; j < maxRow; j++)
            {
                if(index < numbers.Count)
                {
                    if (numbers[index] == 0)
                    {
                        brickClone = brickPrefabBlue;
                    }
                    else if (numbers[index] == 1)
                    {
                        brickClone = brickPrefabRed;
                    }
                    else if (numbers[index] == 2)
                    {
                        brickClone = brickPrefabGreen;
                    }
                    else if (numbers[index] == 3)
                    {
                        brickClone = brickPrefabYellow;
                    }
                    Instantiate(brickClone, new Vector3(i, 0.5f, j), Quaternion.identity);
                    index++;
                    listDelete.Add(brickClone);
                }  
            }
        }
    }

    //Tạo ground theo Vector brick gốc
    public void DrawG1(Vector3 originalPos, int maxSize)
    {
        for (int i = (int)originalPos.x; i <= maxSize + 1; i++)
        {
            for (int j = (int)originalPos.z; j <= maxSize + 1; j++)
            {
                GameObject groundClone = Instantiate(groundCubePrefab, new Vector3(i - 1, 0, j - 1), Quaternion.identity);
                listDelete.Add(groundClone);
                Debug.Log("G1");
            }
        }
    }
    //Tạo bridgeF1 theo vector brick gốc
    public void DrawBridgeF1(Vector3 originalBridge, int sizeBridge)
    {
        for (int i = (int)originalBridge.z; i < sizeBridge; i++)
        {
            GameObject bridgeClone = Instantiate(brickBridgePrefab, new Vector3(originalBridge.x, originalBridge.y, i), Quaternion.identity);
            listDelete.Add(bridgeClone);
            originalBridge.y += 0.1f;
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
        timer = 0;
        int randomColor = Random.Range(0, 4);
        GameObject brickClone = null;
        switch (randomColor)
        {
            case 0:
                brickClone = brickPrefabBlue;            
                break;
            case 1:
                brickClone = brickPrefabRed;
                break;
            case 2:
                brickClone = brickPrefabGreen;
                break;
            case 3:
                brickClone = brickPrefabYellow;
                break;
        }
        Instantiate(brickClone, listSpawmBrick[0], Quaternion.identity);
        listSpawmBrick.Remove(listSpawmBrick[0]);
    }

}
