using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNumberMonitorizer : MonoBehaviour
{
    [SerializeField] private TextMeshPro _levelNumber;
    void Start()
    {
        _levelNumber.text = "level:" + SceneManager.GetActiveScene().buildIndex.ToString();
    }
}
