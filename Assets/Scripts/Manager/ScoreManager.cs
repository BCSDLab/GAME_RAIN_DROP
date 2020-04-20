using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager _instance = null;
    public Text scoreLabel;
    public Text levelLabel;
    public Text currentScore, BestScore;
    public static int CScore;
    public static int BScore;

    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
                if (_instance == null)
                    Debug.Log("ScoreManager is not Find");
            }
            return _instance;
        }
    }

    public void CheckScore()
    {
        Social.LoadScores(GPGSIds.leaderboard_leaderboard, Scores =>
        {
            if (Scores.Length > 0)
            {
                foreach (IScore score in Scores)
                {
                    if (score.value <= CScore)
                    {
                        BScore = CScore;
                    }
                }
            }
        });
    }

    public void ScoreUpdate()
    {
        scoreLabel.text = CScore.ToString();
        currentScore.color = new Color(255, 255, 255, 255);
        currentScore.text = CScore.ToString();
        BestScore.text = PlayerPrefs.GetFloat("BEST", 0).ToString();
        BestScore.color = new Color(255, 255, 255, 255);
    }

    public void ResetScrore()
    {
        CScore = 0;
    }
}