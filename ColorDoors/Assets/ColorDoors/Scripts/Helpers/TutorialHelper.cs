using System;
using System.Collections;
using System.Collections.Generic;
using ColorDoors.Scripts.Events;
using UnityEngine;

public class TutorialHelper : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI;

    private void OnEnable()
    {
        EventBus<FirstInputReceivedEvent>.AddListener(OnFirstInput);
    }

    private void OnDisable()
    {
        EventBus<FirstInputReceivedEvent>.AddListener(OnFirstInput);
    }

    private void OnFirstInput(object sender, FirstInputReceivedEvent firstInputReceivedEvent)
    {
        Destroy(tutorialUI);
    }
}
