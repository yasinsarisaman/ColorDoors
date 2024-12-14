using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events.Game;
using TMPro;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject _star1, _star2, _star3;
    [SerializeField] private GameObject _starEmpty1, _starEmpty2, _starEmpty3;
    [SerializeField] private TextMeshProUGUI _remainingTimeTxt;

    private void OnEnable()
    {
        EventBus<LevelCompletedWithTime>.AddListener(OnLevelCompletedWithTime);
    }


    private void OnDisable()
    {
        EventBus<LevelCompletedWithTime>.RemoveListener(OnLevelCompletedWithTime);
    }

    private void OnLevelCompletedWithTime(object sender, LevelCompletedWithTime levelCompletedWithTime)
    {

        float remainingTime = levelCompletedWithTime.RemainingTime;

        if (remainingTime < 3.0f)
        {
            ActivateStars(1);
        }
        else if (remainingTime > 3.0f && remainingTime < 6.0f)
        {
            ActivateStars(2);
        }
        else
        {
            ActivateStars(3);
        }
        
        int _seconds = (int)remainingTime % 60;
        int _minutes = (int)remainingTime / 60;
        _remainingTimeTxt.text = "Remaining Time: " + _minutes + ":" + _seconds.ToString("00");
    }

    private void ActivateStars(int starCount)
    {
        switch (starCount)
        {
            case 1:
                _star1.SetActive(true);
                _starEmpty2.SetActive(true);
                _starEmpty3.SetActive(true);
                break;
            case 2:
                _star1.SetActive(true);
                _star2.SetActive(true);
                _starEmpty3.SetActive(true);
                break;
            case 3:
                _star1.SetActive(true);
                _star2.SetActive(true);
                _star3.SetActive(true);
                break;
            default:
                _star1.SetActive(true);
                _starEmpty2.SetActive(true);
                _starEmpty3.SetActive(true);
                break;
        }
    }
}
