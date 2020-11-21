using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hankey : MonoBehaviour
{
    public BombData bombData;

    float timeToLive;
    float timeToReady;

    private void Start()
    {
        timeToLive = Time.time + bombData.livingTime;
        timeToReady = Time.time + bombData.readiness;
    }

    private void Update()
    {
        if (timeToLive - Time.time < 0)
            Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (timeToReady - Time.time < 0)
            if (collision.gameObject.CompareTag("Player"))
            {
                Shark shark = collision.gameObject.GetComponent<Shark>();
                shark.Injure(bombData.injureTime);
                Destroy(gameObject);
            }
    }
}
