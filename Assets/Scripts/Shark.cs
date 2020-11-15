using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public float maxSpeed;
    public float maxSpeedTime;
    public float maxSpeedStopTime;
    public float TurnSpeed;
    public Controller controller;


    const float EPS = 0.000001f;
    Rigidbody2D shark;
    Transform front;
    Transform rear;
    Vector3 direction;
    float acceleration;
    float deceleration;
    float reqiredSpeed;

    void Start()
    {
        shark = GetComponent<Rigidbody2D>();
        front = transform.Find("Front").GetComponent<Transform>();
        rear = transform.Find("Rear").GetComponent<Transform>();
        direction = (front.position - rear.position).normalized;
    }

    private void FixedUpdate()
    {
        Speed_Control();
        Direction_Control();
    }

    void Update()
    {
        if (Input.GetKeyDown(controller.Up_Button()))
            reqiredSpeed = maxSpeed;
        if (Input.GetKeyUp(controller.Up_Button()))
            reqiredSpeed = 0;
        if (Input.GetKey(controller.Left_Button()))
            shark.transform.Rotate(Vector3.forward * TurnSpeed * Time.deltaTime);
        if (Input.GetKey(controller.Right_Button()))
            shark.transform.Rotate(Vector3.back * TurnSpeed * Time.deltaTime);

    }

    void Direction_Control()
    {
        Vector3 currenDirection = (front.position - rear.position).normalized;
        if (currenDirection == direction)
            return;
        direction = currenDirection;
        shark.velocity = (direction * shark.velocity.magnitude);
    }

    void Speed_Control()
    {
        if (reqiredSpeed > maxSpeed)
            reqiredSpeed = maxSpeed;
        if (Mathf.Abs(shark.velocity.sqrMagnitude - reqiredSpeed * reqiredSpeed) <= EPS)
            return;
        acceleration = maxSpeed / maxSpeedTime * Time.fixedDeltaTime;
        deceleration = maxSpeed / maxSpeedStopTime * Time.fixedDeltaTime;
        float currentSpeed = shark.velocity.magnitude;
        float speedDifference = reqiredSpeed - currentSpeed;
        float maxSpeedChange = speedDifference < 0 ? deceleration : acceleration;
        if (Mathf.Abs(speedDifference) < maxSpeedChange)
            shark.velocity = direction * reqiredSpeed;
        else
        if (speedDifference > 0)
            shark.velocity = direction * (currentSpeed + maxSpeedChange);
        else
            shark.velocity = direction * (currentSpeed - maxSpeedChange);

    }
}
