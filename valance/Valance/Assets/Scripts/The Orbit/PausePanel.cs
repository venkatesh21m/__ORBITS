using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PausePanel : MonoBehaviour
{
    [SerializeField] AudioMixer audio;
    // Start is called before the first frame update
    void Start()
    {


        
    }


    public void ChangeAudioVolume(float value)
    {
        audio.SetFloat("Audio", value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
