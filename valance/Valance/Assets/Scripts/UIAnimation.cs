using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] float strength = 0.5f;
    [SerializeField] int vibration = 5;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DOAnimation", 1, 1.5f);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void DOAnimation()
    {
        transform.DOShakeScale(1, strength, vibration);
    }
}
