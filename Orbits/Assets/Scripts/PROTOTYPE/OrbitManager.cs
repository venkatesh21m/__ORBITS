using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{

    public static OrbitManager Instance;

    List<RotateAround> Orbits;
    int currentOrbitindex;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Orbits = new List<RotateAround>();
        Orbits = GetComponentsInChildren<RotateAround>().ToList();
    }

    public bool SetCurrentOrbit(float radius)
    {
        currentOrbitindex = (int)radius-1;
        Debug.LogError(currentOrbitindex);
        return Orbits[currentOrbitindex]./*GetComponentInChildren<RotateAround>().*/closckwise;
    }

    public bool GetOrbitDirection(int value)
    {
        return Orbits[value]./*GetComponentInChildren<RotateAround>().*/closckwise;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
