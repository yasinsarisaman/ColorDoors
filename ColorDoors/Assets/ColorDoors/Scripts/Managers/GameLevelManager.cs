using System;
using ColorDoors.Scripts.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ColorDoors.Scripts.Managers
{
    public class GameLevelManager : MonoBehaviour
    {
        private void Start()
        {
            GameHelper.AddLevelStatus(SceneManager.GetActiveScene().buildIndex,false);
        }

        private void OnEnable()
        {
            EventBus<ChangeLevelEvent>.AddListener(OnLevelChangedEvent);

        }

        private void OnDisable()
        {
            EventBus<ChangeLevelEvent>.RemoveListener(OnLevelChangedEvent);
        }
        
        private void OnLevelChangedEvent(object sender, ChangeLevelEvent changeLevelEvent)
        {
            Scene activeScene = SceneManager.GetActiveScene();
            switch (changeLevelEvent.LevelChange)
            {
                case LevelChange.levelChange_GoNextLevel:
                    GameHelper.UpdateLevelStatus(SceneManager.GetActiveScene().buildIndex + 1, false);
                    GameHelper.SaveLevelStatuses();
                    SceneManager.LoadScene(activeScene.buildIndex + 1);
                break;
                case LevelChange.levelChange_RestartLevel:
                    SceneManager.LoadScene(activeScene.buildIndex);
                break;
                case LevelChange.levelChange_GoBackToMainMenu:
                    SceneManager.LoadScene(1);
                break;
            }
        }
    }
}