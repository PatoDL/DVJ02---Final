using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    public LevelData levelData;

    // Update is called once per frame
    void Update()
    {
        if(!levelData)
        {
            levelData = GameObject.Find("LevelData").GetComponent<LevelData>();
        }
    }

    public void GoToLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// "next" or "prev"
    /// </summary>
    /// <param name="level"></param>
    public void GoToLevel(string level)
    {
        if(level == "next")
        {
            SceneManager.LoadScene(levelData.nextLevel);
        }
        else if(level == "prev")
        {
            SceneManager.LoadScene(levelData.prevLevel);
        }
    }
}
