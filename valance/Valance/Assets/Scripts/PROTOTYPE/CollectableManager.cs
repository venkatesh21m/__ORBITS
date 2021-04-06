using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace THEORBIT
{
    public class CollectableManager : MonoBehaviour
    {
        public static CollectableManager Instance;

        public GameObject collectable;
        float speed;


        float[] poss = { 1.5f, 2.5f, 3.5f, 4.5f };
        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            Invoke("CreateCollectable", 2.5f);
            speed = collectable.GetComponentInParent<RotateAround>().speed;
            //if (collectable.GetComponentInParent<RotateAround>().closckwise)
            //{
            //    collectable.GetComponentInParent<RotateAround>().speed = speed;
            //}
            //else
            //{
            //    collectable.GetComponentInParent<RotateAround>().speed = -speed;
            //}
            ORBIT_GAMEMANAGER.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        }

        private void HandleGameStateChanged(ORBIT_GAMEMANAGER.GameState current, ORBIT_GAMEMANAGER.GameState previous)
        {
            if(current == ORBIT_GAMEMANAGER.GameState.GameOver && previous == ORBIT_GAMEMANAGER.GameState.Game)
            {
                GetComponent<RotateAround>().speed = 0;
            }
            else
            {
                GetComponent<RotateAround>().speed = speed;
            }
        }

        public void CreateCollectable()
        {
            int index = UnityEngine.Random.Range(0, poss.Length);
            float pos = poss[index];
                
            GetComponentInChildren<RotateAround>().speed = OrbitManager.Instance.GetOrbitDirection(index) ? speed : -speed;

            collectable.GetComponent<SphereCollider>().enabled = true;

            collectable.transform.localScale = Vector3.zero;
            collectable.transform.position = new Vector3(pos, 0, 0);
            collectable.transform.DOScale(new Vector3(.5f, .5f, .5f), .5f);
        }

        public void DisableCollectable()
        {
            collectable.transform.DOScale(Vector3.zero, .5f);
            collectable.GetComponent<SphereCollider>().enabled = false;
            Invoke("CreateCollectable", .25f);
        }

    }
}
