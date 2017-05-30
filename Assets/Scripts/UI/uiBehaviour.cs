using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiBehaviour : MonoBehaviour {

    public Text scoreText;          //score TextBox 
    public GameObject player;       //player object in the scene
	
	// Update is called once per frame
	void Update () {
        //checks if the game object player exists (used for error checking)
        if (player)
        {
            scoreText.text = "Score: " + player.GetComponent<playerBehaviour>().score;      //changes the score Textbox text and gets the players score variable
        }
        //if not 
        //else
            //send a debug message to the console
            //Debug.Log("Player is not found: Check if the player is in the Scene");
	}
}
