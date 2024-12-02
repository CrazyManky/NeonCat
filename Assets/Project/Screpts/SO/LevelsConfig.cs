using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Levels", menuName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<GameLevel> _gameLevelsPrefab;
        [SerializeField] private int _activeLevel;
        
        public int LevelIndex => _activeLevel;
        
        public GameLevel GetLevel()
        {
            if (LevelIndex >= _gameLevelsPrefab.Count)
            {
                return _gameLevelsPrefab[0];
            }
        
            return _gameLevelsPrefab[_activeLevel];
        }
        
        public void LevelComplited()
        {
            _activeLevel++;
        }
        
        public void Reset()
        {
            _activeLevel = 0;
        }
    }
}