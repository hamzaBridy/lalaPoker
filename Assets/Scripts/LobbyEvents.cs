using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyEvents 
{
    public static event Action<LobbyEventArgs> OnPlayerInvitedToLobby;
    public string inviterUserName;
    public string InviterGameId;
    public void PlayerInvited(PlayerInvitedToLobby data)
    {
        inviterUserName = data.invitingUserName;
        InviterGameId = data.gameId;
        OnPlayerInvitedToLobby?.Invoke(new LobbyEventArgs(this));
    }
    
}
public class LobbyEventArgs : EventArgs
{
    public LobbyEvents LobbyEvent { get; set; }

    public LobbyEventArgs(LobbyEvents lobbyEvent)
    {
        LobbyEvent = lobbyEvent;
    }
}
