using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxGenerator : MonoBehaviour {
	public Text TimerText;  //countdown timer UI text
	public GameObject boxprefab;  //prefab to spawn
	public float interval;		//interval to spawn

	private float previous;


	// Use this for initialization
	void Start () {
		previous = Time.time;
		TimerText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		float current = Time.time;
        float height = 3.25f;
		//if time exceeded spawn box
		if (current - previous > interval) {
			previous = current;
            
            //deciding spawn location goes here
            
            float x = Random.Range (-31, -24);
			float z = Random.Range (-4, -9);
			Vector3 spawnPos = new Vector3 (x,height, z);

			//spawning
			Instantiate (boxprefab, spawnPos, Quaternion.identity);
			
		}

		TimerText.text = (interval - Mathf.Floor(current-previous)).ToString();
	}
}
