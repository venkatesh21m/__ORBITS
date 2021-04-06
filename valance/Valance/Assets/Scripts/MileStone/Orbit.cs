using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public bool Direction;
    public Transform ElectronParent;
    public GameObject Electron;
    public LineRenderer LineDrawer;



    private float ThetaScale = 0.01f;
    private int Size;
    private float Theta = 0f;

    private void Start()
    {
       // LineDrawer = GetComponent<LineRenderer>();
    }

    internal void CreateOrbitCircle(int radius)
    {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        LineDrawer.SetVertexCount(Size);
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta);
            float y = radius * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
        if (radius % 2 == 0)
            GetComponentInChildren<RotateAround>().speed = radius*12;
        else
            GetComponentInChildren<RotateAround>().speed = -radius * 12;
    }

    internal void CreateEletrons(int radius, int number)
    {
       if(number == 1)
        {
            GameObject GO = Instantiate(Electron, ElectronParent);
            GO.transform.localPosition = new Vector3(radius, 0, 0);
        }
       else if(number == 2)
        {
            GameObject GO = Instantiate(Electron, ElectronParent);
            GO.transform.localPosition = new Vector3(radius, 0, 0);
            GO = Instantiate(Electron, ElectronParent);
            GO.transform.localPosition = new Vector3(-radius, 0, 0);
        }
    }
}
