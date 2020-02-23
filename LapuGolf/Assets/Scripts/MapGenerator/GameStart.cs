using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{

    public GameObject ob;

    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = ob.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ob.transform.position.y <= 0)
        {
            ob.transform.position = pos;
        }
    }

    public void StartGame()
    {
        ob.SetActive(true);
        ob.transform.position = pos;
    }

    public void ClearMap()
    {
        ob.SetActive(true);
        ob.transform.position = pos;
    }
}
