using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public List<GameObject> spawn;
    public bool reversed;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in spawn)
        {
            if (reversed)
                Instantiate(obj, transform.position, Quaternion.Euler(0, 0, 180));
            else
                Instantiate(obj, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
