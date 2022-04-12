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

    bool closeMenu;
    public GameObject showMenu;
    public GameObject showVolume;
    bool closeVolume;


    //OpTion GUI Manager

    void Start()
    {
        closeMenu = true;
        closeVolume = true;
        showMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        menuBtnShow();
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


    public void showMenuBtn()
    {
        closeMenu = false;
        showMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void menuBtnShow()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && closeMenu)
        {
            closeMenu = false;
            showMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !closeMenu)
        {
            closeMenu = true;
            showMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        
    }
    



    public void ResumeMenu()
    {
        showVolume.SetActive(false);
        showMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ReplayMenu()
    {
        SceneManager.LoadScene("Map-1-Scene");
        Time.timeScale = 1f;
    }
    public void MusicMenu()
    {
        if (closeVolume)
        {
            showVolume.SetActive(true);
            closeVolume = false;
        }
        else
        {
            showVolume.SetActive(false);
            closeVolume = true;
        }
        
    }
    public void OptionMenu()
    {
        SceneManager.LoadScene("OptionScene");
        Time.timeScale = 1f;
    }


}
