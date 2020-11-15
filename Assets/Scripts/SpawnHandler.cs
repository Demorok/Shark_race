using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnHandler : MonoBehaviour
{
    public GameObject playerSpawn;
    public bool reversed;

    public Text info;
    public Text score;
    int counter = -1; //для нивелирования триггера при спавне
    GameObject clone;
    Shark shark;
    void Start()
    {
        if (reversed)
            clone = Instantiate(playerSpawn, transform.position, Quaternion.Euler(0, 0, 180));
        else
            clone = Instantiate(playerSpawn, transform.position, Quaternion.Euler(0, 0, 0));
        shark = clone.GetComponent<Shark>();
    }
    private void Update()
    {
        if (shark.wrongWay)
            info.text = shark.data.nickname + " wrong way!";
        else
            info.text = "";

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shark shark = collision.GetComponent<Shark>();
        if (shark.wrongWay)
            --counter;
        else
            ++counter;
        score.text = Mathf.Max(counter, 0).ToString();
    }
}
