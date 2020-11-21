using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    public Text congratz;

    // Start is called before the first frame update
    void Start()
    {
        congratz.text = FinishEvent.winnerName + " is the winner!";
    }

    public void Play_Again()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
