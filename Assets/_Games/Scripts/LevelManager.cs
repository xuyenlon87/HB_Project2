using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject groundCubePrefab;
    [SerializeField] private GameObject groundSquarePrefab;
    [SerializeField] GameObject brickBridgePrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject botPrefab;
    [SerializeField] private Brick prefabBrick;
    [SerializeField] private GameObject brickPrefab;
    

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
    public List<Vector3> listPositionBrick = new List<Vector3>();
    public List<Material> listMaterial;
    public List<GameObject> listRedBrick;
    public List<GameObject> listYellowBrick;
    public List<GameObject> listGreenBrick;
    public List<GameObject> listBlueBrick;
    public List<Vector3> listSpawmBrickPosition;
    public GameObject map1;
    public bool wave2 = false;
    private static LevelManager instance;
    private float timer;
    private MiniPool<Brick> poolSpawmBrick;


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
        poolSpawmBrick = new MiniPool<Brick>();
        poolSpawmBrick.OnInit(prefabBrick, 1000, map1.transform);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(listSpawmBrickPosition.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                SpawmBrick();
            }
        }
    }

    public void SpawmPlayer(int minX, int maxX, int minZ, int maxZ, Transform parent = null )
    {
        var xPos = Random.Range(minX, maxX);
        var zPos = Random.Range(minZ, maxZ);
        playerClone = Instantiate(playerPrefab, new Vector3(xPos + 0.5f, 1.5f, zPos + 0.5f), Quaternion.identity, parent.transform);

        GameObject botClone = null;
        for (int i = 0; i < listMaterial.Count - 1; i++)
        {
            xPos = Random.Range(minX, maxX);
            zPos = Random.Range(minZ, maxZ);
            botClone = Instantiate(botPrefab, new Vector3(xPos + 0.5f, 1.5f, zPos + 0.5f), Quaternion.identity, parent.transform);
            Renderer color = botClone.GetComponent<Renderer>();
            if (i == 0)
            {
                color.material = listMaterial[0];
            }
            else if (i == 1)
            {
                color.material = listMaterial[1];
            }
            else if (i == 2)
            {
                color.material = listMaterial[2];
            }
        }
    }
    public void DrawmFloor(Vector3 originalBrick, int maxRow, int maxCol, int maxOneColor, Transform parent = null)
    {
        ////Vẽ ground
        //GameObject groundClone = Instantiate(groundCubePrefab, originalBrick, Quaternion.identity); ;
        //groundClone.transform.localScale = new Vector3(groundCubePrefab.transform.localScale.x * (maxRow + 2), groundCubePrefab.transform.localScale.y, groundCubePrefab.transform.localScale.z * (maxCol + 2));
        //groundClone.transform.position = new Vector3(originalBrick.x + 0.5f, originalBrick.y, originalBrick.z + 0.5f);

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
                    float xPos = originalBrick.x + (i - maxRow / 2) + 0.5f; // Tính toán vị trí theo hàng
                    float zPos = originalBrick.z + (j - maxCol / 2) + 0.5f; // Tính toán vị trí theo cột
                    brickClone = Instantiate(brickPrefab, new Vector3(xPos, originalBrick.y + 0.55f, zPos), Quaternion.identity, parent.transform);
                    Renderer color = brickClone.GetComponent<Renderer>();
                    if (numbers[index] == 0)
                    {
                        color.material = listMaterial[0];
                        listYellowBrick.Add(brickClone);
                    }
                    else if (numbers[index] == 1)
                    {
                        color.material = listMaterial[1];
                        listGreenBrick.Add(brickClone);
                    }
                    else if (numbers[index] == 2)
                    {
                        color.material = listMaterial[2];
                        listRedBrick.Add(brickClone);
                    }
                    else if (numbers[index] == 3)
                    {
                        color.material = listMaterial[3];
                        listBlueBrick.Add(brickClone);
                    }
                    index++;
                    listPositionBrick.Add(new Vector3(xPos, originalBrick.y + 0.55f, zPos));
                }
            }
        }
    }

    public void DrawBridge(Vector3 originalBridge, int sizeBridge)
    {
        for (int i = 0; i < sizeBridge; i++)
        {
            GameObject bridgeClone = Instantiate(brickBridgePrefab, new Vector3(originalBridge.x, originalBridge.y, originalBridge.z + i), Quaternion.identity);
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
        poolSpawmBrick.Spawn(listSpawmBrickPosition[0], Quaternion.identity, map1.transform);
        //Renderer renderer = GetComponent<Renderer>();
        //int random = Random.Range(0, listMaterial.Count - 1);
        //renderer.material = listMaterial[random];
        //if (random == 0)
        //{
        //    listRedBrick.Add(gameObject);
        //}
        //else if (random == 1)
        //{
        //    listYellowBrick.Add(gameObject);
        //}
        //else if (random == 2)
        //{
        //    listGreenBrick.Add(gameObject);
        //}
        //else if (random == 3)
        //{
        //    listBlueBrick.Add(gameObject);
        //}
        listSpawmBrickPosition.Remove(listSpawmBrickPosition[0]);
    }

    //public void DrawCircleFloor(Vector3 originalCenter, int radius, int maxOneColor)
    //{
    //    //NavMesh.AddNavMeshData()
    //    //Vẽ ground
    //    GameObject groundClone = Instantiate(groundCubePrefab, originalCenter, Quaternion.identity); ;
    //    groundClone.transform.localScale = new Vector3(groundCubePrefab.transform.localScale.x * (radius * 2) + 2, groundCubePrefab.transform.localScale.y, groundCubePrefab.transform.localScale.z * (radius * 2) + 2);
    //    groundClone.transform.position = new Vector3(originalCenter.x, originalCenter.y, originalCenter.z);

    //    List<int> numbers = new List<int>();
    //    if (numbers != null)
    //    {
    //        numbers.Clear();
    //    }
    //    for (int i = 0; i < maxOneColor; i++)
    //    {
    //        numbers.Add(0);
    //        numbers.Add(1);
    //        numbers.Add(2);
    //        numbers.Add(3);
    //    }
    //    Suffle(numbers);
    //    GameObject brickClone = null;
    //    //Tạo brick có màu trong ma trận 2 chiều
    //    int index = 0;
    //    for (int i = -radius; i <= radius; i++)
    //    {
    //        for (int j = -radius; j <= radius; j++)
    //        {
    //            if (i * i + j * j <= radius * radius)
    //            {
    //                if (index < numbers.Count)
    //                {
    //                    if (numbers[index] == 0)
    //                    {
    //                        brickClone = brickPrefabBlue;
    //                    }
    //                    else if (numbers[index] == 1)
    //                    {
    //                        brickClone = brickPrefabRed;
    //                    }
    //                    else if (numbers[index] == 2)
    //                    {
    //                        brickClone = brickPrefabGreen;
    //                    }
    //                    else if (numbers[index] == 3)
    //                    {
    //                        brickClone = brickPrefabYellow;
    //                    }
    //                    Instantiate(brickClone, new Vector3(originalCenter.x + i, originalCenter.y + 0.55f, originalCenter.z + j), Quaternion.identity);
    //                    index++;
    //                }
    //            }
    //        }
    //    }
    //}
}
