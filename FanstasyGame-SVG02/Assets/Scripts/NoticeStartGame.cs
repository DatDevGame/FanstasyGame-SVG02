using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeStartGame : MonoBehaviour
{

    public GameObject notice;

    float timer;
    float timeDuration;

    bool confirmShowNotice;
    bool startGame = true;
    // Start is called before the first frame update

    private void Awake()
    {
        notice.SetActive(false);
    }
    void Start()
    {
        timeDuration = 5f;
        timer = timeDuration;
        
    }

    // Update is called once per frame
    void Update()
    {
        setShowNotice();
        startGameWhenNotice();
    }

    public void setShowNotice()
    {
        if (startGame == false) return;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            notice.SetActive(true);
            confirmShowNotice = true;
            Time.timeScale = 0f;
        }
    }

    public void startGameWhenNotice()
    {
        if (Input.GetKeyDown(KeyCode.J) && confirmShowNotice)
        {
            notice.SetActive(false);
            confirmShowNotice = false;
            startGame = false;
            Time.timeScale = 1f;
        }
    }
}
