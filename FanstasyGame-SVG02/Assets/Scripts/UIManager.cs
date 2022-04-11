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

    void Start()
    {
        
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

    
}
