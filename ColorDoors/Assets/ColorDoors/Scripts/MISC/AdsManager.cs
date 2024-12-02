using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    void Start()
    {
        AdMobInterModule.ShowInter(OnClosed);
    }

    private void OnClosed()
    {
    }
}
