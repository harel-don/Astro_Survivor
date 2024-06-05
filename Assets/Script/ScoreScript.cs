using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private static ScoreScript _instance;
    private float timer;

    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI timeScore;
    private float score;

    public static ScoreScript Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("manager is null");
            }

            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _instance = this;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        textScore.text = "Score: " + score; 
        // timeScore.text = "Time:" + timer;
    }

    private void FixedUpdate()
    {
        timeScore.text = "Time:" + timer;
    }

    public static void setScore(float adds)
    {
        _instance.score = adds;
    }

    public static void addScore(float add_)
    {
        _instance.score += add_;
    }

    public static float getScore()
    {
        return _instance.score;
    } public static float getTime()
    {
        return _instance.timer;
    }
}
