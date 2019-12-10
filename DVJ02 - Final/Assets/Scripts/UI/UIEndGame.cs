using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndGame : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + GameManager.instance.GetScore();
        timeText.text = "Time: " + (int)GameManager.instance.GetTime();
    }

    public void GoToMenu()
    {
        LevelManager.instance.GoToLevel(0);
    }
}
