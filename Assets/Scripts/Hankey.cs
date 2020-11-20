using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hankey : MonoBehaviour
{
    public float livingTime;
    public float poisonTime;
    public float readiness;

    float timeToLive;
    float timeToReady;

    private void Start()
    {
        timeToLive = Time.time + livingTime;
        timeToReady = Time.time + readiness;
    }

    private void Update()
    {
        if (timeToLive - Time.time < 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (timeToReady - Time.time < 0)
            if (collision.gameObject.CompareTag("Player"))
            {
                Shark shark = collision.gameObject.GetComponent<Shark>();
                shark.Poison(poisonTime);
                Destroy(gameObject);
            }
    }
}
