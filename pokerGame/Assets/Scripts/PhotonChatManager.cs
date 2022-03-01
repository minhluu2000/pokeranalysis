using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat; 
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("ERROR ERROR ERROR: " + message);
    }

    public void OnChatStateChange(ChatState state)
    {
        return;
    }

    public void OnConnected()
    {
        chatClient.Subscribe(new string[] {PhotonNetwork.CurrentRoom.Name});
        Debug.Log("Connected to room " + PhotonNetwork.CurrentRoom.Name);
    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnected from room " + PhotonNetwork.CurrentRoom.Name);
    }

    public void OnGetMessages(string ChannelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);
        }
        chatbox.text = chatbox.text + "\n" + msgs;
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            Debug.Log("Subscribed to " + channels[i]);
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public static PhotonChatManager Instance;
    ChatClient chatClient;
    [SerializeField] TMP_InputField message;
    [SerializeField] TMP_Text chatbox;

    void Awake(){
        Instance = this;
    }

    void Start()
    {
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "US";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(PhotonNetwork.NickName));
    }

    void Update()
    {
        chatClient.Service();
    }

    public void send(){
        if(string.IsNullOrEmpty(message.text)){
            return;
        }
        chatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, message.text);
        message.text = "";
    }
}
