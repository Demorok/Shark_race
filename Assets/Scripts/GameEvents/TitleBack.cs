using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBack : MonoBehaviour
{
    public void Titles()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
