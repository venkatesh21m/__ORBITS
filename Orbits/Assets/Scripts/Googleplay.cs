using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class Googleplay : MonoBehaviour
{

    [SerializeField] TMPro.TMP_Text text;

    #region DEFAULT_UNITY_CALLBACKS
    void Start()
    {

        // Create client configuration
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);

        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
       
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
                text.text = "LogIn success";
            }
            else
            {
                Debug.Log("Login failed");
                text.text = "LogIn failed";

            }
        });
        LogIn();
        // PASTE THESE LINES AT THE END OF Start()
        // Try silent sign-in (second parameter is isSilent)
        //PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
        //text.text = "playgames activated";
        // LogIn();
    }
    #endregion
    #region BUTTON_CALLBACKS
    /// <summary>
    /// Login In Into Your Google+ Account
    /// </summary>
    public void LogIn()
    {
        text.text = "LogIn ----- pressed";

        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
        //Social.localUser.Authenticate((bool success) =>
        //{
        //    if (success)
        //    {
        //        Debug.Log("Login Sucess");
        //    }
        //    else
        //    {
        //        Debug.Log("Login failed");
        //    }
        //});
    }
    public void SignInCallback(bool success)
    {

        text.text = "call back called";

        if (success)
        {
            Debug.Log("(Lollygagger) Signed in!");

            // Change sign-in button text
            text.text = "login -------------- success";
            // Show the user's name
        }
        else
        {
            text.text = "Login------- failed";
            // Show failure message
        }
    }
    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShoweasyLeaderBoard()
    {
        Social.ShowLeaderboardUI(); // Show all leaderboard
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_easy_best); // Show current (Active) leaderboard
    }
    public void OnShowmediumLeaderBoard()
    {
        Social.ShowLeaderboardUI(); // Show all leaderboard
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_medium_best); // Show current (Active) leaderboard
    }
    public void OnShowHardLeaderBoard()
    {
        Social.ShowLeaderboardUI(); // Show all leaderboard
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_hard_best); // Show current (Active) leaderboard
    }
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToeasyLeaderBorad(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_easy_best, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }
    public void OnAddScoreTomediumLeaderBorad(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_medium_best, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }
    public void OnAddScoreToHardLeaderBorad(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_hard_best, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }
    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut()
    {
        //((PlayGamesPlatform)Social.Active).SignOut();
    }
    #endregion
}