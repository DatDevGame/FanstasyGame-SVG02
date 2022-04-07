using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScoreItem : MonoBehaviour
{
    public static ManagerScoreItem ins;

    UIManager UI;
    int setGem;
    AudioSource aus;
    public AudioClip soundGetItem;


    private void Awake()
    {
        ins = this;
    }
    void Start()
    {

        aus = GetComponent<AudioSource>();
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
        aus.PlayOneShot(soundGetItem);
        setGem += Gem;
        UI.setScoreGem("X: " + setGem);
    }
}
