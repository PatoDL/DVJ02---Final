using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public bool inGame;
    int score = 0;
    float time;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        time = maxTime;
        CarController.CollectItem = AddScore;
        CarController.CarDie = GoToGameOver;
    }

    public void SetPause(bool active)
    {
        if(active)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inGame)
        {
            time -= Time.deltaTime;

            if(time<=0)
            {
                GoToGameOver();
            }
        }
    }

    void GoToGameOver()
    {
        inGame = false;
        LevelManager.instance.GoToLevel("next");
    }

    public void AddScore()
    {
        score += 10;
    }

    public float GetTime()
    {
        return maxTime - time;
    }

    public int GetScore()
    {
        return score;
    }

    public void Restart()
    {
        inGame = true;
        time = maxTime;
        score = 0;
    }
}
