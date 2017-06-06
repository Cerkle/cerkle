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
    public int powerTime;
    public float timer;

    public string powerUps;


    public bool canHit;
    public bool powerUp;

    //important positions
    private Vector3 prevPos;
    private Vector3 currVel;
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
        powerUps = gameManager.GetComponent<gameManager>().powerUp.ToString();
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

        if (collision.gameObject.tag == "Enemy" && canHit == false && powerUps == "Mushroom")
        {
            Vector2 dir = collision.contacts[0].point - new Vector2(this.transform.position.x, this.transform.position.y);

            collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(-currVel, dir, ForceMode2D.Impulse);
            Debug.Log(currVel);
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

                switch (powerUps)
                {
                    case "Sickly":
                        StartCoroutine(sickly());
                        break;

                    case "Cyclops":
                        canHit = false;
                        this.GetComponent<SpriteRenderer>().color = Color.red;
                        StartCoroutine(cyclops(2, canHit));
                        break;

                    case "Triclops":
                        StartCoroutine(triclops(7));
                        break;

                    case "Alien":
                        canHit = false;
                        GetComponent<CircleCollider2D>().enabled = false;
                        this.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.3f);
                        StartCoroutine(alien(3, canHit));
                        break;
                        

                    case "Mushroom":
                        canHit = false;
                        powTimer(4);
                        StartCoroutine(mushroom(4, canHit));
                        StartCoroutine("CalcVelocity");
                        break;
                }
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

    private void powTimer(float time)
    {

        powerTime = (int)time;
        timer = 1;
        timer -= 0.3f;

        while(timer <= 0)
        {
            powerTime--;
            timer = 1;
        }
        powerUpText.text = "" + powerTime;
        Debug.Log(timer);
    }

    private IEnumerator sickly()
    {
        int dist;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        
        for(int i = 0; i < gos.Length; i++)
        {
            foreach(GameObject enemy in gos)
            {
                Vector3 position = enemy.transform.position;
                if ((transform.position - position).magnitude < 4 )
                {
                    Vector2 enemyDir = enemy.transform.position - new Vector3(this.transform.position.x, this.transform.position.y);
                    enemy.GetComponent<Rigidbody2D>().AddForceAtPosition(enemy.transform.position-transform.position / 4, enemyDir, ForceMode2D.Impulse);
                }
            }
        }
        yield return null;
    }

    private IEnumerator cyclops(int time, bool boo)
    {
        

        yield return new WaitForSeconds(time);

        if (boo == false)
            canHit = true;
        else
            canHit = false;

        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator alien(int time, bool boo)
    {
        yield return new WaitForSeconds(time);

        if (boo == false)
            canHit = true;
        else
            canHit = false;

        this.GetComponent<SpriteRenderer>().color = Color.white;
        this.GetComponent<CircleCollider2D>().enabled = true;
    }

    private IEnumerator mushroom(int time, bool boo)
    {
        yield return new WaitForSeconds(time);

        if (boo == false)
            canHit = true;
        else
            canHit = false;
        StopCoroutine("CalcVelocity");
        StopCoroutine(mushroom(1,false));
    }

    private IEnumerator triclops(int time)
    {

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].GetComponent<enemyBehaviour>().canMove = false;
        }

        yield return new WaitForSeconds(time);

        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].GetComponent<enemyBehaviour>().canMove = true;
        }
    }

    IEnumerator CalcVelocity()
    {
        while (Application.isPlaying)
        {
            // Position at frame start
            prevPos = this.transform.position;
            // Wait till it the end of the frame
            yield return new WaitForEndOfFrame();
            // Calculate velocity: Velocity = DeltaPosition / DeltaTime
            currVel = (prevPos - transform.position) / Time.deltaTime;
            Debug.Log(currVel);
        }
    }
}
