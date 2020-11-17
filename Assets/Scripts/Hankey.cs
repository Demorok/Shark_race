using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hankey : MonoBehaviour
{
    public float poisonTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Shark shark = collision.GetComponent<Shark>();
            shark.Poison(poisonTime);
            Destroy(gameObject);
    }
}
