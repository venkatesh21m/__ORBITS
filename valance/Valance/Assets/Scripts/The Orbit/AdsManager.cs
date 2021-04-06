 using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
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

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(BannerId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(BannerId);
    }

    public void ShowInterstitial()
    {
        Advertisement.Show();
    }

    public void ShowRewardedAd()
    {
        if (rewardedReady)
        {
            Advertisement.Show(myPlacementId);
        }
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
           // UiManager.Instance.ContinueGame();
        }
        else if (showResult == ShowResult.Skipped)
        {
            //UiManager.Instance.FinishPressed();
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
        rewardedReady = false;
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            rewardedReady = true;
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}