using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishEvent : MonoBehaviour
{
    public static string winnerName { get; private set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Shark shark = collision.GetComponent<Shark>();
        winnerName = shark.data.name;
    }
}
