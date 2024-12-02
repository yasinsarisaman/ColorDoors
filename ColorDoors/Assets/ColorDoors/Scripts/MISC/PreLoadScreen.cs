using System.Collections;
using System.Collections.Generic;
using Game;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoadScreen : MonoBehaviour
{
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
        AdMobInterModule.Init();
        SceneManager.LoadScene(1);
    }
}
