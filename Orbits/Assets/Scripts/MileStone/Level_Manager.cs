using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Level_Manager : MonoBehaviour
{
    public static Level_Manager Instance;

    public GameObject _Orbit;
    public GameObject _Proton;
    public GameObject _nutron;
    public GameObject _Player;
    public Transform Nucleus;
    public GameObject Photon;
    public int[] OrbitsRadi;


    public CinemachineVirtualCamera _camers;


    List<GameObject> Orbits;
    List<GameObject> Protons;
    List<GameObject> Nutrons;
    GameObject player;
    Level currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Orbits = new List<GameObject>();
        Protons = new List<GameObject>();
        Nutrons = new List<GameObject>();
    }

    
    public void CreateLevel(Level level)
    {
        currentLevel = level;
        //CreatePlayer
        player = Instantiate(_Player, transform).transform.GetChild(0).gameObject;
        player.transform.localPosition = new Vector3(UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f));
        player.GetComponentInParent<RotateAround>().speed = UnityEngine.Random.Range(20, 60);

        //Create Orbits
        CreateOrbits(level);
        //CreateNucleus
        CreateNucleus(level);
       
    }

    private void CreateOrbits(Level level)
    {
        int electrons = 0;
        for (int i = 0; i < level.Orbits; i++)
        {
            GameObject GO = Instantiate(_Orbit, transform);
            Orbit Orbit = GO.GetComponent<Orbit>();
            Orbit.CreateOrbitCircle(OrbitsRadi[i]);
            if (electrons < level.electrons)
            {
                //if (electrons + 2 > level.electrons)
                //{
                if (i % 5 != 0 && i == 0)
                {
                    Orbit.CreateEletrons(OrbitsRadi[i], 2);
                    electrons += 2;
                }
                else
                {
                    Orbit.CreateEletrons(OrbitsRadi[i], 1);
                    electrons++;
                }
                //}
                //else if(electrons+2 <= level.electrons)
                //{
                    //Orbit.CreateEletrons(OrbitsRadi[i], 2);
                    //electrons += 2;
                //}
            }
            Orbits.Add(GO);
        }
        //if(level.LevelNumber > 6)
        //{
        //    _camers.gameObject.SetActive(true);
        //  //  _camers.Follow = player.transform;
        //    _camers.LookAt = player.transform;
        //}
    }

    private void CreateNucleus(Level level)
    {
        for (int i = 0; i < level.Protons; i++)
        {
            GameObject GO = Instantiate(_Proton, Nucleus);
            if (i % 2 == 0)
                GO.GetComponent<RotateAround>().speed = 25;
            else
                GO.GetComponent<RotateAround>().speed = -25;

            GO.transform.GetChild(0).localPosition = new Vector3(UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f));
            float speed = UnityEngine.Random.Range(20, 60);
            GO.GetComponentInParent<RotateAround>().speed = speed;
            Protons.Add(GO.transform.GetChild(0).gameObject);
        }
        
        for (int i = 0; i < level.neutrons; i++)
        {
            GameObject GO = Instantiate(_nutron, Nucleus);
            if (i % 2 == 0)
                GO.GetComponent<RotateAround>().speed = 25;
            else
                GO.GetComponent<RotateAround>().speed = -25;

            GO.transform.GetChild(0).localPosition = new Vector3(UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f));
            float speed = UnityEngine.Random.Range(20, 60);
            GO.GetComponentInParent<RotateAround>().speed = speed;
            Nutrons.Add(GO.transform.GetChild(0).gameObject);
        }
    }


    public void ShootPhoton()
    {
        Photon.transform.position = new Vector3(0, -50, 0);
        Photon.SetActive(true);
        Photon.transform.DOMove(Vector3.zero, 1f);
        Invoke("DisperseNucleus", 1);
    }

    void DisperseNucleus()
    {
        Photon.SetActive(false);
        //FindObjectOfType<LevelManager>().DisperseObjects();
        player.transform.position = Vector3.zero;
        player.transform.DOLocalMove(new Vector3(1, 0, 0), .5f);
        
        if(Protons.Count != null)
        {
            foreach (var item in Protons)
            {
                float speed = UnityEngine.Random.Range(30, 60);
                int Xpos = UnityEngine.Random.Range(OrbitsRadi[0], OrbitsRadi[currentLevel.Orbits]);
                item.GetComponentInParent<RotateAround>().speed = Xpos % 2 == 0 ? speed : -speed;
                // if (Xpos == 0) Xpos = UnityEngine.Random.Range(1, LevelRadius);
                item.transform.DOLocalMove(new Vector3(Xpos, 0, 0), .5f);
            }
        }

        if(Nutrons.Count != null)
        {
            foreach (var item in Nutrons)
            {
                float speed = UnityEngine.Random.Range(30, 60);
                int Xpos = UnityEngine.Random.Range(OrbitsRadi[0], OrbitsRadi[currentLevel.Orbits]);
                item.GetComponentInParent<RotateAround>().speed = Xpos % 2 == 0 ? speed : -speed;
                //if (Xpos == 0) Xpos = UnityEngine.Random.Range(1, LevelRadius);
                item.transform.DOLocalMove(new Vector3(Xpos, 0, 0), .5f);
            }
        }
    }

    public void CheckPlayerCompletedtheLevelOrNot(int raddius)
    {

        if (raddius > OrbitsRadi[currentLevel.Orbits - 1])
        {
            Debug.LogError("LevelFinished");
            player.GetComponent<PlayerControl>().enabled = false;
            player.GetComponent<Rigidbody>().isKinematic = false;
            //  player.GetComponent<Rigidbody>().AddForce(new Vector3(-200,0,0));
            Game_Manager.Instance.FadeIn();
            Game_Manager.Instance.LoadNextLevel();
            Invoke("ResetLevel", .75f);
        }
    }


    public void ResetLevel()
    {
        Destroy(player.transform.parent.gameObject);
        if (Protons.Count != null)
        {
            foreach (var item in Protons)
            {
                Destroy(item.transform.parent.gameObject);
                
            }
            Protons.Clear();
            Protons = new List<GameObject>();
        }
        if (Nutrons.Count != null)
        {
            foreach (var item in Nutrons)
            {
                Destroy(item.transform.parent.gameObject);
            }
            Nutrons.Clear();
            Nutrons = new List<GameObject>();
        }
        foreach (var item in Orbits)
        {
            Destroy(item);
        }
        Orbits.Clear();
        Orbits = new List<GameObject>();
    }
}

