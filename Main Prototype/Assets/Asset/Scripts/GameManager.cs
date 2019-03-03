using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private bool Play; //MM,  Running, Pause, Reset

    private void Start()
    {
        
    }

    public void SetPlayOff() {
        Play = false;
    }

    public void SetPlayOn()
	{
		Play = true;
    }

    public bool getStateOfTheGame()
    {
        return Play;
    }
}
