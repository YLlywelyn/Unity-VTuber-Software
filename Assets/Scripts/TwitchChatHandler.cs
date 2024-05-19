using System.Collections;
using System.Collections.Generic;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
using UnityEngine;

public class TwitchChatHandler : MonoBehaviour
{
    public string[] bannedWords;

    void Start()
    {
        TwitchChatClient.instance.Init(TwitchChatInitialized, TwitchChatInitializationError);
        TwitchChatClient.instance.onChatMessageReceived += OnChatMessageRecieved;
    }

    void TwitchChatInitialized()
    {
        Debug.Log("Twitch chat initialized!");
    }

    void TwitchChatInitializationError(string error)
    {
        Debug.Log("Twitch chat initialization error: " + error);
    }

    void OnChatMessageRecieved(TwitchChatMessage message)
    {
        foreach (string bannedWord in bannedWords)
        {
            if (message.Message.Contains(bannedWord))
            {
                Debug.Log(string.Format("{0} sent message with banned word ({1})", message.User.DisplayName, bannedWord));
                return;
            }
        }

        Debug.Log(string.Format("{0} said {1}", message.User.DisplayName, message.Message));
    }
}
