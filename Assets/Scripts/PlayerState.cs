using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public GameObject playerSpawn;
    public GameObject playerDoor;
    public bool reversed;
    public int winCondition;

    public Text info;
    public Text score;
    public Image cooldown;


    int counter = -1; //для нивелирования триггера при спавне
    GameObject clone;
    Shark shark;
    void Start()
    {
        clone = Instantiate(playerSpawn, transform);
        if (reversed)
            clone.transform.Rotate(0,0,-90);
        else
            clone.transform.Rotate(0, 0, 90);
        shark = clone.GetComponent<Shark>();
    }
    private void Update()
    {
        if (shark.wrongWay)
            info.text = shark.data.nickname + " wrong way!";
        else
            info.text = "";
        if (counter >= winCondition)
            Destroy(playerDoor);
        cooldown.fillAmount = shark.cooldownTimeNormalised;

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
