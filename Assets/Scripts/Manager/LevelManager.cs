using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float levelSection = 10f;
    public int gameLevel = 1;
    public float fRunningTime = 0f;
    public Slider LvLable;
    public Text LvText;

    private static LevelManager _instance = null;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
                if (_instance == null)
                    Debug.Log("LevelManager is nowhere");
            }
            return _instance;
        }
    }
        
    public void Initialize()
    {
        LvLable.value = 0;
        gameLevel = 1;
        fRunningTime = 0;
        levelSection = 10;
    }

    public int GetGameLevel()
    {
        return gameLevel;
    }

    public void LevelUpdate()
    {
        fRunningTime += Time.deltaTime;

        LvLable.value = fRunningTime / levelSection;

        if(fRunningTime >= levelSection)
        {
            fRunningTime = 0;
            levelSection += 2;
            gameLevel++;
            ArrowManager.Instance.MakeBoom();
        }
    }

    void Start()
    {
        //arrowManager = FindObjectOfType<ArrowManager>();
        Initialize();
    }
    
    void Update()
    {
        LvText.text = gameLevel.ToString() + " Lv";
    }
}
