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
    public int counter { get; private set; }

    GameObject clone;
    Shark shark;
    void Start()
    {
        clone = Instantiate(playerSpawn, transform.position, transform.rotation);
        if (!reversed)
            clone.transform.Rotate(0,0,180);
        shark = clone.GetComponent<Shark>();
    }
    private void Update()
    {
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
            if (!shark.wrongWay & shark.checkpoints >= 4)
            {
                shark.New_Lap();
                counter++;
            }
            score.text = counter.ToString();
        }
    }
}
