using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UiManager : MonoBehaviour
{
    public Image LoadingImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void FadeIn()
    {
        LoadingImage.DOFade(1, .5f);
    }
    internal void FadeOut()
    {
        LoadingImage.DOFade(0, .1f);
    }

   
}
