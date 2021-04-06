using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace THEORBIT
{
    public class ORBIT_GAMEMANAGER : MonoBehaviour
    {
        public static ORBIT_GAMEMANAGER Instance;


        public int Score;
        public enum GameState
        {
            //tutorial,
            LevelDifficult,
            Game,
            Pause,
            GameOver
        }

        GameState currentState;
        GameState previousState;
        Difficulty currentDifficulty;
        Difficulty previousDifficulty;

        public ORBIT_UTILS.OnGameStateChanged OnGameStateChanged;
        public ORBIT_UTILS.OnGameDifficultyChanged onGameDifficultyChanged;
        public ORBIT_UTILS.OnScoreChanged onScoreChanged;


        int GameOverCount;

        private void Awake()
        {
            Instance = this;
            OnGameStateChanged = new ORBIT_UTILS.OnGameStateChanged();
            onGameDifficultyChanged = new ORBIT_UTILS.OnGameDifficultyChanged();
            onScoreChanged = new ORBIT_UTILS.OnScoreChanged();
        }
        // Start is called before the first frame update
        void Start()
        {
            currentState = GameState.LevelDifficult;
            currentDifficulty = Difficulty.tutorial;
            Score = 0;
            GameOverCount = 0;
        }

        public void UpdateState(GameState state)
        {
            previousState = currentState;
            currentState = state;

            switch (currentState)
            {
                case GameState.LevelDifficult:
                    break;
                case GameState.Game:
                    if(previousState != GameState.Pause)
                    {
                        Score = 0;
                    }
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    GameOverCount++;
                    if (GameOverCount % 5 == 0)
                    {
                        AdsManager.Instance.ShowRewardedAd();
                    }
                    switch (currentDifficulty)
                    {
                        case Difficulty.easy:
                            if(ORBIT_UIMANAGER.Instance.EasyBest < Score)
                            {
                                ORBIT_UIMANAGER.Instance.EasyBest = Score;
                                GetComponent<Googleplay>().OnAddScoreToeasyLeaderBorad(Score);
                            }
                            break;
                        case Difficulty.medium:
                            if (ORBIT_UIMANAGER.Instance.MediumBest < Score)
                            {
                                ORBIT_UIMANAGER.Instance.MediumBest = Score;
                                GetComponent<Googleplay>().OnAddScoreTomediumLeaderBorad(Score);
                            }
                            break;
                        case Difficulty.Hard:
                            if (ORBIT_UIMANAGER.Instance.HardBest < Score)
                            {
                                ORBIT_UIMANAGER.Instance.HardBest = Score;
                                GetComponent<Googleplay>().OnAddScoreToHardLeaderBorad(Score);

                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            OnGameStateChanged.Invoke(currentState, previousState);
        }


        public void UpdateDifficulty(Difficulty difficulty)
        {
            previousDifficulty = currentDifficulty;
            currentDifficulty = difficulty;
            onGameDifficultyChanged.Invoke(currentDifficulty, previousDifficulty);
        }


        public void RestartGame()
        {
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }



        internal void AdScore()
        {
            Score++;
            onScoreChanged.Invoke(Score);
            CollectableManager.Instance.DisableCollectable();
        }


     

    }


}

[System.Serializable]
public enum Difficulty
{
    tutorial,
    easy,
    medium,
    Hard
}