using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderBehaviour : MonoBehaviour {

    public Slider thisSlider;
    public float intValue;
    public bool change;

	// Use this for initialization
	void Start () {
        change = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (thisSlider.value != intValue)
        {
            change = true;
        }
        else
            change = false;

        if (change == true)
        {
            updateSlider();
        }
        else
            return;

	}
    private void updateSlider()
    {
        if (thisSlider.value < intValue && intValue > 0)
        {
            thisSlider.value += 1f;
        }
        else if (thisSlider.value > intValue && intValue > 0)
        {
            thisSlider.value -= 1f;
        }
        else if (intValue <= 0f)
        {
            intValue = 0;
        }
        else if (thisSlider.value == intValue)
        {
            return;
        }
    }
}
