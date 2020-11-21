using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackDirection : MonoBehaviour
{
    public List<GameObject> engineReferences;
    public Vector3 trackDirection { get; private set; }

    Transform front;
    Transform rear;

    // Start is called before the first frame update
    void Start()
    {
        Initialise_Engine_Variables();
        trackDirection = (front.position - rear.position).normalized;
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
