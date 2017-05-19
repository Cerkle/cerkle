using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour {

    public bool canMove;
    public bool startMove;

    public int speed;
    private int direction;

    private Vector3 spawnPos;
    private Vector3 playerPos;

    private float step;

	// Use this for initialization
	void Start () {
        spawnPos = new Vector3(Random.Range(-4, 4.5f), Random.Range(-2.85f, 2.85f), 0);          //initialize the start position
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;        
        startMove = true;                                                                        //setting the movement
        speed = 1;                                                                               //speed of the enemy
        direction = Mathf.RoundToInt(Random.Range(0, 3));

        if (spawnPos.x <= playerPos.x + 0.5f && spawnPos.y >= playerPos.y + 0.5f || spawnPos.x >= playerPos.x + 0.5f || spawnPos.y <= playerPos.y + 0.5f)
            this.transform.position = spawnPos;
        else
            spawnPos = new Vector3(Random.Range(-4, 4.5f), Random.Range(-2.85f, 2.85f), 0);
    }
	
	// Update is called once per frame
	void Update () {

        

        if (canMove == true)                                                                    //if the enemy can move
        {
            step = speed * Time.deltaTime;                                                      //update the speed value (multiplied by deltatime for smooth movement)
            switch (direction)
            {
                case 0:
                    transform.Translate(Vector3.right * step);
                    break;
                case 1:
                    transform.Translate(-Vector3.right * step);
                    break;
                case 2:
                    transform.Translate(Vector3.up * step);
                    break;
                case 3:
                    transform.Translate(-Vector3.up * step);
                    break;
            }
        }
        else                                                                                
        {
            return;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            switch (direction)
            {
                case 0:
                    direction = 1;
                    break;
                case 1:
                    direction = 0;
                    break;
                case 2:
                    direction = 3;
                    break;
                case 3:
                    direction = 2;
                    break;
            }

        if (collision.gameObject.tag == "Enemy")
            switch (direction)
            {
                case 0:
                    direction = 1;
                    break;
                case 1:
                    direction = 0;
                    break;
                case 2:
                    direction = 3;
                    break;
                case 3:
                    direction = 2;
                    break;
            }
    }
}


