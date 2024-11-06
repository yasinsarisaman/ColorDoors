using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBtn : MonoBehaviour
{
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private Sprite _soundOnSprite;

    private static Image _existingImage;
    private bool _isSoundOn = true;
    void Start()
    {
        _existingImage = GetComponent<Image>();
    }
    
    public void ChangeImage()
    {
        if (_existingImage != null && (_soundOffSprite != null || _soundOnSprite != null))
        {
            if (_isSoundOn)
            {
                _existingImage.sprite = _soundOffSprite;
                _isSoundOn = false;
            }
            else
            {
                _existingImage.sprite = _soundOnSprite;
                _isSoundOn = true;
            }
            AudioListener.volume = _isSoundOn ? 1.0f : 0.0f;
        }
        else
        {
            Debug.LogError("Image component or new sprite is null. Make sure to assign them in the Inspector!");
        }
    }

}