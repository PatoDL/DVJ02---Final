using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Time: " + (int)GameManager.instance.GetTime();
        scoreText.text = "Score: " + GameManager.instance.GetScore();
    }

    public void SetPause(bool active)
    {
        GameManager.instance.SetPause(active);
    }

    public void GoToMenu()
    {
        LevelManager.instance.GoToLevel(0);
    }
}
