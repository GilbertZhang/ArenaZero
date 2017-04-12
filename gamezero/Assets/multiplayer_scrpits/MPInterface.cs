using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MPLobbyListener
{
    void SetLobbyStatusMessage(string message);
    void HideLobby();
}

public interface MPUpdateListener
{
    void UpdateReceived(string participantId, float posX, float posY, float velX, float velY, float rotZ);
    void UpdateReceived(string participantId, float m_currentlaunchforce);
}