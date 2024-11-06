using System;
using ColorDoors.Scripts.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ColorDoors.Scripts.Managers
{
    public class GameLevelManager : MonoBehaviour
    {
        
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
            SceneManager.LoadScene(changeLevelEvent.LevelToLoad);
        }
    }
}