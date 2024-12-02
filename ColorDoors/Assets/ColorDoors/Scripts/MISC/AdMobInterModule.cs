using System;
using GoogleMobileAds.Api;
using UnityEngine;
namespace Game
{
    public class AdMobInterModuleMono : MonoBehaviour
    {
        public InterstitialAd interstitialAd;

        void OnApplicationFocus(bool hasFocus)
        {
            Debug.Log("[AdInterModule] OnApplicationFocus");
            if (hasFocus)
            {
                AdMobInterModule.LoadInterstitialAd();
            }
        }
    }

    public static class AdMobInterModule
    {
        static AdMobInterModuleMono instance = null;
        static string adUnitID = null;

        public static Action<AdValue> onAdPaid;
        public static Action onAdImpressionRecorded;
        public static Action onAdClicked;
        public static Action onAdFullScreenContentOpened;
        public static Action onAdFullScreenContentClosed;
        public static Action onAdFullScreenContentFailed;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Reset()
        {
            instance = null;
            adUnitID = null;

            onAdPaid = null;
            onAdImpressionRecorded = null;
            onAdClicked = null;
            onAdFullScreenContentOpened = null;
            onAdFullScreenContentClosed = null;
            onAdFullScreenContentFailed = null;
        }

        public static void Init()
        {
            Debug.Assert(instance == null, "AdMobInterModule already initialized.");
            Debug.Log("[AdMobInterModule] Initialization.");

            GameObject adMobInterModuleGO = new GameObject("AdMobInterModule");
            instance = adMobInterModuleGO.AddComponent<AdMobInterModuleMono>();
            GameObject.DontDestroyOnLoad(adMobInterModuleGO);
            FillADUnitID();

            LoadInterstitialAd();
        }

        public static void LoadInterstitialAd()
        {
            // Clean up the old ad before loading a new one.
            if (instance.interstitialAd != null)
            {
                instance.interstitialAd.Destroy();
                instance.interstitialAd = null;
            }

            Debug.Log("[AdMobInterModule] Loading the interstitial ad.");


            var adRequest = new AdRequest();
            InterstitialAd.Load(adUnitID, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("[AdMobInterModule] interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("[AdMobInterModule] Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    instance.interstitialAd = ad;
                    RegisterEventHandlers(instance.interstitialAd);
                });
        }

        public static void ShowInter(Action _onInterClosed)
        {
            if (instance.interstitialAd != null && instance.interstitialAd.CanShowAd())
            {
                Debug.Log("[AdMobInterModule] Showing interstitial ad.");

                instance.interstitialAd.OnAdFullScreenContentClosed += _onInterClosed;
                instance.interstitialAd.Show();
            }
            else
            {
                Debug.LogError("[AdMobInterModule] Interstitial ad is not ready yet.");
            }
        }

        public static bool HasInter()
        {
            return instance != null &&
                   instance.interstitialAd != null &&
                   instance.interstitialAd.CanShowAd();
        }

        static void RegisterEventHandlers(InterstitialAd interstitialAd)
        {
            // Raised when the ad is estimated to have earned money.
            interstitialAd.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("[AdMobInterModule] Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));

                onAdPaid?.Invoke(adValue);
            };
            // Raised when an impression is recorded for an ad.
            interstitialAd.OnAdImpressionRecorded += () =>
            {
                Debug.Log("[AdMobInterModule] Interstitial ad recorded an impression.");

                onAdImpressionRecorded?.Invoke();
            };
            // Raised when a click is recorded for an ad.
            interstitialAd.OnAdClicked += () =>
            {
                Debug.Log("[AdMobInterModule] Interstitial ad was clicked.");

                onAdClicked?.Invoke();
            };
            // Raised when an ad opened full screen content.
            interstitialAd.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("[AdMobInterModule] Interstitial ad full screen content opened.");

                onAdFullScreenContentOpened?.Invoke();
            };
            // Raised when the ad closed full screen content.
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("[AdMobInterModule] Interstitial ad full screen content closed.");

                onAdFullScreenContentClosed?.Invoke();
                LoadInterstitialAd();
            };
            // Raised when the ad failed to open full screen content.
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("[AdMobInterModule] Interstitial ad failed to open full screen content " +
                               "with error : " + error);

                onAdFullScreenContentFailed?.Invoke();
                LoadInterstitialAd();
            };
        }

        static void FillADUnitID()
        {
            //Release
            if (Debug.isDebugBuild == false)
            {
                    #if UNITY_ANDROID
                    adUnitID = "ca-app-pub-1126931214625629/9104631765";
                    return;
                    //#elif UNITY_IPHONE
                    //unitID = "ca-app-pub-3940256099942544/4411468910";
                    #else
                    //unitID = "unexpected_platform";
                    #endif
            }

            //Test
                #if UNITY_ANDROID
                adUnitID = "ca-app-pub-3940256099942544/1033173712";
                return;
                #endif
        }


    }
}