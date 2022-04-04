using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScoreItem : MonoBehaviour
{
    public static ManagerScoreItem ins;

    UIManager UI;
    int setGem;


    private void Awake()
    {
        ins = this;
    }
    void Start()
    {

        UI = FindObjectOfType<UIManager>();
        UI.setScoreGem("x: " + setGem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScoreGem()
    {
        
    }
    
    public void ReceiveGem(int Gem)
    {
        setGem += Gem;
        UI.setScoreGem("X: " + setGem);
    }
}
