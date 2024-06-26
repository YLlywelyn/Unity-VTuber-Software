using System.Collections;
using System.Collections.Generic;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
using UnityEngine;

public class UIChatHandler : MonoBehaviour
{
    public UIChatMessage chatMessagePrefab;

    public bool handleBroadcasterMessages = false;

    public string[] bannedWords;

    void Start()
    {
        TwitchChatClient.instance.onChatMessageReceived += OnChatMessageRecieved;

        if (handleBroadcasterMessages)
            TwitchChatClient.instance.onBroadcasterMessageReceived += OnChatMessageRecieved;
    }

    void OnChatMessageRecieved(TwitchChatMessage message)
    {
        if (message.EmoteOnly)
            return;

        foreach (string bannedWord in bannedWords)
        {
            if (message.Message.Contains(bannedWord))
            {
                Debug.Log(string.Format("{0} sent message with banned word ({1})", message.User.DisplayName, bannedWord));
                return;
            }
        }

        Debug.Log(string.Format("{0} said {1}", message.User.DisplayName, message.Message));

        UIChatMessage uiMessage = Instantiate<UIChatMessage>(chatMessagePrefab, transform);
        uiMessage.transform.SetAsFirstSibling();
        uiMessage.message = message;
    }
}
