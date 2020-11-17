using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishEvent : MonoBehaviour
{
    public GameObject endingWindow;

    Text text;

    private void Start()
    {
        text = endingWindow.transform.Find("Text").GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        Shark shark = collision.GetComponent<Shark>();
        text.text = shark.data.name + " is the winner!";
        endingWindow.SetActive(true);
    }
}
