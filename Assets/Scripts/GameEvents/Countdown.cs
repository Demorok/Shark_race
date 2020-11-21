using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float countdown;
    public Text text;
    public AudioSource mainTheme;

    AudioClip countdownSound;
    AudioClip startSound;
    AudioSource audioPlayer;

    float timeToStart;

    void Start()
    {
        Time.timeScale = 0;
        timeToStart = Time.realtimeSinceStartup + countdown;

        audioPlayer = GetComponent<AudioSource>();
        countdownSound = Resources.Load<AudioClip>("Sound/Start_3_2_1");
        startSound = Resources.Load<AudioClip>("Sound/Start_scream");
        audioPlayer.PlayOneShot(countdownSound);
    }

    // Update is called once per frame
    void Update()
    {
        float timeLeft = timeToStart - Time.realtimeSinceStartup;
        if (timeLeft > 0)
            text.text = Mathf.Ceil(timeLeft).ToString();
        else if (!audioPlayer.isPlaying && Time.timeScale == 0)
        {
            text.text = "Go!";
            audioPlayer.PlayOneShot(startSound);
            Time.timeScale = 1;
            mainTheme.Play();
        }
        else if(!audioPlayer.isPlaying && Time.timeScale == 1)
        {
            Destroy(gameObject);
        }
    }
}
