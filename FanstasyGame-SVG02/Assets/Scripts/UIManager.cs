using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text scoreGem;

    public Text LevelPlayer;
    // Start is called before the first frame update
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
