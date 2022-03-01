using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField nicknameText;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    void Awake(){
        Instance = this;
    }

    void Start()
    {
        Debug.Log("Connected to Master");
        if (PhotonNetwork.IsConnected){
            return;
        }
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)){
            CreateRoom();
            setNickname();
        }
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Connected to Lobby");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby(){
        MenuManager.Instance.OpenMenu("set name");
    }

    public void CreateRoom(){
        if(string.IsNullOrEmpty(roomNameInputField.text)){
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom(){
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("Joined Room");
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 7){
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient){
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        MenuManager.Instance.OpenMenu("error");
        errorText.text = "Room Creation Failed: " + message;
        Debug.Log("Failed to Join Room");
    }

    public void StartGame(){
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info){
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Joining Room");
    }

    public override void OnLeftRoom(){
        PhotonNetwork.LoadLevel(0);
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].RemovedFromList){
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void setNickname(){
        if (string.IsNullOrEmpty(nicknameText.text)){
            return;
        }
        PhotonNetwork.NickName = nicknameText.text;
        MenuManager.Instance.OpenMenu("title");
    }
}
