using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUISettingsBtn : MonoBehaviour
{
    private Button _button;
    private float _timer;
    void Start()
    {
        _button = this.GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    void Update()
    {
        if (_button.interactable) return;
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _timer = 0.0f;
            _button.interactable = true;
        }
    }

    void OnButtonClick()
    {
        _button.interactable = false;
        _timer = 1.0f;
    }
}
