using UnityEngine;
using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
using UnityEngine.UI;

public class SNSController : MonoBehaviour
{
    public static SNSController Instance = null;

    void Start()
    {

        //Check if instance already exists
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

#if UNITY_ANDROID
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
#endif

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
#endif
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    // This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    void ProcessAuthentication(bool success)
    {
        if (success)
        {
        }
        else
        {
        }
    }

    public void LogoutMainSNS()
    {
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.SignOut();
#endif
    }

    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

    public void ReportLeaderboard(long score, string leaderboard)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboard, success => { });
        }
    }

    public void ReportAchievement(string achievementID, double progress)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(achievementID, progress, stuff => { });
        }
    }
}