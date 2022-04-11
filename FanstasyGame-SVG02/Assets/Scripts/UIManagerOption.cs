using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerOption : MonoBehaviour
{

    public GameObject showTutorial;
    // Start is called before the first frame update

    private void Awake()
    {
        showTutorial.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //OpTion GUI Manager
    public void buttonStart()
    {
        SceneManager.LoadScene("Map-1-Scene");
    }

    public void buttonTutorial()
    {
        showTutorial.SetActive(true);

    }
    public void closeTutotial()
    {
        showTutorial.SetActive(false);
        Time.timeScale = 1f;
    }

    public void quitOption()
    {
        Application.Quit();
    }
}
