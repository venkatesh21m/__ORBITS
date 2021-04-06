using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace THEORBIT
{
    public class ORBIT_LEVELMANAGER : MonoBehaviour
    {
        public GameObject[] firstelectrons;
        public GameObject[] secondelectrons;
        public GameObject[] thirdelectrons;
        public ORBIT_GAMEMANAGER GM;

        RotateAround[] rotations;
       
        
        // Start is called before the first frame update
        void Start()
        {
            // GM = ORBIT_GAMEMANAGER.Instance;
            GM.onGameDifficultyChanged.AddListener(HandleLevelDifficultyChanged);
            GM.OnGameStateChanged.AddListener(HandleGameStateChanged);
            rotations = GetComponentsInChildren<RotateAround>();
        }


        private void OnEnable()
        {
          
        }


        private void HandleGameStateChanged(ORBIT_GAMEMANAGER.GameState current, ORBIT_GAMEMANAGER.GameState previous)
        {
            if(current == ORBIT_GAMEMANAGER.GameState.GameOver && previous == ORBIT_GAMEMANAGER.GameState.Game)
            {
                StopRotation();
            }
            
            if(current == ORBIT_GAMEMANAGER.GameState.Game && previous == ORBIT_GAMEMANAGER.GameState.GameOver || current == ORBIT_GAMEMANAGER.GameState.LevelDifficult && previous == ORBIT_GAMEMANAGER.GameState.GameOver)
            {
                StartRotation();
            }
            //if(current == ORBIT_GAMEMANAGER.GameState.tutorial&&previous == ORBIT_GAMEMANAGER.GameState.LevelDifficult)
            //{
            //    StartCoroutine(StartTutorial());
            //}
        }

       
        private void HandleLevelDifficultyChanged(Difficulty currentdifficulty,Difficulty previousdifficulty)
        {
            switch (currentdifficulty)
            {
                case Difficulty.tutorial:
                    secondelectrons[1].SetActive(true);
                    secondelectrons[1].transform.DOScale(new Vector3(0.5f, .5f, .5f),.5f);
                    if(previousdifficulty == Difficulty.easy)
                    {
                        foreach (var item in firstelectrons)
                        {
                            item.transform.DOScale(Vector3.zero, .5f);
                            Invoke("DiablefirstElectrons", .5f);
                        }
                    }
                    else
                    {
                        //Tutorial
                    }

                    break;
                case Difficulty.easy:
                    if(previousdifficulty == Difficulty.tutorial)
                    {
                        foreach (var item in firstelectrons)
                        {
                            item.SetActive(true);
                            item.transform.DOScale(new Vector3(.5f, .5f, .5f), .5f);
                        }
                    }
                    else if(previousdifficulty == Difficulty.medium)
                    {
                        foreach (var item in secondelectrons)
                        {
                            item.transform.DOScale(Vector3.zero, .5f);
                            Invoke("DiableSecondElectrons", .5f);
                        }
                        foreach (var item in thirdelectrons)
                        {
                            item.transform.DOScale(Vector3.zero, .5f);
                            Invoke("Diablethirdelectrons", .5f);
                        }
                    }
                   

                    break;
                case Difficulty.medium:
                    if (previousdifficulty == Difficulty.easy)
                    {
                        foreach (var item in secondelectrons)
                        {
                            item.SetActive(true);
                            item.transform.DOScale(new Vector3(.5f,.5f,.5f),.5f);
                        }
                    }
                    else if (previousdifficulty == Difficulty.Hard)
                    {
                        foreach (var item in thirdelectrons)
                        {
                            item.transform.DOScale(Vector3.zero, .5f);
                            Invoke("Diablethirdelectrons", .5f);
                        }
                    }
                    break;

                case Difficulty.Hard:
                    if (previousdifficulty == Difficulty.medium)
                    {
                        foreach (var item in thirdelectrons)
                        {
                            item.SetActive(true);
                            item.transform.DOScale(new Vector3(.5f, .5f, .5f), .5f);
                        }
                    }
                    break;
                default:
                    break;
            }
        }



        //private IEnumerator StartTutorial()
        //{
        //    secondelectrons[1].SetActive(true);
        //    secondelectrons[1].transform.DOMove(new Vector3(.5f,.5f,.5f),.5f);
        //}


        void DiablefirstElectrons()
        {
            foreach (var item in firstelectrons)
            {
                item.SetActive(false);
            }
        }
        void DiableSecondElectrons()
        {
            foreach (var item in secondelectrons)
            {
                item.SetActive(false);
            }
        }
        void Diablethirdelectrons()
        {
            foreach (var item in thirdelectrons)
            {
                item.SetActive(false);
            }
        }
       

        void StopRotation()
        {
            foreach (var item in rotations)
            {
                item.speed = 0;
            }
        }

        void StartRotation()
        {
            for (int i = 0; i < rotations.Length; i++)
            {
                if(i%2 == 0)
                {
                    rotations[i].speed = 80;
                }
                else
                {
                    rotations[i].speed = -80;
                }
            }
        }

    }
}
