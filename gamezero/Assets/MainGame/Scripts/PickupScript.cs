using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {
	public string pickuptag;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag (pickuptag)) {
			
			//destroy game object
			Destroy (other.gameObject);

			actionOnPickUp ();

		}
	}
	void actionOnPickUp(){
		// action code here
		Debug.Log("pick up"+Time.time.ToString());
	}
}
