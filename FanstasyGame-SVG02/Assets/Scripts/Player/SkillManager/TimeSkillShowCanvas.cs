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

    float timerAttack;
    float timerFireball;
    float timerDash;
    // Start is called before the first frame update
    void Start()
    {
        ins = this;
    }

    // Update is called once per frame
    void Update()
    {
        setTime();
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
}
