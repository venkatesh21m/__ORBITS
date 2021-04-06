using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;

    public int LevelNumber;
    public Level_Manager LM;
    public UiManager UI;

    public List<Level> levels;
    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LM.ShootPhoton();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponentInChildren<PlayerControl>().JumpToInnerOrbit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponentInChildren<PlayerControl>().JumpToOuterOrbit();
        }
    }

    public void StartGame()
    {
        LM.CreateLevel(levels[LevelNumber]);
    }

    internal void FadeIn()
    {
        UI.FadeIn();
    }

    internal void LoadNextLevel()
    {
        StartCoroutine("NextLevel");
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(.85f);
        LevelNumber++;
        LM.CreateLevel(levels[LevelNumber]);
        yield return new WaitForSeconds(.5f);
        UI.FadeOut();
    }
}


[System.Serializable]
public struct Level
{
    public int LevelNumber;
    public int Orbits;
    public int electrons;
    public int Protons;
    public int neutrons;
}