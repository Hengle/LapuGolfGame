using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{

    public GameObject go;
    public int GridX = 5;
    public int GridY = 5;
    public Vector3 OriginPos = new Vector3(0, 0, 0);
    public GameObject[,] Tiles;
    public GameObject[] MenuBtns;
    public Object[] Blocks;

    public GameObject curGo;

    private float offsetX = 0;
    private float offsetY = 0;
    private float offsetZ = 0;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new GameObject[GridX, GridY];

        LoadBlockMenu();

        curGo = Blocks[0] as GameObject;

        if (go == null)
        {
            go = Resources.Load("GeneratedCube") as GameObject;
        }
        else if (go != null)
        {
            OriginPos = go.transform.position;
        }

        offsetX = go.transform.localScale.x;
        offsetY = go.transform.localScale.y;
        offsetZ = go.transform.localScale.z;

        for (int i = 0; i < GridX; i++)
        {
            for (int j = 0; j < GridY; j++)
            {
                Debug.Log(i);
                Tiles[i, j] = Instantiate(go, OriginPos + new Vector3(i * offsetX, 0, j * offsetZ), go.transform.rotation);
                Tiles[i, j].transform.SetParent(transform);
            }
        }
    }

    void LoadBlockMenu()
    {
        Blocks = Resources.LoadAll("Prefab/Gameplay/Ground");
        MenuBtns = new GameObject[Blocks.Length];
        GameObject btnPrefab = Resources.Load("Prefab/Gameplay/UI/MenuBtn") as GameObject;
        int i = 0;
        foreach (Object ob in Blocks)
        {
            MenuBtns[i] = Instantiate(btnPrefab) as GameObject;
            MenuBtns[i].name = "Blocks" + i.ToString();
            MenuBtns[i].transform.GetChild(0).GetComponent<Text>().text = ob.name;
            MenuBtns[i].transform.SetParent(GameObject.Find("Menu").transform);
            MenuBtns[i].GetComponent<Button>().onClick.AddListener(() => GetCurGO(ob));
            i++;
        }
    }

    public void GetCurGO(Object ob)
    {
        Debug.Log(ob.name);
        curGo = Resources.Load("Prefab/Gameplay/Ground/" + ob.name) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

       
    }
}