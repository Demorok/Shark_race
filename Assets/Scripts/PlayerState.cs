using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public GameObject playerSpawn;
    public GameObject playerDoor;
    public StreamGenerator sg;
    public bool reversed;
    public int winCondition;

    public Text info;
    public Text score;
    public Image cooldown;

    public Rigidbody2D sharkBody { get; private set; }
    public int counter { get; private set; }

    GameObject clone;
    Shark shark;
    void Start()
    {
        counter = -1; //для нивелирования триггера при спавне
        clone = Instantiate(playerSpawn, transform);
        if (!reversed)
            clone.transform.Rotate(0,0,180);
        shark = clone.GetComponent<Shark>();
        sharkBody = clone.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        shark.flow = sg.flow;
        if (shark.wrongWay)
            info.text = shark.data.nickname + " wrong way!";
        else
            info.text = "";
        if (counter >= winCondition)
        {
            Destroy(playerDoor);
            shark.Set_Winner(true);
        }
        cooldown.fillAmount = shark.cooldownTimeNormalised;
        if (shark.Get_Winner())
            info.text = shark.data.nickname + " claim your prize!";

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shark.Get_Winner())
        {
            if (shark.wrongWay)
                --counter;
            else
                ++counter;
            score.text = Mathf.Max(counter, 0).ToString();
        }
    }
}
