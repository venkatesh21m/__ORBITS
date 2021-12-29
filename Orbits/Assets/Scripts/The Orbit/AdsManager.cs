 using System.Collections;
using UnityEngine;
//using UnityEngine.Advertisements;
using Yodo1.MAS;

public class AdsManager : MonoBehaviour/*, IUnityAdsListener*/
{
    public static AdsManager Instance;

    [SerializeField] public string gameId = "3562062";
    [SerializeField] public bool testMode = true;
    string myPlacementId = "rewardedVideo";
    string BannerId = "bannerAd";


    [HideInInspector] public bool rewardedReady;
    void Start()
    {
        Instance = this;
        
        Yodo1U3dMas.InitializeSdk();

        Yodo1U3dMas.SetBannerAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) => {
            Debug.Log("[Yodo1 Mas] BannerdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Banner ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Banner ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Banner ad error, " + error.ToString());
                    break;
            }
        });

        Yodo1U3dMas.SetInterstitialAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) => {
            Debug.Log("[Yodo1 Mas] InterstitialAdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Interstital ad error, " + error.ToString());
                    break;
            }
        });

        
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Yodo1U3dMas.IsBannerAdLoaded())
        {
            yield return new WaitForSeconds(0.25f);
        }
        int align = Yodo1U3dBannerAlign.BannerTop /*| Yodo1U3dBannerAlign.BannerHorizontalCenter*/;
        Yodo1U3dMas.ShowBannerAd(align);

    
    }

    public void ShowInterstital()
    {
        StartCoroutine(ShowInterstitialAd());
    }

    IEnumerator ShowInterstitialAd()
    {
        while (!Yodo1U3dMas.IsInterstitialAdLoaded())
        {
            yield return new WaitForSeconds(0.25f);
        }

        Yodo1U3dMas.ShowInterstitialAd();
    }
 

  

}