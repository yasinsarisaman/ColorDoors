using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpOKButton : MonoBehaviour
{
    [SerializeField] private float _interactionTime;
    [SerializeField] private Button _okButton;
    [SerializeField] private bool _interactionOpened;

    private void Start()
    {
        _okButton.interactable = _interactionOpened;
    }

    void Update()
    {
        if (_interactionOpened) return;
        if (_interactionTime > 0.0f)
        {
            _interactionTime -= Time.deltaTime;
        }
        else
        {
            _okButton.interactable = true;
            _interactionOpened = true;
        }
    }
}
