using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi.Multiplayer;

public class GameController : MonoBehaviour, MPUpdateListener
{

    public GameObject opponentPrefab;
    public GameObject myPlayer;
    public Complete.CameraControl cameracontrol;


    private Dictionary<string, OpponentController> _opponentScripts;
    private string _myParticipantId;
    private Vector2 _startingPoint = new Vector2(48f, -3.14f);
    private float _startingPointYOffset = 3f;
    private bool _multiplayerReady;

    // Use this for initialization
    void Start()
    {
        SetupMultiplayerGame();
    }

    // Update is called once per frame
    void Update()
    {
        DoMultiplayerUpdate();
    }

    void SetupMultiplayerGame() {
        MultiplayerController.Instance.updateListener = this;
        _myParticipantId = MultiplayerController.Instance.GetMyParticipantId();
        //get the list of all players online
        List<Participant> allPlayers = MultiplayerController.Instance.GetAllPlayers();
        _opponentScripts = new Dictionary<string, OpponentController>(allPlayers.Count - 1);
        // set up for every player in the game
        for (int i = 0; i < allPlayers.Count; i++)
        {
            string nextParticipantId = allPlayers[i].ParticipantId;
            Debug.Log("Setting up car for " + nextParticipantId);
            // set up the initial starting point
            Vector3 playerStartPoint = new Vector3(_startingPoint.x, 2.86f, _startingPoint.y);
            if (nextParticipantId == _myParticipantId)
            {
                // 4
                //myCar.GetComponent<CarController>().SetCarChoice(i + 1, true);
                myPlayer.transform.position = playerStartPoint;
            }
            else
            {
                // 5
                GameObject opponentCar = (Instantiate(opponentPrefab, playerStartPoint, Quaternion.identity) as GameObject);
                cameracontrol.m_Targets.Add(opponentCar.GetComponent<Transform>());
                OpponentController opponentScript = opponentCar.GetComponent<OpponentController>();
                //opponentScript.SetCarNumber(i + 1);
                // 6
                _opponentScripts[nextParticipantId] = opponentScript;
            }
        }
        _multiplayerReady = true;
    }

    void DoMultiplayerUpdate()
    {
        // We will be doing more here
        MultiplayerController.Instance.SendMyUpdate(myPlayer.transform.position.x,
                                            myPlayer.transform.position.z,
                                            //myCar.rigidbody2D.velocity,
                                            myPlayer.GetComponent<Rigidbody>().velocity.x,
                                            myPlayer.GetComponent<Rigidbody>().velocity.z,
                                            myPlayer.transform.rotation.eulerAngles.y);
    }

    public void UpdateReceived(string participantId, float posX, float posZ, float velX, float velZ, float rotY)
    {
        if (_multiplayerReady)
        {
            OpponentController opponent = _opponentScripts[participantId];
            if (opponent != null)
            {
                opponent.SetPlayerInformation(posX, posZ, velX, velZ, rotY);
            }
        }
    }

    public void UpdateReceived(string participantId,float m_currentlaunchforce) {
        if (_multiplayerReady)
        {
            OpponentController opponent = _opponentScripts[participantId];
            if (opponent != null)
            {
                opponent.Fire(m_currentlaunchforce);
            }
        }
    }


}
