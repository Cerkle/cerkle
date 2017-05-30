using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {

    public int highScore;

    private static gameManager instance;
    public static gameManager Instance { get { return instance; } }

    //GamePlay Variables
    public int totalScore;
    public bool music;
    public int difficulty;

    //Character Stats;
    public int speed;
    public int size;
    public int powerUpTime;

    public Sprite charSprite;

    //MainMenu Slider Variables;
    public sliderBehaviour speedSlider;
    public sliderBehaviour sizeSlider;
    public sliderBehaviour powerUpSlider;

    private Scene currScene;    

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
        getScore();
    }

	// Use this for initialization
	void Start () {
        Debug.Log(totalScore);
    }
	
	// Update is called once per frame
	void Update () {
        currScene = SceneManager.GetActiveScene();

        if (speedSlider != null && sizeSlider != null && powerUpSlider != null && currScene.name == "MainMenu")
        {
            Time.timeScale = 1.0f;
            speedSlider.intValue = speed;
            sizeSlider.intValue = size;
            powerUpSlider.intValue = powerUpTime;
        }
        else if (currScene.name != "TestLvl")
        {
            speedSlider = GameObject.Find("SpeedBar").GetComponent<sliderBehaviour>();
            sizeSlider = GameObject.Find("SizeBar").GetComponent<sliderBehaviour>();
            powerUpSlider = GameObject.Find("PowerUpBar").GetComponent<sliderBehaviour>();
        }
	}

    public void saveScores()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("TotalScore", totalScore);
    }

    public void getScore()
    {
        highScore =  PlayerPrefs.GetInt("HighScore");
        totalScore = PlayerPrefs.GetInt("TotalScore");
    }
}
