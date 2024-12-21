using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using Unity.VisualScripting;
using UnityEngine;

public enum JoystickPosition
{
    JoystickPosition_LEFT,
    JoystickPosition_RIGHT,
    JoystickPosition_BOTH
}

public enum GameDifficulty
{
    GameDifficulty_EASY,
    GameDifficulty_MEDIUM,
    GameDifficulty_HARD
}

public static class GameHelper
{
    private static Dictionary<int, bool> _levelStatuses = new Dictionary<int, bool>();
    private static int _lastActiveLevel = 2;
    private static JoystickPosition _joystickPosition;
    private static GameDifficulty _gameDifficulty;

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
        for (int i = 2; i < _levelStatuses.Count; i++)
        {
            PlayerPrefs.SetInt("LevelStatus_" + i, _levelStatuses[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }
    
    public static void LoadLevelStatuses(int totalLevels)
    {
        _levelStatuses.Clear();
        for (int i = 2; i <= totalLevels + 1; i++)
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
        for (int i = 2; i < _levelStatuses.Count; i++)
        {
            if (_levelStatuses[i] == false)
            {
                _lastActiveLevel = i;
            }
        }

        return _lastActiveLevel;
    }

    public static void SaveJoystickPosition(JoystickPosition joystickPosition)
    {
        string jPos = "BOTH";
        switch (joystickPosition)
        {
            case JoystickPosition.JoystickPosition_BOTH:
                jPos = "BOTH";
                break;
            case JoystickPosition.JoystickPosition_LEFT:
                jPos = "LEFT";
                break;
            case JoystickPosition.JoystickPosition_RIGHT:
                jPos = "RIGHT";
                break;
        }
        PlayerPrefs.SetString("JoystickPosition", jPos);
        PlayerPrefs.Save();
    }

    public static void LoadJoystickPosition()
    {
        string jPos = PlayerPrefs.GetString("JoystickPosition", "BOTH");
        if (jPos == "BOTH")
        {
            _joystickPosition = JoystickPosition.JoystickPosition_BOTH;
        }

        if (jPos == "LEFT")
        {
            _joystickPosition = JoystickPosition.JoystickPosition_LEFT;
        }

        if (jPos == "RIGHT")
        {
            _joystickPosition = JoystickPosition.JoystickPosition_RIGHT;
        }
    }

    public static JoystickPosition GetJoystickPosition()
    {
        return _joystickPosition;
    }

    public static void SaveGameDifficulty(GameDifficulty gameDifficulty)
    {
        short difficulty = 2;
        switch (gameDifficulty)
        {
            case GameDifficulty.GameDifficulty_EASY:
                difficulty = 1;
                break;
            case GameDifficulty.GameDifficulty_MEDIUM:
                difficulty = 2;
                break;
            case GameDifficulty.GameDifficulty_HARD:
                difficulty = 3;
                break;
        }
        PlayerPrefs.SetInt("GameDifficulty", difficulty);
        PlayerPrefs.Save();
    }
    
    public static void LoadGameDifficulty()
    {
        int difficulty = PlayerPrefs.GetInt("GameDifficulty", 2);
        switch (difficulty)
        {
            case 1:
                _gameDifficulty = GameDifficulty.GameDifficulty_EASY;
                break;
            case 2:
                _gameDifficulty = GameDifficulty.GameDifficulty_MEDIUM;
                break;
            case 3:
                _gameDifficulty = GameDifficulty.GameDifficulty_HARD;
                break;
        }
    }

    public static GameDifficulty GetGameDifficulty()
    {
        return _gameDifficulty;
    }
}
