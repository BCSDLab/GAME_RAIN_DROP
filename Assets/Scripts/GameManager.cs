using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            _instance = FindObjectOfType<GameManager>();
            if (_instance == null)
                Debug.Log("GameManager is not Find.");
            return _instance;
        }
    }

    public float fGameSpeed = 1;

    //게임 진행위치
    public bool IsStart = true;
    public bool IsTutorial = false;
    public bool IsInGame = false;
    public bool IsOption = false;
    public bool IsResult = false;
    public bool IsPause = false;
    void Start()
    {
        if (_instance == null)
            _instance = this;
        if (_instance != this)
            Destroy(gameObject);

        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.SetResolution(1920, 1080, true);

        AudioManager.Instance.PlayMusic(AudioManager.Instance.Background);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.RainDrop);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.Fire);
        AudioManager.Instance.TotalAudioMuteOf();
        ScoreManager.CScore = 0;
        ScoreManager.BScore = 0;
    }

    void Update()
    {
        Run();

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public void TutorialStart()
    {
        IsStart = false;
        IsTutorial = true;
        IsInGame = false;
        IsOption = false;
        IsResult = false;
        IsPause = false;
    }

    public void InGameStart()
    {
        IsStart = false;
        IsTutorial = false;
        IsInGame = true;
        IsOption = false;
        IsResult = false;
        IsPause = false;
    }

    private void GameInit()
    {
        
    }

    private void Run()
    {
        if (IsTutorial) return;

        if (IsInGame)
        {
            ArrowManager.Instance.ArrowUpdate();
            LevelManager.Instance.LevelUpdate();
            ScoreManager.Instance.ScoreUpdate();
        }
        if(IsResult)
        {
            ScoreManager.Instance.ScoreUpdate();
        }

        AudioManager.Instance.AudioRun();
    }

}
