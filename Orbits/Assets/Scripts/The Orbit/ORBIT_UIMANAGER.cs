using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.UI;

namespace THEORBIT
{
    public class ORBIT_UIMANAGER : MonoBehaviour
    {
        public static ORBIT_UIMANAGER Instance;

        [Header("Panels")]
        public GameObject Titlepanel;
        public GameObject Tutorialpanel;
        public GameObject levelDiificultpanel;
        public GameObject GamePanel;
        public GameObject PausePanel;
        public GameObject GameOverPanel;

        [Header("Panel_detailes")]
        public TMP_Text ScoreText;
        public TMP_Text GameOverScoreText;
        public TMP_Text[] TutorialTexts;
        public TMP_Text easyBest;
        public TMP_Text mediumBest;
        public TMP_Text hardBest;

        int tut;

        public void StartGame()
        {
           
            //if(currentdifficultyIndex == 0)
            //{
            //    ORBIT_GAMEMANAGER.Instance.UpdateState(ORBIT_GAMEMANAGER.GameState.tutorial);
            //}
            //else
            {
                ORBIT_GAMEMANAGER.Instance.UpdateState(ORBIT_GAMEMANAGER.GameState.Game);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
              Instance = this;
           
           
            ORBIT_GAMEMANAGER.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
            ORBIT_GAMEMANAGER.Instance.onScoreChanged.AddListener(HandleScoreChanged);
            StartCoroutine(DisableTitle());
            Invoke("ShowLevelDifficulty", 2);
            Tutorial = true;
            tut = Tutorial_;
            currentdifficultyIndex = 0;
            if (tut == 1)
            {
               Invoke("NextPressed",1);
            }
            easyBest.text = EasyBest.ToString();
            mediumBest.text = MediumBest.ToString();
            hardBest.text = HardBest.ToString();
        }

        #region event Handles
        private void HandleGameStateChanged(ORBIT_GAMEMANAGER.GameState current, ORBIT_GAMEMANAGER.GameState previous)
        {
            if(current==ORBIT_GAMEMANAGER.GameState.Game&&previous == ORBIT_GAMEMANAGER.GameState.LevelDifficult)
            {
                StartCoroutine(disableleveldifficultPanel());
                Invoke("enableGamePanel",.5f);
                ScoreText.text = "0";
            }
            if(current == ORBIT_GAMEMANAGER.GameState.GameOver && previous == ORBIT_GAMEMANAGER.GameState.Game)
            {
                ShowGameOverDetails();
            } 
            if(current == ORBIT_GAMEMANAGER.GameState.Game && previous == ORBIT_GAMEMANAGER.GameState.GameOver)
            {
                StartCoroutine(DiableGameOverPanel());
                Invoke("enableGamePanel", .5f);
                ScoreText.text = "0";
            }
            if (current == ORBIT_GAMEMANAGER.GameState.LevelDifficult && previous == ORBIT_GAMEMANAGER.GameState.GameOver)
            {
                StartCoroutine(DiableGameOverPanel());
                Invoke("ShowLevelDifficulty",.5f);
            }
            if(current == ORBIT_GAMEMANAGER.GameState.Pause&& previous == ORBIT_GAMEMANAGER.GameState.Game)
            {
                ShowPause();
            } 
            if(current == ORBIT_GAMEMANAGER.GameState.Game && previous == ORBIT_GAMEMANAGER.GameState.Pause)
            {
                PausePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }

      

        private void HandleScoreChanged(int score)
        {
            ScoreText.text = score.ToString();
        }


        #endregion

        internal void ShowGameOverDetails()
        {
            GameOverScoreText.text = ScoreText.text;
            StartCoroutine(DisableGamePanel());
            Invoke("ShowGameOverPanel", 1);
        }
        public void RetryPressed()
        {
            ORBIT_GAMEMANAGER.Instance.UpdateState(ORBIT_GAMEMANAGER.GameState.Game);
        } 
        public void HomePressed()
        {
            ORBIT_GAMEMANAGER.Instance.UpdateState(ORBIT_GAMEMANAGER.GameState.LevelDifficult);
        }
        public void PausePressed()
        {
            ORBIT_GAMEMANAGER.Instance.UpdateState(ORBIT_GAMEMANAGER.GameState.Pause);
        }
        public void ResumePressed()
        {
            ORBIT_GAMEMANAGER.Instance.UpdateState(ORBIT_GAMEMANAGER.GameState.Game);
        }


        #region LevelDifficulty functionality


        public TMP_Text[] levelDifficultyTexts;

      

        public Transform levelDifficultyTextPosition;

        int currentdifficultyIndex;
        private bool Tutorial;

        public void NextPressed()
        {
            currentdifficultyIndex++;
            if (currentdifficultyIndex > levelDifficultyTexts.Length - 1)
            {
                currentdifficultyIndex = levelDifficultyTexts.Length - 1;
                return;
            }

            levelDifficultyTexts[currentdifficultyIndex - 1].transform.DOMove(new Vector3(levelDifficultyTextPosition.position.x - 1000, levelDifficultyTextPosition.position.y, 0), .5f);
            levelDifficultyTexts[currentdifficultyIndex].transform.position = new Vector3(levelDifficultyTextPosition.position.x + 1000, levelDifficultyTextPosition.position.y, 0);
            levelDifficultyTexts[currentdifficultyIndex].gameObject.SetActive(true);
            levelDifficultyTexts[currentdifficultyIndex].transform.DOMove(levelDifficultyTextPosition.position, .5f);

            switch (currentdifficultyIndex)
            {
                case 0:
                    showTexts();
                    ORBIT_GAMEMANAGER.Instance.UpdateDifficulty(Difficulty.tutorial);
                    break;
                case 1:
                    StartCoroutine(disableTexts());
                    ORBIT_GAMEMANAGER.Instance.UpdateDifficulty(Difficulty.easy);
                    break;
                case 2:
                    ORBIT_GAMEMANAGER.Instance.UpdateDifficulty(Difficulty.medium);
                    break;
                case 3:
                    ORBIT_GAMEMANAGER.Instance.UpdateDifficulty(Difficulty.Hard);
                    break;
                default:
                    break;
            }
            Tutorial_ = 1;
        }

        public void PreviousPressed()
        {
            currentdifficultyIndex--;
            if (currentdifficultyIndex < 0)
            {
                currentdifficultyIndex = 0;
                return;
            }
            levelDifficultyTexts[currentdifficultyIndex + 1].transform.DOMove(new Vector3(levelDifficultyTextPosition.position.x + 1000, levelDifficultyTextPosition.position.y, 0), .5f);
            levelDifficultyTexts[currentdifficultyIndex].transform.position = new Vector3(levelDifficultyTextPosition.position.x - 1000, levelDifficultyTextPosition.position.y, 0);
            levelDifficultyTexts[currentdifficultyIndex].gameObject.SetActive(true);
            levelDifficultyTexts[currentdifficultyIndex].transform.DOMove(levelDifficultyTextPosition.position, .5f);

            switch (currentdifficultyIndex)
            {
                case 0:
                    showTexts();
                    ORBIT_GAMEMANAGER.Instance.UpdateDifficulty(Difficulty.tutorial);
                    break;
                case 1:
                    disableTexts();
                    ORBIT_GAMEMANAGER.Instance.UpdateDifficulty(Difficulty.easy);
                    break;
                case 2:
                    ORBIT_GAMEMANAGER.Instance.UpdateDifficulty(Difficulty.medium);
                    break;
                case 3:
                    ORBIT_GAMEMANAGER.Instance.UpdateDifficulty(Difficulty.Hard);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Animations

        private void enableGamePanel()
        {
            levelDiificultpanel.SetActive(false);
            Color c = Color.white;
            TMP_Text[] texts = GamePanel.GetComponentsInChildren<TMP_Text>();
            Image images = GamePanel.GetComponentInChildren<Image>();
            foreach (var item in texts)
            {
                c = item.color;
                c.a = 0;
                item.color = c;
            }
           
                c = images.color;
                c.a = 0;
                images.color = c;
          

            GamePanel.SetActive(true);
            foreach (var item in texts)
            {
                item.DOFade(1, .5f);
            }

            images.DOFade(1, .5f);
           
        }

        private IEnumerator DisableGamePanel()
        {
            yield return new WaitForSeconds(.5f); 
           // levelDiificultpanel.SetActive(false);
            Color c = Color.white;
            TMP_Text[] texts = GamePanel.GetComponentsInChildren<TMP_Text>();
            Image[] images = GamePanel.GetComponentsInChildren<Image>();
            foreach (var item in texts)
            {
                item.DOFade(0, .5f);
            }
            foreach (var item in images)
            {
                item.DOFade(0, .5f);
            }
            yield return new WaitForSeconds(.5f);
            GamePanel.SetActive(false);
        }

        private IEnumerator disableleveldifficultPanel()
        {
            TMP_Text[] texts = levelDiificultpanel.GetComponentsInChildren<TMP_Text>();
            Image[] images = levelDiificultpanel.GetComponentsInChildren<Image>();
            foreach (var item in texts)
            {
                item.DOFade(0, .5f);
            }
            foreach (var item in images)
            {
                item.DOFade(0, .5f);
            }
            
            yield return new WaitForSeconds(.5f);

            levelDiificultpanel.SetActive(false);
        }

        private void ShowLevelDifficulty()
        {
            TMP_Text[] texts = levelDiificultpanel.GetComponentsInChildren<TMP_Text>();
            Image[] images = levelDiificultpanel.GetComponentsInChildren<Image>();
            Color c = Color.white;
            foreach (var item in texts)
            {
                c = item.color;
                c.a = 0;
                item.color = c;
            }
            foreach (var item in images)
            {
                c = item.color;
                c.a = 0;
                item.color = c;
            }
            levelDiificultpanel.SetActive(true);

            foreach (var item in texts)
            {
                item.DOFade(1, .5f);
            }
            foreach (var item in images)
            {
                item.DOFade(1, .5f);
            }

        }

        private IEnumerator DisableTitle()
        {
            yield return new WaitForSeconds(3);
            TMP_Text[] texts = Titlepanel.GetComponentsInChildren<TMP_Text>();
            Image Images = Titlepanel.GetComponent<Image>();
            Images.DOFade(0, .5f);
            foreach (var item in texts)
            {
                item.DOFade(0, .5f);
            }
            yield return new WaitForSeconds(.5f);
            Titlepanel.SetActive(false);
        }

        private void ShowGameOverPanel()
        {
            TMP_Text[] texts = GameOverPanel.GetComponentsInChildren<TMP_Text>();
            Image[] images = GameOverPanel.GetComponentsInChildren<Image>();
            Color c = Color.white;
            foreach (var item in texts)
            {
                c = item.color;
                c.a = 0;
                item.color = c;
            }
            foreach (var item in images)
            {
                c = item.color;
                c.a = 0;
                item.color = c;
            }
            GameOverPanel.SetActive(true);

            foreach (var item in texts)
            {
                item.DOFade(1, .5f);
            }
            foreach (var item in images)
            {
                item.DOFade(1, .5f);
            }
        }

        private IEnumerator DiableGameOverPanel()
        {
           // yield return new WaitForSeconds(.5f);
            TMP_Text[] texts = GameOverPanel.GetComponentsInChildren<TMP_Text>();
            Image[] images = GameOverPanel.GetComponentsInChildren<Image>();

            foreach (var item in texts)
            {
                item.DOFade(0, .5f);
            }
            foreach (var item in images)
            {
                item.DOFade(0, .5f);
            }
            yield return new WaitForSeconds(.5f);
            GameOverPanel.SetActive(false);
        }

        private void ShowPause()
        {
            TMP_Text[] texts = PausePanel.GetComponentsInChildren<TMP_Text>();
            Image[] images = PausePanel.GetComponentsInChildren<Image>();
            Color c = Color.white;
            foreach (var item in texts)
            {
                c = item.color;
                c.a = 0;
                item.color = c;
            }
            foreach (var item in images)
            {
                c = item.color;
                c.a = 0;
                item.color = c;
            }
            PausePanel.SetActive(true);

            foreach (var item in texts)
            {
                item.DOFade(1, .5f);
            }
            foreach (var item in images)
            {
                item.DOFade(1, .5f);
            }
            Invoke("StopTime", .5f);
        }

        void StopTime()
        {
            Time.timeScale = 0;
        }

        void showTexts()
        {
            Tutorial = true;

            TMP_Text[] texts = Tutorialpanel.GetComponentsInChildren<TMP_Text>();
            Image[] images = Tutorialpanel.GetComponentsInChildren<Image>();
            Color c = Color.black;
            foreach (var item in texts)
            {
                c = item.color;
                c.a = 0;
                item.color = c;
            }
            foreach (var item in images)
            {
                c = item.color;
                c.a = 0;
                item.color = c;
            }
            Tutorialpanel.SetActive(true);
           
            foreach (var item in TutorialTexts)
            {
                c.a = 0;
                item.color = c;
            }

            foreach (var item in texts)
            {
                item.DOFade(1, .5f);
            }
            foreach (var item in images)
            {
                item.DOFade(.25f, .5f);
            }
            foreach (var item in TutorialTexts)
            {
                item.DOFade(1, .5f);
            }
        }

        IEnumerator disableTexts()
        {
            Tutorial = false;
            TMP_Text[] texts = Tutorialpanel.GetComponentsInChildren<TMP_Text>();
            Image[] images = Tutorialpanel.GetComponentsInChildren<Image>();

            foreach (var item in texts)
            {
                item.DOFade(0, .5f);
            }
            foreach (var item in images)
            {
                item.DOFade(0, .5f);
            }
            yield return new WaitForSeconds(.5f);
            Tutorialpanel.SetActive(false);

            foreach (var item in TutorialTexts)
            {
                item.DOFade(0, .5f);
            }
        }
        #endregion


        // Update is called once per frame
        void Update()
        {
            foreach (var item in TutorialTexts)
            {
                item.transform.LookAt(Camera.main.transform.position);
            }
        }


        #region Playerprefs

        public static string _Tutorial = "Tutorial";
        public static string _EasyBest = "EasyBest";
        public static string _MediumBest = "mediumBest";
        public static string _HardBest = "hardBest";


        public int Tutorial_
        {
            get
            {
                if (PlayerPrefs.HasKey(_Tutorial))
                {
                    return PlayerPrefs.GetInt(_Tutorial);
                    Debug.LogError("Tutorial returned = 1");

                }
                else
                {
                    return 0;
                    Debug.LogError("Tutorial returned 0");

                }
            }

            set
            {
                PlayerPrefs.SetInt(_Tutorial, value);
                Debug.LogError("Tutorial saved");
            }
        }


        public int EasyBest
        {
            get
            {
                if (PlayerPrefs.HasKey(_EasyBest))
                {
                    return PlayerPrefs.GetInt(_EasyBest);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                PlayerPrefs.SetInt(_EasyBest, value);
                easyBest.text = value.ToString();
            }
        }
        public int MediumBest
        {
            get
            {
                if (PlayerPrefs.HasKey(_MediumBest))
                {
                    return PlayerPrefs.GetInt(_MediumBest);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                PlayerPrefs.SetInt(_MediumBest, value);
                mediumBest.text = value.ToString();

            }
        }
        public int HardBest
        {
            get
            {
                if (PlayerPrefs.HasKey(_HardBest))
                {
                    return PlayerPrefs.GetInt(_HardBest);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                PlayerPrefs.SetInt(_HardBest, value);
                hardBest.text = value.ToString();

            }
        }
        #endregion playerPrefs

    }
}
