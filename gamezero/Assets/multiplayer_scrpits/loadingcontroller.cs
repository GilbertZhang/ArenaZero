using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingcontroller : MonoBehaviour, MPLobbyListener
{

    private bool _showLobbyDialog;
    private string _lobbyMessage;
    public Text loadingtext;

    // Use this for initialization
    void Start () {
        quickmatch();
        if (_showLobbyDialog)
        {
            loadingtext.text = _lobbyMessage;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_showLobbyDialog)
        {
            loadingtext.text = _lobbyMessage;
        }
    }

    public void quickmatch()
    {
        _lobbyMessage = "Starting a multi-player game...";
        _showLobbyDialog = true;
        MultiplayerController.Instance.lobbyListener = this;
        MultiplayerController.Instance.SignInAndStartMPGame();
    }

    public void SetLobbyStatusMessage(string message)
    {
        _lobbyMessage = message;
    }

    public void HideLobby()
    {
        _lobbyMessage = "";
        _showLobbyDialog = false;
    }
}
