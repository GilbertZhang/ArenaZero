using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using GooglePlayGames;

public class beginlogin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
	
	// Update is called once per frame
	void Update () {
        MultiplayerController.Instance.TrySilentSignIn();
    }
}
