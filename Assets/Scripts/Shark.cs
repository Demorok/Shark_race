using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shark : MonoBehaviour
{
    public SharkData sharkData;
    public PlayerData playerData;
    public List<GameObject> engineReferences;

    public bool wrongWay { get; private set; }
    public float cooldownTimeNormalised { get; private set; }
    public int checkpoints { get; private set; }

    const float EPS = 0.000001f;
    const float WRONGWAYANGLE = 120;

    GameObject bomb;

    Rigidbody2D shark;

    Transform front;
    Transform rear;
    Transform bombSpawner;

    Vector3 direction;
    Vector3 trackDirection;

    Animator anim;

    AudioSource audioPlayer;
    AudioClip bombPlaced;
    AudioClip injured;

    SpriteRenderer sharkImage;

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
        Initialise_Engine_Variables();

        audioPlayer = GetComponent<AudioSource>();
        shark = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        bomb = Resources.Load<GameObject>("Prefabs/Hankey");
        bombPlaced = Resources.Load<AudioClip>("Sound/bomb_placed");
        injured = Resources.Load<AudioClip>("Sound/boom");

        direction = (front.position - rear.position).normalized;
    }

    private void FixedUpdate()
    {
        Speed_Control();
        Direction_Control();
    }

    void Update()
    {
        Player_Controller();
        Bomb_CD_Control();
        Orientation_Control();
    }

    private void Orientation_Control()
    {
        if (transform.rotation.eulerAngles.z > 180)
            sharkImage.flipX = false;
        else
            sharkImage.flipX = true;
    }

    private void Bomb_CD_Control()
    {
        cooldownTimeNormalised = (timeToReady - Time.time) / sharkData.bombCooldownTime;
    }

    void Direction_Control()
    {
        Vector3 currenDirection = (front.position - rear.position).normalized;
        float angle = Vector3.Angle(currenDirection, trackDirection);
        if (!winner)
            if (angle > WRONGWAYANGLE)
                wrongWay = true;
            else
                wrongWay = false;
        if (currenDirection != direction)
        {
            direction = currenDirection;
            shark.velocity = direction * shark.velocity.magnitude;
        }    
    }

    void Speed_Control()
    {
        float currentSharkSqrSpeed = shark.velocity.sqrMagnitude;
        anim.SetFloat("sqrvelocity", currentSharkSqrSpeed);

        if (recoveryTime - Time.time > 0)
            reqiredSpeed = 0;

        if (reqiredSpeed > sharkData.maxSpeed)
            reqiredSpeed = sharkData.maxSpeed;

        if (Mathf.Abs(currentSharkSqrSpeed - reqiredSpeed * reqiredSpeed) <= EPS)
            return;

        acceleration = sharkData.maxSpeed / sharkData.maxSpeedTime * Time.fixedDeltaTime;
        deceleration = sharkData.maxSpeed / sharkData.maxSpeedStopTime * Time.fixedDeltaTime;

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
            TrackDirection track = collision.GetComponent<TrackDirection>();
            trackDirection = track.trackDirection;
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

    private void Player_Controller()
    {
        if (Input.GetKey(playerData.Up_Button))
            reqiredSpeed = sharkData.maxSpeed;
        if (Input.GetKeyUp(playerData.Up_Button))
            reqiredSpeed = 0;
        if (Input.GetKey(playerData.Left_Button))
            shark.transform.Rotate(Vector3.forward * sharkData.TurnSpeed * Time.deltaTime);
        if (Input.GetKey(playerData.Right_Button))
            shark.transform.Rotate(Vector3.back * sharkData.TurnSpeed * Time.deltaTime);
        if (Input.GetKey(playerData.Trap_Button))
            Deploy_Poo();
    }

    private void Deploy_Poo()
    {
        if (cooldownTimeNormalised <= 0 && Time.timeScale > 0 && (!winner))
        {
            Instantiate(bomb, bombSpawner.position, Quaternion.Euler(0,0,0));
            timeToReady = Time.time + sharkData.bombCooldownTime;
            audioPlayer.PlayOneShot(bombPlaced);
        }
    }
    void Initialise_Engine_Variables()
    {
        foreach (GameObject obj in engineReferences)
        {
            if (obj.name == "Front")
                front = obj.transform;
            else if (obj.name == "Rear")
                rear = obj.transform;
            else if (obj.name == "BombSpawner")
                bombSpawner = obj.transform;
            else if (obj.name == "Shark")
                sharkImage = obj.GetComponent<SpriteRenderer>();
        }
    }
    public void Injure(float seconds)
    {
        recoveryTime = Time.time + seconds;
        audioPlayer.PlayOneShot(injured);
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
