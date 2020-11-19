using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamGenerator : MonoBehaviour
{
    public float timeBeforeChange;
    public float turnSpeed;
    public float turnTime;
    public float flowPower;
    public PlayerState player1;
    public PlayerState player2;
    public Transform movingFlow;

    Vector3 flow;

    const float EPS = 0.000001f;

    Transform front;
    Transform rear;
    Vector3 direction;

    int turnDirection;
    float shadowTurnTime;
    float timeToChange;
    float timeToTurn;

    void Start()
    {
        front = transform.Find("Front").GetComponent<Transform>();
        rear = transform.Find("Rear").GetComponent<Transform>();
        direction = (front.position - rear.position).normalized;
        flow = direction * (player1.counter + player2.counter) * flowPower;
        timeToTurn = Time.time + turnTime;
    }
    private void Update()
    {
        if (timeToChange - Time.time <= 0)
        {
            shadowTurnTime = Random.Range(1, turnTime + 1);
            timeToTurn = Time.time + shadowTurnTime;
            turnDirection = Random.Range(0, 2);
            timeToChange = Time.time + timeBeforeChange;
        }
    }
    private void FixedUpdate()
    {
        Turn_Flow();
        movingFlow.position += -flow * Time.fixedDeltaTime;
    }

    void Turn_Flow()
    {
        if (timeToTurn - Time.time > 0)
        {
            float angleChangeStep = turnSpeed * Time.fixedDeltaTime;
            angleChangeStep = turnDirection > 0 ? angleChangeStep : -angleChangeStep;
            transform.Rotate(Vector3.forward * angleChangeStep);
            direction = (front.position - rear.position).normalized;
            flow = direction * (player1.counter + player2.counter) * flowPower;
        }
    }
}
