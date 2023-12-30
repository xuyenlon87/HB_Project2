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

    public void SpawmPlayer(float minX, float maxX, float minZ, float maxZ )
    {
        var xPos = Random.Range(minX, maxX);
        var zPos = Random.Range(minZ, maxZ);
        playerClone = Instantiate(playerPrefab, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);
        playerClone.GetComponent<JoystickPlayerExample>().fixedJoystick = this.fixedJoystick;
        //listDelete.Add(playerClone);
    }
    public void DrawmFloor(Vector3 originalBrick, int maxRow, int maxCol, int maxOneColor)
    {
        ////Tạo list obj map cũ => clear khi tạo map mới
        //if (listDelete != null)
        //{
        //    foreach (var i in listDelete)
        //    {
        //        Destroy(i);
        //    }
        //    listDelete.Clear(); 
        //}

        //Vẽ ground
        GameObject groundClone = Instantiate(groundCubePrefab, originalBrick , Quaternion.identity); ;
        groundClone.transform.localScale = new Vector3(groundCubePrefab.transform.localScale.x * (maxRow + 2), groundCubePrefab.transform.localScale.y, groundCubePrefab.transform.localScale.z * (maxCol + 2));
        groundClone.transform.position = new Vector3(originalBrick.x + 0.5f, originalBrick.y, originalBrick.z + 0.5f);

        //Tạo list chứa tất cả brick có màu
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
                    // listDelete.Add(brickClone);
                }
            }
        }
    }

    ////Tạo ground theo Vector brick gốc
    //public void DrawGround(Vector3 originalPos, int maxSizeCol, int maxSizeRow)
    //{
    //    for (int i = (int)originalPos.x; i <= maxSizeCol + 1; i++)
    //    {
    //        for (int j = (int)originalPos.z; j <= maxSizeRow + 1; j++)
    //        {
    //            GameObject groundClone = Instantiate(groundCubePrefab, new Vector3(i - 1, 0, j - 1), Quaternion.identity);
    //            listDelete.Add(groundClone);
    //            Debug.Log("G1");
    //        }
    //    }
    //}
    //Tạo bridgeF1 theo vector brick gốc
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
    }

}
