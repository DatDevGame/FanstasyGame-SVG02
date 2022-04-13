using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSkillShowCanvas : MonoBehaviour
{

    public static TimeSkillShowCanvas ins;

    public Image imageAttack;
    public Image imageDash;
    public Image imageFireball;

    public GameObject showNotmanaDash;
    public GameObject showNotmanaFireBall;

    float timerAttack;
    float timerFireball;
    float timerDash;
    // Start is called before the first frame update
    void Start()
    {
        ins = this;

        showNotmanaFireBall.SetActive(false);
        showNotmanaDash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        setTime();
        showNotMana();
    }

    public void showTimeAttack(float timerAttacks)
    {
        timerAttack = timerAttacks;
    }
    public void showTimeFireball(float timerFireballs)
    {
        timerFireball = timerFireballs;
    }
    public void showTimeDash(float timerDashs)
    {
        timerDash = timerDashs;
    }

    public void setTime()
    {
        timerAttack -= Time.deltaTime;
        imageAttack.fillAmount = timerAttack / 0.1f;

        timerFireball -= Time.deltaTime;
        imageFireball.fillAmount = timerFireball / 5f;

        timerDash -= Time.deltaTime;
        imageDash.fillAmount = timerDash / 2f;
    }

    public void showNotMana()
    {
        if (Player.ins.currentPower < 20)
        {
            showNotmanaFireBall.SetActive(true);
        }
        else
        {
            showNotmanaFireBall.SetActive(false);
        }



        if (Player.ins.currentPower < 5)
        {
            showNotmanaDash.SetActive(true);
        }
        else
        {
            showNotmanaDash.SetActive(false);
        }
    }
}
