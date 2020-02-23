using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{

    enum Direction { origin, left, back, right };

    public GameObject grid;
    public int GridX = 5;
    public int GridY = 5;
    public Vector3 OriginPos = new Vector3(0, 0, 0);
    public GameObject[,] Tiles;
    public GameObject[] MenuBtns;
    public Object[] Blocks;
    
    public GameObject curObPos;

    public GameObject curGo;
    Direction curGoDir = Direction.origin;
    int dirIndex=0;
    private float roty = 0;
    private float rotz = 0;

    private float offsetX = 0;
    private float offsetY = 0;
    private float offsetZ = 0;

    private Button button;


    public Vector3 mousepos;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new GameObject[GridX, GridY];

        LoadBlockMenu();

        curGo = Blocks[0] as GameObject;

        GenerateGrid();
    }

    void GenerateGrid()
    {
        OriginPos = grid.transform.position;
        Debug.Log(OriginPos);

        offsetX = grid.transform.localScale.x;
        offsetY = grid.transform.localScale.y;
        offsetZ = grid.transform.localScale.z;

        for (int i = 0; i < GridX; i++)
        {
            for (int j = 0; j < GridY; j++)
            {
                Debug.Log(i);
                Tiles[i, j] = Instantiate(grid, OriginPos + new Vector3(i * offsetX, 0, j * offsetZ), grid.transform.rotation);
                Tiles[i, j].transform.SetParent(transform);
            }
        }
    }

    //generate block menu button
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

    //detect buttonclick event
    public void GetCurGO(Object ob)
    {
        curGo = Resources.Load("Prefab/Gameplay/Ground/" + ob.name) as GameObject;
        Vector3 mouseObPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curGo = Instantiate(curGo, mouseObPos, curGo.transform.rotation);

      
    }

    // Update is called once per frame
    void Update()
    {
        PlaceObjOnMap();
        mouseOnGrid();
        //RotateOb();
    }

    //place curgo on grid
    void PlaceObjOnMap()
    {
        Vector3 mouseObPos = Input.mousePosition;

        Ray castPoint = Camera.main.ScreenPointToRay(mouseObPos);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, 100))
        {
            mouseObPos = hit.point;
            mouseObPos.y = 0.5f;

            curGo.transform.position = mouseObPos;
        }

        //rotate before placing
    }

    void RotateOb()
    {
        Quaternion newRot = curGo.transform.rotation;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            dirIndex++;
            roty += 90;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            dirIndex--;
            roty -= 90;
        }

        int dirIndex0 = Mathf.Abs(dirIndex) % 4;

        switch (dirIndex0)
        {
            case 1:
                curGoDir = Direction.left;
                newRot = Quaternion.Euler(-90, roty, 0);
                break;
            case 2:
                curGoDir = Direction.back;
                newRot = Quaternion.Euler(-90, roty, 0);
                break;
            case 3:
                curGoDir = Direction.right;
                newRot = Quaternion.Euler(-90, roty, 0);
                break;
            case 0:
                curGoDir = Direction.origin;
                newRot = Quaternion.Euler(-90, roty, 0);
                break;
        }

        Debug.Log(newRot);
        curGo.transform.localRotation = newRot;

        curObPos.transform.GetChild(0).GetComponent<Text>().text = curGo.name;
        curObPos.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InputField>().text = Mathf.Round(curGo.transform.position.x).ToString();
        curObPos.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>().text = Mathf.Round(curGo.transform.position.y).ToString();
        curObPos.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<InputField>().text = Mathf.Round(curGo.transform.position.z).ToString();

        curObPos.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = curGoDir.ToString();
    }

    void mouseOnGrid()
    {

        RaycastHit objectHit;
        if (Physics.Raycast(curGo.transform.position, transform.forward, out objectHit, 50))
        {
            Debug.Log("Raycast hitted to: " + objectHit.collider);
        }

    }

    //obselete
    void PlaceObjOnMap02()
    {
        mousepos = Input.mousePosition;
        mousepos.z = 10;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        mousepos.y = 0.5f;

        curGo.transform.position = mousepos;
    }
}