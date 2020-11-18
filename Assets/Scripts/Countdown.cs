using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float countdown;
    public Text text;
    public AudioSource mainTheme;
    public AudioSource startSound;

    float timeToStart;

    void Start()
    {
        Time.timeScale = 0;
        timeToStart = Time.realtimeSinceStartup + countdown;
    }

    // Update is called once per frame
    void Update()
    {
        float timeLeft = timeToStart - Time.realtimeSinceStartup;
        if (timeLeft > 0)
            text.text = Mathf.Ceil(timeLeft).ToString();
        else
        {
            Time.timeScale = 1;
            mainTheme.Play();
            startSound.Play();
            Destroy(gameObject);
        }

    }
}
