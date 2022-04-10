using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Start Game
    public Text scoreGem;
    public Text LevelPlayer;


    //OpTion GUI Manager
    public GameObject showTutorial;
    bool setBoolTutorial = false;

    void Start()
    {
        showTutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScoreGem(string txt)
    {
        if (scoreGem)
        {
            scoreGem.text = txt;
        }
    }

    public void setLevelPlayer(string txt)
    {
        if (LevelPlayer)
        {
            LevelPlayer.text = txt;
        }
    }

    //OpTion GUI Manager
    public void buttonStart()
    {
        SceneManager.LoadScene("Map-1-Scene");
    }

    public void buttonTutorial()
    {
        if (setBoolTutorial == false)
        {
            showTutorial.SetActive(true);
            setBoolTutorial = true;
        }
        else
        {
            showTutorial.SetActive(false);
            setBoolTutorial = false;
        }

    }

    public void quitOption()
    {
        Application.Quit();
    }

   
}
