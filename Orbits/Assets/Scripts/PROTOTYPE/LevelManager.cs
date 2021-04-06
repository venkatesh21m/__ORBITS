using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject[] protons;
    public GameObject[] Nutrons;
    public GameObject Player;

    public OrbitManager Orbits;

    public int LevelRadius;

    public void DisperseObjects()
    {
        Player.transform.DOLocalMove(new Vector3(2, 0, 0), .5f);
        foreach (var item in protons)
        {
            int Xpos = UnityEngine.Random.Range(-LevelRadius, LevelRadius);
            if (Xpos == 0) Xpos = UnityEngine.Random.Range(1, LevelRadius);
            item.transform.DOLocalMove(new Vector3(Xpos, 0, 0),.5f);
        }
        foreach (var item in Nutrons)
        {
            int Xpos = UnityEngine.Random.Range(-LevelRadius, LevelRadius);
            if (Xpos == 0) Xpos = UnityEngine.Random.Range(1, LevelRadius);
            item.transform.DOLocalMove(new Vector3(Xpos, 0, 0), .5f);
        }

    }
  
}
