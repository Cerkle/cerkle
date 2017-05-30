using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBehaviour : MonoBehaviour {

    //player variables
    public int health;
    public int speed;
    public int size;
    public int powerUpTime;
    public int score;
    public int powerUpScore;


    public bool canHit;
    public bool powerUp;

    //important positions
    private Vector2 currPos;
    public Vector2 mousePos;

    public GameObject gameManager;
    public gameManager playerVars;
    public GameObject enemy;
    public Slider powerUpSlider;
    public Text powerUpText;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        playerVars = gameManager.GetComponent<gameManager>();
    }

    // Use this for initialization
    void Start () {
        //initializing the player variables
        health = 1;
        canHit = true;
        powerUp = false;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = playerVars.charSprite;
        speed = playerVars.speed / 10;
        size = playerVars.size / 10;
        powerUpTime = playerVars.powerUpTime / 10;
        powerUpSlider.maxValue = powerUpTime;
	}
	
	// Update is called once per frame
	void Update () {

        //Update the players current position constantly
        currPos = this.GetComponent<Transform>().position;
        
        //if the left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            if (transform.position.x <= Screen.width)
            //get the mouses position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //update the speed by deltatime for smooth movement
            float step = speed * Time.deltaTime;
            //move towards the mouse position
            transform.position = Vector2.MoveTowards(currPos, mousePos, step);
        }

        if (powerUpScore == powerUpTime)
        {
            powerUpText.text = "PowerUp Ready";
            activatePowerUp();
        }

        powerUpSlider.value = powerUpScore;


        //if health is less than equal to 0
        if (health <= 0)
        {
            //kill yourself
            killMe();
        }
	}

    //this is a built in function is used for detecting collisions when they start
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the player collides with another game oject with the tag Collectable
        if (collision.gameObject.tag == "Collectable")
        {
            //increment the score by 1
            score++;
            if (powerUpSlider.value < powerUpTime)
                adjustPowerUp();
            //destroy the colliding object
            collision.gameObject.transform.position = new Vector3(Random.Range(-4, 4.5f), Random.Range(-2.85f, 2.85f), 0);
            Instantiate(enemy);
        }

        //if the player collides with another game object with the tag Enemy
        if (collision.gameObject.tag == "Enemy" && canHit == true)
        {
            //decrease health
            health--;
            //set canHit to false so the player wont get constantly hurt
            canHit = false;
        }
    }

    //function to be called when you want to 
    public void killMe()
    {
        //destroys this game object
        saveScore();
        GameObject.Find("GameOverMenu").GetComponent<menuBehaviour>().active = true;
        GameObject.Find("MenuTitleText").GetComponent<Text>().text = "Game Over";
        Time.timeScale = 0.0f;
        Destroy(this.gameObject);
    }

    public void adjustPowerUp()
    {
        if (powerUpScore < powerUpTime)
        {
            powerUpScore++;
        }
    }

    public void activatePowerUp()
    {
        if (powerUpScore == powerUpTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                powerUpScore = 0;
                powerUpText.text = "";
            }
        }
    }

    private void saveScore()
    {
        playerVars.totalScore += score;
        if (score > playerVars.highScore)
        {
            playerVars.highScore = score;
            GameObject.Find("MenuTextBox").GetComponent<Text>().text = "New HighScore!\n" + "Score: " + score + "\n Highscore: " + playerVars.highScore;
        }
        else
        GameObject.Find("MenuTextBox").GetComponent<Text>().text = "Try Again!\n" + "Score: " + score + "\n Highscore: " + playerVars.highScore;
        playerVars.saveScores();
    }
}
