using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace THEORBIT
{
    public class ORBIT_UTILS 
    {
        public class OnGameStateChanged:UnityEvent<ORBIT_GAMEMANAGER.GameState, ORBIT_GAMEMANAGER.GameState> { };
        public class OnGameDifficultyChanged:UnityEvent<Difficulty,Difficulty> { };
        public class OnScoreChanged : UnityEvent<int> { };
    }
}
