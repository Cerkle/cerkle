using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterButtonController : MonoBehaviour
{
    //This Button Variable
    private Button thisButt;
    //Score if it is enabled or not
    public int isEnabled;
    //GameManager reference
    public gameManager gameManager;
    //Sprite Holder
    public Sprite charSprite;
    //Power Up Holder
    public string powerUp;

    public Image lockedSprite;

    //Character Selection Variables
    public int speed;
    public int size;
    public int powerUpTime;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
    }

    // Use this for initialization
    void Start()
    {
        thisButt = this.gameObject.GetComponent<Button>();
        if (gameManager.totalScore >= isEnabled)
        {
            thisButt.interactable = true;
            lockedSprite.color = Color.white;
        }
        else
        {
            thisButt.interactable = false;
            lockedSprite.color = Color.black;
        }
    }

    public void clicked()
    {
        int charSpeed = speed;
        int charSize = size;
        int charPower = powerUpTime;

        gameManager.speed = charSpeed;
        gameManager.size = charSize;
        gameManager.powerUpTime = charPower;
        gameManager.powerUp =  (gameManager.powerUps) System.Enum.Parse(typeof(gameManager.powerUps), powerUp);

        GameObject.Find("CharacterImage").GetComponent<Image>().sprite = charSprite;
        gameManager.GetComponent<gameManager>().charSprite = charSprite;
        Debug.Log(speed);
    }
}
