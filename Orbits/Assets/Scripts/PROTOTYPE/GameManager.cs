using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public GameObject Photon;
    
    public TMPro.TMP_Text score;






    int _score = 0;


    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AdScore()
    {
        _score++;
        score.text = _score.ToString();
        //CollectableManager.Instance.DisableCollectable();
    }


    public void StartGame()
    {
        Photon.transform.position = new Vector3(0, -50, 0); 
        Photon.SetActive(true);
        Photon.transform.DOMove(Vector3.zero,1f);
        Invoke("DisperseNucleus", 1);
    }

    void DisperseNucleus()
    {
        Photon.SetActive(false);
        FindObjectOfType<LevelManager>().DisperseObjects();
    }

    internal void FadeIn()
    {
       
    }
}
