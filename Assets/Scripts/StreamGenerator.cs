using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamGenerator : MonoBehaviour
{
    public SGData sGData;
    public PlayerState player1Spawner;
    public PlayerState player2Spawner;
    public Transform targetObject;
    public List<GameObject> engineReferences;

    Transform front;
    Transform rear;

    Vector3 flow;
    Vector3 direction;

    int turnDirection;
    float shadowTurnTime;
    float timeToChange;
    float timeToTurn;

    void Start()
    {
        Initialise_Engine_Variables();
        direction = (front.position - rear.position).normalized;
        flow = direction * (player1Spawner.counter + player2Spawner.counter) * sGData.streamPower;
        timeToTurn = Time.time + sGData.turnTime;
    }
    private void Update()
    {
        if (timeToChange - Time.time <= 0)
            Change_Stream_Direction();
    }
    private void FixedUpdate()
    {
        if (timeToTurn - Time.time > 0)
            Turn_Stream();
        targetObject.position += -flow * Time.fixedDeltaTime;
    }
    private void Change_Stream_Direction()
    {
        shadowTurnTime = Random.Range(1, sGData.turnTime + 1);
        timeToTurn = Time.time + shadowTurnTime;
        turnDirection = Random.Range(0, 2);
        timeToChange = Time.time + sGData.timeBeforeChange;
    }

    void Turn_Stream()
    {
            float angleChangeStep = sGData.turnSpeed * Time.fixedDeltaTime;
            angleChangeStep = turnDirection > 0 ? angleChangeStep : -angleChangeStep;
            transform.Rotate(Vector3.forward * angleChangeStep);
            direction = (front.position - rear.position).normalized;
            flow = direction * (player1Spawner.counter + player2Spawner.counter) * sGData.streamPower;
    }

    void Initialise_Engine_Variables()
    {
        foreach (GameObject obj in engineReferences)
        {
            if (obj.name == "Front")
                front = obj.transform;
            else if (obj.name == "Rear")
                rear = obj.transform;
        }
    }
}
