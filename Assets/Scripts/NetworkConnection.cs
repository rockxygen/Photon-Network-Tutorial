using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkConnection : MonoBehaviourPunCallbacks
{
    public static NetworkConnection Instance;
    public Text statusText;

    private string _gameVersion = "0.0.0";
    public string roomName = "Photon";


    private void Awake()
    {
        if(NetworkConnection.Instance == null)
        {
            NetworkConnection.Instance = this;
        }
    }

    private void Start()
    {
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        statusText.text = "Connecting to server ...";
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        statusText.text = "Connected to server " + PhotonNetwork.ServerAddress;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        statusText.text = "Joined to Lobby!";
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        statusText.text = "Could not connect to random room!";
        CreateRoom();
    }

    public void CreateRoom()
    {
        RoomOptions opts = new RoomOptions { MaxPlayers = 4, IsOpen = true, IsVisible = true };
        PhotonNetwork.JoinOrCreateRoom(roomName, opts, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        statusText.text = roomName + " room was created!";
        //PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        statusText.text = "You entered to " + roomName + " room...";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        statusText.text = "Disconnect to server " + cause;
        Debug.Log(cause);
    }
}
