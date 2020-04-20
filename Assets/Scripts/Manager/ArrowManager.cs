using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)] public float speed = 50f;
    [SerializeField] [Range(0f, 100f)] private float radius = 3f;
    private static ArrowManager _instance = null;
    public GameObject Boom;
    public GameObject[] Arrows;
    private Transform ArrowHolder = null;
    private int count = 0;

    public static ArrowManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(ArrowManager)) as ArrowManager;

                if (_instance == null)
                    Debug.Log("ArrowManager is nowhere");
            }
            return _instance;
        }
    }

    public GameObject RandomArrow()
    {
        return Arrows[Random.Range(0, LevelManager.Instance.GetGameLevel()) % (Arrows.Length)];
    }

    public void MakeArrowToSelect(int num, float Rad)
    {
        float random = Random.Range(0, 100);
        float x = Rad * Mathf.Cos(random);
        float y = Rad * Mathf.Sin(random);
        try
        {
            GameObject instance = Instantiate(Arrows[num], new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(ArrowHolder);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public GameObject MakeArrowToVector(int number, Vector3 pos)
    {
        try
        {
            GameObject instance = Instantiate(Arrows[number], pos, Quaternion.identity);
            instance.transform.SetParent(ArrowHolder);
            return instance;
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex.ToString());
            return null;
        }
       
    }

    public void MakeArrow()
    {
        float random = Random.Range(0, 100);
        float x = radius * Mathf.Cos(random);
        float y = radius * Mathf.Sin(random);
        try
        {
            GameObject instance = Instantiate(RandomArrow(), new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(ArrowHolder);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public void MakeVectorArrow(float angle)
    {
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);
        ScoreManager.CScore++;
        try
        {
            GameObject instance = Instantiate(RandomArrow(), new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(ArrowHolder);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public GameObject MakeBoom()
    {
        float random = Random.Range(0, 100);
        float x = radius * Mathf.Cos(random);
        float y = radius * Mathf.Sin(random);

        GameObject Object = Instantiate(Boom, new Vector3(x, y, 0f), Quaternion.identity);
        Object.transform.SetParent(ArrowHolder);
        return Object;
    }

    public void ArrowUpdate()
    {
        if (count >= 100 / LevelManager.Instance.gameLevel)
        {
            count = 0;
            MakeArrow();
        }
        count++;
    }

    private void Awake()
    {
        ArrowHolder = GameObject.Find("ArrowParent").transform;
    }

    
}