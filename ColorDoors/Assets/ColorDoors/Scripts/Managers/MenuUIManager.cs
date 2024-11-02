using System;
using TMPro;
using UnityEngine;

namespace ColorDoors.Scripts.Managers
{
    public class MenuUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject winMenu, loseMenu ,gameplayMenu;

        private void OnEnable()
        {
            EventBus<LevelCompletedEvent>.AddListener(OnLevelCompletedEvent);

        }
        
        private void OnDisable()
        {
            EventBus<LevelCompletedEvent>.RemoveListener(OnLevelCompletedEvent);
        }
        private void OnLevelCompletedEvent(object sender, LevelCompletedEvent e)
        {
            switch (e.CompletionState)
            {
                case CompletionStates.CompletionState_WIN:
                    winMenu.SetActive(true);
                    gameplayMenu.SetActive(false);
                    break;
                case CompletionStates.CompletionState_LOSE_TIMEOUT:
                    loseMenu.SetActive(true);
                    gameplayMenu.SetActive(false);
                    break;
                default:
                    gameplayMenu.SetActive(false);
                    break;
            }
        }
    }
}