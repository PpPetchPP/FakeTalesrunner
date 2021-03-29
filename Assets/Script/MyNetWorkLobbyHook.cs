using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
public class MyNetWorkLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager,
                                                          GameObject lobbyPlayer,
                                                          GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        CheckIP actualPlayer = gamePlayer.GetComponent<CheckIP>();
        actualPlayer.Name_Player = lobby.playerName;
        //actualPlayer.nameLabel.text = lobby.playerName;
    }
}
