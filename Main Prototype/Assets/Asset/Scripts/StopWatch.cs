using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour {
    public float stopWatch;
    public Text watchText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        stopWatch += Time.deltaTime;
        watchText.text = stopWatch.ToString();  

    }
}
