using System;
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
    public float pooCooldownTime;
    public PlayerData data;
    public GameObject bomb;
    public SpriteRenderer sharkImage;
    public AudioSource stunSound;

    public bool wrongWay { get; private set; }
    public float cooldownTimeNormalised { get; private set; }
    public int checkpoints { get; private set; }

    const float EPS = 0.000001f;


    Rigidbody2D shark;
    Transform front;
    Transform rear;
    Transform pooSpawner;
    Vector3 direction;
    Vector3 trackDirection;

    Animator anim;
    AudioSource deployBombSound;

    float acceleration;
    float deceleration;
    float reqiredSpeed;
    float timeToReady;
    float recoveryTime;
    bool winner = false;

    string prePreviousCheckpoint;
    string previousCheckpoint;

    void Start()
    {
        shark = GetComponent<Rigidbody2D>();
        front = transform.Find("Front").GetComponent<Transform>();
        rear = transform.Find("Rear").GetComponent<Transform>();
        pooSpawner = transform.Find("PooSpawner").GetComponent<Transform>();

        deployBombSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

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
        Time_Control();
        if (transform.rotation.eulerAngles.z > 180)
            sharkImage.flipX = false;
        else
            sharkImage.flipX = true;
    }

    private void Time_Control()
    {
        cooldownTimeNormalised = (timeToReady - Time.time) / pooCooldownTime;
    }

    void Direction_Control()
    {
        Vector3 currenDirection = (front.position - rear.position).normalized;
        float angle = Vector3.Angle(currenDirection, trackDirection);
        if (!winner)
            if (angle > 100)
                wrongWay = true;
            else
                wrongWay = false;
        if (currenDirection != direction)
            direction = currenDirection;
    }

    void Speed_Control()
    {
        float currentSharkSqrSpeed = shark.velocity.sqrMagnitude;
        anim.SetFloat("velocity", currentSharkSqrSpeed);
        if (recoveryTime - Time.time > 0)
            reqiredSpeed = 0;
        if (reqiredSpeed > maxSpeed)
            reqiredSpeed = maxSpeed;
        if (Mathf.Abs(currentSharkSqrSpeed - reqiredSpeed * reqiredSpeed) <= EPS)
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Track"))
            if (collision.name != previousCheckpoint & collision.name!= prePreviousCheckpoint & !wrongWay)
            {
                prePreviousCheckpoint = previousCheckpoint;
                previousCheckpoint = collision.name;
                checkpoints++;
            }
    }

    private void Shark_Controller()
    {
        if (Input.GetKey(data.Up_Button))
            reqiredSpeed = maxSpeed;
        if (Input.GetKeyUp(data.Up_Button))
            reqiredSpeed = 0;
        if (Input.GetKey(data.Left_Button))
            shark.transform.Rotate(Vector3.forward * TurnSpeed * Time.deltaTime);
        if (Input.GetKey(data.Right_Button))
            shark.transform.Rotate(Vector3.back * TurnSpeed * Time.deltaTime);
        if (Input.GetKey(data.Trap_Button))
            Deploy_Poo();
    }

    private void Deploy_Poo()
    {
        if (cooldownTimeNormalised <= 0 && Time.timeScale > 0 && (!winner))
        {
            Instantiate(bomb, pooSpawner.position, Quaternion.Euler(0,0,0));
            timeToReady = Time.time + pooCooldownTime;
            deployBombSound.Play();
        }
    }

    public void Poison(float seconds)
    {
        recoveryTime = Time.time + seconds;
        stunSound.Play();
    }

    public void Set_Winner(bool state)
    {
        winner = state;
    }

    public bool Get_Winner()
    {
       return winner;
    }

    public void New_Lap()
    {
        checkpoints = 0;
        previousCheckpoint = "";
        prePreviousCheckpoint = "";
    }
}
