
using UnityEngine;
using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

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

        // Authenticate and register a ProcessAuthentication callback
        // This call needs to be made before we can proceed to other calls in the Social API

#if UNITY_ANDROID
        PlayGamesPlatform.Activate();
#endif
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    // This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    void ProcessAuthentication(bool success)
    {
        if (success) DontDestroyOnLoad(gameObject);
    }

    public static void ShowLeaderboard()
    {
        if (Social.localUser.authenticated) Social.ShowLeaderboardUI();
    }
    public static void ShowAchievements()
    {
        if (Social.localUser.authenticated) Social.ShowAchievementsUI();
    }
    public static void ReportLeaderboard(long score, string leaderboard)
    {
        if (Social.localUser.authenticated) Social.ReportScore(score, leaderboard, success => { });
    }
    public static void ReportAchievement(string achievementID, double progress)
    {
        if (Social.localUser.authenticated) Social.ReportProgress(achievementID, progress, stuff => { });
    }
}