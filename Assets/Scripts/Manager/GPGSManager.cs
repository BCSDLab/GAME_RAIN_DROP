using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSManager : MonoBehaviour

{

    //싱글톤 패턴
    private static GPGSManager _instance;

    public static GPGSManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<GPGSManager>() as GPGSManager;
            return _instance;
        }
    }

    private static bool _authenticating = false;
    public bool Authenticated { get { return Social.Active.localUser.authenticated; } }

    //achievement increments we are accumulating locally, waiting to send to the games API
    private Dictionary<string, int> _pendingIncrements = new Dictionary<string, int>();

    //GooglePlayGames 초기화
    public void Initialize(bool gpgs_log)
    {
        //PlayGamesPlatform 로그 활성화/비활성화
        PlayGamesPlatform.DebugLogEnabled = gpgs_log;
        //Social.Active 초기화
        PlayGamesPlatform.Activate();
    }

    void Awake()
    {
        // 안드로이드 빌더 초기화
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);

        // 구글 플레이 로그를 확인할려면 활성화
        Initialize(true);
        if (_authenticating == false)
            Login();

    }
    // 로그인
    public void Login()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated() == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("Sign in successful!");
                    _authenticating = true;
                }
                else
                {
                    Debug.LogWarning("Failed to sign in with Google Play");
                }
            });
        }
        else
        {
            PlayGamesPlatform.Instance.SignOut();
            _authenticating = false;
        }
    }

    public void Logout()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated() == true)
            PlayGamesPlatform.Instance.SignOut();
    }

    public void ShowLeaderboardUI()
    {
        if (_authenticating == true)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("Sign in successful!");
                    Social.ShowLeaderboardUI();
                }
                else
                {
                    Debug.LogWarning("Failed to sign in with Google Play");
                }
            });
        }
    }
    public void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }
    public void OnAchievement_1()
    {
        Social.ReportProgress(GPGSIds.achievement, 100.0f, (bool success) =>
        {
            if (success)
            {
                Debug.Log("초보 탈출 획득 성공");
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_3, 1, null);
            }
            else
            {
                Debug.Log("초보 탈출 획득 실패");
            }
        });
    }
}

