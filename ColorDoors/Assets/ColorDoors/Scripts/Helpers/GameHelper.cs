using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using Unity.VisualScripting;
using UnityEngine;

public static class GameHelper
{
    private static Dictionary<int, bool> _levelStatuses = new Dictionary<int, bool>();
    private static int _lastActiveLevel = 1;

    public static void AddLevelStatus(int levelIndex,bool isLocked)
    {
            _levelStatuses.TryAdd(levelIndex,isLocked);
    }

    public static void UpdateLevelStatus(int levelIndex, bool isLocked)
    {
        if (levelIndex >= 1)
        {
            _levelStatuses[levelIndex] = isLocked;
        }
    }

    public static bool CanAccessLevel(int levelIndex)
    {
        return levelIndex < _levelStatuses.Count && !_levelStatuses[levelIndex];
    }
    
    public static void SaveLevelStatuses()
    {
        for (int i = 1; i < _levelStatuses.Count; i++)
        {
            PlayerPrefs.SetInt("LevelStatus_" + i, _levelStatuses[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }
    
    public static void LoadLevelStatuses(int totalLevels)
    {
        _levelStatuses.Clear();
        for (int i = 1; i < totalLevels; i++)
        {
            int status = PlayerPrefs.GetInt("LevelStatus_" + i, 1); 
            _levelStatuses.Add(i,status == 1);
        }
    }

    public static Dictionary<int,bool> GetLevelStatuses()
    {
        return _levelStatuses;
    }

    public static int GetLastActiveLevel()
    {
        for (int i = 1; i < _levelStatuses.Count; i++)
        {
            if (_levelStatuses[i] == false)
            {
                _lastActiveLevel = i;
            }
        }

        return _lastActiveLevel;
    }
}
