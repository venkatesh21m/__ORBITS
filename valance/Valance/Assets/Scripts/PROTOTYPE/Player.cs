using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THEORBIT;

namespace THEORBIT
{
    public class Player : MonoBehaviour
    {
        float radius;
        public OrbitManager OM;
        public AudioSource ASource;
        public AudioClip collected;
        public AudioClip jump;
        public AudioClip GameOver;
        
        RotateAround Rotate;
        float speed;
        bool gameRunning = false;

        private void Start()
        {
            radius = .5f;
            Rotate = GetComponentInParent<RotateAround>();
            // OM = OrbitManager.Instance;
            speed = Rotate.speed;
            ORBIT_GAMEMANAGER.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        }

        private void HandleGameStateChanged(ORBIT_GAMEMANAGER.GameState current, ORBIT_GAMEMANAGER.GameState previous)
        {
            if (current == ORBIT_GAMEMANAGER.GameState.Game && previous == ORBIT_GAMEMANAGER.GameState.GameOver|| current == ORBIT_GAMEMANAGER.GameState.LevelDifficult && previous == ORBIT_GAMEMANAGER.GameState.GameOver)
            {
                transform.localPosition = new Vector3(.5f, 0, 0);
                radius = .5f;
                GetComponentInParent<RotateAround>().speed = -75;
            }
            if(current == ORBIT_GAMEMANAGER.GameState.Game)
            {
                gameRunning = true;
            }
            if(current == ORBIT_GAMEMANAGER.GameState.GameOver)
            {
                gameRunning = false;
            }

        }

        //Update is called once per frame
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.RightArrow))
        //    {
        //        JumpToOuterOrbit();
        //    }
        //    if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    {
        //        JumpToInnerOrbit();
        //    }
        //}

        public void JumpToInnerOrbit()
        {
            // Vector3 pos = transform.position;
            if (!gameRunning)
            {
                return;
            }

            if (radius <= 1.5f)
            {
                return;
            }
            else
            {
                ASource.PlayOneShot(jump);
                radius -= 1;
                if (OM.SetCurrentOrbit((int)radius+.5f))
                {
                    Rotate.speed = +speed;
                }
                else
                {
                    Rotate.speed = -speed;
                }
                // pos.x = radius;
                // transform.DOMove(new Vector3(radius, 0, 0), .5f);
                transform.DOLocalMove(new Vector3(radius, 0, 0), .25f);
                // transform.localPosition = new Vector3(radius,0,0);
            }
        }

        public void JumpToOuterOrbit()
        {
            // Vector3 pos = transform.position;

            if (!gameRunning)
            {
                return;
            }

            if (radius >= 4.5f)
            {
                return;
            }
            else
            {
                ASource.PlayOneShot(jump);
                radius += 1;
                if (OM.SetCurrentOrbit((int)radius+.5f))
                {
                    Rotate.speed = +speed;
                }
                else
                {
                    Rotate.speed = -speed;
                }


                // pos.x = radius;
                transform.DOLocalMove(new Vector3(radius, 0, 0), .25f);

                //transform.DOMove(new Vector3(radius, 0, 0), .5f);
                // transform.localPosition = new Vector3(radius, 0, 0);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Electron"))
            {
                ORBIT_GAMEMANAGER.Instance.UpdateState(ORBIT_GAMEMANAGER.GameState.GameOver);
                GetComponentInParent<RotateAround>().speed = 0;
                // StartCoroutine(GameOver());
                ASource.PlayOneShot(GameOver);

            }
            else if (other.CompareTag("Collectable"))
            {
                ORBIT_GAMEMANAGER.Instance.AdScore();
                ASource.PlayOneShot(collected);
            }
        }


       
    }
}
