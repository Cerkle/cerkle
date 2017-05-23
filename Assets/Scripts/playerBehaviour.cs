using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBehaviour : MonoBehaviour {

    //player variables
    public int health, speeds;
    public int speed;
    public int iForget;
    public int score;
    public int powerUpScore;


    public bool canHit;
    public bool powerUp;

    //important positions
    private Vector2 currPos;
    public Vector2 mousePos;

    public GameObject enemy;
    public Slider powerUpSlider;
    public Text powerUpText;

    

	// Use this for initialization
	void Start () {
        //initializing the player variables
        health = 1;
        speed = 3;
        score = 0;
        canHit = true;
        powerUp = false;
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

        activatePowerUp();

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
            if (powerUpSlider.value < 10)
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
        Destroy(this.gameObject);
        GameObject.Find("GameOverMenu").GetComponent<menuBehaviour>().active = true;
    }

    public void adjustPowerUp()
    {
        if (powerUpScore != 5)
        {
            powerUpScore++;
            powerUpSlider.value = powerUpScore;
        }
        else
        {
            powerUpText.text = "PowerUp Ready";
            powerUpScore = 5;
        }
    }

    public void activatePowerUp()
    {
        if (powerUpScore == 5)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                powerUpScore = 0;
                powerUpText.text = "";
            }
        }
    }
}
