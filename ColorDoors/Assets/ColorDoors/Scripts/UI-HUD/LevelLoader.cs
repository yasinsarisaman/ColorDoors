using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        LoadLevels();
    }


    private static void LoadLevels()
    {
        GameHelper.LoadLevelStatuses(SceneManager.sceneCountInBuildSettings);
    }

    public void PlayLastActiveLevel()
    {
        SceneManager.LoadScene(GameHelper.GetLastActiveLevel());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
