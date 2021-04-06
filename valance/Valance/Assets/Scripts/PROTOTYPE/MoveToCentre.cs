using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCentre : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Vector3.zero,.02f);
    }
}
