using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THEORBIT;

public class Nucleus : MonoBehaviour
{

    [HideInInspector] public Player player;
    [HideInInspector] public GameObject[] Protons;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInChildren<Player>();
        Protons = GameObject.FindGameObjectsWithTag("Proton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
