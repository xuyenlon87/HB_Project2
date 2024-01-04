using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject groundCubePrefab;
    [SerializeField] private GameObject groundSquarePrefab;
    [SerializeField] GameObject brickPrefabGreen;
    [SerializeField] GameObject brickPrefabRed;
    [SerializeField] GameObject brickPrefabBlue;
    [SerializeField] GameObject brickPrefabYellow;
    [SerializeField] GameObject brickBridgePrefab;
    [SerializeField] GameObject playerPrefab;
    //[SerializeField] private List<int> numbers = new List<int>();

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

    public void SpawmPlayer(int minX, int maxX, int minZ, int maxZ )
    {
        var xPos = Random.Range(minX, maxX);
        var zPos = Random.Range(minZ, maxZ);
        playerClone = Instantiate(playerPrefab, new Vector3(xPos + 0.5f, 1.5f, zPos + 0.5f), Quaternion.identity);
        listDelete.Add(playerClone);
    }
    public void DrawmFloor(Vector3 originalBrick, int maxRow, int maxCol, int maxOneColor)
    {
        //Vẽ ground
        GameObject groundClone = Instantiate(groundCubePrefab, originalBrick , Quaternion.identity); ;
        groundClone.transform.localScale = new Vector3(groundCubePrefab.transform.localScale.x * (maxRow + 2), groundCubePrefab.transform.localScale.y, groundCubePrefab.transform.localScale.z * (maxCol + 2));
        groundClone.transform.position = new Vector3(originalBrick.x + 0.5f, originalBrick.y, originalBrick.z + 0.5f);

        //Tạo list chứa tất cả brick có màu
        List<int> numbers = new List<int>();
        if (numbers != null)
        {
            numbers.Clear();
        }
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
        for (int i = 0; i < maxRow; i++)
        {
            for (int j = 0; j < maxCol; j++)
            {
                if (index < numbers.Count)
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
                    float xPos = originalBrick.x + (i - maxRow/2 )+1 ; // Tính toán vị trí theo hàng
                    float zPos = originalBrick.z + (j - maxCol/2 )+1 ; // Tính toán vị trí theo cột
                    Instantiate(brickClone, new Vector3(xPos, originalBrick.y + 0.55f, zPos), Quaternion.identity);
                    index++;
                    listDelete.Add(brickClone);
                }
            }
        }
    }

    public void DrawBridge(Vector3 originalBridge, int sizeBridge)
    {
        for (int i = 0; i < sizeBridge; i++)
        {
            GameObject bridgeClone = Instantiate(brickBridgePrefab, new Vector3(originalBridge.x, originalBridge.y, originalBridge.z + i), Quaternion.identity);
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
        listDelete.Add(brickClone);
    }

    public void DrawCircleFloor(Vector3 originalCenter, int radius, int maxOneColor)
    {
        //Vẽ ground
        GameObject groundClone = Instantiate(groundCubePrefab, originalCenter, Quaternion.identity); ;
        groundClone.transform.localScale = new Vector3(groundCubePrefab.transform.localScale.x * (radius * 2) + 2, groundCubePrefab.transform.localScale.y, groundCubePrefab.transform.localScale.z * (radius * 2) + 2);
        groundClone.transform.position = new Vector3(originalCenter.x, originalCenter.y, originalCenter.z);

        List<int> numbers = new List<int>();
        if (numbers != null)
        {
            numbers.Clear();
        }
        for (int i = 0; i < maxOneColor; i++)
        {
            numbers.Add(0);
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);
        }
        Suffle(numbers);
        GameObject brickClone = null;
        //Tạo brick có màu trong ma trận 2 chiều
        int index = 0;
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                if (i * i + j * j <= radius * radius)
                {
                    if (index < numbers.Count)
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
                        Instantiate(brickClone, new Vector3(originalCenter.x + i, originalCenter.y + 0.55f, originalCenter.z + j), Quaternion.identity);
                        index++;
                        // listDelete.Add(brickClone);
                    }
                }
            }
        }
    }
}
