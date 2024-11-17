using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class LevelBtn : MonoBehaviour
{
    [SerializeField] private GameObject lockImg;
    [SerializeField] private Button levelBtn;
    [SerializeField] private int levelIndex;

    private void Start()
    {
        if (GameHelper.CanAccessLevel(levelIndex) || levelIndex == 1)
        {
            lockImg.SetActive(false);
            levelBtn.interactable = true;
        }
    }

    public void OnLevelButtonClicked()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
