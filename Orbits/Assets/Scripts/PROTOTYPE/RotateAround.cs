using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    //public Transform Centre;
    [Range(-200,200)]
    public float speed;
    [HideInInspector] public bool closckwise;

    // Start is called before the first frame update
    void Start()
    {
        if(speed > 0)
        {
            closckwise = true;
        }
        else if(speed<0)
        {
            closckwise = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, speed * Time.deltaTime);
    }
}
