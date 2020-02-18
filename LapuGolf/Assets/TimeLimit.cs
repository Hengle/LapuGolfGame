using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{
    public int TotalTime = 60;
    public Text time;

    void Start()
    {
        StartCoroutine(Count());
    }

    private void Update()
    {
       
        if (TotalTime != 0)
        {
            time.text = TotalTime.ToString();
        }
        else
        {
            time.text = "Game Over";
        }
    }

    IEnumerator Count()
    {
        while (TotalTime >= 0)
        {
            yield return new WaitForSeconds(1);
            TotalTime--;
        }

    }
}
