using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMap1 : MonoBehaviour
{
    public static BossMap1 ins;

    AudioSource aus;
    Animator anim;
    Rigidbody2D rb;

    public GameObject powerUp;

    //Health bar - Power bar
    public Slider healthSlider;
    public Slider powerSlider;
    public Canvas statusBoss1;

    //sound
    public AudioClip sounDead;

    public float currentHealthBoss;
    float maxHealthBoss;

    public float powerAttackSkill;

    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        ins = this;

        maxHealthBoss = 500f;
        currentHealthBoss = maxHealthBoss;
        healthSlider.maxValue = maxHealthBoss;
        healthSlider.value = currentHealthBoss;

        powerSlider.maxValue = 100f;

    }

    // Update is called once per frame
    void Update()
    {
        statusBoss1.transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    public void ReceiveDameBoss(int dame)
    {
        currentHealthBoss -= dame;
        healthSlider.value = currentHealthBoss;
        if (currentHealthBoss <= 0)
        {
            DeadBoss();
        }
        Debug.Log("HealthBoss" + currentHealthBoss);
    }
    public void DeadBoss()
    {
        transform.position = new Vector2(transform.position.x, 19.31f);
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("WalkBoss1", false);
        aus.PlayOneShot(sounDead);
        GetComponent<Rigidbody2D>().isKinematic = true;
        this.rb.constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetBool("DeadBoss1", true);
        Destroy(gameObject, 5f);
    }

    public void setPower(int power)
    { 

        powerAttackSkill += power;
        powerSlider.value = powerAttackSkill;
        if (powerAttackSkill > 100)
        {
            powerAttackSkill = 100;
        }
    }
    public void dePower(int power)
    {
        powerAttackSkill -= power;
    }
}
