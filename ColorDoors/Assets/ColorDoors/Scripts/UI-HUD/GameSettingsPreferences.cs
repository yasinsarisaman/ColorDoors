using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsPreferences : MonoBehaviour
{
    [SerializeField] private Button _joystickLeft;
    [SerializeField] private Button _joystickRight;
    [SerializeField] private Button _joystickBoth;
    
    [SerializeField] private Button _difficultyEasyBtn;
    [SerializeField] private Button _difficultyMediumBtn;
    [SerializeField] private Button _difficultyHardBtn;
    
    private void Start()
    {
        /* JOYSTICK POSITION */
        GameHelper.LoadJoystickPosition();
        
        SwitchJoystickBtnInteractable(GameHelper.GetJoystickPosition());
        
        
        /* GAME DIFFICULTY */
        GameHelper.LoadGameDifficulty();

        SwitchDifficultyBtnInteractable(GameHelper.GetGameDifficulty());
    }

    public void SaveGameDifficulty(int difficulty)
    {
        GameDifficulty gameDifficulty = GameDifficulty.GameDifficulty_MEDIUM;
        switch (difficulty)
        {
            case 1:
                gameDifficulty = GameDifficulty.GameDifficulty_EASY;
                break;
            case 2:
                gameDifficulty = GameDifficulty.GameDifficulty_MEDIUM;
                break;
            case 3:
                gameDifficulty = GameDifficulty.GameDifficulty_HARD;
                break;
        }
        GameHelper.SaveGameDifficulty(gameDifficulty);
        SwitchDifficultyBtnInteractable(gameDifficulty);
    }

    public void SaveJoystickPosition(int jbtnPos)
    {
        JoystickPosition jp = JoystickPosition.JoystickPosition_BOTH;
        switch (jbtnPos)
        {
            case 0:
                jp = JoystickPosition.JoystickPosition_LEFT;
                break;
            case 1:
                jp = JoystickPosition.JoystickPosition_BOTH;
                break;    
            case 2:
                jp = JoystickPosition.JoystickPosition_RIGHT;
                break;
                
        }
        GameHelper.SaveJoystickPosition(jp);
        SwitchJoystickBtnInteractable(jp);
        EventBus<JoystickPositionChangedEvent>.Emit(this, new JoystickPositionChangedEvent(jp));
    }

    private void SwitchDifficultyBtnInteractable(GameDifficulty gd)
    {
        switch (gd)
        {
            case GameDifficulty.GameDifficulty_EASY:
                _difficultyEasyBtn.interactable = false;
                _difficultyMediumBtn.interactable = true;
                _difficultyHardBtn.interactable = true;
                break;
            case GameDifficulty.GameDifficulty_MEDIUM:
                _difficultyMediumBtn.interactable = false;
                _difficultyEasyBtn.interactable = true;
                _difficultyHardBtn.interactable = true;
                break;
            case GameDifficulty.GameDifficulty_HARD:
                _difficultyHardBtn.interactable = false;
                _difficultyEasyBtn.interactable = true;
                _difficultyMediumBtn.interactable = true;
                break;
        }
    }

    private void SwitchJoystickBtnInteractable(JoystickPosition jp)
    {
        switch (jp)
        {
            case JoystickPosition.JoystickPosition_LEFT:
                _joystickLeft.interactable = false;
                _joystickRight.interactable = true;
                _joystickBoth.interactable = true;
                break;
            case JoystickPosition.JoystickPosition_RIGHT:
                _joystickRight.interactable = false;
                _joystickLeft.interactable = true;
                _joystickBoth.interactable = true;
                break;
            case JoystickPosition.JoystickPosition_BOTH:
                _joystickBoth.interactable = false;
                _joystickLeft.interactable = true;
                _joystickRight.interactable = true;
                break;
        }
    }
}
