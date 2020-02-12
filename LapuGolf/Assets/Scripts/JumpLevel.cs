using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JumpLevel : MonoBehaviour
{
    private int countdown = 5;
    public string nextSceneName;
    public Text currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Count");
    }

    // Update is called once per frame
    void Update()        
    {
        if (countdown <= 0) {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(nextSceneName);
            currentLevel.text = "Level" + nextSceneName.Substring(nextSceneName.Length - 1);
        } 
    }

    IEnumerator Count()
    {
        while (countdown > 0)
        {
            yield return new WaitForSeconds(1);
            countdown--;
        }
    }

}
