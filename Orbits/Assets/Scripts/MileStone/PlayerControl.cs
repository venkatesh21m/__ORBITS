using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Level_Manager LM;

    int curentOrbitIndex;
    float speed;
    private void Start()
    {
        LM = Level_Manager.Instance;
        curentOrbitIndex = -1;
        speed = GetComponentInParent<RotateAround>().speed;
    }

    public void JumpToOuterOrbit()
    {
        curentOrbitIndex = curentOrbitIndex + 1;
        int pos = LM.OrbitsRadi[curentOrbitIndex];
        transform.DOLocalMove(new Vector3(pos,0,0),.25f);
        GetComponentInParent<RotateAround>().speed = pos % 2 == 0 ? speed : -speed;
        LM.CheckPlayerCompletedtheLevelOrNot(pos);
    }

    public void JumpToInnerOrbit()
    {
        
        curentOrbitIndex = curentOrbitIndex - 1;
        if(curentOrbitIndex < 0)
        {
            curentOrbitIndex = 0;
        }
        int pos = LM.OrbitsRadi[curentOrbitIndex];
        transform.DOLocalMove(new Vector3(pos, 0, 0), .25f);
        GetComponentInParent<RotateAround>().speed = pos % 2 == 0 ? speed : -speed;
        LM.CheckPlayerCompletedtheLevelOrNot(pos);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Electron"))
        {

        }
        else if (other.CompareTag("Proton"))
        {

        }
        else if (other.CompareTag("Nutron"))
        {

        }
    }
}
