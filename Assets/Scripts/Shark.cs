using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shark : MonoBehaviour
{
    public float maxSpeed;
    public float maxSpeedTime;
    public float maxSpeedStopTime;
    public float TurnSpeed;
    public bool wrongWay;
    public PlayerController data;
    public Vector3 trackDirection;

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
        Shark_Controller();
    }

    void Direction_Control()
    {
        Vector3 currenDirection = (front.position - rear.position).normalized;
        float angle = Vector3.Angle(currenDirection, trackDirection);
        if (angle > 100)
            wrongWay = true;
        else
            wrongWay = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Track"))
        {
            Transform trackFront = collision.transform.Find("Front").GetComponent<Transform>();
            Transform trackRear = collision.transform.Find("Rear").GetComponent<Transform>();
            trackDirection = (trackFront.position - trackRear.position).normalized;
        }
    }

    private void Shark_Controller()
    {
        if (Input.GetKeyDown(data.Up_Button))
            reqiredSpeed = maxSpeed;
        if (Input.GetKeyUp(data.Up_Button))
            reqiredSpeed = 0;
        if (Input.GetKey(data.Left_Button))
            shark.transform.Rotate(Vector3.forward * TurnSpeed * Time.deltaTime);
        if (Input.GetKey(data.Right_Button))
            shark.transform.Rotate(Vector3.back * TurnSpeed * Time.deltaTime);
    }
}
