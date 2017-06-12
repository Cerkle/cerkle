﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonController : MonoBehaviour {

    public string menuName;     //inspector string to find the game object
    public bool active;     //inspector boolean to set the for the menu

    public Text textTitle;
    public Text textBox;

    public void menuButton()
    {
        //create the menu game object
        GameObject menu;

        //find and set the menu variable to the game object in the scene
        menu = GameObject.Find(menuName);
        //set the menu game objects active variable
        menu.GetComponent<menuBehaviour>().active = active;
    }

    //loads the scene with a string in the inspector
    public void loadLevel(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }

    //pauses the game
    public void pause()
    {
        Time.timeScale = 0.0f;
    }

    //unpauses the game
    public void unpause()
    {
        Time.timeScale = 1.0f;
    }

    //disables a button that you attach in the inspector
    public void disableButton(Button button)
    {
        button.interactable = false;
    }

    //enables a button that you attach in the inspector
    public void enableButton(Button button)
    {
        button.interactable = true;
    }

    public void changeMenuTitleText(string text)
    {
        textTitle.text = text;
    }

    public void changeMenuText(string text)
    {
        textBox.text = text;
    }
}
