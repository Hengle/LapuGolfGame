using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public Color originColor;
    public Color mouseonColor;
    public Color selectColor;

    public GameObject grid;
    public GameObject building;
    private Vector3 newpos;
    private bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = originColor;
        newpos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (selected == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = selectColor;
        }
        else if (selected == false)
        {

        }

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Block")
            {
               
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && building == null)
        {
            selected = true;
            building = Instantiate(gameObject.transform.GetComponentInParent<MapGenerator>().curGo, newpos, transform.rotation) as GameObject;
        }
        else if (Input.GetMouseButton(1) && building != null) 
        {
            selected = false;
            Destroy(building);
            Debug.Log("awsl");
        }
        
        gameObject.GetComponent<SpriteRenderer>().color = mouseonColor;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = originColor;
    }

}
